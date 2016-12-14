using CSRToolWebApp.Models;
using Se.So.Scania.InfoMate.Scania_AD;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CSRToolWebApp.Common
{
    public static class ScaniaADCommunication
    {
        private static ScaniaAD CreateNewADClient(string domainName)
        {
            switch (domainName)
            {
                case "xds":
                case "XDS": return new Dmz("LDAP://ldapse02.scania.com/DC=xds,DC=scania,DC=com");

                //case "global":
                //case "GLOBAL": return new Dmz("LDAP://ldapse02.scania.com/DC=global,DC=scania,DC=com");

                default:
                    throw new ScaniaADException(String.Format("Unsupported domain '{0}'", domainName));
            }
        }

        public static bool AuthenticateUser(string userId, string password)
        {
            try
            {
                string domainName = Helper.Application.LoginDomain;
                ScaniaAD adClient = CreateNewADClient(domainName);

                return adClient.IsAuthenticated(String.Format(@"{0}\{1}", Helper.Application.LoginDomain, userId), password);
            }
            catch (ScaniaADException)
            {
                throw;
            }
            catch (Exception ex)
            {
                MethodBase methodBase = MethodBase.GetCurrentMethod();
                throw new ScaniaADException(String.Format("An error occured in {0}.{1}", methodBase.DeclaringType.FullName, methodBase.Name), ex);
            }
        }

        public static UserModel GetUserInfo(string userId)
        {
            try
            {
                string domainName = Helper.Application.LoginDomain;
                ScaniaAD adClient = CreateNewADClient(domainName);

                SortedList properties = adClient.GetProperties4User(userId);

                UserModel user = new UserModel();
                user.Id = userId;
                user.FirstName = properties["givenname"] == null ? string.Empty : properties["givenname"].ToString();
                user.LastName = properties["sn"] == null ? string.Empty : properties["sn"].ToString();
                user.Phone = properties["mobile"] == null ? string.Empty : properties["mobile"].ToString();
                user.Email = properties["mail"] == null ? string.Empty : properties["mail"].ToString();
                user.CountryCode = properties["c"] == null ? string.Empty : properties["c"].ToString();
                user.Country = properties["co"] == null ? string.Empty : properties["co"].ToString();

                user.Roles = adClient.GetMemberOF(userId).OfType<string>().ToList();

                return user;
            }
            catch (ScaniaADException)
            {
                throw;
            }
            catch (Exception ex)
            {
                MethodBase methodBase = MethodBase.GetCurrentMethod();
                throw new ScaniaADException(String.Format("An error occured in {0}.{1}", methodBase.DeclaringType.FullName, methodBase.Name), ex);
            }
        }
    }

    public class ScaniaADException : System.Exception
    {
        public ScaniaADException(string message)
            : base(message)
        {
        }
        public ScaniaADException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}