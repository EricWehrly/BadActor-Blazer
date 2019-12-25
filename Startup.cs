using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using BadActor.Attributes;
using System;
using Blazored.Modal;
using BadActor.Shared;

namespace BadActor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazoredModal();
            services.AddSingleton(typeof(AppState));

            foreach (var definedType in Assembly.GetExecutingAssembly().DefinedTypes)
            {
                if (HasClassAttribute(definedType, typeof(AutoRegisterAttribute))) {

                    services.AddTransient(definedType);
                }
            }
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }

        private static bool HasClassAttribute(Type clsType, Type attribType)
        {
            if (clsType == null)
                throw new ArgumentNullException("clsType");
            return clsType.GetCustomAttributes(attribType, true).Any() || (clsType.BaseType != null && HasClassAttribute(clsType.BaseType, attribType));
        }
    }
}
