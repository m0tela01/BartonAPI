-- USE `sazerac`;
-- DROP procedure IF EXISTS `GetEmployeeById`;

DELIMITER $$
USE `sazerac`$$
-- Used in GetEmployeeById()
CREATE PROCEDURE `GetEmployeeById` (IN clockNumber INT)
BEGIN
	SELECT
		emp.clocknumber,
		emp.senioritynumber,
		emp.shiftpref,
		emp.empname,
		emp.senioritydate,
		emp.prebuilthours,
		emp.weekendothours,
		emp.totalhours,
		emp.totalhours,
		job.jobname,
		dept.departmentname
			FROM `sazerac`.`employee` AS emp 
			INNER JOIN `sazerac`.`job` AS job 
			ON emp.jobid = job.jobid
			INNER JOIN `sazerac`.`department` AS dept
			ON job.deptid = dept.deptid
			WHERE emp.clocknumber = clockNumber;
END$$

DELIMITER ;

