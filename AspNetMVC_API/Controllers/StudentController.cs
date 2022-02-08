using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AspNetMVC_API.Models.ViewModels;
using AspNetMVC_API_BusinessLogicLayer.Repository;
using AspNetMVC_API_Entity.Models;

namespace AspNetMVC_API.Controllers
{
    [System.Web.Http.RoutePrefix("student")]
    public class StudentController : ApiController
    {
        StudentRepo studentRepo = new StudentRepo();

        public ResponseData GetAll()
        {
            try
            {
                var list = studentRepo.GetAll().Select(x => new { x.Id, x.Name, x.Surname, x.RegisterDate }).ToList();

                if (list != null)
                {
                    if (list.Count == 0)
                    {
                        return new ResponseData()
                        {
                            Success = true,
                            Message = "There are no registered student yet!",
                            Data = list
                        };
                    }
                    return new ResponseData()
                    {
                        Success = true,
                        Message = "All Student list has been successfully sent!",
                        Data = list
                    };
                }
                else
                {
                    return new ResponseData()
                    {
                        Success = false,
                        Message = "Error has occured while fetching all stundents list!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [System.Web.Http.Route("detail/{id:int:min(1)}")]
        public ResponseData GetDetail(int id)
        {
            try
            {
                var student = studentRepo.GetById(id);
                if (student == null)
                {
                    throw new Exception("There are no records found associated with the id sent.");
                }

                return new ResponseData()
                {
                    Success = true,
                    Message = "Record found!",
                    Data = new
                    {
                        student.Id,
                        student.Name,
                        student.Surname,
                        student.RegisterDate
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public ResponseData GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    var student = studentRepo.GetById(id);
                    if (student == null)
                    {
                        throw new Exception($"There are no records for id {id}!");
                    }

                    return new ResponseData()
                    {
                        Success = true,
                        Message = "Record has been found!",
                        Data = new { student.Id, student.Name, student.Surname, student.RegisterDate }
                    };
                }
                else
                {
                    return new ResponseData()
                    {
                        Success = false,
                        Message = "Negative value can not be sent!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        //api/student/Insert
        [System.Web.Http.HttpPost]
        public ResponseData Insert([FromBody] StudentViewModel model)    //[FromUri] => Postman params, [FromBody] => Postman FromBody form-data
        {
            try
            {
                if (model == null)
                {
                    return new ResponseData()
                    {
                        Success = false,
                        Message = "In order to perform insertion, proper data format is required!"
                    };
                }
                Student newStudent = new Student()
                {
                    Name = model.name,
                    Surname = model.surname,
                    RegisterDate = model.registerdate
                };

                if (studentRepo.Insert(newStudent) > 0)
                {
                    return new ResponseData()
                    {
                        Success = true,
                        Message = $"New student record has been added. Id = {newStudent.Id}"
                    };
                }
                else
                {
                    throw new Exception("Error has occured during student record insertion!");
                }
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [System.Web.Http.HttpPost]
        public ResponseData Update([FromBody] StudentViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return new ResponseData()
                    {
                        Success = false,
                        Message = "Data entry is required to perform update action!"
                    };
                }

                var student = studentRepo.GetById(model.id);
                if (student == null)
                {
                    return new ResponseData()
                    {
                        Success = false,
                        Message = "Student record hasn't been found. Update failed!"
                    };
                }

                student.Name = model.name;
                student.Surname = model.surname;
                if (studentRepo.Update() > 0)
                {
                    return new ResponseData()
                    {
                        Success = true,
                        Message = "Record has been successfully updated."
                    };
                }
                else
                {
                    throw new Exception("An unexpected error has occured during updating student record.");
                }

            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [System.Web.Http.HttpPost]
        public ResponseData Delete(int id)
        {
            try
            {
                var student = studentRepo.GetById(id);
                if (student == null)
                {
                    throw new Exception("There are no records associated with the id sent.");
                }

                if (studentRepo.Delete(student) > 0)
                {
                    return new ResponseData()
                    {
                        Success = true,
                        Message = "Record has been successfully deleted."
                    };
                }
                else
                {
                    throw new Exception("An unexpected error has occured during student record deletion!");
                }
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        
    }
}