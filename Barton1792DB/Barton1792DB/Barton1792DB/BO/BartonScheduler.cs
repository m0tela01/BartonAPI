using Barton1792DB.DBO;
using System;
using System.Collections.Generic;
using System.Text;
using Barton1792DB.DAO;

namespace Barton1792DB.BO
{
    public static class BartonScheduler
    {
        private static int NOTELIGIIBLETOSCHEDULE => -1;
        private static string LABOR => "Labor";

        public static Readers readers = new Readers();
        public static Writers writers = new Writers();

        public static List<Schedule> GenerateWeekdaySchedule()
        {
            //Get data to generate schedule
            List<Employee> employees = readers.GetEmployees(new List<Employee>());
            List<Template> templates = readers.GetTemplate(new List<Template>());
            //Insert the data from the previous scheduling run to the history table
            writers.InsertPreviousScheduleToScheduleHistory();
            //Clear the previous schedule for the contents of the schedule table
            writers.ClearScheduleBeforeInsert();

            DateTime today = DateTime.Today;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysUntilTuesday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            DateTime nextMonday = today.AddDays(daysUntilTuesday);

            List<Schedule> schedules = new List<Schedule>();    // For insert
            Schedule schedule;
            // this data is ordered by senority number, job, and shiftpreference
            // put business logic here for scheduling
            //jobid - 17 jobname - LABOR   deptid - 6
            //

            for (int i = 0; i < employees.Count; i++)
            {
                for (int j = 0; j < templates.Count; j++)
                {
                    schedule = new Schedule();
                    //Need Some kind of field for absent talk about this
                    //if (employees[i].AbsentFlag == 1)
                    if (employees[i].SeniorityNumber == -99)
                    {
                        //Employee has some kind of absent status - NOTELIGIIBLETOSCHEDULE
                        schedule.ScheduleDate = nextMonday;
                        schedule.SeniorityNumber = employees[i].SeniorityNumber;
                        schedule.ClockNumber = employees[i].ClockNumber;
                        schedule.EmployeeName = employees[i].EmployeeName;
                        schedule.DepartmentName = employees[i].DepartmentName;
                        schedule.ShiftPreference = employees[i].ShiftPreference;
                        schedule.Shift = NOTELIGIIBLETOSCHEDULE;
                        schedule.JobName = employees[i].JobName;
                    }
                    else
                    {

                        if (employees[i].JobName == templates[j].JobName)
                        {
                            bool preferedShift1 = false;
                            //bool preferedShift2 = false;
                            bool preferedShift3 = false;
                            //Schedule schedule = new Schedule();
                            schedule.ScheduleDate = nextMonday;
                            schedule.SeniorityNumber = employees[i].SeniorityNumber;
                            schedule.ClockNumber = employees[i].ClockNumber;
                            schedule.EmployeeName = employees[i].EmployeeName;
                            schedule.DepartmentName = employees[i].DepartmentName;
                            schedule.ShiftPreference = employees[i].ShiftPreference;
                        // Give them what they want trial 1
                        tryagain:
                            if (employees[i].ShiftPreference == 1)
                            {

                                if (templates[j].Shift1 > 0)
                                {
                                    templates[j].Shift1 -= 1;
                                    schedule.Shift = 1;
                                    schedule.JobName = templates[j].JobName;
                                    schedules.Add(schedule);
                                }
                                else
                                {
                                    preferedShift1 = true;
                                    employees[i].ShiftPreference = 2;
                                    goto tryagain;
                                }
                            }
                            else if (employees[i].ShiftPreference == 2)
                            {
                                if (templates[j].Shift2 > 0)
                                {
                                    templates[j].Shift2 -= 1;
                                    schedule.Shift = 2;
                                    schedule.JobName = templates[j].JobName;
                                    schedules.Add(schedule);
                                }
                                else
                                {
                                    //gotPreference = false;
                                    //if the prefered shift was 3 or 2 previously go to labor
                                    if (preferedShift3 == true)
                                    {
                                        employees[i].ShiftPreference = 4;
                                    }
                                    else
                                    {
                                        //preferedShift2 = true;
                                        employees[i].ShiftPreference = 3;
                                        goto tryagain;
                                    }
                                }
                            }
                            else if (employees[i].ShiftPreference == 3)
                            {
                                if (templates[j].Shift3 > 0)
                                {
                                    templates[j].Shift3 -= 1;
                                    schedule.Shift = 3;
                                    schedule.JobName = templates[j].JobName;
                                    schedules.Add(schedule);
                                }
                                else
                                {
                                    //if the prefered shift was 1 go to labor now
                                    ///if it was 2 try shift1 (and shift2 again) then go to labor
                                    if (preferedShift1 == true)// && preferedShift2 == true)
                                    {
                                        employees[i].ShiftPreference = 4;
                                    }
                                    //if shift 3 was prefered go try shift 1 & 2 before going to labor
                                    else
                                    {
                                        employees[i].ShiftPreference = 1;
                                        preferedShift3 = true;
                                    }
                                    goto tryagain;
                                }
                            }
                            //if the employee doesnt fit in any shift for their job put them in labor with their preference
                            else if (employees[i].ShiftPreference == 4)
                            {
                                //jobid - 17, jobname - LABOR,   deptid - 6
                                Console.WriteLine("No shifts available for: " + employees[i].JobName + " going to labor.");
                                schedule.JobName = LABOR;
                                schedule.Shift = employees[i].ShiftPreference;
                                schedules.Add(schedule);
                            }
                            else
                            {
                                Console.WriteLine("No Shift Preference");
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(schedule.JobName))
                    {
                        break;
                    }
                }
            }

            //write current schedule to history
            writers.InsertCurrentSchedule(schedules);
            return schedules;
        }
    }
}
