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
Define ADO.Net of database inside class library <br/>
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
<add name="SqlConString" connectionString="data source=IpAddress;initial catalog=CP;user id=userId;password=password;integrated security=False;" />
```
### Step Four
In this step we will register notification on project<br/>
to do this follow steps bellow <br/>
  - Add new class with name <b>NotificationComponents</b>
  - Here we will add a function for register notification (sql dependency)
  - <b>RegisterNotification</b> function register the notification, when result changed on db <b>SqlDep_OnChange</b> function will be call
  - Code inside <b>SqlDep_OnChange</b> function run when a data change on db and this function trigger a code inside _Layout 
### Step Five
  The final step is about adding javascript code inside Layout
  When a notification added to database <b>notificationHub.client.notify</b> from Layout will be call and inside tihs function notification panel will be updated.
  Dont Remember to add Home Controller and <b>GetNotificationContacts</b> action result inside it<br/>
  And inside Global.ajax we should start and stop <b>SqlDependency</b>



Note that for SignalR to use WebSocket, IIS 8 or IIS 8 Express must be used, the server must be using Windows 8, Windows Server 2012, or later, and WebSocket must be enabled in IIS. For information on how to enable WebSocket in IIS, Click link bellow<br/>
https://docs.microsoft.com/en-us/iis/get-started/whats-new-in-iis-8/iis-80-websocket-protocol-support
