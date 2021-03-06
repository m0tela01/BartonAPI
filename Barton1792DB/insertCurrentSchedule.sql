-- USE `sazerac`;
-- DROP procedure IF EXISTS `InsertCurrentSchedule`;


DELIMITER $$
USE `sazerac`$$
-- Updates schedule table with current schedule from most recent scheuling run.
-- Used in InsertCurrentSchedule(). 
CREATE PROCEDURE `InsertCurrentSchedule` ()
BEGIN
	TRUNCATE `InsertCurrentSchedule`;
	INSERT INTO `schedule` 
    (
		senioritynumber,
        clocknumber,
        empname,
        jobname,
        departmentname,
        s1,
        s2,
        s3,
        shiftpref,
        datestart
    )
    VALUES 
    (
		`SeniorityNumbe`,
        `ClockNumber`,
        `EmployeeName`,
        `DepartmentName`,
        `JobName`,
        `Shift1`,
        `Shift2`,
        `Shift3`,
        `ShiftPreference`,
        `datestart`
    );
        
END$$

DELIMITER ;