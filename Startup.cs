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
            await SecondaryUpdates(serviceProvider);
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<AccountUser>>();
            var ContextManager = serviceProvider.GetRequiredService<ApplicationDbContext>();


            string[] roleNames = { "Banker", "Cashier", "Client" };
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




            //currency
            var CurEx = ContextManager.Currency.Where(v => v.NAME == "BGN").FirstOrDefault();
            if (CurEx == null)
            {
                var Cur = new Currency
                {
                    NAME = "BGN",
                    FULL_NAME = "Bulgarian lev",
                    FIXED_RATE = 1
                };
                ContextManager.Currency.Add(Cur);
                var Cur2 = new Currency
                {
                    NAME = "USD",
                    FULL_NAME = "U.S Dolar",
                    FIXED_RATE = 1.6
                };
                ContextManager.Currency.Add(Cur2);
                var Cur3 = new Currency
                {
                    NAME = "RUB",
                    FULL_NAME = "Russian ruble",
                    FIXED_RATE = 45.85
                };
                ContextManager.Currency.Add(Cur3);
                var Cur4 = new Currency
                {
                    NAME = "EUR",
                    FULL_NAME = "Euro",
                    FIXED_RATE = 0.51
                };
                ContextManager.Currency.Add(Cur4);
                var Cur5 = new Currency
                {
                    NAME = "GBP",
                    FULL_NAME = "U.K pound",
                    FIXED_RATE = 0.44
                };
                ContextManager.Currency.Add(Cur5);
                var Cur6 = new Currency
                {
                    NAME = "JPY",
                    FULL_NAME = "Japanese yen",
                    FIXED_RATE = 67.83
                };
                ContextManager.Currency.Add(Cur6);
                ContextManager.SaveChanges();
            }

            //razplashtatelna,  spestovna,  depozitna
            var AccTypeEx = ContextManager.AccountType.Where(v => v.NAME == "Current Account").FirstOrDefault();
            if (AccTypeEx == null)
            {
                var AccType = new AccountType
                {
                    NAME = "Current Account",
                    INTEREST = 0,
                    TAX = 2.50
                };
                ContextManager.AccountType.Add(AccType);
                var AccType2 = new AccountType
                {
                    NAME = "Savings Account",
                    INTEREST = 7,
                    TAX = 1.0
                };
                ContextManager.AccountType.Add(AccType2);
                var AccType3 = new AccountType
                {
                    NAME = "Deposit Account",
                    INTEREST = 0,
                    TAX = 5.00
                };
                ContextManager.AccountType.Add(AccType3);
                ContextManager.SaveChanges();
            }

            var TransCatEx = ContextManager.TransactionCategory.Where(v => v.NAME == "Withdrawal").FirstOrDefault();
            if (TransCatEx == null)
            {
                var TransCat = new TransactionCategory
                {
                    NAME = "Withdrawal",
                    COEF = -1
                };
                ContextManager.TransactionCategory.Add(TransCat);

                var TransCat2 = new TransactionCategory
                {
                    NAME = "Deposit",
                    COEF = 1
                };
                ContextManager.TransactionCategory.Add(TransCat2);
                ContextManager.SaveChanges();
            }


            var ClientEx = ContextManager.Client.Where(v => v.NAME == "Client1").FirstOrDefault();
            if (ClientEx == null)
            {
                var User = new AccountUser
                {
                    UserName = "Client1",
                    Email = "admin2@email.com",
                    EmailConfirmed = true
                };
                string adminPassword = "Admin1234!";
                var createPowerUser = await UserManager.CreateAsync(User, adminPassword);
               await UserManager.AddToRoleAsync(User, "Client");

                var ClientNow = new Client
                {
                    address = "Vitoshka 17",
                    EGN = "8706065400",
                    NAME = "Boiko Borisov",
                    user = User,
                    UserId = User.Id,
                    PN = "+359887123456"

                };
                await ContextManager.Client.AddAsync(ClientNow);

            }
        }
        private async Task SecondaryUpdates(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<AccountUser>>();
            var ContextManager = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var _user = await UserManager.FindByEmailAsync("admin@email.com");

            var MainAcc = ContextManager.Employee.Where(v => v.UserId == _user.Id).FirstOrDefault();
            if (MainAcc == null || MainAcc.UserId != _user.Id)
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


                var ClientNow = await ContextManager.Client.Where(v => v.EGN == "8706065400").FirstAsync();
                var CurrCur = await ContextManager.Currency.Where(v => v.ID == 0).FirstAsync();
                var CurrAccType = await ContextManager.AccountType.Where(v => v.ID == 0).FirstAsync();
                var BankAccEx = new BankAccount
                {
                    CurrencyId = 0,
                    currency = CurrCur,
                    BALANCE = 2000,
                    AccTypeId = 0,
                    acctype = CurrAccType,
                    ClientId = ClientNow.ID,
                    client = ClientNow

                };
                ContextManager.BankAccount.Add(BankAccEx);
                ContextManager.SaveChanges();
            }
        }

        }
       
}