using System.Data;
using System.Data.SqlClient;
using System.Security.Permissions;
using Database.Model;
using Microsoft.Toolkit.Uwp.Notifications;


namespace WPF.Util;

public class NotificationBroker
{
    /// <summary>
    /// Name of services in the database
    /// </summary>
    private const string QueueName = "NotificationQueue";
    private const string ServiceName = "NotificationChange";
    
    /// <summary>
    /// Check if the broker is active
    /// </summary>
    public bool IsActive => _isActive;

    private bool _isActive;
    private SqlDependency? _dependency;
    private const string ConnectionString = "Server=ftp.huttennl.nl,1433;Database=StudentBegeleid;User Id=sa;Password=9CknApvBHa2aNuovTirqhmEd";
    //The userId that is the receiver
    private readonly int _userId;

    public NotificationBroker(int userId)
    {
        _userId = userId;
    }


    private void Initialization()
    {
        // Create a dependency connection.
        SqlDependency.Start(ConnectionString);
        CanRequestNotifications();
        _isActive = true;
    }
    /// <summary>
    /// Start to listen for notifications
    /// </summary>
    /// <returns>DataTable with the notification data</returns>
    public DataTable StartNotification()
    {
        var dt = new DataTable();
        var connection = new SqlConnection(ConnectionString);  
        connection.Open();


        using var command = new SqlCommand(
            "SELECT Description, ReceiverId FROM dbo.Notifications",
            connection);
        this._dependency = new SqlDependency(command);
        _dependency.OnChange += OnRateChange;
        Initialization();
        dt.Load(command.ExecuteReader(CommandBehavior.CloseConnection));

        return dt;
    }  
    /// <summary>
    /// Event that runs whenever something is done to the database table
    /// </summary>
    /// <param name="s">The sender</param>
    /// <param name="e">The arguments</param>
    private void OnRateChange(object s, SqlNotificationEventArgs e)
    {
        _isActive = false;
        // resubscribe
        var dt = StartNotification();
        if ((int) dt.Rows[^1]["ReceiverId"] == _userId)
        {
            new ToastContentBuilder()
                .AddText($"{ dt.Rows[^1]["Description"]}")
                .Show();
        }
    }  
    /// <summary>
    /// Stop listening to for changes to the database
    /// </summary>
    public void StopNotification() {  
        SqlDependency.Stop(ConnectionString, QueueName);  
        _isActive = false;
    }  
    private bool CanRequestNotifications()
    {
        var permission =
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

    /// <summary>
    /// Expected format for each notification
    /// </summary>
    /// <param name="context"><see cref="DatabaseContext"/></param>
    /// <param name="studentSupervisor"><see cref="StudentSupervisor"/></param>
    /// <param name="message">The text of the description</param>
    /// <returns>Boolean if successful</returns>
    public static bool CreateNotification(DatabaseContext context,StudentSupervisor studentSupervisor, string message)
    {
        try
        {
            context.Notifications.Add(new Notification() {Description = message, Receiver = studentSupervisor});
            context.SaveChanges();
            return true;
        }
        catch (SqlException)
        {
            
            return false;
        }
    }
}