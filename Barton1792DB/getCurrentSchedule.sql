-- USE `sazerac`;
-- DROP procedure IF EXISTS `GetCurrentSchedule`;


DELIMITER $$
USE `sazerac`$$
-- Selects current schedule from most recent scheuling run.
-- Used in GetSchedules(). 
CREATE PROCEDURE `GetCurrentSchedule` ()
BEGIN
	SELECT * FROM schedule;
        
END$$

DELIMITER ;