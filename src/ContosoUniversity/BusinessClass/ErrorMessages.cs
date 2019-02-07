using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.BusinessClass
{
    public static class ErrorMessages
    {
        public static string AuthenticateError()
        {
            return "Invalid login or password.";
        }

        public static string ErrorCourseEnrollement()
        {
            return "You already subscribed to this lesson";
        }

        public static string ErrorSize()
        {
            return "The size of the image is limited to 100kb";
        }

        public static string ErrorExtension()
        {
            return "Image extention authorized is png or jpeg";
        }
    }
}