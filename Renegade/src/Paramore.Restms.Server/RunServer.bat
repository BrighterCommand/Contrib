REM You need to run at an elevated command prompt if you need to reserve the url you are using
netsh http add urlacl url=http://+:8080/ user=machine\username
.\bin\Debug\paramore.restms.server.exe
netsh http delete urlacl url=http://+:8080/

