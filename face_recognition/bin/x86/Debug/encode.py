import face_recognition
import numpy as np
import os
import sys
# 检测FaceFile下的所有文件,循环处理后写入FaceData
known_face_encodings = []
known_face_names = []
FaceFilePash = os.getcwd()+"\\FaceFile\\"

print("开始创建模型数据")
for a,b,c in os.walk(FaceFilePash):
    for x in c:
        #print(x.split('.')[0])
        #print(x)
        encoding =face_recognition.face_encodings(face_recognition.load_image_file("./FaceFile/"+x))[0]
        np.save("./FaceData/"+x.split('.')[0],encoding)
        print("生成"+x.split('.')[0]+"模型")
        #known_face_encodings.append(encoding)
        #known_face_names.append(x.split('.')[0])
print("生成完毕")

#print(known_face_encodings)
#print(known_face_names)




# biden_image = face_recognition.load_image_file("./FaceFile/wg.jpg")
# biden_face_encoding = face_recognition.face_encodings(biden_image)[0]


# known_face_encodings = [
#     obama_face_encoding,
#     biden_face_encoding,
# ]

#print(obama_face_encoding)
#print(known_face_encodings)

#np.save("./FaceData/1",obama_face_encoding)
#np.save("./FaceData/2",biden_face_encoding)


# known_face_names = [
#     "李孝利",
#     "Reznov"
# ]
