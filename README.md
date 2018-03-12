# SignalR-Notification
When a record added to Notification table of database we will display it on web using SignalR

### Step One
Create database and Notification table on it <br/>
After that run query shown bellow on sql management studio 
```html
alter database CP set enable_broker with rollback immediate;
```
### Step Two
Create a class library with name SignalR-Entity <br/>
Add Define ADO.Net of database inside class library <br/>
Add reference to SignalR-entity from main project </br>

### Step Three
Inside main project add <b>OWIN Startup Class</b> from Add/NewItem <br/>
After that add code bellow inside Configuration of OWIN Startup Class
```html
app.MapSignalR();
```
Then create SignalR Hub Class from Add/NewItem with name <b>NotificationHub</b></br>
Now add new connection string in web.config inside <connectionstring> tag like <br/>
```html
<add name="SqlConString" connectionString="data source=IpAddress;initial catalog=CP;user id=userId;password=password;integrated security=True;" />
```
### Step Four
In this step we will register notification on project<br/>
to do this follow steps bellow <br/>
  - Add new class with name <b>NotificationComponents</b>
  - Here we will add a function for register notification (sql dependency)
