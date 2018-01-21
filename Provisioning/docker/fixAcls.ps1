$path = "C:\inetpub\wwwroot\MvcMusicStore_deploy"
$acl = Get-Acl $path
Set-Acl $path $acl