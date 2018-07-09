################################################################
##################本脚本必须运行在管理员权限下!#################
################################################################
#Powershell默认未加载该程序集手动需手动加载
#加载程序集:PresentationFramework（PresentationFramework.dll 中）
Add-Type -AssemblyName PresentationFramework
Add-Type -AssemblyName System.Core

#格式化时间.以分钟为单位计算时间 暂未使用
Function timelimit([string]$num)
{
        $FunMinutes=(get-date).AddHours(-8.5)
        $FunMinutes=$FunMinutes.AddSeconds($num)
        return  $FunMinutes.ToString('HH:mm:ss')
        
}


#检测防火墙规则是否存在
Function Firewallrule()
{
#输出空只返回命令成功与否
$firewallrule=&{netsh advfirewall firewall show rule name="IISIPVISIT" |out-null;$?}
if ( $firewallrule -like "False" ) 
{
Write-Host "防火墙规则不存在,新建防火墙规则!"
netsh advfirewall firewall add rule name="IISIPVISIT" dir=in action=block protocol=TCP localport=80
}
else 
{
Write-Host "防火墙规则存在,程序继续!"
}
}
#调用文件选取对话框
function Show-OpenFileDialog()
{
  $type = 'Microsoft.Win32.OpenFileDialog'
  
  #创建新对象Microsoft.Win32.OpenFileDialog
  $dialog = New-Object -TypeName $type 
  $dialog.Title = '选择需要筛选的日志文件'
  $dialog.Filter =  'Log|*.Log|All|*.*'
  if ($dialog.ShowDialog() -eq $true)
  {
   #使用return向函数外返回变量值
   return $logfilename=$dialog.FileName
  }
  else
  {
    Write-Warning '未选取任何日志文件,程序退出!!'
    exit
  }
}
###############################################################
#选择需要筛选的日志文件文件
$logfilename=Show-OpenFileDialog
#筛选循环的时间间隔
#$visittime = Read-Host  "您希望的筛选时间间隔(单位:秒)"
#判断防火墙规则是否存在
Firewallrule
#设置筛选时间的上下界限
$timesection=read-host "您希望设定筛选的时间区间与筛选间隔时间(单位:秒)当前默认时间为当前时间-8小时"
$visitnumber=read-host "你说需要筛选的访问次数限制(大于该数都将会被添加入黑名单)"
#开始循环筛选日志,并添加IP到防火墙列表内
cls
while ($timesection -ne 0)
{
#按照时间区间筛选
$timelowlimit=timelimit(-$timesection)
$timeuplimit=timelimit
write-host "当前查询区间为$timelowlimit"至"$timeuplimit"
$logcontent=./LogParser.exe -o:csv "Select count(cs-uri-stem) as VisitCounts,c-ip as [ClientIP] from $logfilename where time>'$timelowlimit' and time<'$timeuplimit' group by cs-uri-stem,c-ip having VisitCounts>$visitnumber ORDER BY VisitCounts DESC" -q:on
write-host "本次筛选出"$logcontent.Count "个IP"
#数组A
$logcontent=$logcontent -replace ("(\b\d{1,8},)|\s","") |select -Skip 1 
#数组B  集合
$logcontentcolle=$logcontentcolle+$logcontent
write-host "黑名单列表内现已存在"$logcontentcolle.Count "个IP"
#使用.NET类去除重复列(转化为对象)
[System.Collections.Generic.HashSet[string]]$iplisttemp=$logcontentcolle
$iplist=[string]$iplisttemp -replace ("\s",",")
netsh advfirewall firewall set rule name="IISIPVISIT"  new remoteip= $iplist
$logcontentcolle=$iplisttemp
$logcontentcolle >blackip.txt
Start-Sleep -Seconds $timesection
write-host ---------------------------------
}
#按照全局访问数量筛选(数组)   
#$xmllog=./LogParser.exe -o:csv "Select count(cs-uri-stem) as VisitCounts,c-ip as [ClientIP] from $logfilename  group by cs-uri-stem,c-ip having VisitCounts>4000 ORDER BY VisitCounts DESC" -q:on
