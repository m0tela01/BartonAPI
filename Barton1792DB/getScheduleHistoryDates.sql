-- USE `sazerac`;
-- DROP procedure IF EXISTS `GetScheduleHistoryDates`;


DELIMITER $$
USE `sazerac`$$
-- Selects data for employee table in app. 
-- Used in GetScheduleHistoryDates(). 
CREATE PROCEDURE `GetScheduleHistoryDates` ()
BEGIN
	SELECT DISTINCT scheduledate 
		FROM schedule_history
			ORDER BY scheduledate ASC;
END$$

DELIMITER ;
