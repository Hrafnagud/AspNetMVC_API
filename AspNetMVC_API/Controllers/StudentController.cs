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
    }
}