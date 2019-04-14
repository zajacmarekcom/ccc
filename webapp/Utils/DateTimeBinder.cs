using System;
using System.Globalization;
using System.Web.Mvc;

namespace webapp.Utils
{
    public class DateTimeBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            try
            {
                if (!string.IsNullOrEmpty(value.AttemptedValue))
                {
                    var date = DateTime.ParseExact(value.AttemptedValue, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    return date;
                }
            }
            catch(Exception)
            {
                return new DefaultModelBinder().BindModel(controllerContext, bindingContext);
            }
            return new DefaultModelBinder().BindModel(controllerContext, bindingContext);
        }
    }
}