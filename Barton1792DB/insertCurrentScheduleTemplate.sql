
-- USE `sazerac`;
-- DROP procedure IF EXISTS `sazerac`.`InsertCurrentScheduleTemplate`;

DELIMITER $$
USE `sazerac`$$
-- Used in UpdateTemplate()
CREATE DEFINER=`sazerac_user`@`%` PROCEDURE `InsertCurrentScheduleTemplate`(IN jobName VARCHAR(50),
				departmentName VARCHAR(50),
                shift1 INT,
                shift2 INT,
                shift3 INT
				)
BEGIN
		INSERT INTO `template` SET
			jobname = jobName,
            departmentname = departmentName,
			s1 = shift1,
			s2 = shift2,
			s3 = shift3;                
END$$

DELIMITER ;
;
