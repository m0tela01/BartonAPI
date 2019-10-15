-- USE `sazerac`;
-- DROP procedure IF EXISTS `InsertPreviousScheduleToScheduleHistory`;

DELIMITER $$
USE `sazerac`$$
-- Used in GenerateSchedule()
CREATE PROCEDURE `InsertPreviousScheduleToScheduleHistory` ()
BEGIN
	INSERT INTO `schedule_history` 
		SELECT * 
		FROM `schedule`; 
END$$

DELIMITER ;

