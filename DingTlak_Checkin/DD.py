import uiautomator2 as u2
#不再使用DD发送打卡消息,使用Server酱
#from dingtalkchatbot.chatbot import DingtalkChatbot
import requests
import time ,oss2,datetime ,logging


imagename = ((datetime.datetime.now().__format__('%Y%m%d%H%M')))+'.jpg'
#填入你钉钉机器人的API地址
webhook = '钉钉机器人API地址'
#xiaoding = DingtalkChatbot(webhook)

#具体操作请查询阿里OSS的SDK说明
#填入你ali_oss相关权限的AccessKey,
auth = oss2.Auth('AccessKey ID', '	Access Key Secret')
#填入
bucket = oss2.Bucket(auth, 'EndPoint（地域节点）', 'Bucket 域名')
try:
    #先使用adb devices查看你的设备手机设备序列号并填入
    d = u2.connect_usb(serial='DeviceKey')
    print('手机端连接成功!')
    d.screen_on()
    d.unlock()
    #d.press('Home')
    #d(text='钉钉').click_exists(timeout=10.0)
    d.app_start('com.alibaba.android.rimet')
    print('启动钉钉,等待10秒后开始执行点击操作')
    time.sleep(10)
    d.click(550, 1840)
    time.sleep(10)
    d.click(160, 1150)
    print('点击操作完成,等待10秒截图')
    time.sleep(10)
    image = d.screenshot()
    image.save(imagename)
    print('截图完成,开始上传到ALIOSS')
    bucket.put_object_from_file(imagename, imagename)
    bucket.restore_object(imagename)
    print('截图上传并解冻完毕,等待5秒')
    time.sleep(5)
    d.app_stop('com.alibaba.android.rimet')
    print('结束钉钉应用进程,等待10秒')
    time.sleep(10)
    d.press('Home')
    d.screen_off()
    print('手机屏幕关闭手机端操作完毕,等待60秒')
except Exception as e:
    print(e)
    
    failmsg='打卡失败\n\n'+'失败原因:\n'+str(e)
    requests.get(url='https://sc.ftqq.com/SCKEY.send',params={'text':'打卡失败','desp':failmsg})
    #xiaoding.send_markdown(title='失败',text=failmsg)
    exit()
#能访问到上传图片的访问地址
mdimage = '![截图](https://test.com/'+imagename+')'


time.sleep(60)

try:
    requests.get(url='https://sc.ftqq.com/SCKEY.send',params={'text':'打卡成功','desp':mdimage})
    #xiaoding.send_markdown(title='成功',text=mdimage)
    print('消息发送成功,打卡结束')
except Exception as a:
    print(a)

