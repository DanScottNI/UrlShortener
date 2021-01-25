using FluentMigrator.Runner;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UrlBitlyClone.Infrastructure.ActionFilters
{

    /// <summary>
    /// Updates the database schema if it needs to be updated.
    /// </summary>
    /// <seealso cref="IActionFilter" />
    public class UpdateSchemaFilter : IActionFilter
    {
        private readonly IMigrationRunner runner;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSchemaFilter"/> class.
        /// </summary>
        /// <param name="runner">The migration runner.</param>
        public UpdateSchemaFilter(IMigrationRunner runner)
        {
            this.runner = runner;
        }

        /// <inheritdoc/>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (this.runner.HasMigrationsToApplyUp())
            {
                this.runner.MigrateUp();
            }
        }

        /// <inheritdoc/>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do nothing.
        }
    }
}
