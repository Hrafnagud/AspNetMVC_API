using AspNetMVC_API_BusinessLogicLayer.Repository;
using AspNetMVC_API_Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AspNetMVC_API
{
    /// <summary>
    /// Summary description for WebServiceStudent
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceStudent : System.Web.Services.WebService
    {
        StudentRepo studentRepo = new StudentRepo();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<Student> GetAll()
        {
            try
            {
                List<Student> list = studentRepo.GetAll();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public string Insert(string name, string surname)
        {
            try
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname))
                {
                    throw new Exception("Both name and surname must be provided!");
                }

                Student newStudent = new Student()
                {
                    Name = name,
                    Surname = surname
                };
                int insertResult = studentRepo.Insert(newStudent);
                if (insertResult > 0)
                {
                    //First Approach
                    return "Record has been successfully saved. Id = " + newStudent.Id;
                }
                else
                {
                    throw new Exception("An unexpected error has occured during record insertion!");
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
