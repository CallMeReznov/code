# Face_Recognition

**利用c#写的GUI调用python的facerecognition库,实现人脸识别并联动串口设备**

Debug目录下  
* FaceData
* FaceFile

两个目录是必须的
  
* config.ini:远程摄像头地址与联动COM口与对比差异值.  
* core.py  是需要调用的核心python文件.   
* encode.py  图片编码模块。  



运行需要python的**face_recognition**库.   


### 使用方法

把人脸图片放到FaceFile文件夹内(也可以使用主程序自带的功能进行切图).  
1. 开启主程序点击**重建人脸库**,进行人脸库重建.  
2. 重建完毕以后点击**启动**即可使用(第一次调试默认勾选1 3两个选项,第二个选项为串口).  

如遇到程序无法正常运行可以单独执行脚本进行测试.  
启动命令为: ` python core.py 1 0 1 `  
参数第一位是调试模式 将会显示设置的图像画面         1为开.  
参数第二位是联动模式 对比成功后会联动串口设备       1为开.  
参数第三位是本地模式 开启后会显示本地摄像头画面     1为开.    



串口设备为淘宝10块钱的ch340.因为太便宜了无法点动.可以购买带点动的设备取消sleep.
