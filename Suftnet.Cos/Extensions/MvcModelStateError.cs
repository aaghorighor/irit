namespace Suftnet.Cos.Extension
{
    using Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    public static class MvcModelStateError
    {
        public static IList<ErrorReasonInfo> AjaxErrors(this ModelStateDictionary modelState)
        {
            var errors = new List<ErrorReasonInfo>();
            var builder = new StringBuilder();
            var logger = GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>();

            foreach (var state in modelState)
            {
                if (state.Value.Errors.Count > 0)
                {
                    var err = new ErrorReasonInfo { PropertyName = state.Key };
                    foreach (var error in state.Value.Errors)
                    {
                        err.Error += error.ErrorMessage;
                        builder.Append(error.ErrorMessage);
                        builder.AppendLine();
                    }

                    errors.Add(err);
                }
            }

            logger.Log(builder.ToString(),Common.EventLogSeverity.Error);

            return errors;
        }
    }
}