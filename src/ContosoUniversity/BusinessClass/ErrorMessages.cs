using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.BusinessClass
{
    public static class ErrorMessages
    {
        #region Authenticate
        public static string AuthenticateError()
        {
            return "Invalid login or password.";
        }

        public static string ErrorCourseEnrollement()
        {
            return "You already subscribed to this lesson";
        } 
        #endregion

        #region CheckImage
        public static string ErrorSize()
        {
            return "The size of the image is limited to 100kb";
        }

        public static string ErrorExtension()
        {
            return "Image extention authorized is png or jpeg";
        }
        #endregion

        #region CourseSession

        public static string ErrorMessageSameCourse()
        {
            return "you have already this course";
        }

        public static string ErrorMessageNegativeTime()
        {
            return "Time can't be negative";
        }

        public static string ErrorMessageNotSameDay()
        {
            return "the day of the course and the start date are not same";
        }
        #endregion
    }
}