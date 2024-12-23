using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApp.Models.HospitalData;
namespace WebApp.Controllers.HospitalController
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Patient")]
    public class PatientController : ControllerBase
    {

        private static List<Patient> patientData = new List<Patient>
        { };

        [HttpGet]

        public ActionResult<IEnumerable<Patient>> Get()
        {
            try
            {
                if (!patientData.Any())
                {
                    return Ok(new
                    {
                        Message = "No Patient Data Available",
                        TimeStamp = DateTime.UtcNow
                    });
                }

                var response = new
                {
                    Result = patientData,
                    Message = "PatientData Fetched Successfully",
                    TimeStamp = DateTime.UtcNow
                };
                return Ok(response);

            }
            catch (FormatException e)
            {
                return StatusCode(500, new
                {
                    Message = "An error occured while fetching the Patient",
                    ErrorDetails = e,
                    TimeStamp = DateTime.UtcNow

                });


            }
        }

        [HttpPost]

        public ActionResult Post([FromBody] Patient patient)
        {
            try
            {
                patient.Id = patient.Id == Guid.Empty ? Guid.NewGuid() : patient.Id;

                patientData.Add(patient);

                return CreatedAtAction(nameof(Get), new { id = patient.Id }, new
                {
                    Result = patient,
                    Message = "Patient Data Addedd Successfully",
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

        public ActionResult<Patient> Get(Guid id)
        {
            try
            {

                var Patient = patientData.FirstOrDefault(p => p.Id == id);
                if (Patient == null)
                {
                    return NotFound(new
                    {
                        Message = "Patient Not Found with specific Id",
                        TimeStamp = DateTime.UtcNow
                    });
                }

                return Ok(new
                {
                    Result = Patient,
                    Message = "Patient Data Fetched Successfully",
                    TimeStamp = DateTime.UtcNow

                });

            }
            catch (FormatException)
            {
                return BadRequest(new
                {
                    Message = "Patient Not found with specific Id"
                });
            }
        }

        [HttpPut("{id}")]

        public ActionResult Put(Guid id, [FromBody] Patient updarePatient)
        {
            try
            {
                var patient = patientData.FirstOrDefault(p => p.Id == id);
                if (patient == null)
                {
                    return NotFound(new
                    {
                        Message = "Patient Not Found with specific Id",
                        TimeStamp = DateTime.UtcNow
                    });
                }

                patient.Name = updarePatient.Name;
                patient.AdmissionDate = updarePatient.AdmissionDate;
                patient.Address = updarePatient.Address;
                patient.PhoneNumber = updarePatient.PhoneNumber;
                patient.DateOfBirth = updarePatient.DateOfBirth;
                patient.Gender = updarePatient.Gender;


                return Ok(new
                {
                    Message = "Patient Data Updated Successfully",
                    Result = "Success",
                    UpdatedPatient = new
                    {
                        patient.Id,
                        patient.Name,
                        patient.Address,
                        patient.AdmissionDate,
                        patient.DateOfBirth,
                        patient.Gender

                    }
                });
            }
            catch (FormatException)
            {
                return BadRequest(new
                {
                    Result = "Patient not found with given Id",
                    TimeStamp = DateTime.UtcNow
                });
            }
        }

        [HttpDelete("{id}")]

        public ActionResult Delete(Guid id)
        {
            try
            {
                var patient = patientData.FirstOrDefault();
                if(patient == null)
                {
                    return NotFound(new
                    {
                        Message = "Patient Not found with the Given Id"
                    });

                   

                }
                patientData.Remove(patient);
                return Ok(new { Message = "Patient Data Removed Successfully" });
            }
            catch (FormatException)
            {
                return BadRequest(new
                {
                    Message = "Invalid Id Format, Please enter correct Id Format"
                });
            }
        }

    
    }
}
