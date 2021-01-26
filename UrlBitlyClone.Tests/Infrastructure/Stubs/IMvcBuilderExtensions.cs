using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace UrlBitlyClone.Tests.Infrastructure.Stubs
{
    public static class IMvcBuilderExtensions
    {
        public static IMvcBuilder AddControllerDifferentAssembly<T>(this IMvcBuilder builder)
            where T : ControllerBase
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var entityTypes = typeof(T).Assembly.DefinedTypes;

            foreach (var item in from c in entityTypes
                                 where IsController(c)
                                 select c.AsType())
            {
                builder.Services.TryAddTransient(item, item);
            }

            builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            return builder;
        }

        public static IMvcBuilder AddActionFiltersDifferentAssembly<T>(this IMvcBuilder builder)
            where T : IActionFilter
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var entityTypes = typeof(T).Assembly.DefinedTypes;

            foreach (var item in from c in entityTypes
                                 where IsActionFilter(c)
                                 select c.AsType())
            {
                builder.Services.TryAddTransient(item, item);
            }
            
            return builder;
        }

        /// <summary>
        /// Determines if a given <paramref name="typeInfo"/> is an <see cref="IActionFilter"/>.
        /// </summary>
        /// <param name="typeInfo">The <see cref="TypeInfo"/> candidate.</param>
        /// <returns><code>true</code> if the type is an action filter; otherwise false.</returns>
        private static bool IsActionFilter(TypeInfo typeInfo)
        {
            if (!typeInfo.IsClass)
            {
                return false;
            }
            
            if (!typeof(IActionFilter).IsAssignableFrom(typeInfo))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determines if a given <paramref name="typeInfo"/> is a controller.
        /// </summary>
        /// <param name="typeInfo">The <see cref="TypeInfo"/> candidate.</param>
        /// <returns><code>true</code> if the type is a controller; otherwise false.</returns>
        private static bool IsController(TypeInfo typeInfo)
        {
            if (!typeInfo.IsClass)
            {
                return false;
            }

            if (typeInfo.IsAbstract)
            {
                return false;
            }

            // We only consider public top-level classes as controllers. IsPublic returns false for nested
            // classes, regardless of visibility modifiers
            if (!typeInfo.IsPublic)
            {
                return false;
            }

            if (typeInfo.ContainsGenericParameters)
            {
                return false;
            }

            if (typeInfo.IsDefined(typeof(NonControllerAttribute)))
            {
                return false;
            }

            if (!typeInfo.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase) &&
                !typeInfo.IsDefined(typeof(ControllerAttribute)))
            {
                return false;
            }

            return true;
        }
    }
}
