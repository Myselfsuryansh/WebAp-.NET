using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Students")]
    public class StudentController : ControllerBase
    {

        public static List<Student> studentsData = new List<Student>
        {

        };

        [HttpGet]

        public ActionResult<IEnumerable<Student>> Get()
        {
            try
            {
                if (!studentsData.Any())
                {
                    return Ok(new
                    {
                        Message = "No student Data Available",
                        Timestamp = DateTime.UtcNow,
                    });
                }

                var response = new
                {
                    Result = studentsData,
                    Message = "Sudent Data Fetched Successfully",
                    Timestamp = DateTime.UtcNow,
                };
                return Ok(response);
            }

            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "An error occured while fetching the product",
                    ErrorDetails = e,
                    TimeStamp = DateTime.UtcNow
                });

            }

        }

        [HttpPost]

        public ActionResult Post([FromBody] Student student)
        {
            try
            {
                student.Id = student.Id == Guid.Empty ? Guid.NewGuid() : student.Id;

                studentsData.Add(student);

                return CreatedAtAction(nameof(Get), new { id = student.Id }, new
                {
                    Result = student,
                    Message = " Student Added Successfully",
                    Timestamp = DateTime.UtcNow
                });
            }
            catch (FormatException )
            {
                return BadRequest(new
                {
                    Message = "Invalid Post Format"
                });
            }
        }

        [HttpGet("{id}")]

        public ActionResult<Student> Get(Guid id )
        {
            try
            {
                var student = studentsData.FirstOrDefault(p => p.Id == id);
                if(student == null)
                {
                    return NotFound(new
                    {
                        Message = "Student Not found with specific ID",
                        Timestamp = DateTime.UtcNow
                    });


                }
                return Ok(new
                {
                    Result = student,
                    Message = "Student Fetched Successfully",
                    Timestamp = DateTime.UtcNow
                });
            }

            catch (FormatException)
            {
                return BadRequest(new
                {
                    Message = "Student not found with specific Id"
                });
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Student updateStudent)
        {
            try
            {
                var student = studentsData.FirstOrDefault(p => p.Id == id);
                if (student == null)
                {
                    return NotFound(
                        new
                        {
                            Result = "Student not found with the given Id",
                            Timestamp = DateTime.UtcNow
                        }
                   );
                }

                
                student.Name = updateStudent.Name;
                student.Email = updateStudent.Email;
                student.Addess = updateStudent.Addess;
                student.PinCde = updateStudent.PinCde;

                return Ok(new
                {
                    Message = "Student Updated Successfully",
                    Result = "Sucess",
                    UpdatedStudent = new
                    {
                            student.Id,
                            student.Name,
                            student.Email,
                            student.Addess,
                            student.PinCde,
                            Timestamp = DateTime.UtcNow

                    }
                });


            }
            catch (FormatException)
            {
                return BadRequest(new
                {
                    Message = "Invalid put format. Please provide a valid GUID.",
                    Timestamp = DateTime.UtcNow
                });
            }
        }


        [HttpDelete("{id}")]

        public ActionResult Delete(Guid id)
        {
            try {

                var student = studentsData.FirstOrDefault();
                if(student == null)
                {
                    return NotFound(new { Message = "Student not found with the given Id" });
                }

                studentsData.Remove(student);
                return Ok(new { Message = " Student Deleted Successfully" });

            }

            catch (FormatException) {
                return BadRequest(new
                {
                    Message = "Invalid Id format, Please enter valid Id format"
                });
            }

        }
    }
}
