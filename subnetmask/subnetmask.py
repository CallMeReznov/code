""" 
/*
 * @Author: Reznov 
 * @Date: 2018-05-31 17:17:10 
 * @Last Modified by: Reznov
 * @Last Modified time: 2018-06-01 09:23:04
 */
 """
from IPy import IP
import re

#地网专用子网计算器
print('地网专用子网计算器')
nwk_address = input('请输入网络地址: ')
nwk_mask = input('请输入子网掩码位数: ')

if re.match('^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$',nwk_address) :
    if nwk_mask == 28 or nwk_mask == 24:
        n = int(nwk_address.split('.')[-1])
        while  n < 254 :
            ip = IP('%s/%s' %(nwk_address,nwk_mask))
            print ('网关:%s' % ip[1])
            print ('地址:%s-%s' % (ip[1],str(ip[-2]).split('.')[-1]))
            print ('掩码:%s' %ip.strNormal(2).split('/')[-1])
            nwk_len=int(ip.len())
            #网络地址替换成下个网段的地址
            n = int(nwk_address.split('.')[-1])+nwk_len  #int  网段长度+自身长度
            nwk_address=(re.match('^\d{1,3}\.\d{1,3}\.\d{1,3}\.',nwk_address).group())+str(n)
    else:
        print('请输入真实的掩码!')
else:
    print('未输入信息或输入信息不正确,程序退出!')


        