import csv
import configparser
import netmiko
import datetime
import time
import os

#本日时间戳   20191117
BAkpath = str(datetime.date.today().__format__('%Y%m%d'))

if __name__ == '__main__':
    #创建备份日的目录文件夹
    print('开始运行'+BAkpath+'日备份任务')
    if os.path.exists(BAkpath) :
        print('今日备份文件目录已存在,跳过创建进入备份流程')
        print('-'*50)
    else :
        print('今日备份目录未创建,开始创建')
        print('-'*50)
        try:
            os.makedirs(BAkpath) 
            print('创建成功')
            print('-'*50)
        except Exception as ex:
            print('创建错误,错误信息\n'+ex)
            print('-'*50)


    #载入需要备份设备的相关信息
    with open("host.csv",'r') as f:
        reader = csv.DictReader(f)
        for line in reader:
            DName = line['DName']
            DMod = line['DMod']
            Dlogin = line['Dlogin']
            DIp = line['DIp']
            DPort = line['DPort']
            DUser = line['DUser']
            DPasswd = line['DPasswd']
            DEpasswd = line['DEpasswd']
            print('设备序列Id:'+line['Id'])
            print('设备名称:'+DName)
            print('设备型号:'+DMod)
            print('设备登录方式:'+Dlogin)
            print('设备地址:'+DIp)
            print('设备端口:'+DPort)
            print('设备账户:'+DUser)
            print('设备密码:'+DPasswd)
            print('设备Enable密码:'+DEpasswd)
            print('-'*50)
            if DMod =='Cisco' :
                if Dlogin =='Telnet' :
                    try :
                        print('使用telnet')
                        print('-'*50)
                        Csn = netmiko.ConnectHandler(device_type='cisco_ios_telnet',host=DIp,username=DUser,password=DPasswd)
                        print(Csn.send_command_timing('enable'))
                        print(Csn.send_command_timing(DEpasswd))
                        BAkcfg = Csn.send_command_timing('show running-config')
                        BAkfile = './'+BAkpath+'/'+DName+'.cnf'
                        with open(BAkfile,'w') as cnf :
                            cnf.write(BAkcfg)
                        Csn.disconnect()
                        print('-'*50)
                        print('执行完毕,关闭连接')
                        print('-'*50)
                    except  Exception as ex:
                        print(ex)
                        print('-'*50)
                        print('执行失败')
                        Csn.disconnect()
                elif Dlogin == 'Ssh':
                    try :
                        print('使用Ssh')
                        print('-'*50)
                        Csn = netmiko.ConnectHandler(device_type='cisco_ios',host=DIp,username=DUser,password=DPasswd)
                        print(Csn.send_command_timing('enable'))
                        print(Csn.send_command_timing(DEpasswd))
                        BAkcfg = Csn.send_command_timing('show running-config')
                        BAkfile = './'+BAkpath+'/'+DName+'.cnf'
                        with open(BAkfile,'w') as cnf :
                            cnf.write(BAkcfg)
                        Csn.disconnect()
                        print('-'*50)
                        print('执行完毕,关闭连接')
                    except  Exception as ex:
                        print(ex)
                        print('-'*50)
                        print('执行失败')
                        Csn.disconnect()
                else:
                    print('未知思科设备登陆方式\n请确定输入有误')
            elif DMod =='Huawei':
                if Dlogin =='Telnet' :
                    pass
                elif Dlogin == 'Ssh':
                    pass
                else:
                    print('未知华为设备登陆方式\n请确定输入有误')                    
            elif DMod =='H3c':
                if Dlogin =='Telnet' :
                    pass
                elif Dlogin == 'Ssh':
                    pass
                else:
                    print('未知H3c设备登陆方式\n请确定输入有误')                
            else :
                print('未知设备\n请确定输入有误')
        f.close()
    print('计划内任务运行完毕.')