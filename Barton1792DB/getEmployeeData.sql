-- USE `sazerac`;
-- DROP procedure IF EXISTS `GetEmployeeData`;


DELIMITER $$
USE `sazerac`$$
-- Selects data for employee table in app. 
-- Used in GetEmployees(). 
CREATE PROCEDURE `GetEmployeeData` ()
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
			ON job.deptid = dept.deptid;
        
END$$

DELIMITER ;
