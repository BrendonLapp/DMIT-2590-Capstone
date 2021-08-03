--Drop DATABASE
DROP DATABASE Capstone_PROD_1;
GO

--CREATE DATABASE
CREATE DATABASE Capstone_PROD_1;
GO

--USE DATABASE
USE Capstone_PROD_1;
GO

 --DROP TABLES (Must Alter Some Tables To Drop Them)
DROP TABLE TB_Capstone_ALLOCATION;
DROP TABLE TB_Capstone_OVERTIME;
DROP TABLE TB_Capstone_OVERTIME_TYPE;
DROP TABLE TB_Capstone_PROJECT_DETAIL;
DROP TABLE TB_Capstone_PROJECT;
DROP TABLE TB_Capstone_PROJECT_CATEGORY;
DROP TABLE TB_Capstone_ENTITLED_TIME_OFF; 
DROP TABLE TB_Capstone_ABSENCE_DETAIL;
DROP TABLE TB_Capstone_OFFDAY_TYPE;
DROP TABLE TB_Capstone_SCHEDULE_TYPE_DETAILS; 
DROP TABLE TB_Capstone_SCHEDULE_TYPE;
ALTER TABLE TB_Capstone_ROLE
DROP CONSTRAINT FK_CreatedByRole;
ALTER TABLE TB_Capstone_ROLE
DROP CONSTRAINT FK_UpdatedByRole;
ALTER TABLE TB_Capstone_DEPARTMENT
DROP CONSTRAINT FK_CreatedByDepartment;
ALTER TABLE TB_Capstone_DEPARTMENT
DROP CONSTRAINT FK_UpdatedByDepartment;
ALTER TABLE TB_Capstone_AREA
DROP CONSTRAINT FK_CreatedByArea;
ALTER TABLE TB_Capstone_AREA
DROP CONSTRAINT FK_UpdatedByArea;
ALTER TABLE TB_Capstone_UNIT
DROP CONSTRAINT FK_CreatedByUnit;
ALTER TABLE TB_Capstone_UNIT
DROP CONSTRAINT FK_UpdatedByUnit;
ALTER TABLE TB_Capstone_TEAM
DROP CONSTRAINT FK_CreatedByTeam;
ALTER TABLE TB_Capstone_TEAM
DROP CONSTRAINT FK_UpdatedByTeam;
ALTER TABLE TB_Capstone_POSITION
DROP CONSTRAINT FK_CreatedByPosition;
ALTER TABLE TB_Capstone_POSITION
DROP CONSTRAINT FK_UpdatedByPosition;
ALTER TABLE TB_Capstone_EMPLOYEE
DROP CONSTRAINT FK_UpdatedByEmployee;
ALTER TABLE TB_Capstone_TEAM_HISTORY
DROP CONSTRAINT FK_CreatedByTeamHistory;
ALTER TABLE TB_Capstone_TEAM_HISTORY
DROP CONSTRAINT FK_UpdatedByTeamHistory;
DROP TABLE TB_Capstone_TEAM_HISTORY;
DROP TABLE TB_Capstone_PAID_HOLIDAY;
DROP TABLE TB_Capstone_EMPLOYEE;
DROP TABLE TB_Capstone_ROLE;
DROP TABLE TB_Capstone_POSITION;
DROP TABLE TB_Capstone_TEAM; 
DROP TABLE TB_Capstone_UNIT;
DROP TABLE TB_Capstone_AREA;
DROP TABLE TB_Capstone_DEPARTMENT;
GO

--CREATE TABLES
CREATE TABLE TB_Capstone_ROLE
(
	RoleID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	RoleTitle VARCHAR(30) NOT NULL,
	Description VARCHAR(50) NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	CreatedBy INT NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_DeactivationDateRole CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateRole CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateRole CHECK (UpdatedDate >= CreationDate)
);

CREATE TABLE TB_Capstone_DEPARTMENT
(
	DepartmentID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	DepartmentName VARCHAR (30) NOT NULL, 
	Description VARCHAR (50) NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE  NULL,
	CreatedBy INT NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_DeactivationDateDepartment CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateDepartment CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateDepartment CHECK (UpdatedDate >= CreationDate)
);

CREATE TABLE TB_Capstone_AREA
(
	AreaID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	DepartmentID INT FOREIGN KEY REFERENCES TB_Capstone_DEPARTMENT(DepartmentID) NOT NULL,
	AreaName VARCHAR (30) NOT NULL, 
	Description VARCHAR (50) NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	CreatedBy INT NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_DeactivationDateArea CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateArea CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateArea CHECK (UpdatedDate >= CreationDate)
);

CREATE TABLE TB_Capstone_UNIT
(
	UnitID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	AreaID INT FOREIGN KEY REFERENCES TB_Capstone_AREA(AreaID) NOT NULL,
	UnitName VARCHAR(30) NOT NULL,
	Description VARCHAR(50) NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	CreatedBy INT NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_DeactivationDateUnit CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateUnit CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateUnit CHECK (UpdatedDate >= CreationDate)
);

CREATE TABLE TB_Capstone_TEAM
(
	TeamID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	UnitID INT FOREIGN KEY REFERENCES TB_Capstone_UNIT (UnitID) NOT NULL,
	TeamName VARCHAR(30) NOT NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	CreatedBy INT NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_DeactivationDateTeam CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateTeam CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateTeam CHECK (UpdatedDate >= CreationDate)
);

CREATE TABLE TB_Capstone_POSITION
(
	PositionID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	PositionTitle VARCHAR(30) NOT NULL,
	Description VARCHAR(50) NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	CreatedBy INT NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_DeactivationDatePosition CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDatePosition CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDatePosition CHECK (UpdatedDate >= CreationDate)
);

CREATE TABLE TB_Capstone_EMPLOYEE
(
	EmployeeID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	PositionID INT FOREIGN KEY REFERENCES TB_Capstone_POSITION (PositionID) NOT NULL,
	UserID VARCHAR (50) UNIQUE NOT NULL,
	FirstName VARCHAR(40) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	PhoneNumber VARCHAR(16) CHECK (PhoneNumber LIKE '+[0-9]([0-9][0-9][0-9])-[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]') NOT NULL,
	AlternatePhoneNumber VARCHAR(16) CHECK (AlternatePhoneNumber LIKE '+[0-9]([0-9][0-9][0-9])-[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]') NULL,
	StationNumber INT NULL,
	ComputerNumber INT NULL,
	CompanyPhoneNumber VARCHAR(16) CHECK (CompanyPhoneNumber LIKE '+[0-9]([0-9][0-9][0-9])-[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]') NOT NULL,
	BirthDate DATE NOT NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	EmergencyContactName1 VARCHAR(50) NOT NULL,
	EmergencyContactPhoneNumber1 VARCHAR(16) CHECK (EmergencyContactPhoneNumber1 LIKE '+[0-9]([0-9][0-9][0-9])-[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]') NOT NULL,
	EmergencyContactName2 VARCHAR(50) NULL,
	EmergencyContactPhoneNumber2 VARCHAR(16) CHECK (EmergencyContactPhoneNumber2 LIKE '+[0-9]([0-9][0-9][0-9])-[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]') NULL,
	CreatedBy INT NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_DeactivationDateEmployee CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateEmployee CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateEmployee CHECK (UpdatedDate >= CreationDate)
);

CREATE TABLE TB_Capstone_OFFDAY_TYPE
(
	OffDayID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	Name VARCHAR(50) NOT NULL,
	AbbreviatedName VARCHAR(15) NOT NULL,
	Description VARCHAR(50) NULL,
	PTO BIT NOT NULL,
	Notes VARCHAR(100) NULL,
	Color Varchar(7) NOT NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	CreatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_DeactivationDateOffDayType CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateOffDayType CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateOffDayType CHECK (UpdatedDate >= CreationDate)
);

CREATE TABLE TB_Capstone_SCHEDULE_TYPE
(
	ScheduleTypeID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	Name VARCHAR(30) NOT NULL,
	Description VARCHAR(50) NULL,
	HoursPerDay DECIMAL(5,2) CHECK (HoursPerDay >= 0 AND HoursPerDay <= 24) NOT NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	CreatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_DeactivationDateScheduleType CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateScheduleType CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateScheduleType CHECK (UpdatedDate >= CreationDate)
);
CREATE TABLE TB_Capstone_SCHEDULE_TYPE_DETAILS
(
	ScheduleTypeDetailID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	ScheduleTypeID INT FOREIGN KEY REFERENCES TB_Capstone_SCHEDULE_TYPE(ScheduleTypeID) NOT NULL,
	EmployeeID INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NOT NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	CreatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_DeactivationDateScheduleTypeDetails CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateScheduleTypeDetails CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateSchdeduleTypeDetails CHECK (UpdatedDate >= CreationDate)
);

CREATE TABLE TB_Capstone_ENTITLED_TIME_OFF
(
	OffDayID INT FOREIGN KEY REFERENCES TB_Capstone_OFFDAY_TYPE(OffDayID),
	EmployeeID INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID),
	HoursAccumulated Decimal(8,2) CHECK(HoursAccumulated >=0) NOT NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	CreatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NULL,
	UpdatedDate DATE NULL,
	PRIMARY KEY(OffDayID,EmployeeID),
	CONSTRAINT CK_DeactivationDateEntitledTimeOff CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateEntitledTimeOff CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateEntitledTimeOff CHECK (UpdatedDate >= CreationDate)
);

CREATE TABLE TB_Capstone_ABSENCE_DETAIL
(
	AbsenceID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	OffDayID INT FOREIGN KEY REFERENCES TB_Capstone_OFFDAY_TYPE(OffDayID) NOT NULL,
	EmployeeID INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NOT NULL,
	HalfDay VARCHAR(2) CHECK (HalfDay IN ('AM', 'PM')) NULL,
	AbsenceDate DATE NOT NULL,
	Hours DECIMAL(5,2) CHECK (Hours > 0.00 AND Hours <= 24.00) NULL,
	Notes VARCHAR(100) NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	CreatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_ActivationDateAbsenceDetail CHECK (ActivationDate >= CreationDate),
	CONSTRAINT CK_DeactivationDateAbsenceDetail CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateAbsenceDetail CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateAbsenceDetail CHECK (UpdatedDate >= CreationDate)
);

CREATE TABLE TB_Capstone_TEAM_HISTORY
(
	TeamHistoryID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	TeamID INT FOREIGN KEY REFERENCES TB_Capstone_TEAM(TeamID) NOT NULL,
	EmployeeID INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NOT NULL,
	RoleID INT FOREIGN KEY REFERENCES TB_Capstone_ROLE(RoleID) NOT NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	CreatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID)  NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID)  NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_DeactivationDateTeamHistory CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateTeamHistory CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateTeamHistory CHECK (UpdatedDate >= CreationDate)
);


CREATE TABLE TB_Capstone_PROJECT_CATEGORY
(
	ProjectCategoryID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	CategoryName VARCHAR(30) NOT NULL,
	Description VARCHAR(50) NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	Global BIT NOT NULL,
	Color Varchar(7) NOT NULL,
	CreatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_DeactivationDateProjectCategory CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateProjectCategory CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateProjectCategory CHECK (UpdatedDate >= CreationDate)
);

CREATE TABLE TB_Capstone_PROJECT
(
	ProjectID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	ProjectCategoryID INT FOREIGN KEY REFERENCES TB_Capstone_PROJECT_CATEGORY(ProjectCategoryID)NOT NULL,
	ProjectName VARCHAR(30) NOT NULL,
	Description VARCHAR(50) NOT NULL,
	StartDate DATE NOT NULL,
	ProjectedEndDate DATE NOT NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	CreatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_StartDateProject CHECK(StartDate >= ActivationDate),
	CONSTRAINT CK_ProjectedEndDateProject CHECK (ProjectedEndDate >= CreationDate),
	CONSTRAINT CK_DeactivationDateProject CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateProject CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateProject CHECK (UpdatedDate >= CreationDate)
);

CREATE TABLE TB_Capstone_ALLOCATION
(
	AllocationID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	ProjectID INT FOREIGN KEY REFERENCES TB_Capstone_PROJECT(ProjectID) NOT NULL,
	EmployeeID INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NOT NULL,
	AllocatedDays DECIMAL(4,1) CHECK (AllocatedDays >=0.0) NOT NULL,
	Year INT CHECK(Year >= 0) NOT NULL,
	January DECIMAL(4,1) CHECK (January >= 0.0 AND January <= 31.0) NOT NULL,
	February DECIMAL(4,1) CHECK (February >= 0.0 AND February <= 29.0 ) NOT NULL,
	March DECIMAL(4,1) CHECK (March > = 0.0 AND March <= 31.0) NOT NULL,
	April DECIMAL(4,1) CHECK (April >= 0.0 AND April <= 30.0) NOT NULL,
	May DECIMAL(4,1) CHECK (May >= 0.0 AND May <= 31.0) NOT NULL,
	June DECIMAL(4,1) CHECK (June >= 0.0 AND June <= 30.0) NOT NULL,
	July DECIMAL(4,1) CHECK (July >= 0.0 AND July <= 31.0) NOT NULL,
	August DECIMAL(4,1) CHECK (August >= 0.0 AND August <= 31.0) NOT NULL,
	September DECIMAL(4,1) CHECK (September >= 0.0 AND September <= 30.0) NOT NULL,
	October DECIMAL(4,1) CHECK (October >= 0.0 AND October <= 31.0) NOT NULL,
	November DECIMAL(4,1) CHECK (November >= 0.0 AND November <= 30.0) NOT NULL,
	December DECIMAL(4,1) CHECK (December >= 0.0 AND December <= 31.0) NOT NULL,
	Notes VARCHAR(50) NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	CreatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NULL,
	UpdatedDate DATE NULL, 
	CONSTRAINT CK_DeactivationDateAllocation CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateAllocation CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateAllocation CHECK (UpdatedDate >= CreationDate)
);

CREATE TABLE TB_Capstone_PROJECT_DETAIL
(
	ProjectDetailID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	ProjectID INT FOREIGN KEY REFERENCES TB_Capstone_PROJECT(ProjectID) NOT NULL,
	EmployeeID INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NOT NULL,
	StartTime DATETIME NOT NULL,
	EndTime DATETIME NOT NULL,
	Notes VARCHAR(50) NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	CreatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_EndtimeProjectDetail CHECK (Endtime > StartTime),
	CONSTRAINT CK_DeactivationDateProjectDetail CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateProjectDetail CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateProjectDetail CHECK (UpdatedDate >= CreationDate)
);

CREATE TABLE TB_Capstone_OVERTIME_TYPE
(
	OvertimeTypeID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	Name VARCHAR(20) NOT NULL,
	PayMultiplier DECIMAL(3,2) CHECK (PayMultiplier > 0.00) NOT NULL,
	Description VARCHAR(50) NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	CreatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_DeactivationDateOvertimeType CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateOvertimeType CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateOvertimeType CHECK (UpdatedDate >= CreationDate)
);

CREATE TABLE TB_Capstone_OVERTIME
(
	OvertimeID INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
	EmployeeID INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) null,
	ProjectID INT FOREIGN KEY REFERENCES TB_Capstone_PROJECT(ProjectID) not null,
	OvertimeTypeID INT FOREIGN KEY REFERENCES TB_Capstone_OVERTIME_TYPE (OvertimeTypeID) NOT NULL,
	SubmissionDate DATE NOT NULL,
	ReviewDate DATE NULL,
	Amount Decimal(7,2) CHECK (Amount > 0.00) NOT NULL,
	StartTime DATETIME NOT NULL,
	EndTime DATETIME NOT NULL,
	SubmissionNotes VARCHAR(100) Null,
	ApprovalNotes VARCHAR(100) Null,
	Approved VARCHAR(1) CHECK (Approved IN ('A', 'D', 'P')) NOT NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	CreatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_SubmissionDateOvertime CHECK (SubmissionDate >= ActivationDate),
	CONSTRAINT CK_ReviewDateOvertime CHECK (ReviewDate >= SubmissionDate),
	CONSTRAINT CK_EndTimeOvertime CHECK (Endtime >= StartTime),
	CONSTRAINT CK_DeactivationDateOvertime CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDateOvertime CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDateOvertime CHECK (UpdatedDate >= CreationDate)
);

CREATE TABLE TB_Capstone_PAID_HOLIDAY
(
	PaidHolidayID INT PRIMARY KEY CLUSTERED Identity(1,1),
	HolidayName VARCHAR(50) NOT NULL,
	HolidayDate DATE NOT NULL,
	Notes VARCHAR(100) NULL,
	ActivationDate DATE NOT NULL,
	DeactivationDate DATE NULL,
	CreatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NOT NULL,
	CreationDate DATE NOT NULL,
	UpdatedBy INT FOREIGN KEY REFERENCES TB_Capstone_EMPLOYEE(EmployeeID) NULL,
	UpdatedDate DATE NULL,
	CONSTRAINT CK_DeactivationDatePaidHoliday CHECK (DeactivationDate >= ActivationDate),
	CONSTRAINT CK_CreationDatePaidHoliday CHECK (CreationDate <= DeactivationDate),
	CONSTRAINT CK_UpdatedDatePaidHoliday CHECK (UpdatedDate >= CreationDate)
);

--INSERTS PRE_ALTERS

--Test Data For TB_Capstone_DEPARTMENT
INSERT INTO TB_Capstone_DEPARTMENT(DepartmentName,Description,ActivationDate,CreatedBy,CreationDate)
VALUES ('Department Alpha', 'The first department', CAST('20190226' AS DATE), 1,  CAST('20190226' AS DATE)); 

INSERT INTO TB_Capstone_DEPARTMENT(DepartmentName,Description,ActivationDate,CreatedBy,CreationDate)
VALUES ('Department Beta', 'The second department', CAST('20190226' AS DATE), 1,  CAST('20190226' AS DATE)); 

INSERT INTO TB_Capstone_DEPARTMENT(DepartmentName,Description,ActivationDate,CreatedBy,CreationDate)
VALUES ('Department Gamma', 'The third department', CAST('20190226' AS DATE), 1,  CAST('20190226' AS DATE)); 

INSERT INTO TB_Capstone_DEPARTMENT(DepartmentName,Description,ActivationDate,CreatedBy,CreationDate)
VALUES ('Department Delta', 'The fourth department', CAST('20190226' AS DATE), 1,  CAST('20190226' AS DATE)); 

INSERT INTO TB_Capstone_DEPARTMENT(DepartmentName,Description,ActivationDate,CreatedBy,CreationDate)
VALUES ('Department Epsilion', 'The fifth department', CAST('20190226' AS DATE), 1,  CAST('20190226' AS DATE)); 

INSERT INTO TB_Capstone_DEPARTMENT(DepartmentName,Description,ActivationDate,CreatedBy,CreationDate)
VALUES ('Department Zeta', 'The sixth department', CAST('20190226' AS DATE), 1,  CAST('20190226' AS DATE)); 

INSERT INTO TB_Capstone_DEPARTMENT(DepartmentName,Description,ActivationDate,CreatedBy,CreationDate)
VALUES ('Department Eta', 'The seventh department', CAST('20190226' AS DATE), 1,  CAST('20190226' AS DATE)); 

INSERT INTO TB_Capstone_DEPARTMENT(DepartmentName,Description,ActivationDate,CreatedBy,CreationDate)
VALUES ('Department Theta', 'The eighth deparment', CAST('20190226' AS DATE), 1,  CAST('20190226' AS DATE)); 

INSERT INTO TB_Capstone_DEPARTMENT(DepartmentName,Description,ActivationDate,CreatedBy,CreationDate)
VALUES ('Department Iota', 'The ninth department', CAST('20190226' AS DATE), 1,  CAST('20190226' AS DATE)); 

INSERT INTO TB_Capstone_DEPARTMENT(DepartmentName,Description,ActivationDate,CreatedBy,CreationDate)
VALUES ('Department Kappa', 'The tenth department', CAST('20190226' AS DATE), 1,  CAST('20190226' AS DATE)); 

--Test Data For TB_Capstone_AREA
INSERT INTO TB_Capstone_AREA(DepartmentID, AreaName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 'Area 1', 'The first area', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_AREA(DepartmentID, AreaName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 'Area 2', 'The second area', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_AREA(DepartmentID, AreaName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 'Area 3', 'The third area', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_AREA(DepartmentID, AreaName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 'Area 4', 'The fourth area', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_AREA(DepartmentID, AreaName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (5, 'Area 5', 'The fifth area', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_AREA(DepartmentID, AreaName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (6, 'Area 6', 'The sixth area', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_AREA(DepartmentID, AreaName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (7, 'Area 7', 'The seventh area', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_AREA(DepartmentID, AreaName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (8, 'Area 8', 'The eighth area', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_AREA(DepartmentID, AreaName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (9, 'Area 9', 'The ninth area', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_AREA(DepartmentID, AreaName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (10, 'Area 10', 'The tenth area', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

--Test Data for TB_Capstone_UNIT
INSERT INTO TB_Capstone_UNIT(AreaID, UnitName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 'Unit 1', 'The first unit', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_UNIT(AreaID, UnitName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 'Unit 2', 'The second unit', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_UNIT(AreaID, UnitName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 'Unit 3', 'The third unit', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_UNIT(AreaID, UnitName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 'Unit 4', 'The fourth unit', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_UNIT(AreaID, UnitName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (5, 'Unit 5', 'The fifth unit', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_UNIT(AreaID, UnitName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (6, 'Unit 6', 'The sixth unit', CAST('20190226' AS DATE),1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_UNIT(AreaID, UnitName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (7, 'Unit 7', 'The seventh unit', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_UNIT(AreaID, UnitName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (8, 'Unit 8', 'The eighth unit', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_UNIT(AreaID, UnitName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (9, 'Unit 9', 'The ninth unit', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_UNIT(AreaID, UnitName, Description, ActivationDate, CreatedBy, CreationDate)
VALUES (10, 'Unit 10', 'The tenth unit', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

--Test Data For TB_Capstone_TEAM
INSERT INTO TB_Capstone_TEAM(UnitID, TeamName, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 'Team Code Logic', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_TEAM(UnitID, TeamName, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 'Logical Development', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_TEAM(UnitID, TeamName, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 'Future Tech Developers', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_TEAM(UnitID, TeamName, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 'Proactive Solutions', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

--Test Data for TB_Capstone_ROLE
INSERT INTO TB_Capstone_ROLE(RoleTitle, Description, ActivationDate, CreatedBy, CreationDate)
VALUES ('Employee', 'Basic employee', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_ROLE(RoleTitle, Description, ActivationDate, CreatedBy, CreationDate)
VALUES ('Supervisor', 'Supervisor role', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_ROLE(RoleTitle, Description, ActivationDate, CreatedBy, CreationDate)
VALUES ('Team Admin', 'Admin for their own team', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_ROLE(RoleTitle, Description, ActivationDate, CreatedBy, CreationDate)
VALUES ('Global Admin', 'Admin for every team', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

--Test Data for TB_Capstone_POSITION
INSERT INTO TB_Capstone_POSITION(PositionTitle, Description, ActivationDate, CreatedBy, CreationDate)
VALUES('Programmer', 'Entry level programmer', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_POSITION(PositionTitle, Description, ActivationDate, CreatedBy, CreationDate)
VALUES('Database Programmer', 'Entry level DBA', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_POSITION(PositionTitle, Description, ActivationDate, CreatedBy, CreationDate)
VALUES('Front End Developer', 'Creates user friendly designs', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_POSITION(PositionTitle, Description, ActivationDate, CreatedBy, CreationDate)
VALUES('Web Developer', 'Creates beautiful websites', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

--Test Data FOR TB_Capstone_EMPLOYEE
INSERT INTO TB_Capstone_EMPLOYEE(PositionID, UserID, FirstName, LastName, PhoneNumber, AlternatePhoneNumber, CompanyPhoneNumber, BirthDate, ActivationDate, EmergencyContactName1, EmergencyContactPhoneNumber1, EmergencyContactName2, EmergencyContactPhoneNumber2, CreatedBy, CreationDate)
VALUES (4, 'DBourne1', 'Denny', 'Bourne', '+1(780)-555-5555', '+1(780)-555-5556', '+1(780)-111-1111', CAST('19991121' AS DATE), CAST('20190226' AS DATE), 'Zakaria Tate', '+9(999)-999-9999', 'Katelyn Sharpe', '+9(888)-888-8888', 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_EMPLOYEE(PositionID, UserID, FirstName, LastName, PhoneNumber, AlternatePhoneNumber, CompanyPhoneNumber, BirthDate, ActivationDate, EmergencyContactName1, EmergencyContactPhoneNumber1, EmergencyContactName2, EmergencyContactPhoneNumber2, CreatedBy, CreationDate)
VALUES (4, 'EMacleod1', 'Erin', 'Macleod', '+1(780)-555-5555', '+1(780)-555-5556', '+1(780)-111-1111', CAST('19991121' AS DATE), CAST('20190226' AS DATE), 'Jane Cox', '+9(999)-999-9999', 'Alexandra Snow', '+9(888)-888-8888', 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_EMPLOYEE(PositionID, UserID, FirstName, LastName, PhoneNumber, AlternatePhoneNumber, CompanyPhoneNumber, BirthDate, ActivationDate, EmergencyContactName1, EmergencyContactPhoneNumber1, EmergencyContactName2, EmergencyContactPhoneNumber2, CreatedBy, CreationDate)
VALUES (4, 'NHart1', 'Neriah', 'Hart', '+1(780)-555-5555', '+1(780)-555-5556', '+1(780)-111-1111', CAST('19991121' AS DATE), CAST('20190226' AS DATE), 'Rhys Cox', '+9(999)-999-9999', 'Lana Atkinson', '+9(888)-888-8888', 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_EMPLOYEE(PositionID, UserID, FirstName, LastName, PhoneNumber, AlternatePhoneNumber, CompanyPhoneNumber, BirthDate, ActivationDate, EmergencyContactName1, EmergencyContactPhoneNumber1, EmergencyContactName2, EmergencyContactPhoneNumber2, CreatedBy, CreationDate)
VALUES (4, 'THammond1', 'Tadhg', 'Hammond', '+1(780)-555-5555', '+1(780)-555-5556', '+1(780)-111-1111', CAST('19991121' AS DATE), CAST('20190226' AS DATE), 'Theodore Quinn', '+9(999)-999-9999', 'Cory Lambert', '+9(888)-888-8888', 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_EMPLOYEE(PositionID, UserID, FirstName, LastName, PhoneNumber, AlternatePhoneNumber, CompanyPhoneNumber, BirthDate, ActivationDate, EmergencyContactName1, EmergencyContactPhoneNumber1, EmergencyContactName2, EmergencyContactPhoneNumber2, CreatedBy, CreationDate)
VALUES (4, 'RBlackmore1', 'Randall', 'Blackmore', '+1(780)-555-5555', '+1(780)-555-5556', '+1(780)-111-1111', CAST('19991121' AS DATE), CAST('20190226' AS DATE), 'Adrian Atkins', '+9(999)-999-9999', 'Linda Whitaker', '+9(888)-888-8888', 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_EMPLOYEE(PositionID, UserID, FirstName, LastName, PhoneNumber, AlternatePhoneNumber, CompanyPhoneNumber, BirthDate, ActivationDate, EmergencyContactName1, EmergencyContactPhoneNumber1, EmergencyContactName2, EmergencyContactPhoneNumber2, CreatedBy, CreationDate)
VALUES (4, 'TSimmons1', 'Taryn', 'Simmons', '+1(780)-555-5555', '+1(780)-555-5556', '+1(780)-111-1111', CAST('19991121' AS DATE), CAST('20190226' AS DATE), 'Sara Donovan', '+9(999)-999-9999', 'Bella Vasquez', '+9(888)-888-8888', 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_EMPLOYEE(PositionID, UserID, FirstName, LastName, PhoneNumber, AlternatePhoneNumber, CompanyPhoneNumber, BirthDate, ActivationDate, EmergencyContactName1, EmergencyContactPhoneNumber1, EmergencyContactName2, EmergencyContactPhoneNumber2, CreatedBy, CreationDate)
VALUES (4, 'VTucker1', 'Vikram', 'Tucker', '+1(780)-555-5555', '+1(780)-555-5556', '+1(780)-111-1111', CAST('19991121' AS DATE), CAST('20190226' AS DATE), 'Dale Guerra', '+9(999)-999-9999', 'Ria Calderon', '+9(888)-888-8888', 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_EMPLOYEE(PositionID, UserID, FirstName, LastName, PhoneNumber, AlternatePhoneNumber, CompanyPhoneNumber, BirthDate, ActivationDate, EmergencyContactName1, EmergencyContactPhoneNumber1, EmergencyContactName2, EmergencyContactPhoneNumber2, CreatedBy, CreationDate)
VALUES (1, 'RonanGarcia1', 'Ronan', 'Garcia', '+1(780)-555-5555', '+1(780)-555-5556', '+1(780)-111-1111', CAST('19790101' AS DATE), CAST('20190226' AS DATE), 'Tanya Berg', '+9(999)-999-9999', 'Fatima Shaffer', '+9(888)-888-8888', 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_EMPLOYEE(PositionID, UserID, FirstName, LastName, PhoneNumber, AlternatePhoneNumber, CompanyPhoneNumber, BirthDate, ActivationDate, EmergencyContactName1, EmergencyContactPhoneNumber1, EmergencyContactName2, EmergencyContactPhoneNumber2, CreatedBy, CreationDate)
VALUES (2, 'CarlosGlenn1', 'Carlos', 'Glenn', '+1(780)-555-5555', '+1(780)-555-5556', '+1(780)-111-1111', CAST('19720101' AS DATE), CAST('20190226' AS DATE), 'Gregory Mora', '+9(999)-999-9999', 'Amy Gates', '+9(888)-888-8888', 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_EMPLOYEE(PositionID, UserID, FirstName, LastName, PhoneNumber, AlternatePhoneNumber, CompanyPhoneNumber, BirthDate, ActivationDate, EmergencyContactName1, EmergencyContactPhoneNumber1, EmergencyContactName2, EmergencyContactPhoneNumber2, CreatedBy, CreationDate)
VALUES (3, 'KeatonDay1', 'Keaton', 'Day', '+1(780)-555-5555', '+1(780)-555-5556', '+1(780)-111-1111', CAST('19900101' AS DATE), CAST('20190226' AS DATE), 'Carmen Rubio', '+9(999)-999-9999', 'Lisa Jimenez', '+9(888)-888-8888', 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_EMPLOYEE(PositionID, UserID, FirstName, LastName, PhoneNumber, AlternatePhoneNumber, CompanyPhoneNumber, BirthDate, ActivationDate, EmergencyContactName1, EmergencyContactPhoneNumber1, EmergencyContactName2, EmergencyContactPhoneNumber2, CreatedBy, CreationDate)
VALUES (4, 'ZakariaLambert1', 'Zakaria', 'Lambert', '+1(780)-555-5555', '+1(780)-555-5556', '+1(780)-111-1111', CAST('19991121' AS DATE), CAST('20190226' AS DATE), 'Timothy Norris', '+9(999)-999-9999', 'Jerry Stephens', '+9(888)-888-8888', 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_EMPLOYEE(PositionID, UserID, FirstName, LastName, PhoneNumber, AlternatePhoneNumber, CompanyPhoneNumber, BirthDate, ActivationDate, EmergencyContactName1, EmergencyContactPhoneNumber1, EmergencyContactName2, EmergencyContactPhoneNumber2, CreatedBy, CreationDate)
VALUES (4, 'Administrator', 'Global', 'Admin', '+1(780)-555-5555', '+1(780)-555-5556', '+1(780)-111-1111', CAST('19991121' AS DATE), CAST('20190226' AS DATE), 'Zakaria Tate', '+9(999)-999-9999', 'Katelyn Sharpe', '+9(888)-888-8888', 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_EMPLOYEE(PositionID, UserID, FirstName, LastName, PhoneNumber, AlternatePhoneNumber, CompanyPhoneNumber, BirthDate, ActivationDate, EmergencyContactName1, EmergencyContactPhoneNumber1, EmergencyContactName2, EmergencyContactPhoneNumber2, CreatedBy, CreationDate)
VALUES (3, 'TeamAdmin', 'Team', 'Admin', '+1(780)-555-5555', '+1(780)-555-5556', '+1(780)-111-1111', CAST('19991121' AS DATE), CAST('20190226' AS DATE), 'Zakaria Tate', '+9(999)-999-9999', 'Katelyn Sharpe', '+9(888)-888-8888', 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_EMPLOYEE(PositionID, UserID, FirstName, LastName, PhoneNumber, AlternatePhoneNumber, CompanyPhoneNumber, BirthDate, ActivationDate, EmergencyContactName1, EmergencyContactPhoneNumber1, EmergencyContactName2, EmergencyContactPhoneNumber2, CreatedBy, CreationDate)
VALUES (2, 'Supervisor', 'Super', 'Visor', '+1(780)-555-5555', '+1(780)-555-5556', '+1(780)-111-1111', CAST('19991121' AS DATE), CAST('20190226' AS DATE), 'Zakaria Tate', '+9(999)-999-9999', 'Katelyn Sharpe', '+9(888)-888-8888', 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_EMPLOYEE(PositionID, UserID, FirstName, LastName, PhoneNumber, AlternatePhoneNumber, CompanyPhoneNumber, BirthDate, ActivationDate, EmergencyContactName1, EmergencyContactPhoneNumber1, EmergencyContactName2, EmergencyContactPhoneNumber2, CreatedBy, CreationDate)
VALUES (1, 'Employee', 'Emp', 'Loyee', '+1(780)-555-5555', '+1(780)-555-5556', '+1(780)-111-1111', CAST('19991121' AS DATE), CAST('20190226' AS DATE), 'Zakaria Tate', '+9(999)-999-9999', 'Katelyn Sharpe', '+9(888)-888-8888', 1, CAST('20190226' AS DATE));
--Alter table statements to add UserID foreign key to parent tables of TB_Capstone_EMPLOYEE
ALTER TABLE TB_Capstone_DEPARTMENT
ADD CONSTRAINT FK_CreatedByDepartment
FOREIGN KEY (CreatedBy) REFERENCES TB_Capstone_EMPLOYEE(EmployeeID);

ALTER TABLE TB_Capstone_DEPARTMENT
ADD CONSTRAINT FK_UpdatedByDepartment
FOREIGN KEY (UpdatedBy) REFERENCES TB_Capstone_EMPLOYEE(EmployeeID);

ALTER TABLE TB_Capstone_EMPLOYEE
ADD CONSTRAINT FK_UpdatedByEmployee
FOREIGN KEY (UpdatedBy) REFERENCES TB_Capstone_EMPLOYEE(EmployeeID);

ALTER TABLE TB_Capstone_ROLE
ADD CONSTRAINT FK_CreatedByRole
FOREIGN KEY (CreatedBy) REFERENCES TB_Capstone_EMPLOYEE(EmployeeID);

ALTER TABLE TB_Capstone_ROLE
ADD CONSTRAINT FK_UpdatedByRole
FOREIGN KEY (UpdatedBy) REFERENCES TB_Capstone_EMPLOYEE(EmployeeID);

ALTER TABLE TB_Capstone_AREA
ADD CONSTRAINT FK_CreatedByArea
FOREIGN KEY (CreatedBy) REFERENCES TB_Capstone_EMPLOYEE(EmployeeID);

ALTER TABLE TB_Capstone_AREA
ADD CONSTRAINT FK_UpdatedByArea
FOREIGN KEY (UpdatedBy) REFERENCES TB_Capstone_EMPLOYEE(EmployeeID);

ALTER TABLE TB_Capstone_UNIT
ADD CONSTRAINT FK_CreatedByUnit
FOREIGN KEY (CreatedBy) REFERENCES TB_Capstone_EMPLOYEE(EmployeeID);

ALTER TABLE TB_Capstone_UNIT
ADD CONSTRAINT FK_UpdatedByUnit
FOREIGN KEY (UpdatedBy) REFERENCES TB_Capstone_EMPLOYEE(EmployeeID);

ALTER TABLE TB_Capstone_TEAM
ADD CONSTRAINT FK_CreatedByTeam
FOREIGN KEY (CreatedBy) REFERENCES TB_Capstone_EMPLOYEE(EmployeeID);

ALTER TABLE TB_Capstone_TEAM
ADD CONSTRAINT FK_UpdatedByTeam
FOREIGN KEY (UpdatedBy) REFERENCES TB_Capstone_EMPLOYEE(EmployeeID);

ALTER TABLE TB_Capstone_POSITION
ADD CONSTRAINT FK_CreatedByPosition
FOREIGN KEY (CreatedBy) REFERENCES TB_Capstone_EMPLOYEE(EmployeeID);

ALTER TABLE TB_Capstone_POSITION
ADD CONSTRAINT FK_UpdatedByPosition
FOREIGN KEY (UpdatedBy) REFERENCES TB_Capstone_EMPLOYEE(EmployeeID);

ALTER TABLE TB_Capstone_TEAM_HISTORY
ADD CONSTRAINT FK_CreatedByTeamHistory
FOREIGN KEY (CreatedBy) REFERENCES TB_Capstone_EMPLOYEE(EmployeeID);

ALTER TABLE TB_Capstone_TEAM_HISTORY
ADD CONSTRAINT FK_UpdatedByTeamHistory
FOREIGN KEY (UpdatedBy) REFERENCES TB_Capstone_EMPLOYEE(EmployeeID);

--TEST DATA POST-ALTERS

--Test Data For TB_Capstone_SCHEDULE_TYPE
INSERT INTO TB_Capstone_SCHEDULE_TYPE(Name, Description, HoursPerDay, ActivationDate, CreatedBy, CreationDate)
VALUES('Regular Hours', 'Regular schedule for employees', 7.25, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE(Name, Description, HoursPerDay, ActivationDate, CreatedBy, CreationDate)
VALUES('Part Time', 'Part-time schedule for employees', 1.00, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE(Name, Description, HoursPerDay, ActivationDate, CreatedBy, CreationDate)
VALUES('Compressed Work Week 1', 'First type of a compressed work week', 8.07, CAST('20190226' AS DATE),1,CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE(Name, Description, HoursPerDay, ActivationDate, CreatedBy, CreationDate)
VALUES('Compressed Work Week 2', 'Second type of a compressed work week', 9.07, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE(Name, Description, HoursPerDay, ActivationDate, CreatedBy, CreationDate)
VALUES('Earned Time Off', 'Schedule for employees who can earn time off', 7.58, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

--Test Data For TB_Capstone_SCHEDULE_TYPE_DETAILS
INSERT INTO TB_Capstone_SCHEDULE_TYPE_DETAILS(ScheduleTypeID, EmployeeID, ActivationDate, CreatedBy, CreationDate)
VALUES(1, 2, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE_DETAILS(ScheduleTypeID, EmployeeID, ActivationDate, CreatedBy, CreationDate)
VALUES(2, 3, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE_DETAILS(ScheduleTypeID, EmployeeID, ActivationDate, CreatedBy, CreationDate)
VALUES(3, 4, CAST('20190226' AS DATE),1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE_DETAILS(ScheduleTypeID, EmployeeID, ActivationDate, CreatedBy, CreationDate)
VALUES(4, 5, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE_DETAILS(ScheduleTypeID, EmployeeID, ActivationDate, CreatedBy, CreationDate)
VALUES(4, 1, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE_DETAILS(ScheduleTypeID, EmployeeID, ActivationDate, CreatedBy, CreationDate)
VALUES(4, 6, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE_DETAILS(ScheduleTypeID, EmployeeID, ActivationDate, CreatedBy, CreationDate)
VALUES(4, 7, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE_DETAILS(ScheduleTypeID, EmployeeID, ActivationDate, CreatedBy, CreationDate)
VALUES(4, 8, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE_DETAILS(ScheduleTypeID, EmployeeID, ActivationDate, CreatedBy, CreationDate)
VALUES(4, 9, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE_DETAILS(ScheduleTypeID, EmployeeID, ActivationDate, CreatedBy, CreationDate)
VALUES(4, 10, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE_DETAILS(ScheduleTypeID, EmployeeID, ActivationDate, CreatedBy, CreationDate)
VALUES(4, 11, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE_DETAILS(ScheduleTypeID, EmployeeID, ActivationDate, CreatedBy, CreationDate)
VALUES(1, 12, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE_DETAILS(ScheduleTypeID, EmployeeID, ActivationDate, CreatedBy, CreationDate)
VALUES(1, 13, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE_DETAILS(ScheduleTypeID, EmployeeID, ActivationDate, CreatedBy, CreationDate)
VALUES(1, 14, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_SCHEDULE_TYPE_DETAILS(ScheduleTypeID, EmployeeID, ActivationDate, CreatedBy, CreationDate)
VALUES(1, 15, CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

-- Test Data For TB_Capstone_OFFDAY_TYPE
INSERT INTO TB_Capstone_OFFDAY_TYPE(Name, AbbreviatedName, Description, PTO, Notes, Color, ActivationDate, CreatedBy, CreationDate)
VALUES ('Sick Day', 'SICK', 'A sick day for the employee', '0', 'SICK is only used when an employee is unable to come to work due to an illness.', '#ff0000', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_OFFDAY_TYPE(Name, AbbreviatedName, Description, PTO, Notes, Color, ActivationDate, CreatedBy, CreationDate)
VALUES ('Entilted time off', 'ENTO', 'Entilted time off for the employee', '1', 'ENTO is earned by the employee working for an extended period of time', '#0000ff', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_OFFDAY_TYPE(Name, AbbreviatedName, Description, PTO, Notes, Color, ActivationDate, CreatedBy, CreationDate)
VALUES ('Family emergency', 'FAMILYEMG', 'Employee has a family emergency', '0', 'Employee needs to leave due to a family emergency.','#009900', CAST('20190226' AS DATE),1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_OFFDAY_TYPE(Name, AbbreviatedName, Description, PTO, Notes, Color, ActivationDate, CreatedBy, CreationDate)
VALUES ('Paid Training', 'TRAIN1', 'A paid for training day for the employee', '1', 'TRAIN1 is used when an employee is being paid while being trained.', '#9900cc', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

-- Test Data For TB_Capstone_OVERTIME_TYPE
INSERT INTO TB_Capstone_OVERTIME_TYPE(Name, PayMultiplier, Description, ActivationDate, CreatedBy, CreationDate)
VALUES ('Regular Overtime', 1.5, 'Rate given for regular overtime.', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

INSERT INTO TB_Capstone_OVERTIME_TYPE(Name, PayMultiplier, Description, ActivationDate, CreatedBy, CreationDate)
VALUES ('Holiday Pay', 2.0, 'Rate given for holiday work', CAST('20190226' AS DATE), 1, CAST('20190226' AS DATE));

--Test Data For TB_Capstone_PROJECT_CATEGORY
INSERT INTO TB_Capstone_PROJECT_CATEGORY (CategoryName, Description, Color, ActivationDate, DeactivationDate, Global, CreatedBy, CreationDate, UpdatedBy, UpdatedDate)
VALUES ('Category1', 'This is the first category', '#ffff99',  CAST('20190226' AS DATE),  CAST('20190226' AS DATE), 1, 1,  CAST('20190226' AS DATE), 1,  CAST('20190226' AS DATE))

INSERT INTO TB_Capstone_PROJECT_CATEGORY (CategoryName, Description, Color, ActivationDate, DeactivationDate, Global, CreatedBy, CreationDate, UpdatedBy, UpdatedDate)
VALUES ('Tier 1', 'Projects over $50,000', '#ff9900',  CAST('20190226' AS DATE),  CAST('20200226' AS DATE), 1, 1,  CAST('20190226' AS DATE), 1,  CAST('29990226' AS DATE))

INSERT INTO TB_Capstone_PROJECT_CATEGORY (CategoryName, Description, Color, ActivationDate, DeactivationDate, Global, CreatedBy, CreationDate, UpdatedBy, UpdatedDate)
VALUES ('Tier 2', 'Projects under $50,000', '#cc3300', CAST('20190226' AS DATE),  CAST('20210430' AS DATE), 1, 1,  CAST('20190226' AS DATE), 1,  CAST('20190226' AS DATE))

INSERT INTO TB_Capstone_PROJECT_CATEGORY (CategoryName, Description, Color, ActivationDate, DeactivationDate, Global, CreatedBy, CreationDate, UpdatedBy, UpdatedDate)
VALUES ('Tier 3', 'Regular support projects', '#6699ff',  CAST('20190227' AS DATE),  CAST('20190913' AS DATE), 1, 1,  CAST('20190226' AS DATE), 1,  CAST('20190328' AS DATE))

-- Test Data For TB_Capstone_PAID_HOLIDAY
INSERT INTO TB_Capstone_PAID_HOLIDAY (HolidayName, HolidayDate, Notes, ActivationDate, CreatedBy, CreationDate)
VALUES ('Canada Day', CAST('20190701' AS DATE), 'National Holiday', CAST('20190318' AS DATE), 2, CAST('20190318' AS DATE));

INSERT INTO TB_Capstone_PAID_HOLIDAY (HolidayName, HolidayDate, Notes, ActivationDate, CreatedBy, CreationDate)
VALUES ('Easter Monday', CAST('20190422' AS DATE), 'National Holiday', CAST('20190318' AS DATE), 2, CAST('20190318' AS DATE));

INSERT INTO TB_Capstone_PAID_HOLIDAY (HolidayName, HolidayDate, Notes, ActivationDate, CreatedBy, CreationDate)
VALUES ('Christmas Eve', CAST('20191224' AS DATE), 'National Holiday', CAST('20190318' AS DATE), 2, CAST('20190318' AS DATE));

INSERT INTO TB_Capstone_PAID_HOLIDAY (HolidayName, HolidayDate, Notes, ActivationDate, CreatedBy, CreationDate)
VALUES ('Christmas Day', CAST('20191225' AS DATE), 'National Holiday', CAST('20190318' AS DATE), 2, CAST('20190318' AS DATE));

-- Test Data For TB_Capstone_PROJECT
INSERT INTO TB_Capstone_PROJECT (ProjectCategoryID, ProjectName, Description, StartDate, ProjectedEndDate, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 'Project 1', 'This is the first project', CAST('20190321' AS DATE), CAST('20190420' AS DATE), CAST('20190321' AS DATE), 1, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_PROJECT (ProjectCategoryID, ProjectName, Description, StartDate, ProjectedEndDate, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 'Project 12', 'This is the second project', CAST('20190321' AS DATE), CAST('20190420' AS DATE), CAST('20190321' AS DATE), 1, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_PROJECT (ProjectCategoryID, ProjectName, Description, StartDate, ProjectedEndDate, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 'Project 3', 'This is the third project', CAST('20190321' AS DATE), CAST('20190420' AS DATE), CAST('20190321' AS DATE), 1, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_PROJECT (ProjectCategoryID, ProjectName, Description, StartDate, ProjectedEndDate, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 'Project 4', 'This is the fourth project', CAST('20190321' AS DATE), CAST('20190420' AS DATE), CAST('20190321' AS DATE), 1, CAST('20190321' AS DATE));

--Test Data For TB_Capstone_PROJECT_DETAIL
INSERT INTO TB_Capstone_PROJECT_DETAIL(ProjectID, EmployeeID, StartTime, Endtime, Notes, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 3, '2019-03-21T14:00:00', '2019-03-21T15:00:00', 'Capstone redevelopment work', CAST('20190321' AS DATE), 1, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_PROJECT_DETAIL(ProjectID, EmployeeID, StartTime, Endtime, Notes, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 3, '2019-03-21T15:20:00', '2019-03-21T15:40:00', 'Capstone redevelopment work', CAST('20190321' AS DATE),1, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_PROJECT_DETAIL(ProjectID, EmployeeID, StartTime, Endtime, Notes, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 3, '2019-03-21T16:00:00', '2019-03-21T17:00:00', 'Capstone redevelopment work', CAST('20190321' AS DATE), 1, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_PROJECT_DETAIL(ProjectID, EmployeeID, StartTime, Endtime, Notes, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 3, '2019-03-21T17:20:00', '2019-03-21T17:40:00', 'Capstone redevelopment work', CAST('20190321' AS DATE), 1, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_PROJECT_DETAIL(ProjectID, EmployeeID, StartTime, Endtime, Notes, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 3, '2019-03-21T08:00:00', '2019-03-21T13:00:00', 'Capstone redevelopment work', CAST('20190321' AS DATE), 1, CAST('20190321' AS DATE));

--Test Data For TB_Capstone_ABSENCE_DETAIL
INSERT INTO TB_Capstone_ABSENCE_DETAIL(OffDayID, EmployeeID, HalfDay, AbsenceDate, Hours, Notes, ActivationDate,CreatedBy, CreationDate)
VALUES (1, 1, 'AM', CAST('20190321' AS DATE), 3.5, 'Caught the flu',  CAST('20190321' AS DATE),2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ABSENCE_DETAIL(OffDayID, EmployeeID, AbsenceDate, Notes, ActivationDate,CreatedBy, CreationDate)
VALUES (2, 2, CAST('20190321' AS DATE), 'On a trip.',  CAST('20190321' AS DATE),2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ABSENCE_DETAIL(OffDayID, EmployeeID, HalfDay, AbsenceDate, Hours, Notes, ActivationDate,CreatedBy, CreationDate)
VALUES (3, 3, 'PM', CAST('20190321' AS DATE), 3.5, 'Child hurt in accident',  CAST('20190321' AS DATE),2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ABSENCE_DETAIL(OffDayID, EmployeeID, AbsenceDate, Notes, ActivationDate,CreatedBy, CreationDate)
VALUES (4, 4, CAST('20190321' AS DATE), 'Sensitivity training',  CAST('20190321' AS DATE),2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ABSENCE_DETAIL(OffDayID, EmployeeID, HalfDay, AbsenceDate, Hours, Notes, ActivationDate,CreatedBy, CreationDate)
VALUES (1, 1, 'AM', CAST('20190401' AS DATE), 3.5, 'Caught the flu.',  CAST('20190401' AS DATE),2, CAST('20190401' AS DATE));

INSERT INTO TB_Capstone_ABSENCE_DETAIL(OffDayID, EmployeeID, HalfDay, AbsenceDate, Hours, Notes, ActivationDate,CreatedBy, CreationDate)
VALUES (1, 1, 'AM', CAST('20190411' AS DATE), 3.5, 'Caught the flu.',  CAST('20190401' AS DATE),2, CAST('20190401' AS DATE));

INSERT INTO TB_Capstone_ABSENCE_DETAIL(OffDayID, EmployeeID, HalfDay, AbsenceDate, Hours, Notes, ActivationDate,CreatedBy, CreationDate)
VALUES (1, 2, 'AM', CAST('20190401' AS DATE), 3.5, 'Caught the flu.',  CAST('20190401' AS DATE),2, CAST('20190401' AS DATE));

INSERT INTO TB_Capstone_ABSENCE_DETAIL(OffDayID, EmployeeID, HalfDay, AbsenceDate, Hours, Notes, ActivationDate,CreatedBy, CreationDate)
VALUES (2, 3, 'AM', CAST('20190401' AS DATE), 3.5, 'Family vacation',  CAST('20190401' AS DATE),2, CAST('20190401' AS DATE));

INSERT INTO TB_Capstone_ABSENCE_DETAIL(OffDayID, EmployeeID, HalfDay, AbsenceDate, Hours, Notes, ActivationDate,CreatedBy, CreationDate)
VALUES (2, 4, 'AM', CAST('20190401' AS DATE), 3.5, 'Fishing trip',  CAST('20190401' AS DATE),2, CAST('20190401' AS DATE));

INSERT INTO TB_Capstone_ABSENCE_DETAIL(OffDayID, EmployeeID, HalfDay, AbsenceDate, Hours, Notes, ActivationDate,CreatedBy, CreationDate)
VALUES (2, 5, 'AM', CAST('20190401' AS DATE), 3.5, 'Hunting trip',  CAST('20190401' AS DATE),2, CAST('20190401' AS DATE));

INSERT INTO TB_Capstone_ABSENCE_DETAIL(OffDayID, EmployeeID, HalfDay, AbsenceDate, Hours, Notes, ActivationDate,CreatedBy, CreationDate)
VALUES (3, 6, 'AM', CAST('20190401' AS DATE), 3.5, 'Family member in hospital',  CAST('20190401' AS DATE),2, CAST('20190401' AS DATE));

INSERT INTO TB_Capstone_ABSENCE_DETAIL(OffDayID, EmployeeID, HalfDay, AbsenceDate, Hours, Notes, ActivationDate,CreatedBy, CreationDate)
VALUES (3, 7, 'AM', CAST('20190401' AS DATE), 3.5, 'Child sick',  CAST('20190401' AS DATE),2, CAST('20190401' AS DATE));

INSERT INTO TB_Capstone_ABSENCE_DETAIL(OffDayID, EmployeeID, HalfDay, AbsenceDate, Hours, Notes, ActivationDate,CreatedBy, CreationDate)
VALUES (3, 8, 'AM', CAST('20190401' AS DATE), 3.5, 'Sick spouse',  CAST('20190401' AS DATE),2, CAST('20190401' AS DATE));

INSERT INTO TB_Capstone_ABSENCE_DETAIL(OffDayID, EmployeeID, HalfDay, AbsenceDate, Hours, Notes, ActivationDate,CreatedBy, CreationDate)
VALUES (3, 9, 'AM', CAST('20190401' AS DATE), 3.5, 'Family member passing',  CAST('20190401' AS DATE),2, CAST('20190401' AS DATE));

INSERT INTO TB_Capstone_ABSENCE_DETAIL(OffDayID, EmployeeID, HalfDay, AbsenceDate, Hours, Notes, ActivationDate,CreatedBy, CreationDate)
VALUES (4, 10, 'AM', CAST('20190401' AS DATE), 3.5, 'Managment training',  CAST('20190401' AS DATE),2, CAST('20190401' AS DATE));

INSERT INTO TB_Capstone_ABSENCE_DETAIL(OffDayID, EmployeeID, HalfDay, AbsenceDate, Hours, Notes, ActivationDate,CreatedBy, CreationDate)
VALUES (4, 11, 'AM', CAST('20190401' AS DATE), 3.5, 'Programming seminar',  CAST('20190401' AS DATE),2, CAST('20190401' AS DATE));

--Test Data For TB_Capstone_ENTITLED_TIME_OFF
INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 1, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 1, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 1, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 1, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 2, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 2, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 2, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 2, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 3, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 3, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 3, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 3, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 4, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 4, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 4, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 4, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 5, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 5, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 5, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 5, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 6, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 6, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 6, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 6, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 7, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 7, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 7, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 7, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 8, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 8, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 8, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 8, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 9, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 9, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 9, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 9, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 10, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 10, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 10, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 10, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 11, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 11, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 11, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 11, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 12, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 12, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 12, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 12, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 13, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 13, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 13, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 13, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 14, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 14, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 14, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 14, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 15, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 15, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 15, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ENTITLED_TIME_OFF (OffDayID, EmployeeID, HoursAccumulated, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 15, 20, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

--Test Data For TB_TEAM_HISTORY
INSERT INTO TB_Capstone_TEAM_HISTORY (TeamID, EmployeeID, RoleID, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 1, 4, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_TEAM_HISTORY (TeamID, EmployeeID, RoleID, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 2, 4, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_TEAM_HISTORY (TeamID, EmployeeID, RoleID, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 3, 4, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_TEAM_HISTORY (TeamID, EmployeeID, RoleID, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 4, 4, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_TEAM_HISTORY (TeamID, EmployeeID, RoleID, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 5, 4, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_TEAM_HISTORY (TeamID, EmployeeID, RoleID, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 6, 4, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_TEAM_HISTORY (TeamID, EmployeeID, RoleID, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 7, 4, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_TEAM_HISTORY (TeamID, EmployeeID, RoleID, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 8, 1, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_TEAM_HISTORY (TeamID, EmployeeID, RoleID, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 9, 1, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_TEAM_HISTORY (TeamID, EmployeeID, RoleID, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 10, 1, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_TEAM_HISTORY (TeamID, EmployeeID, RoleID, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 11, 1, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_TEAM_HISTORY (TeamID, EmployeeID, RoleID, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 12, 4, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_TEAM_HISTORY (TeamID, EmployeeID, RoleID, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 13, 3, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_TEAM_HISTORY (TeamID, EmployeeID, RoleID, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 14, 2, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_TEAM_HISTORY (TeamID, EmployeeID, RoleID, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 15, 1, CAST('20190321' AS DATE), 2, CAST('20190321' AS DATE));

--Test Data For TB_Capstone_OVERTIME
INSERT INTO TB_Capstone_OVERTIME (EmployeeID, ProjectID, OvertimeTypeID, SubmissionDate, Amount, StartTime, EndTime, Approved, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 1, 1, CAST('20190321' AS DATE), 210, '2019-03-21T08:00:00', '2019-03-21T13:00:00', 'P', CAST('20190321' AS DATE), 1, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_OVERTIME (EmployeeID, ProjectID, OvertimeTypeID, SubmissionDate, Amount, StartTime, EndTime, Approved, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 2, 2, CAST('20190321' AS DATE), 210, '2019-03-21T08:00:00', '2019-03-21T13:00:00', 'P', CAST('20190321' AS DATE), 1, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_OVERTIME (EmployeeID, ProjectID, OvertimeTypeID, SubmissionDate, Amount, StartTime, EndTime, Approved, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 3, 2, CAST('20190321' AS DATE), 210, '2019-03-21T08:00:00', '2019-03-21T13:00:00', 'P', CAST('20190321' AS DATE),1, CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_OVERTIME (EmployeeID, ProjectID, OvertimeTypeID, SubmissionDate, Amount, StartTime, EndTime, Approved, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 4, 1, CAST('20190321' AS DATE), 210, '2019-03-21T08:00:00', '2019-03-21T13:00:00', 'P', CAST('20190321' AS DATE), 1, CAST('20190321' AS DATE));

--Test Data For TB_Capstone_ALLOCATION
INSERT INTO TB_Capstone_ALLOCATION (ProjectID, EmployeeID, AllocatedDays, Year, January, February, March, April, May, June, July, August, September, October, November, December, ActivationDate, CreatedBy, CreationDate)
VALUES (1, 1, 119.5, 2019, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 9.5, CAST('20190321' AS DATE), 1,  CAST('20190321' AS DATE));

INSERT INTO TB_Capstone_ALLOCATION (ProjectID, EmployeeID, AllocatedDays, Year, January, February, March, April, May, June, July, August, September, October, November, December, ActivationDate, CreatedBy, CreationDate)
VALUES (2, 2, 119.5, 2019, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 9.5, CAST('20190321' AS DATE), 1,  CAST('20190321' AS DATE));  

INSERT INTO TB_Capstone_ALLOCATION (ProjectID, EmployeeID, AllocatedDays, Year, January, February, March, April, May, June, July, August, September, October, November, December, ActivationDate, CreatedBy, CreationDate)
VALUES (3, 3, 119.5, 2019, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 9.5, CAST('20190321' AS DATE), 1,  CAST('20190321' AS DATE)); 

INSERT INTO TB_Capstone_ALLOCATION (ProjectID, EmployeeID, AllocatedDays, Year, January, February, March, April, May, June, July, August, September, October, November, December, ActivationDate, CreatedBy, CreationDate)
VALUES (4, 4, 119.5, 2019, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 9.5, CAST('20190321' AS DATE), 1,  CAST('20190321' AS DATE)); 
