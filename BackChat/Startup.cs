using System;
using BackChat.Interfaces;
using BackChat.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BackChat.WebSocketManager;

namespace BackChat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<IUsuariosProvider, UsuariosProvider>();
            services.AddSingleton<ISalasProvider, SalasProvider>();
            services.AddSingleton<IChatTracebilityProvider, ChatTracebilityProvider>();
            services.AddSingleton<IEnroladosProvider, EnroladosProvider>();
          
            services.AddWebSocketManager();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env,
             IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseWebSockets();

            // Add ws services
            app.MapWebSocketManager("/notifications", serviceProvider.GetService<MessageHandlerProvider>());
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
        }
        
    }
}
