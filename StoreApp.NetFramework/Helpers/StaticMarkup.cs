using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreApp.NetFramework.Helpers
{
    public static class StaticMarkup
    {
        public static string GetSignInErrorMarkup()
        {
            return "<span style='z-index: 1;"
                        + "LEFT: 660px; POSITION: absolute; TOP: 30px; color:Pink'>" +
                            "Invalid credentials" +
                            "</span>";
        }

        public static string GetSignUpErrorMarkup()
        {
            return "<span style='z-index: 1;"
                        + "LEFT: 550px; POSITION: absolute; TOP: 30px; color:Pink'>" +
                            "Sorry, we are unable to create a user instance at this time" +
                            "</span>";
        }

        public static string GetServerSideErrorMarkup(string message)
        {
            return $"<span style='z-index: 1;"
                        + "LEFT: 550px; POSITION: absolute; TOP: 30px; color:Pink;font-size:16px'>" +
                            $"{message}" +
                            "</span>";
        }
    }
}