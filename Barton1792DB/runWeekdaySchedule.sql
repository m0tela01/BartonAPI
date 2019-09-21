CREATE PROCEDURE `new_procedure` ()
BEGIN

Use Sazerac;
 
Declare int @shift = 1, --start with first shift
        int @JobId = 1, --start with first job
        int @workersNeeded,
        int @maxJobId;
 
--put number of workers needed per jobId into a #temp table
create table #tempJobIdValues
 
Select *
into #AvailableWorkers
From employee
where available = 1
order by shiftpref, empid desc
 
while @shift <= 3
Begin 
    --get maxJobId value
    select @maxJobId = Count(jobId)
    from job 
    join on job.jobid = employee.jobid
    where shift = @shift --will # of jobs depend on shift?!?!
 
    while @jobId <= @maxJobId --can change to @jobid <= @maxjobid (selected by shift (can have more jobs per shift)) --can count() jobs
    begin 
        select @workersNeeded = numOfWorkersNeeded
        from #tempJobIdValues
        where shift = @shift and JobId = @jobId 
 
        --fill in schedule by jobid
        select top @workersNeeded *
        into dbo.ThisWeeksSchedule --might want a sp specifically for inserts for consistency
        from #AvailableWorkers
        where JobId = @JobId and shift = @shift
 
        @jobId++
    end
 
    --remove workers from temp table because they've been accounted for already
    with WorkersThatBeenUsed as {
        select @jobId
        from dbo.ThisWeeksSchedule
        where shift = @shift
    }
    delete aw
    from #AvailableWorkers aw
        join WorkersThatBeenUsed wtbu
            on aw.EmployeeId = wtbu.EmployeeId
 
    --placing the rest of the available workers into thisWeeksSchedule as a Laborer
    select * -- change jobId to Laborer
    into dbo.ThisWeeksSchedule
    from #AvailableWorkers
    where shift = @shift
 
    @shift++
end


END