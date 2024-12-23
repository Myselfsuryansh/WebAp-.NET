using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApp.Models.HospitalData;

namespace WebApp.Controllers.HospitalController
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Administrator")]
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
            catch (FormatException e)
            {
                return StatusCode(500, new
                {
                    Message = "An error occured while fetching the Administrator",
                    ErrorDetails = e,
                    TimeStamp = DateTime.UtcNow

                });
            }
        }

        [HttpPost]

        public ActionResult Post([FromBody] Administrator administrator)
        {
            try
            {
                administrator.Id = administrator.Id == Guid.Empty ? Guid.NewGuid() : administrator.Id;
                AdministratorData.Add(administrator);

                return CreatedAtAction(nameof(Get), new { id = administrator.Id }, new
                {
                    Results = AdministratorData,
                    Message = "Administrator Added Successfully",
                    TimeStamp = DateTime.UtcNow
                });


            }
            catch (FormatException)
            {
                return BadRequest(new
                {
                    Message = "Invalid Post Request",
                });
            }

        }

        [HttpGet("{id}")]

        public ActionResult<Administrator> Get(Guid id)
        {
            try
            {
                var Administrator = AdministratorData.FirstOrDefault(a => a.Id == id);
                if (Administrator == null)
                {
                    return NotFound(new
                    {
                        Message = "Administrator Not Found with specific Id",
                        TimeStamp = DateTime.UtcNow
                    });
                }
                return Ok(new
                {
                    Result = Administrator,
                    Message = "Administator Data Fetched Successfully",
                    TimeStamp = DateTime.UtcNow
                });
            }
            catch (FormatException)
            {
                return BadRequest(new
                {
                    Message = "Administrator Not Found with the Specific Id",
                    TimeStamp = DateTime.UtcNow
                });
            }
        }


        [HttpPut("{id}")]

        public ActionResult Put(Guid id, [FromBody] Administrator UpdatedSAdministrator)
        {
            try
            {
                var administrator = AdministratorData.FirstOrDefault(a=> a.Id == id);
                if(administrator == null)
                {
                    return NotFound(new
                    {
                        Message = "Administaror Not Found with Specific Id",
                        TimeStamp = DateTime.UtcNow
                    });
                }
                administrator.Name = UpdatedSAdministrator.Name;
                administrator.Email = UpdatedSAdministrator.Email;
                administrator.ContactNumber = UpdatedSAdministrator.ContactNumber;

                return Ok(new
                {
                    Message = "Admministrator Data Added Successfully",
                    TimeStamp = DateTime.UtcNow
                }); 
            }
            catch(FormatException)
            {
                return BadRequest(new
                {
                    Message = "Adminstartor Not Found with specific Id",
                    TimeStamp = DateTime.UtcNow
                });
            }
        }


        [HttpDelete("{id}")]

        public ActionResult Delete(Guid id)
        {
            try
            {
                var administrator = AdministratorData.FirstOrDefault();

                if(administrator == null)
                {
                    return NotFound(new
                    {
                        Message = "Administrator Not found  with the Given id"
                    });
                }
                AdministratorData.Remove(administrator);
                return Ok(new
                {
                    Message = "Administartor Data Deleted Successully"
                });

            }
            catch (FormatException)
            {
                return BadRequest(new
                { 
                    Message = "Invalid Id Format, Please Enter correct ID Format"
                });
            }
        }
    }
}
