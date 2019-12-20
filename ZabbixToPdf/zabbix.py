import csv ,requests ,sqlite3 ,paramiko ,base64 ,time ,pdfkit ,math
from pyzabbix import ZabbixAPI 
from flask import json ,render_template ,request ,redirect ,url_for
from flask_script import Manager ,Server
from flask import Flask
from flask_cors import CORS

PdfCount= {'sum':'0','loop':'1','prx':'0%','count':'0'}
#zabbix地址
ZabbixServer = 'http://1.1.1.1/'
z_api = ZabbixAPI(ZabbixServer)
z_api.login('Admin','zabbix')
title= '服务器巡检表'
timeline = time.strftime("%Y-%m-%d",time.localtime())
def gethistory (hostid,key):
    x = z_api.item.get(hostids=hostid,search={"key_":key})
    try:
        return x[0]['lastvalue']
    except :
        return "0"

def getname (hostid):
    x = z_api.host.get(hostids=hostid)
    return x[0]['name'] ,x[0]['host']

app = Flask(__name__)


#首页,使用ZabbixAPI输出IP列表
@app.route('/' ,methods=('POST','GET'))
def index():
    if request.method == 'GET' :
        HostStat = z_api.host.get(output=['host','name'])
        return render_template('index.html', HostStat=HostStat)
    elif  request.method == 'POST' :
        gouid= request.form.get("grouid")
        if gouid == '0':
            return redirect(url_for('index'))
        else :
            HostStat = z_api.host.get(output=['host','name'],
                                    groupids=gouid)
            return render_template('index.html', HostStat=HostStat)
   

#具体报告
@app.route('/r/<hostid>')
def report(hostid):
    systype = gethistory(hostid,"system.uname")
    if "Windows" in systype :
        print ("Windows")
        systype = gethistory(hostid,"system.uname")
        hostname = getname(hostid)
        freememory = int(int(gethistory(hostid,"vm.memory.size[free]")) /1024/1024)
        usememory = int(int(gethistory(hostid,"vm.memory.size[used]")) /1024/1024)
        usememorypav = gethistory(hostid,"vm.vmemory.size[pavailable]")
        usecpupav = gethistory(hostid,"system.cpu.load[percpu,avg1]")
        disktotalc = int(int(gethistory(hostid,"vfs.fs.size[C:,total]")) /1024/1024/1024)

        diskfreec = int(int(gethistory(hostid,"vfs.fs.size[C:,free]")) /1024/1024/1024)

        diskfreepavc = gethistory(hostid,"vfs.fs.size[C:,pfree]")
        disktotald = int(int(gethistory(hostid,"vfs.fs.size[D:,total]")) /1024/1024/1024)
        diskfreed = int(int(gethistory(hostid,"vfs.fs.size[D:,free]")) /1024/1024/1024)
        diskfreepavd = gethistory(hostid,"vfs.fs.size[D:,pfree]")
        disktotale = int(int(gethistory(hostid,"vfs.fs.size[E:,total]")) /1024/1024/1024)
        diskfreee = int(int(gethistory(hostid,"vfs.fs.size[E:,free]")) /1024/1024/1024)
        diskfreepave =  gethistory(hostid,"vfs.fs.size[E:,pfree]")
        disktotalf = int(int(gethistory(hostid,"vfs.fs.size[F:,total]")) /1024/1024/1024)
        diskfreef = int(int(gethistory(hostid,"vfs.fs.size[F:,free]")) /1024/1024/1024)
        diskfreepavf = gethistory(hostid,"vfs.fs.size[F:,pfree]")

    
        return render_template('windows.html' ,tl=timeline,title=title,st=systype,hn=hostname[0],hi=hostname[1],fm=freememory,um=usememory,ump=usememorypav,ucp=usecpupav,
        dtc=disktotalc,dfc=diskfreec,dfcp=diskfreepavc,
        dtd=disktotald,dfd=diskfreed,dfdp=diskfreepavd,
        dte=disktotale,dfe=diskfreee,dfep=diskfreepave,
        dtf=disktotalf,dff=diskfreef,dffp=diskfreepavf)
    elif "Linux" in systype :
        print ("linux")
        systype = gethistory(hostid,"system.uname")
        hostname = getname(hostid)
        freememory = int(int(gethistory(hostid,"vm.memory.size[free]")) /1024/1024)
        usememory = int(int(gethistory(hostid,"vm.memory.size[used]")) /1024/1024)
        usememorypav = gethistory(hostid,"vm.vmemory.size[pavailable]")
        usecpupav = gethistory(hostid,"system.cpu.load[percpu,avg15]")

        sql=sqlite3.connect('data.db')
        cur= sql.cursor()
        curexe= cur.execute("select HostUser , HostPasswd , HostIp from hostinfo where HostId="+hostid)
        for row in curexe :
            hostuser=row[0]
            hostpasswd=row[1]
            hostip=row[2]
        hostuser = str(base64.b64decode(hostuser.encode('utf-8')),'utf-8')
        hostpasswd = str(base64.b64decode(hostpasswd.encode('utf-8')),'utf-8')
        sql.close()
        sshclient = paramiko.SSHClient()
        sshclient.set_missing_host_key_policy(paramiko.AutoAddPolicy())
        sshclient.connect(hostname=hostip,port=22,username=hostuser,password=hostpasswd)
        dfhstdin,dfhstout,dfhstderr = sshclient.exec_command('df -h | tail -n +2')
        #不知道为什么不识别全局变量的路径,必须指定全路径
        macstdin,macstout,macstderr = sshclient.exec_command("/sbin/ip addr |grep link/ether|awk -F ' '  '{print $2}' ")
        topstdin,topstout,topstderr = sshclient.exec_command("top -b  -c-n1 |head -n20")
        freestdin,freestout,freestderr = sshclient.exec_command("free")
        laststdin,laststout,laststderr = sshclient.exec_command("last -n10")
        orastdin,orastout,orastderr = sshclient.exec_command("netstat -anl |grep 1521")
        dfhstout=dfhstout.read().decode('utf-8')
        macstout=macstout.read().decode('utf-8')
        topstout=topstout.read().decode('utf-8')
        freestout=freestout.read().decode('utf-8')
        laststout=laststout.read().decode('utf-8')
        orastout=orastout.read().decode('utf-8')
        sshclient.close()


        return render_template('linux.html',tl=timeline,st=systype,hn=hostname[0],hi=hostname[1],fm=freememory,um=usememory,ump=usememorypav,ucp=usecpupav,
        dfh=dfhstout,mac=macstout,top=topstout,free=freestout,last=laststout,oracle=orastout)
    else :
        return "无法生成报告,请确定是否有相关系统与配置"
    




@app.route('/pdfconvert')
def pdfconvert():
    PdfCount['count']=0
    PdfCount['loop']=0
    HostStat = z_api.host.get(output=['host','name'])
    PdfCount['sum']=len(HostStat)
    for x1 in HostStat :
        PdfCount['count'] += 1
        print(x1['hostid'])
        print(len(HostStat))
        pdfkit.from_url('http://127.0.0.1:5000/r/'+x1['hostid'],'.\\pdf\\'+x1['name']+'.pdf')
    PdfCount['loop']=1
    return "生成完毕"

@app.route('/count')
def count():
    l1=int(PdfCount['count'])
    l2=int(PdfCount['sum'])
    PdfCount['prx']=str(int(l1/l2*100))+'%'
    return PdfCount


@app.route('/loading')
def loading():

    return render_template('load.html')


if __name__ == '__main__' :
    CORS(app,supports_credentials=True)
    app.run(debug=True, threaded=True)