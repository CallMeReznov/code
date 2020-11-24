import serial

com = serial.Serial('COM3',9600,timeout=1)

print(str(com.readline(),'UTF-8').replace('\r','').replace('\n',''))
com.close()
