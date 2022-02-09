using AspNetMVC_API_BusinessLogicLayer.Repository;
using AspNetMVC_API_Entity.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private bool IsAuthenticated
        {
            get
            {
                bool result = false;
                try
                {
                    string authorization = "";
                    authorization = HttpContext.Current.Request.Headers["Authorization"];
                    if (authorization != null)
                    {
                        authorization = authorization.Replace("Basic", "");
                        byte[] byteArray = Convert.FromBase64String(authorization);
                        string usernamePassword = System.Text.Encoding.UTF8.GetString(byteArray);
                        //programming103:103103 <= username:password form
                        bool usernameResult = usernamePassword.Split(':').First().Equals(ConfigurationManager.AppSettings["USERNAME"].ToString());
                        bool passwordResult = usernamePassword.Split(':').Last().Equals(ConfigurationManager.AppSettings["PASSWORD"].ToString());

                        result = (usernameResult && passwordResult) ? true : false;
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    result = false;
                    return result;
                }
            }
        }

        private void CheckCredentials()
        {
            if (!IsAuthenticated)
            {
                throw new Exception("Wrong username or password entry. Try again");
            }
        }

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
                CheckCredentials();
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
                CheckCredentials();
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
                    //return "Record has been successfully saved. Id = " + newStudent.Id;

                    //Second Approach
                    string jsonString = JsonConvert.SerializeObject(newStudent);
                    return jsonString;
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

        [WebMethod]
        public string Delete(int id)
        {
            try
            {
                CheckCredentials();
                if (id > 0)
                {
                    Student student = studentRepo.GetById(id);
                    if (student == null)
                    {
                        throw new Exception("There is no such studentd. Delete operation failed!");
                    }
                    int deleteResult = studentRepo.Delete(student);
                    if (deleteResult > 0)
                    {
                        return "Record has been successfully deleted.";
                    }
                    else
                    {
                        throw new Exception("An unexpected error has occured during record deletion process!");
                    }
                }
                else
                {
                    throw new Exception("Provided id must be greater than zero");
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public string Update(int currentId, string newName, string newSurname)
        {
            try
            {
                CheckCredentials();
                if (currentId <= 0)
                {
                    throw new Exception("Provided id must be greater than zero!");
                }
                if (string.IsNullOrEmpty(newName) && string.IsNullOrEmpty(newSurname))
                {
                    throw new Exception("Both name and surname fields for update, must be updated!");
                }
                Student currentStudent = studentRepo.GetById(currentId);
                if (currentStudent == null)
                {
                    throw new Exception("No student found. Update operation failed!");
                }

                //If newName parameter is not null, update will take place.
                if (!string.IsNullOrEmpty(newName))
                {
                    currentStudent.Name = newName;
                }

                //If newSurname parameter is not null, update will take place.
                if (!string.IsNullOrEmpty(newSurname))
                {
                    currentStudent.Surname = newSurname;
                }

                int updateResult = studentRepo.Update();
                if (updateResult > 0)
                {
                    return "Record has been successfully updated.";
                }
                else
                {
                    throw new Exception("An unexpected error has occured. Record update failed.");
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


    }
}
