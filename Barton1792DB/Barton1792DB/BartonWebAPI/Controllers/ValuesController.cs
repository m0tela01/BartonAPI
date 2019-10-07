using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barton1792DB.DBO;
using Barton1792DB.DAO;
using Barton1792DB.BO;
using DVAC;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace BartonWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        Readers readers = new Readers();
        // GET api/values
        //[HttpGet]
        //public ActionResult<Context> Get()
        //{
        //    Barton1792DB.DAO.Readers readers = new Barton1792DB.DAO.Readers();
        //    Context c = readers.GetEmployees();
        //    return c;
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            //Context c = Context.from_csv("Assets/EmployeeTableToDB.csv");
            //Context c = Context.from_sql_query("", q);

            //return "selected employee:" + string.Join(" ", c[c["senioritynumber"] == id][0]);
            return "";
        }

        //[HttpGet("{id}")]
        //public ActionResult<List<Employee>> GetEmployee(string id)
        //{
        //    List<Employee> employeeTable = readers.GetEmployees(new List<Employee>());
        //    return employeeTable;
        //}

        [HttpGet]
        public ActionResult<List<Schedule>> GetSchedule()
        {
            List<Schedule> CurrentSchedule = new List<Schedule>();
            List<Employee> CurrentEmployeeData = readers.GetEmployees(new List<Employee>());
            List<Template> CurrentSchedulingTemplate = readers.GetTemplate(new List<Template>());

            BartonSchedulerWeekday.GenerateSchedule(CurrentEmployeeData, CurrentSchedulingTemplate);
            CurrentSchedule = readers.GetSchedules(CurrentSchedule);
            return CurrentSchedule;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPost]
        public void Post([FromBody]IEnumerable<Employee> employees)
        {

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
