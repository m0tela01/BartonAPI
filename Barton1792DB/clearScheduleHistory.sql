-- USE `sazerac`;
-- DROP procedure IF EXISTS `ClearScheduleHistory`;

DELIMITER $$
USE `sazerac`$$
-- Used to clear the History table in ClearHistory()
CREATE PROCEDURE `ClearScheduleHistory` ()
BEGIN
	TRUNCATE schedule_history;
END$$

DELIMITER ;