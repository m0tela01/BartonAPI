-- USE `sazerac`;
-- DROP PROCEDURE IF EXISTS UpdateEmployeeById;

DELIMITER $$
USE `sazerac` $$
-- Used in UpdateTemplateByJobId()
CREATE PROCEDURE `UpdateTemplateByJobId` (
					jobName VARCHAR(50),
					deptName VARCHAR(50),
					Shift1 INT,
					Shift2 INT,
					Shift3 INT
					)
BEGIN
UPDATE `template`
		SET 
		s1 = Shift1,
		s2 = Shift2,
		s3 = Shift3
		WHERE jobid = (SELECT jobid
						FROM `job` 
						WHERE job.jobname = jobName);
END$$
DELIMITER ;