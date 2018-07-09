import json
import sys
import requests
#从API地址内获取队列JSON信息
mq_api = requests.get(url='API地址', auth=('账户', '密码'))
mq_json = json.loads(mq_api.content.decode())

#队列状态信息
def FaceComparestat(x):
    for i in mq_json :
        if i['name'] == x :
            print (i['messages_ready'])
            break
    return

#接收参数传递函数名返回队列数量
FaceComparestat(sys.argv[1]) 
