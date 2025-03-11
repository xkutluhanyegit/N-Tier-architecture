using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applications.Validations.FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Applications.DependencyInjection.AspectCore
{
    public static class DependencyInjectionmodule
    {
        public static IServiceCollection AddAspectCoreServices(this IServiceCollection services)
        {
            

            //Fluent Validation Dependencies 
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>());


            return services;
        }
    }
}