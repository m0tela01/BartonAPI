-- USE `sazerac`;
-- DROP procedure IF EXISTS `ClearScheduleBeforeInsertCurrentSchedule`;

DELIMITER $$
USE `sazerac`$$
-- Used in GenerateCurrentSchedule()
CREATE PROCEDURE `ClearScheduleBeforeInsertCurrentSchedule` ()
BEGIN
	TRUNCATE `schedule`;
END$$

DELIMITER ;

