To set app for dev\
Install .net sdk\
install node js\
istall docker and have it running\
run in home directiory\
```
docker-compose up -d
```
```
cd API
```
```
dotnet user-secrets set "UserId" "sa"
```
```
dotnet user-secrets set "Password" "pa55w0d!"
```
```
dotnet ef database update
```
```
cd ../View
```
```
npm install -g @angular/cli
```
this gives you everyhting you need installed and starts up your database in your docker container, to run the web app now you need two terminals in one
```
cd API
```
```
dotnet run
```
in the other
```
cd View
```
```
ng serve
``` 
