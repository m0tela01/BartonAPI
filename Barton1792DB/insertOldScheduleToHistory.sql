-- USE `sazerac`;
-- DROP procedure IF EXISTS `InsertOldScheduleToHistory`;

InsertOldScheduleToHistory
DELIMITER $$
USE `sazerac`$$
-- Updates schedule table with current schedule from most recent scheuling run.
-- Used in InsertOldScheduleToHistory(). 
CREATE PROCEDURE `InsertOldScheduleToHistory` (IN senioritynumber INT,
			clocknumber INT,
			empname VARCHAR(50),
			jobname VARCHAR(50),
			departmentname VARCHAR(50),
            shift INT,
			shiftpref INT SIGNED,
            scheduledate DATETIME)
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
		`senioritynumber`,
		`clocknumber`,
		`empname`,
		`jobname`,
		`departmentname`,
		`shift`,
		`shiftpref`,
		`scheduledate`
    );
        
END$$

DELIMITER ;