using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.HospitalData;

namespace WebApp.Controllers.HospitalController
{
    [Route("api/[controller]")]
    [ApiController]
    public class NurseController : ControllerBase
    {

        private static List<Nurse> nurseData = new List<Nurse> { };

        [HttpGet]

        public ActionResult<IEnumerable<Nurse>> Get()
        {
            try
            {
                if (!nurseData.Any())
                {
                    return Ok(new
                    {
                        Message = "No Nurse Data Available",
                        TimeStamp = DateTime.Now
                    });
                }

                var response = new
                {
                    Result = nurseData,
                    Message = "Nurse Data Fetched Successfully",
                    TimeStamp = DateTime.Now
                };
                return Ok(response);

            }
            catch (FormatException)
            {
                return StatusCode(500, new
                {
                    Message = "An error occured while fetching the Nurse Data",
                    TimeStamp = DateTime.Now
                });
            }
        }


        [HttpPost]

        public ActionResult Post([FromBody] Nurse nurse)
        {
            try
            {
                nurse.Id = nurse.Id == Guid.Empty ? Guid.NewGuid() : nurse.Id;

                nurseData.Add(nurse);

                return CreatedAtAction(nameof(Get), new {id = nurse.Id}, new
                {
                    Resuult = nurseData.Count > 0,
                    Message = "Nurse Data Added Successfully",
                    TimeStamp = DateTime.UtcNow
                });

            }
            catch (FormatException)
            {
                return BadRequest(new
                {
                    Message = "Invalid Post Request"
                });
            }
        }


        [HttpGet("{id}")]

        public ActionResult<Nurse> Get(Guid id)
        {
            try
            {
                var Nurse = nurseData.FirstOrDefault(n=> n.Id == id);
                if(Nurse == null)
                {
                    return NotFound(new
                    {
                        Message = "Nurse Not Found with Specific Id",
                        TimeStamp = DateTime.Now
                    });
                }

                return Ok(new
                {
                    Result = Nurse,
                    Message = "Nurse Data Fetched Successfully",
                    TimeStamp = DateTime.UtcNow
                });

            }
            catch (FormatException)
            {
                return BadRequest(new
                {
                    Message = "Nurse Not found with specific Id"
                });
            }
        }

        [HttpPut("{id}")]

        public ActionResult Put(Guid id, [FromBody] Nurse UpdatedNurseData)
        {
            try
            {
                var nurse = nurseData.FirstOrDefault(n=>  n.Id == id);
                if(nurse == null)
                {
                    return NotFound(new
                    {
                        Message = "No Nurse Found with specific Id",
                        TimeStamp = DateTime.UtcNow
                    });
                }

                nurse.Name = UpdatedNurseData.Name; 
                nurse.Email = UpdatedNurseData.Email;
                nurse.ContactNumber = UpdatedNurseData.ContactNumber;

                return Ok(new
                {
                    Message = "Nurse Data Updated Successfully",
                    Result ="Success",
                    UpdatedNurse = new
                    {
                        nurse.Id,
                        nurse.Name,
                        nurse.Email,
                        nurse.ContactNumber
                    }

                });



            }
            catch (FormatException)
            {
                return StatusCode(500, new
                {
                    Mesage = "No Nurse Data Found by specific Id",
                    TimeStamp = DateTime.UtcNow
                });
            }
        }


        [HttpDelete("{id}")]

        public ActionResult Delete(Guid id)
        {
            try
            {
                var nurse = nurseData.FirstOrDefault();
                if(nurse == null)
                {
                    return NotFound(new
                    {
                        Message = "Nurse Not found with the Given Id"
                    });
                }
                nurseData.Remove(nurse);
                return Ok(new
                {
                    Message = "Nurse Data Removed Successfully"
                });

            }
            catch(FormatException)
            {
                return BadRequest(new
                {
                    Message = "Invalid Id Format, Please enter Correct Id Format"
                });
            }
        }
    }
}
