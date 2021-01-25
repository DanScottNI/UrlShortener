using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Mvc;

namespace UrlBitlyClone.Controllers
{
    public class UpdateSchemaController : Controller
    {
        private readonly IMigrationRunner runner;

        public UpdateSchemaController(IMigrationRunner runner)
        {
            this.runner = runner;
        }

        public IActionResult Index()
        {
            if (runner.HasMigrationsToApplyUp())
            {
                runner.MigrateUp();
            }

            return View();
        }
    }
}
