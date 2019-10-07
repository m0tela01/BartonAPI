-- USE `sazerac`;
-- DROP procedure IF EXISTS `InsertOldScheduleToHistory`;

InsertOldScheduleToHistory
DELIMITER $$
USE `sazerac`$$
-- Updates schedule table with current schedule from most recent scheuling run.
-- Used in InsertOldScheduleToHistory(). 
CREATE PROCEDURE `InsertOldScheduleToHistory` ()
BEGIN
	INSERT INTO `schedule_history` 
    (
		senioritynumber,
        clocknumber,
        empname,
        jobname,
        departmentname,
        shift,
        shiftpref,
        scheduledate
    )
    VALUES 
    (
		`SeniorityNumbe`,
        `ClockNumber`,
        `EmployeeName`,
        `DepartmentName`,
        `JobName`,
        `Shift`,
        `ShiftPreference`,
        `scheduledate`
    );
        
END$$

DELIMITER ;