using Microsoft.AspNetCore.Mvc;
using WebApp.Models.HospitalData;

namespace WebApp.Controllers.HospitalController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private static List<Doctor> doctorData = new List<Doctor>
        { };

        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> Get()
        {
            try
            {
                if (!doctorData.Any())
                {
                    return Ok(new
                    {
                        Message = "No Doctors Data Available",
                        Timestamp = DateTime.UtcNow
                    });
                }

                var response = new
                {
                    Result = doctorData,
                    Message = "Doctors Data Fetched Successfully",
                    Timestamp = DateTime.UtcNow
                };
                return Ok(response);

            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "An error occured  while fetching  the Doctor",
                    ErrorDetails = e,
                    TimeStamp = DateTime.UtcNow

                });

            }
        }

        [HttpPost]

        public ActionResult Post([FromBody] Doctor doctor)
        {
            try
            {
                doctor.Id = doctor.Id == Guid.Empty ? Guid.NewGuid() : doctor.Id;

                doctorData.Add(doctor);

                return CreatedAtAction(nameof(Get), new { id = doctor.Id }, new
                {
                    Result = doctor,
                    Message = "Doctor Data Added Successfully",
                    TimeStamp = DateTime.UtcNow
                });

            }
            catch (FormatException)
            {
                return BadRequest(
                    new
                    {
                        Message = "Invalid Post Request"
                    });
            }

        }

        [HttpGet("{id}")]

        public ActionResult<Doctor> Get(Guid id)
        {
            try
            {
                var doctor  = doctorData.FirstOrDefault(d => d.Id == id);
                if(doctor == null)
                {
                    return NotFound(new
                    {
                        Message = "Doctor Not found with specific Id",
                        TimeStamp = DateTime.UtcNow
                    });
                }

                return Ok(new
                {
                    Result = doctor,
                    Message = "Doctor Data Fetched Suyccessfully",
                    TimeStamp = DateTime.UtcNow

                });

            }
            catch (FormatException)
            {
                return BadRequest(new
                {
                    Message = "Doctor not found with specific Id"
                });
            }
        }

        [HttpPut("{id}")]

        public ActionResult Put(Guid id, [FromBody] Doctor updateDoctor) {
            try
            {
                var doctor = doctorData.FirstOrDefault(p => p.Id == id);
                if(doctor == null)
                {
                    return NotFound(new
                    {
                        Message = "Doctor not found with specif Id",
                        TimeStamp = DateTime.UtcNow
                    });
                }

                doctor.Name = updateDoctor.Name;
                doctor.Specialization = updateDoctor.Specialization;
                doctor.Email = updateDoctor.Email;
                doctor.ContactNumber = updateDoctor.ContactNumber;

                return Ok(new
                {
                    Message = "Doctor Data Updated Successfully",
                    Result = "Success",
                    UpdatedDoctor = new
                    {
                        doctor.Id,
                        doctor.Name,
                        doctor.Specialization,
                        doctor.Email,
                        TimeStamp = DateTime.UtcNow
                    }
                });

            }
            catch (FormatException)
            {
                return BadRequest(new
                {
                    Result = "Doctor Not found with given Id",
                    TimeStamp = DateTime.UtcNow
                });
            }
        }


        [HttpDelete("{id}")]

        public ActionResult Delete(Guid id)
        {
            try
            {
                var doctor = doctorData.FirstOrDefault();

                if(doctor == null)
                {
                    return NotFound(new { Message = "Doctor Not found  with the Given id" });

                }

                doctorData.Remove(doctor);
                return Ok(new { Message = "Doctor data  Deleted Successfully" });

            }
            catch (FormatException)
            {
                return BadRequest(new
                {
                    Message = "Invalid Id Format, Please Enter correct Id Format"
                });
            }
        }
    }
}
