-- creates tables for sazerac - barton 1792 database
-- clocknumber is like empid and senioritynumber is just a 1-300
DROP TABLE IF EXISTS sazerac.employee;
CREATE TABLE `sazerac`.`employee` (
	clocknumber INT SIGNED,
    senioritynumber INT SIGNED,
    shiftpref INT SIGNED, 
    empname varchar(50), 
    senioritydate DATE,
    prebuilthours INT,
    weekendothours INT,
    totalhours INT,
    jobid INT,
		  PRIMARY KEY (clocknumber), 
        FOREIGN KEY (jobid) REFERENCES jobid
);


DROP TABLE IF EXISTS sazerac.job;
CREATE TABLE `sazerac`.`job` (
	jobid INT SIGNED,
    jobname varchar(50), 
    deptid INT SIGNED,
		  PRIMARY KEY (jobid), 
          FOREIGN KEY (deptid) REFERENCES deptid
);


DROP TABLE IF EXISTS sazerac.department;
CREATE TABLE `sazerac`.`department` (
  departmentname varchar(50), 
    deptid INT SIGNED,
		  PRIMARY KEY (departmentname), 
        FOREIGN KEY (deptid) REFERENCES deptid
);


DROP TABLE IF EXISTS sazerac.shift;
CREATE TABLE `sazerac`.`shift` (
	jobid INT SIGNED,
    deptid INT SIGNED,
    s1 INT SIGNED,
    s2 INT SIGNED,
    s3 INT SIGNED,
		  PRIMARY KEY (jobid)
);

DROP TABLE IF EXISTS sazerac.template;
CREATE TABLE `sazerac`.`template`(
  jobname varchar(50),
    departmentname varchar(50),
    s1 INT SIGNED,
    s2 INT SIGNED,
    s3 INT SIGNED


DROP TABLE IF EXISTS sazerac.schedule;
CREATE TABLE `sazerac`.`schedule`(
  senioritynumber INT SIGNED,
    clocknumber INT SIGNED,
    empname varchar(50),
    jobname varchar(50), 
    departmentname varchar(50),
    s1 INT SIGNED,
    s2 INT SIGNED,
    s3 INT SIGNED,
    shiftpref INT SIGNED, 
      PRIMARY KEY (senioritynumber),
        FOREIGN KEY (clocknumber) REFERENCES clocknumber
);

