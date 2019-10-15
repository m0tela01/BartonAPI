-- USE `sazerac`;
-- DROP procedure IF EXISTS `ClearScheduleTemplateBeforeInsertCurrentTemplate`;

DELIMITER $$
USE `sazerac`$$
-- Used in UpdateSchedule()
CREATE PROCEDURE `ClearScheduleTemplateBeforeInsertCurrentTemplate` ()
BEGIN
	TRUNCATE `template`;
END$$

DELIMITER ;

