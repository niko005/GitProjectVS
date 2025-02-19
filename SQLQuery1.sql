CREATE TABLE Priorities (
    PriorityID int IDENTITY(1,1)  PRIMARY KEY,
    PriorityName NVARCHAR(20) NOT NULL
);

CREATE TABLE Statuses (
    StatusID int IDENTITY(1,1)  PRIMARY KEY,
    StatusName NVARCHAR(20) NOT NULL
)

ALTER DATABASE [C:\Users\nikam\OneDrive\Документы\employee.mdf] SET ENABLE_BROKER;

UPDATE Notifications SET IsRead = 0 WHERE NotificationsToEmployeeID=2 AND IsRead = 1


select * from Priorities

CREATE TABLE Notifications (
    NotificationID int IDENTITY(1,1) PRIMARY KEY,
    NotificationsToEmployeeID int NOT NULL,
    Message NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    IsRead bit NOT NULL DEFAULT 0,
    FOREIGN KEY (NotificationsToEmployeeID) REFERENCES users(EmployeeID)
);


INSERT INTO Priorities (PriorityName) VALUES (N'низкий'), (N'средний'), (N'высокий');
INSERT INTO Statuses(StatusName) VALUES (N'открыто'), (N'в процессе'), (N'завершено');


CREATE TABLE Tasks (
    TaskID int IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(max),
    AssignedToEmployee int,
    Date_register Date,
    PriorityID int NOT NULL,
    Deadline DATETIME,
    StatusID int NOT NULL,
    CompletionDate DATE,
    FOREIGN KEY (AssignedToEmployee) REFERENCES users(EmployeeID),
    FOREIGN KEY (PriorityID) REFERENCES Priorities(PriorityID),
    FOREIGN KEY (StatusID) REFERENCES Statuses(StatusID)
);


CREATE TABLE users
(
	EmployeeID INT PRIMARY KEY IDENTITY(1,1),
	email NVARCHAR(50),
	username NVARCHAR(MAX),
	password NVARCHAR(MAX),
	lastname NVARCHAR(MAX),
	firstname NVARCHAR(MAX),
	isadmin BIT DEFAULT 0,
	date_register DATE,
    insert_date  DATE NULL,
    update_date DATE NULL,
    delete_date DATE NULL
)


SELECT Tasks.TaskID, Tasks.Title, Tasks.Description, users.Lastname + ' ' + users.Firstname AS AssignedToEmployee,
                            Statuses.StatusName AS Status, Tasks.Deadline
                            FROM Tasks
                            INNER JOIN users ON Tasks.AssignedToEmployee = users.EmployeeID
                            INNER JOIN Statuses ON Tasks.StatusID = Statuses.StatusID
                            WHERE Tasks.StatusID IS NOT NULL AND Tasks.StatusID <> 3




SELECT Tasks.TaskID, Tasks.Title, Tasks.Description, 
       users.lastname + ' ' + users.firstname AS AssignedToEmployee, 
       Statuses.StatusName AS Status, Tasks.Deadline , Priorities.PriorityName as Priority
FROM Tasks 
INNER JOIN users ON Tasks.AssignedToEmployee = users.EmployeeID
INNER JOIN Statuses ON Tasks.StatusID = Statuses.StatusID 
INNER JOIN Priorities ON Tasks.PriorityID = Priorities.PriorityID
WHERE Tasks.StatusID <> 3 
AND Tasks.Deadline >= '2024-06-01' 
AND Tasks.Deadline <= '2024-11-30'
AND Tasks.AssignedToEmployee = 3
AND Priorities.PriorityID =1




 
SELECT Tasks.TaskID, Tasks.Title, Tasks.Description, users.Lastname + ' ' + users.Firstname AS AssignedToEmployee,
Statuses.StatusName AS Status, Tasks.Deadline
FROM Tasks
INNER JOIN users ON Tasks.AssignedToEmployee = users.EmployeeID
INNER JOIN Statuses ON Tasks.StatusID = Statuses.StatusID
WHERE Tasks.StatusID IS NOT NULL AND Tasks.StatusID <> 3 AND users.EmployeeID = 2