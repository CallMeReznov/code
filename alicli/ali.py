import os ,requests ,subprocess
#检查aliyuncli.exe与config.ini是否存在
try :
    os.path.isfile('./aliyun.exe')
    #os.path.isfile('./config.ini')
except :
    pass

#检测AliyunCli文件是否存在,不存在就创建.
def CheckConfig():
    AliConfig = os.environ.get('USERPROFILE')+'\.aliyun\config.json'
    if os.path.isfile(AliConfig) :
        print ('检测到Aliyuncli配置文件,无需配置')
        return True
    else :
        print ('未检测到Aliyuncli配置文件')
        try :
            subprocess.Popen('./aliyun.exe configure set   --profile Geist   --mode AK   --region ap-southeast-1   --access-key-id XXXXXXXXXXXXXXXXXXXXX   --access-key-secret XXXXXXXXXXXXXXXXXXXXXXXXXXXX')
        except :
            #写错误日志
            pass
            
            


def AddNewFireWallRule():
    r = requests.get('https://api.ip.sb/ip')
    SelfIp = r.text.replace('\n','')
    print('当前IP地址为'+SelfIp)
    #具体命令行查询alicli并自定义.
    AddRuleCmd = 'aliyun.exe ecs AuthorizeSecurityGroup --RegionId ap-southeast-1 --SecurityGroupId XXXXXXXXXXXXXXXXXXXXXXX --IpProtocol tcp --PortRange 3391/3391 --SourceCidrIp '+SelfIp+' --Priority 1'
    subprocess.Popen(AddRuleCmd)



if __name__ == "__main__" :
    CheckConfig()
    AddNewFireWallRule()