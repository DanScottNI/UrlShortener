using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace UrlBitlyClone.Infrastructure.ActionFilters
{
    /// <summary>
    /// Controller convention to force an update schema on every page except for the error page.
    /// </summary>
    /// <seealso cref="IControllerModelConvention" />
    public class UpdateSchemaFilterControllerConvention : IControllerModelConvention
    {
        /// <inheritdoc/>
        public void Apply(ControllerModel controller)
        {
            if (!controller.ControllerName.StartsWith("Error", System.StringComparison.OrdinalIgnoreCase))
            {
                controller.Filters.Add(new TypeFilterAttribute(typeof(UpdateSchemaFilter)));
            }
        }
    }
}
