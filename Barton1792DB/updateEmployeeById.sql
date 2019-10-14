-- USE `sazerac`;
-- DROP PROCEDURE IF EXISTS UpdateEmployeeById;
DELIMITER $$
USE `sazerac` $$
CREATE PROCEDURE `UpdateEmployeeById`(IN
                    clockNumber INT, 
					seniorityNumber INT, 
					shiftPref INT,
					empName VARCHAR(50),
					senorityDate DATE,
					prebuiltHours INT,
					weekendOtHours INT,
					totalHours INT,
					jobId INT
                    )
BEGIN
	UPDATE `employee` 
    SET
	senioritynumber = seniorityNumber,
	shiftpref = shiftPref,
	empname = empName,
	senioritydate = senorityDate,
	prebuilthours = prebuiltHours,
	weekendothours = weekendOtHours,
	totalhours = totalHours,
	jobid = jobId
    where clocknumber  = clockNumber;
END$$
DELIMITER ;