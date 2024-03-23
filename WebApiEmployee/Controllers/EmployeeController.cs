using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiEmployee.Models;

namespace WebApiEmployee.Controllers
{
    public class EmployeeController : ApiController
    {
        DbLogic dblogic = new DbLogic();
        [HttpGet]
        [Route("api/Employee")]
        public IHttpActionResult Get()
        {
            return Ok(dblogic.GetEmp());
        }
        [HttpGet]
        [Route("api/Employee/{id}")]
        public IHttpActionResult Get(int id)
        {
         
            var Employee = dblogic.GetEmp().Where(m => m.EmpID == id).FirstOrDefault();
            if (Employee == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(Employee);
            }
        }
        [HttpPost]
        [Route("api/Employee")]
        public IHttpActionResult Insert([FromBody] EmpModel emp)
        {
            try
            {
                string result = dblogic.AddEmp(emp);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPut]
        [Route("api/Employee/{id}")]
        public IHttpActionResult update(int id, EmpModel emp)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updated = dblogic.UpdateEmp(id, emp);

                if (updated)
                {
                    return Ok("Employee updated successfully");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpDelete]
        [Route("api/Employee/{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (ModelState.IsValid == true)
            {
                dblogic.DeleteEmp(id);
                return Ok("Record deleted successfully");
            }
            else
            {
                return NotFound();
            }
        }

    }
}
