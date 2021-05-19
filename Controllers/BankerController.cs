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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.user)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }


        //a second one 
        public async Task<IActionResult> ViewAll()
        {
            //var applicationDbContext = _context.Employee.Include(e => e.user);
           // _userManager.IsInRoleAsync("Client");
            var Clients = await _userManager.GetUsersInRoleAsync("Client");
            ViewData["AllClients"] = Clients.ToList();
            var Bankers = await _userManager.GetUsersInRoleAsync("Banker");
            ViewData["AllBankers"] = Bankers.ToList();
            var Cashiers = await _userManager.GetUsersInRoleAsync("Cashier");
            ViewData["AllCashiers"] = Cashiers.ToList();
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
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.user)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
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
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.ID == id);
        }
    }
}
