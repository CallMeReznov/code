# NodeMcu_Temperature

**使用NodeMcu开发板与BMP180模块制作的环境温度检测工具,使用Arduino语言编写,属于学习中的作品**  

* node.ino     Arduino主程序代码,请预先下载好**NodeMcu**开发板与**BMP180**库  
* temperature.py    python取值,只取一次.  
* main.py   python取值,循环取值.  
* temperature c# winform写的GUI版获取API并发送给串口,后期增加了电话报警与同步温度信息到摄像头画面的功能.


### 使用方法

我现在的使用场景是配合zabbix,利用zabbix的UserParameter取值  
把设备插入机器内测试通过后,在/etc/zabbix/zabbix_agentd.conf内添加  
```Shell  
UserParameter=serroomtemp, sudo cat /dev/ttyUSB0 |head -n 1
```
即可  


NodeMcu开发板10块,BMP180模块2元,配上面包板之类的20块钱即可制作简单的机房温度监控,配合zabbix可以设置监控报警.



![成品](../NodeMcu_Temperature/Node.jpg)  

PS: 在linux环境下使用ch340串口转USB我发现默认集成的驱动会有莫名其妙的问题,上官方下载源码也编译不过去,最后还是在github上找了一个修正版的编译过后替换就完美了.
