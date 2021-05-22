using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bank.Data;
using Bank.Areas.Identity.Data;
using Bank.Models;
using Microsoft.AspNetCore.Identity;

namespace Bank
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
            services.AddControllersWithViews();
            services.AddRazorPages(options => 
            {
            
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Banker",
                policy => policy.RequireRole("Banker", "Client"));
                options.AddPolicy("Cashier",
                    policy => policy.RequireRole("Cashier", "Client"));
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ApplicationDbContext")));

            services.AddDefaultIdentity<AccountUser>(options => options.SignIn.RequireConfirmedAccount = true)
                  .AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            IServiceProvider serviceProvider = (IServiceProvider)app.ApplicationServices.CreateScope();
            await CreateRoles(serviceProvider); // tuk dava exception za bazata ot danni
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<AccountUser>>();
            var ContextManager = serviceProvider.GetRequiredService<ApplicationDbContext>();

            string[] roleNames = { "Banker", "Cashier", "Client"};
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                // ensure that the role does not exist
                if (!roleExist)
                {
                    //create the roles and seed them to the database: 
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // find the user with the admin email 
            var _user = await UserManager.FindByEmailAsync("admin@email.com");

            // check if the user exists
            if (_user == null)
            {
                //Here you could create the super admin who will maintain the web app
                var poweruser = new AccountUser
                {
                    UserName = "Admin",
                    Email = "admin@email.com",
                    EmailConfirmed = true
                };
                string adminPassword = "Admin1234!";

                var createPowerUser = await UserManager.CreateAsync(poweruser, adminPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(poweruser, "Banker");
                }
                else
                {
                    await UserManager.DeleteAsync(poweruser);
                    throw new Exception("Error while creating admin!");
                }

            }

            var MainAcc = ContextManager.Employee.Where(v => v.UserId == _user.Id).FirstOrDefault();
            if (MainAcc == null || MainAcc.UserId != _user.Id )
            {
                //Here you could create the super admin who will maintain the web app but his employee part
                var powerEmployee = new Employee
                {
                    NAME = "Emily Phoreas",
                    PN = "634",
                    user = _user,
                    UserId = _user.Id
                };

               ContextManager.Employee.Add(powerEmployee);
               ContextManager.SaveChanges();

            }

            var CurEx = ContextManager.Currency.Where(v => v.NAME == "BGN").FirstOrDefault();
            if (CurEx == null )
            {
                var Cur = new Currency
                {
                    NAME = "BGN",
                    FULL_NAME = "Bulgarian lev"
                };
                ContextManager.Currency.Add(Cur);
                var Cur2 = new Currency
                {
                    NAME = "USD",
                    FULL_NAME = "U.S Dolar"
                };
                ContextManager.Currency.Add(Cur2);
                var Cur3 = new Currency
                {
                    NAME = "RUB",
                    FULL_NAME = "Russian ruble"
                };
                ContextManager.Currency.Add(Cur3);
                var Cur4 = new Currency
                {
                    NAME = "EUR",
                    FULL_NAME = "Euro"
                };
                ContextManager.Currency.Add(Cur4);
                var Cur5 = new Currency
                {
                    NAME = "GBP",
                    FULL_NAME = "U.K pound"
                };
                ContextManager.Currency.Add(Cur5);
                ContextManager.SaveChanges();
            }

            //razplashtatelna,  spestovna,  depozitna
            var AccTypeEx = ContextManager.AccountType.Where(v => v.NAME == "Current Account").FirstOrDefault();
            if (AccTypeEx == null)
            {
                var AccType = new AccountType
                {
                    NAME = "Current Account",
                    LIHVA = 0,
                    MESECHNA_TAKSA = 2.50
                };
                ContextManager.AccountType.Add(AccType);
                ContextManager.SaveChanges();
                var AccType2 = new AccountType
                {
                    NAME = "Saving Account",
                    LIHVA = 7.5,
                    MESECHNA_TAKSA = 1.0
                };
                ContextManager.AccountType.Add(AccType2);
                ContextManager.SaveChanges();
                var AccType3 = new AccountType
                {
                    NAME = "Deposit Account",
                    LIHVA = 0,
                    MESECHNA_TAKSA = 5.00
                };
                ContextManager.AccountType.Add(AccType3);
                ContextManager.SaveChanges();
            }

        }
    }
}