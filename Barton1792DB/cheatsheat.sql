USE sazerac;

update employee set shiftpref = 1 where clocknumber = 1232;

select
s1,
s2,
s3
FROM template
WHERE jobid = (SELECT jobid
		FROM `job` 
        WHERE job.jobname = "FORKLIFT OPER" );

call GetEmployeeById(1237);

INSERT INTO schedule_history 
SELECT * 
FROM schedule; 

select * from employee;

select * from job;

select * from employee inner join job on employee.jobid = job.jobid;

select * from template;

select * from department;

select * from schedule;

select * from schedule_history;



select * from employee where jobid in (10,3,5,22,25);


-- truncate employee;

-- truncate department;

-- truncate job;

-- truncate schedule;

-- truncate schedule_history;