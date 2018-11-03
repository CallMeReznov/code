import configparser, ftplib, time, os, datetime, shutil

# 载入配置文件参数
config = configparser.ConfigParser()
config.read('config.ini')
# 远程FTP地址
FTP_ADDR = config.get('config', 'ftp_addr')
# 远程FTP用户名
FTP_USER = config.get('config', 'ftp_user')
# 远程FTP密码
FTP_PASSWD = config.get('config', 'ftp_passwd')
# 远程FTP路径
FTP_PATH = config.get('config', 'ftp_path')
# 本地备份文件路径(文件夹)
FTP_LOCAL = config.get('config', 'ftp_local')
# 备份前置任务
BAK_STARTSHELL = config.get('config', 'bak_startshell')
# 备份后置任务
BAK_ENDSHELL = config.get('config', 'bak_endshell')

# 自动删除本地过期备份文件
DEL_DATE = int(config.get('config', 'del_date'))

# 获取当前日期
BAK_DATE = time.strftime('%Y%m%d', time.localtime())
TIME_START = time.time()
# 执行前置任务
if BAK_STARTSHELL == '':
    print('=' * 40)
    print('无前置任务,直接执行备份任务!')
else:
    print('=' * 40)
    print('开始执行前置命令,请等待!')
    print('=' * 40)
    if os.system(BAK_STARTSHELL) == 0:
        print('=' * 40)
        print('前置任务执行完毕')
    else:
        print('=' * 40)
        print('前置任务执行异常,程序退出!')
        exit()

# 打包文件
print('=' * 40)
print('打包开始！' + time.strftime('%H:%M:%S', time.localtime()))
# 打包指定目录下所有文件
shutil.make_archive('./Bakfile/' + BAK_DATE, 'zip', FTP_LOCAL)
print('打包完毕！' + time.strftime('%H:%M:%S', time.localtime()))
# FTP上传备份文件
FTP = ftplib.FTP(FTP_ADDR)
# FTP模式 0主动模式 1 #被动模式
FTP.set_pasv(0)

FTP.login(FTP_USER, FTP_PASSWD)
# 设置远程路径
FTP.cwd(FTP_PATH)
bufsize = 8196

fp = open('./Bakfile/' + BAK_DATE + '.zip', 'rb')
print('=' * 40)
print('上传开始！' + time.strftime('%H:%M:%S', time.localtime()))

FTP.storbinary('STOR ' + BAK_DATE + '.zip', fp, bufsize)
print('上传完毕！' + time.strftime('%H:%M:%S', time.localtime()))
fp.close
FTP.quit()

# 删除本地过期备份文件
print('=' * 40)
print('开始删除本地过期备份文件，过期时间%s天' % DEL_DATE)
# 设定删除日期
DEL_DATE = (datetime.datetime.now() -
            datetime.timedelta(days=DEL_DATE)).strftime('%Y%m%d')
for current_path, subfolders, filesname in os.walk('.\\Bakfile'):
    for files in filesname:
        if int(files[0:8]) <= int(DEL_DATE):
            try:
                os.remove('.\\Bakfile\\' + files)
                print('删除' + files)
            except FileNotFoundError as e:
                print('Error:', e)
                break
        else:
            break

print('=' * 40)
print('过期备份文件删除完毕！')

# 执行后置任务
if BAK_ENDSHELL == '':
    print('=' * 40)
    print('无后置任务!')
else:
    print('=' * 40)
    print('开始执行后置任务,请等待!')
    print('=' * 40)
    if os.system(BAK_ENDSHELL) == 0:
        print('=' * 40)
        print('后置任务执行完毕')
    else:
        print('=' * 40)
        print('后置任务执行异常,程序退出!')
        exit()

TIME_END = time.time()
BAK_TIME = str(TIME_END - TIME_START)
print('=' * 40)
print('备份结束!\n本次任务共耗时%s秒' % BAK_TIME[0:3])
