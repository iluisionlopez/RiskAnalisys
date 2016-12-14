using CSRTool.Common;
using CSRToolWebApp.Common;
using CSRToolWebApp.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;

namespace CSRToolWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Default()
        {
            UserModel user = new UserModel();

            if (SessionHandler.LoggedInUser == null)
            {
                user = GetUserFromRequest();
                SessionHandler.LoggedInUser = user;
                Logging.LogInfo(String.Format("User '{0}' logged in", user.Id), SessionHandler.CurrentSession.SessionID); //userId not provided by WAM
            }
            else
            {
                user = SessionHandler.LoggedInUser;
            }
            var model = new AssessmentModel();
            model.Assessor = user;
            var strOperation = "assessmentcustomers/?user={0}";
            model.AssessmentList = AssessmentModel.GetCustomerAssessmnets(new RestService(string.Format(strOperation, model.Assessor.Id)));
            strOperation = "assessmentsuppliers/?user={0}";
            model.AssessmentList = model.AssessmentList.Concat(AssessmentModel.GetSupplierAssessmnets(new RestService(string.Format(strOperation, model.Assessor.Id))));
            return View("Default", model);
        }

        [HttpGet]
        public ActionResult GetAssessmentsForUser()
        {
            var user = new UserModel();
            var model = new AssessmentModel {Assessor = user.GetInLoggedUser()};
            model.AssessmentList = model.GetUserAssessmnets();
            return View("Default", model);
        }


        public ActionResult UserInfo()
        {
            return View(SessionHandler.LoggedInUser);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Info()
        {
            return View();
        }

        public ActionResult Webscan()
        {
            return View();
        }

        public ActionResult ChangeLanguage(string langCode, string returnUrl)
        {
            SessionHandler.SelectedLanguage = new CultureInfo(langCode);
            return Redirect(returnUrl);
        }


        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult LogIn(string returnUrl)
        {
            //  Set default language if none is chosen
            if (SessionHandler.SelectedLanguage == null)
            {
                SessionHandler.SelectedLanguage = Helper.Request.GetDefaultLanguage();
            }

            if (SessionHandler.LoggedInUser == null)
            {
                var user = GetUserFromRequest();
                SessionHandler.LoggedInUser = user;
                Logging.LogInfo(String.Format("User '{0}' logged in", user.UserName), SessionHandler.CurrentSession.SessionID);  
            }

            return RedirectToRoute("Home");
           
        }

        private UserModel GetUserFromRequest()
        {
            var user = new UserModel();

            if (Request.Headers.AllKeys.Contains("xdsuser"))
            {
                user.UserName = Request.Headers["xdsuser"];

                if (Request.Headers.AllKeys.Contains("firstname"))
                    user.FirstName = Request.Headers["firstname"];

                if (Request.Headers.AllKeys.Contains("lastname"))
                    user.LastName = Request.Headers["lastname"];

                if (Request.Headers.AllKeys.Contains("email"))
                    user.Email = Request.Headers["email"];

                if (Request.Headers.AllKeys.Contains("iv-groups"))
                {
                    var groups = Request.Headers["iv-groups"];
                    Logging.LogInfo(String.Format("iv-groups '{0}' ", groups), SessionHandler.CurrentSession.SessionID);
                }

                //Handle cases where logged in user does not exist in database
                //Get userid from service 
                var foundUserInDB = UserModel.GetUserByName(user.UserName);

                if (foundUserInDB == null) //User not found in database
                {
                    //Save
                    var genericResponse = UserModel.SaveFirstTimeUser(user);
                    
                    //If OK - try again
                    if (genericResponse.ResponseMessage.Equals("OK"))
                        foundUserInDB = UserModel.GetUserByName(user.UserName);
                }

                if (foundUserInDB != null) user.Id = foundUserInDB.Id;
            }

            //  Show log on view
            //return View(new LogInInfo());
            
            // TODO: re-route to wam login if no xdsuser found
            //Only to bypass WAM in dev set to first user
            if (user.UserName.IsEmpty())
            {
                user = UserModel.GetUserByName("afofzj");
                //Only for test in debug
                //user.UserName = "MyTestUser";
                //user.FirstName = "My Test";
                //user.LastName = "User";
                //user.Email = "toolkitsupport@scania.com";
                //user.Phone = "+46 723 87 90 87";

                //Save
                //var genericResponse = UserModel.SaveFirstTimeUser(user);

                ////If OK - try again
                //if (genericResponse.ResponseMessage.Equals("OK"))
                //    user = UserModel.GetUserByName(user.UserName);
            }

            return user;
        }

        [HttpPost]
        public ActionResult LogIn(LogInInfo loginInfo, string returnUrl)
        {
            try
            {
                //check if the username or password is empty
                if (string.IsNullOrEmpty(loginInfo.UserId) || string.IsNullOrEmpty(loginInfo.Password))
                {
                    ViewBag.ErrorMessage = new UserMessage(UserMessageType.Error, "*** User name and password cannot be empty ***");
                    return View(loginInfo);
                }

                //outside scania network? comment this
                //Authenticate user
                //if (!ScaniaADCommunication.AuthenticateUser(loginInfo.UserId, loginInfo.Password))
                //{
                //    Logging.LogWarning(String.Format("Failed to authenticate user '{0}'", loginInfo.UserId));
                //    ViewBag.ErrorMessage = new UserMessage(UserMessageType.Error, "*** Failed to log in. User name or password may be incorect. ***");
                //    return View(loginInfo);
                //}

                //  Get user info and set logged in user
                //SessionHandler.LoggedInUser = ScaniaADCommunication.GetUserInfo(loginInfo.UserId);

                //outside scania network? decomment this
                var user = new UserModel
                {
                    Id = Constants.UserAnna.ToString(),
                    UserName    = "ahawpd",
                    FirstName   = "Anna",
                    LastName    = "Häll Rivera",
                    Phone       = "0723-879087"
                };
                SessionHandler.LoggedInUser = user;



                Logging.LogInfo(String.Format("User '{0}' logged in", loginInfo.UserId), SessionHandler.CurrentSession.SessionID);

                if (!String.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToRoute("Home");
                }
            }
            catch (ScaniaADException ex)
            {
                Logging.LogError(String.Format("ScaniaADException occured during login for user '{0}' --> {1}", loginInfo.UserId, ex.Message), SessionHandler.CurrentSession.SessionID);
                ViewBag.ErrorMessage = new UserMessage(UserMessageType.Error, "*** An error occured, please try again or contact Scania Support. ***");
                return View(loginInfo);
            }
            catch (Exception ex)
            {
                Logging.LogError(String.Format("Unhandled exception occured during login for user '{0}' --> {1}", loginInfo.UserId, ex.Message), SessionHandler.CurrentSession.SessionID);
                ViewBag.ErrorMessage = new UserMessage(UserMessageType.Error, "*** An error occured, please try again or contact Scania Support. ***");
                return View(loginInfo);
            }
        }

        public ActionResult LogOut(string returnUrl)
        {
            Logging.LogInfo(String.Format("User '{0}' logged out", SessionHandler.LoggedInUser.Id), SessionHandler.CurrentSession.SessionID);
            SessionHandler.ClearSession();
            return RedirectToRoute("LogIn");
        }
    }
}