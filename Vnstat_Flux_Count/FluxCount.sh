#!/bin/sh
#获取昨天日期
CuTDateTime="`date +%m/%d/%Y -d "-1day"`"
#获取指定日期流量总数
ServerFluxCount=`vnstat -i eth0 -d |grep $CuTDateTime |awk -F "|" '{print $3}'`

if [ ${ServerFluxCount##*[0-9]} == "MiB" ]
then
    #打印记录的流量(单位Mib)
    echo ${ServerFluxCount::-5}|bc

else
    #打印记录的流量(单位Gib转换为Mib)
    echo ${ServerFluxCount::-5}*1000  |bc |awk '{print int($0)}'
fi
