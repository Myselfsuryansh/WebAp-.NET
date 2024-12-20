using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.HospitalData;

namespace WebApp.Controllers.HospitalController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private static List<Administrator> AdministratorData = new List<Administrator> { };


        [HttpGet]

        public ActionResult<IEnumerable<Administrator>> Get()
        {
            try
            {
                if (!AdministratorData.Any())
                {
                    return Ok(new
                    {
                        Message = "No Administrator Data Available",
                        TimeStamp = DateTime.UtcNow
                    });
                }

                var response = new
                {
                    Result = AdministratorData,
                    Message = "Adminstator Data Fetched Successfully",
                    TimeStamp = DateTime.UtcNow
                };
                return Ok(response);

            }
            catch(FormatException e)
            {
                return StatusCode(500, new
                {
                    Message = "An error occured while fetching the Administrator",
                    ErrorDetails = e,
                    TimeStamp = DateTime.UtcNow

                });
            }
        }
    }
}
