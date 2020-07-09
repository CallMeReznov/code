print('开始载入模块!')
import serial
import sys
import face_recognition
import cv2
import numpy as np
import time
import threading
import configparser
import os
config = configparser.ConfigParser()
config.read('config.ini',encoding='utf-8')
tolerance = config.get('config', 'Tolerance')
print('载入图像!')
if sys.argv[3] == '1':
    video_capture = cv2.VideoCapture(0)
elif sys.argv[3] == '0': 
    video_capture = cv2.VideoCapture(config.get('config', 'Rtsp'))

known_face_encodings = []
known_face_names =[]
print("载入人脸库")
FaceDataPash = os.getcwd()+"\\FaceData\\"
for a,b,c in os.walk(FaceDataPash) :
    for x in c :
        print("正在载入人脸数据:"+x.split('.')[0])
        known_face_encodings.append(np.load("./FaceData/"+x))
        known_face_names.append(x.split('.')[0])



print("载入完毕")
face_locations = []
face_encodings = []
face_names = []
process_this_frame = True
print('获取摄像头图像,开始对比')
print('当前人脸对比阈值:'+config.get('config', 'Tolerance'))
if sys.argv[1]=='1' :
    print("Debug模式开启,将开启抓拍视频窗口")
if sys.argv[2]=='1' :
    print("联动模式开启,将联动串口设备")

def face():
    print("对比线程启动!,等待5秒后开始对比")
    time.sleep(5)
    while True :
            time.sleep(1)
            face_locations = face_recognition.face_locations(rgb_small_frame ,model="hog")
            face_encodings = face_recognition.face_encodings(rgb_small_frame, face_locations)
            face_names = []
            for face_encoding in face_encodings:
                matches = face_recognition.compare_faces(known_face_encodings, face_encoding, tolerance=float(tolerance))
                name = "陌生人"
                face_distances = face_recognition.face_distance(known_face_encodings, face_encoding)
                best_match_index = np.argmin(face_distances)
                if matches[best_match_index]:
                    name = known_face_names[best_match_index]
                    if sys.argv[2] == '1' :
                        try:
                            Scom = serial.Serial(config.get('config', 'Com'),9600,timeout=1)
                            Sopen = b'\xa0\x01\x01\xa2'
                            Sclose = b'\xa0\x01\x00\xa1'
                            Scom.write(Sopen)
                            print("端口:开")
                            time.sleep(0.5)
                            Scom.write(Sclose)
                            print("端口:关")
                        except Exception as e:
                            print(e)
                        Scom.close()       
                face_names.append(name)
                print("侦测到人脸:"+name)

n = threading.Thread(target=face)
n.start()

while True:
    ret, frame = video_capture.read()

    small_frame = cv2.resize(frame, (0, 0), fx=0.25, fy=0.25)
    rgb_small_frame = small_frame[:, :, ::-1]
    if sys.argv[1] == '1':
        cv2.imshow('Video', small_frame)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break


video_capture.release()
cv2.destroyAllWindows()