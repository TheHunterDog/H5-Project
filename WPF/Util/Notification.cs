using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using Database.Model;
using Microsoft.Toolkit.Uwp.Notifications;
// using OnChangeEventHandler = Microsoft.Data.SqlClient.OnChangeEventHandler;
// using SqlCommand = Microsoft.Data.SqlClient.SqlCommand;
// using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;
// using SqlDependency = Microsoft.Data.SqlClient.SqlDependency;
// using SqlNotificationEventArgs = Microsoft.Data.SqlClient.SqlNotificationEventArgs;

namespace WPF.Util;

public class NotificationBroker
{
    private const string QUEUENAME = "NotificationQueue";
    private const string SERVICENAME = "NotificationChange";
    public bool IsActive
    {
        get
        {
            return _isActive;
        }
    }

    private bool _isActive = false; 
    private delegate void RateChangeNotification(DataTable table);  
    private SqlDependency dependency;  
    string ConnectionString = "Server=ftp.huttennl.nl,1433;Database=StudentBegeleid;User Id=sa;Password=9CknApvBHa2aNuovTirqhmEd";
    private int _userID;

    public NotificationBroker(int userId)
    {
        _userID = userId;
    }


    public void Initialization()
    {
        // Create a dependency connection.
        SqlDependency.Start(this.ConnectionString);
        CanRequestNotifications();
        _isActive = true;
    }
    
    public DataTable StartNotification()
    {
        DataTable dt = new DataTable();
        SqlConnection connection = new SqlConnection(this.ConnectionString);  
        connection.Open();


        using (SqlCommand command = new SqlCommand(
                   "SELECT Description, ReceiverId FROM dbo.Notifications",
                   connection))
        {

            // SqlCommand command = new SqlCommand();  
            // command.CommandText = "SELECT Id ,Description ,SenderId ,ReceiverId ,StudentSupervisorId ,TeacherId FROM Notifications";  
            // command.Connection = connection;  
            // command.CommandType = CommandType.Text;  

            this.dependency = new SqlDependency(command);
            dependency.OnChange += new OnChangeEventHandler(OnRateChange);
            
            

            Initialization();

            dt.Load(command.ExecuteReader(CommandBehavior.CloseConnection));
        }

        return dt;
    }  
    private void OnRateChange(object s, SqlNotificationEventArgs e) {
        // resubscribe
        DataTable dt = StartNotification();
        if ((int) dt.Rows[0]["ReceiverId"] == _userID)
        {
            new ToastContentBuilder()
                .AddText($"{ dt.Rows[0]["Description"]}")
                .Show();
        }

    }  
    public void StopNotification() {  
        SqlDependency.Stop(this.ConnectionString, "QueueName");  
        _isActive = false;
    }  
    private bool CanRequestNotifications()
    {
        SqlClientPermission permission =
            new SqlClientPermission(
                PermissionState.Unrestricted);
        try
        {
            permission.Demand();
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    public static bool CreateNotification(DatabaseContext d,StudentSupervisor studentSupervisor, string message)
    {
        d.Notifications.Add(new Notification() {Description = message,Receiver = studentSupervisor});
        d.SaveChanges();
        return true;
    }
}