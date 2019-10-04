-- USE `sazerac`;
-- DROP procedure IF EXISTS `GetTemplate`;


DELIMITER $$
USE `sazerac`$$
-- Selects current schedule template as populated with shift and job data from most recent scheuling run.
-- Used in Get(). 
CREATE PROCEDURE `GetTemplate` ()
BEGIN
	SELECT
		dept.departmentname,
        job.jobname,
        sft.s1,
        sft.s2,
        sft.s3		
			FROM shift AS sft
			INNER JOIN `sazerac`.`job` AS job
			ON sft.jobid = job.jobid
			INNER JOIN `sazerac`.`department` AS dept
			ON job.deptid = dept.deptid;
        
END$$

DELIMITER ;