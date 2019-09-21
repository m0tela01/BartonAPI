-- creates tables for sazerac - barton 1792 database
-- 
DROP TABLE IF EXISTS sazerac.employee;
CREATE TABLE `sazerac`.`employee` (
	empid INT SIGNED,
    shiftpref INT SIGNED, 
    empname varchar(50), 
    job varchar(50),
    jobid INT SIGNED,
		PRIMARY KEY (empid), 
        FOREIGN KEY (jobid) REFERENCES jobid
);


DROP TABLE IF EXISTS sazerac.job;
CREATE TABLE `sazerac`.`job` (
	jobid INT SIGNED,
    job varchar(50), 
    deptid INT SIGNED,
		PRIMARY KEY (jobid), 
        FOREIGN KEY (deptid) REFERENCES deptid
);


DROP TABLE IF EXISTS sazerac.department;
CREATE TABLE `sazerac`.`department` (
    dept varchar(50), 
    deptid INT SIGNED,
		PRIMARY KEY (dept), 
        FOREIGN KEY (deptid) REFERENCES deptid
);


DROP TABLE IF EXISTS sazerac.department;
CREATE TABLE `sazerac`.`shift` (
	jobid varchar(50),
    deptid INT SIGNED,
    s1 INT SIGNED,
    s2 INT SIGNED,
    s3 INT SIGNED,
		PRIMARY KEY (jobid)
);



