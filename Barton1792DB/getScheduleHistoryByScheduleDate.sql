-- USE `sazerac`;
-- DROP procedure IF EXISTS `GetScheduleHistoryByScheduleDate`;


DELIMITER $$
USE `sazerac`$$
-- Selects data for employee table in app. 
-- Used in GetScheduleHistoryByScheduleDate(). 
CREATE PROCEDURE `GetScheduleHistoryByScheduleDate` (IN scheduleDate VARCHAR(50) )
BEGIN
	SELECT * FROM schedule_history 
		WHERE scheduledate = scheduleDate;
END$$

DELIMITER ;
