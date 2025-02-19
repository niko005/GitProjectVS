CREATE TABLE Priorities (
    PriorityID TINYINT IDENTITY(1,1)  PRIMARY KEY,
    PriorityName NVARCHAR(20) NOT NULL);
CREATE TABLE Statuses (
    StatusID TINYINT IDENTITY(1,1)  PRIMARY KEY,
    StatusName NVARCHAR(20) NOT NULL);
INSERT INTO Priorities (PriorityName) VALUES (N'Низкий'), (N'Средний'), (N'Высокий');
INSERT INTO Statuses(StatusName) VALUES (N'Открыто'), (N'В процессе'), (N'Завершено');
CREATE TABLE users(
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
	email NVARCHAR(100),
	username NVARCHAR(255),
	password NVARCHAR(255),
	lastname NVARCHAR(255),
	firstname NVARCHAR(255),
    isadmin bit default 0,
    insert_date  DATE,
    update_date DATE NULL,
    delete_date DATE NULL);
CREATE TABLE Notifications (
    NotificationID int IDENTITY(1,1) PRIMARY KEY,
    NotificationsToEmployeeID int NOT NULL,
    Message NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    IsRead bit NOT NULL DEFAULT 0,
    FOREIGN KEY (NotificationsToEmployeeID) REFERENCES users(EmployeeID));
CREATE TABLE Documents(
    DocumentID int IDENTITY(1,1)  PRIMARY KEY,
    Document VARBINARY(MAX) NULL,
    DocumentName NVARCHAR(255) NULL);
CREATE TABLE Tasks (
    TaskID int IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(max),
    AssignedToEmployee int,
    Date_register Date,
    PriorityID TINYINT NOT NULL,
    Deadline DATETIME,
    StatusID TINYINT NOT NULL,
    CompletionDate DATE,
    DocumentID int NULL,
    FOREIGN KEY (AssignedToEmployee) REFERENCES users(EmployeeID),
    FOREIGN KEY (PriorityID) REFERENCES Priorities(PriorityID),
    FOREIGN KEY (StatusID) REFERENCES Statuses(StatusID),
    FOREIGN KEY (DocumentID) REFERENCES Documents(DocumentID) ON DELETE CASCADE);

select * from Users
SELECT * FROM users Where isadmin = 1 and delete_date IS NULL
SELECT username, isadmin FROM users Where users.isadmin = 1
SELECT EmployeeID, username, isadmin FROM users Where isadmin = 1 and delete_date IS NULL
SELECT COUNT(*) FROM Notifications WHERE NotificationsToEmployeeID=2 AND IsRead=0


SELECT Tasks.TaskID, Tasks.Title, Tasks.Description, users.Lastname + ' ' + users.Firstname AS AssignedToEmployee, CompletionDate,
    Statuses.StatusName AS Status, Tasks.Deadline, Priorities.PriorityName as Priority,
    Tasks.PriorityID, Tasks.StatusID, Documents.DocumentName as DocumentName, Documents.Document as Document, Documents.DocumentID as DocumentID
    FROM Tasks
    INNER JOIN users ON Tasks.AssignedToEmployee = users.EmployeeID
    INNER JOIN Statuses ON Tasks.StatusID = Statuses.StatusID
    INNER JOIN Priorities ON Tasks.PriorityID = Priorities.PriorityID
    FULL JOIN Documents ON Tasks.DocumentID = Documents.DocumentID
    WHERE Tasks.StatusID IS NOT NULL

