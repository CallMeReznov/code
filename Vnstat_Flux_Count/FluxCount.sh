#!/bin/sh
#获取昨天日期
CuTDateTime="`date +%m/%d/%Y -d "-1day"`"
#获取指定日期流量总数
ServerFluxCount=`vnstat -i eth0 -d |grep $cur_dateTime |awk -F "|" '{print $3}'`

if [ ${ServerFluxCount##*[0-9]} == "MiB" ]
then
    #打印记录的流量(单位Mib)
    echo ${ServerFluxCount::-5}

else
    #打印记录的流量(单位Gib转换为Mib)
    echo ${ServerFluxCount::-5} |awk '{print int($0)*1000}'
fi