using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PRG521_Practical.Areas.Identity.Data;

[assembly: HostingStartup(typeof(PRG521_Practical.Areas.Identity.IdentityHostingStartup))]
namespace PRG521_Practical.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SLDContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SLDContextConnection")));

                services.AddDefaultIdentity<SLDUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<SLDContext>();
            });
        }
    }
}