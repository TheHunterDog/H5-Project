ALTER DATABASE StudentBegeleid SET ENABLE_BROKER;

CREATE QUEUE NotificationQueue
CREATE SERVICE NotificationChange ON QUEUE NotificationQueue
    ([http://schemas.microsoft.com/SQL/Notifications/PostQueryNotification]);


GRANT SUBSCRIBE QUERY NOTIFICATIONS TO DATABASE_PRINCIPAL