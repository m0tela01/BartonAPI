-- USE `sazerac`;
-- DROP PROCEDURE IF EXISTS InsertCurrentScheduleMoveOldScheduleToHistory;
DELIMITER $$
USE `sazerac` $$
-- Used in GenerateSchedule() for weekdays
CREATE PROCEDURE `InsertCurrentScheduleMoveOldScheduleToHistory`(IN senioritynumber INT,
			clocknumber INT,
			empname VARCHAR(50),
			jobname VARCHAR(50),
			departmentname VARCHAR(50),
            shift INT,
			shiftpref INT SIGNED,
            scheduledate DATETIME)
BEGIN
	INSERT INTO `schedule_history` 
		SELECT * 
		FROM `schedule`; 
	--
	TRUNCATE `schedule`;
	--
    INSERT INTO `schedule` 
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