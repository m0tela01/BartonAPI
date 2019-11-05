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

-- DROP PROCEDURE IF EXISTS UpdateCurrentScheduleTemplate;

-- INSERT INTO schedule_history 
-- SELECT * 
-- FROM schedule; 

select * from employee;

select * from job;

select * from employee inner join job on employee.jobid = job.jobid;

select * from template;

select * from department;

select * from schedule;

select * from schedule_history;

select distinct scheduledate from schedule_history;

select * from schedule_history where scheduledate = "2019-10-14";

call GetScheduleHistoryByScheduleDate('2019-10-14');

select * from employee where jobid in (10,3,5,22,25);

insert into `schedule_history` (
			senioritynumber,
			clocknumber,
			empname,
			jobname,
			departmentname,
            shift,
			shiftpref,
            scheduledate
		)
		VALUES 
		(
			'3',
			'1237',
			'Lindsay, Lohan3',
            'PACKER OPER',
			'BOTT',
			'1',
			'1',
            '2019-10-14 00:00:00'
		);

delete from `schedule_history` where scheduledate = "2019-10-14" and clocknumber = 1237;

-- truncate employee;

-- truncate department;

-- truncate job;

-- truncate schedule;

-- truncate schedule_history;