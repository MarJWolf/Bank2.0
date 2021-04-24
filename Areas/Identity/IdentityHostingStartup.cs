using System;
using Bank.Areas.Identity.Data;
using Bank.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Bank.Areas.Identity.IdentityHostingStartup))]
namespace Bank.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
           // builder.ConfigureServices((context, services) => {
           //     services.AddDbContext<BankDbContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("BankDbContextConnection")));

            //    services.AddDefaultIdentity<AccountUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<BankDbContext>();
           // });
        }
    }
}