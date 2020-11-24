import serial
import atexit
import time

com = serial.Serial('COM3',9600,timeout=1)

@atexit.register
def clean():
    com.close()
    print('手动关闭')

while True:
    time.sleep(1)
    #print(com.readline())
    print(str(com.readline(),'UTF-8').replace('\r','').replace('\n',''))
