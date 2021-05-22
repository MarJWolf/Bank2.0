using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bank.Data;
using Bank.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Bank.Areas.Identity.Data;

namespace Bank.Controllers
{
    [Authorize(Policy = "Banker")]
    public class BankerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AccountUser> _userManager;

        public BankerController(ApplicationDbContext context, UserManager<AccountUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Banker
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Employee.Include(e => e.user);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Banker/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vUser = await _userManager.FindByIdAsync(id);

            if (vUser == null)
            {
                return NotFound();
            }
            string role =  _userManager.GetRolesAsync(vUser).Result.First();

            switch (role){

                case "Client":
                    int clientId = _context.Client.Where(v => v.UserId == id).FirstOrDefault().ID;
                    return RedirectToAction("Details", "Client", new { Id =  clientId});
                case "Banker":
                case "Cashier":
                default:
                    var employee = _context.Employee.Where(v => v.UserId == id).FirstOrDefault();
                    return View(employee);
            }
        }


        //a second one 
        public async Task<IActionResult> ViewAll()
        {
            var UserClients = await _userManager.GetUsersInRoleAsync("Client");
            List<Client> clients = new List<Client>();
            foreach (AccountUser item in UserClients) {
                var temp = _context.Client.Where(v => v.UserId == item.Id).FirstOrDefault();
                temp.user = item;
                clients.Add(temp);
            }
            ViewData["AllClients"] = clients;
            var UserBankers = await _userManager.GetUsersInRoleAsync("Banker");
            List<Employee> bankers = new List<Employee>();
            foreach (AccountUser item in UserBankers)
            {
                var temp = _context.Employee.Where(v => v.UserId == item.Id).FirstOrDefault();
                temp.user = item;
                bankers.Add(temp);
            }
            ViewData["AllBankers"] = bankers;
            var UserCashiers = await _userManager.GetUsersInRoleAsync("Cashier");
            List<Employee> cashiers = new List<Employee>();
            foreach (AccountUser item in UserCashiers)
            {
                var temp = _context.Employee.Where(v => v.UserId == item.Id).FirstOrDefault();
                temp.user = item;
                cashiers.Add(temp);
            }
            ViewData["AllCashiers"] = cashiers;
            return View();
        }

        // GET: Banker/Create
        public IActionResult Create(string id)
        {
            ViewData["UserId"] = id;
            return View();
        }

        // POST: Banker/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NAME,PN,UserId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", employee.UserId);
            return View(employee);
        }
        
        public async Task<IActionResult> CreateBankAcc([Bind("INTEREST,BALANCE,CurrencyId,ClientId,AccTypeId")] BankAccount BA)
        {
            if (ModelState.IsValid)
            {
                _context.Add(BA);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(BA);
        }

        // GET: Banker/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", employee.UserId);
            return View(employee);
        }

        // POST: Banker/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NAME,PN,UserId")] Employee employee)
        {
            if (id != employee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", employee.UserId);
            return View(employee);
        }

        // GET: Banker/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var employee =  _context.Employee.Where(v => v.ID == id).FirstOrDefault();
            if (id == null || employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Banker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            var user = await _userManager.FindByIdAsync(employee.UserId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.ID == id);
        }
    }
}
