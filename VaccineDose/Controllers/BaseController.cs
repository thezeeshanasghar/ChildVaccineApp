using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VaccineDose.Controllers
{
    public class BaseController : ApiController
    {
        public static string GetMessageFromExceptionObject(Exception ex)
        {
            String message = ex.Message;
            if(ex is DbEntityValidationException)
            {
                var errorMessages = ((DbEntityValidationException)ex).EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                return string.Concat(ex.Message, "<br />The validation errors are: ", fullErrorMessage);
            }
            message += (ex.InnerException != null) ? ("<br />" + ex.InnerException.Message) : "";
            message += (ex.InnerException != null && ex.InnerException.InnerException != null) ? ("<br />" + ex.InnerException.InnerException.Message) : "";
            return message;
        }
        protected DateTime calculateDate(DateTime date, int GapInDays)
        {
            // For 3 months
            if (GapInDays == 84)
                return date.AddMonths(3);
            // For 9 Year 1 month
            else if (GapInDays == 3315)
                return date.AddYears(9).AddMonths(1);
            // For 10 Year 6 month
            else if (GapInDays == 3833)
                return date.AddYears(10).AddMonths(6);
            // For 1 to 15 years
            else if (GapInDays == 365 || GapInDays == 730 || GapInDays == 1095 ||
                GapInDays == 1460 || GapInDays == 1825 || GapInDays == 2190 || GapInDays == 2555 ||
                GapInDays == 2920 || GapInDays == 3285 || GapInDays == 3650 || GapInDays == 4015 ||
                GapInDays == 4380 || GapInDays == 4745 || GapInDays == 5110 || GapInDays == 5475)
                return date.AddYears((int)(GapInDays / 365));
            // From 6 months to 11 months
            else if (GapInDays >= 168 && GapInDays <= 334)
                return date.AddMonths((int)(GapInDays / 28));
            // From 13 months to 20 months
            else if (GapInDays >= 395 && GapInDays <= 608)
                return date.AddMonths((int)(GapInDays / 29));
            // From 21 months to 11 months
            else if (GapInDays >= 639 && GapInDays <= 1795)
                return date.AddMonths((int)(GapInDays / 30));
            else
                return date.AddDays(GapInDays);
        }

        protected bool IsJson(string input)
        {
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]");
        }
    }
}

