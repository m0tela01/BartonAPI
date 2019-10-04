-- USE `sazerac`;
-- DROP procedure IF EXISTS `InsertCurrentScheduleTemplate`;

InsertCurrentScheduleTemplate
DELIMITER $$
USE `sazerac`$$
-- Updates schedule table with current schedule from most recent scheuling run.
-- Used in InsertCurrentScheduleTemplate(). 
CREATE PROCEDURE `InsertCurrentScheduleTemplate` ()
BEGIN
	UPDATE `template` SET
		s1 = `Shift1`,
        s2 = `Shift2`,
        s3 = `Shift3` WHERE 
			jobname = `JobName` AND
			departmentname = `DepartmentName`;
END$$

DELIMITER ;