namespace Suftnet.Cos.Extension
{
    using Suftnet.Cos.Core;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;  
    using System.Web.Http.ModelBinding;

    public static class ModelStateError
    {  
        public static IEnumerable<string> ToErrorList(this ModelStateDictionary modelState)
        {
            var errors = new List<string>();

             if (!modelState.IsValid)
            {
                IEnumerable<ModelError> modelerrors = modelState.SelectMany(x => x.Value.Errors);
                foreach (var modelerror in modelerrors)
                {
                    errors.Add(modelerror.ErrorMessage);
                }
            }

             return errors;
        }

        public static string Error(this ModelStateDictionary modelState)
        {
            var errors = new StringBuilder();

            if (!modelState.IsValid)
            {
                IEnumerable<ModelError> modelerrors = modelState.SelectMany(x => x.Value.Errors);
                foreach (var modelerror in modelerrors)
                {
                    if(modelerror.Exception != null)
                    {
                        errors.AppendLine(modelerror.Exception.Message);
                    }
                    else
                    {
                        errors.AppendLine(modelerror.ErrorMessage);
                    }                   
                }
            }

            return errors.ToString();
        }        

    }

}