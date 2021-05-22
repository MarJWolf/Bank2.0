using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bank.Data;
using Bank.Models;

namespace Bank.Controllers
{
    public class CashierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CashierController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cashier
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult ViewAll()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "ID", "ID");
            ViewData["BankAccId"] = new SelectList(_context.BankAccount, "ID", "ID");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ViewAll(string empID, string bankAccID, DateTime transTime)
        {
            if (empID != null && bankAccID != null && transTime != null)
            {
                //pay attention to this lil hoe better tomm 
                var applicationDbContext = _context.Transaction.Where(v => v.EmployeeId == int.Parse(empID) && v.BankAccId == int.Parse(bankAccID) && v.DATE == transTime);
                return View(await applicationDbContext.ToListAsync());
            }
            else {
                return View();
            }
           
        }

        // GET: Cashier/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .Include(t => t.employee)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Cashier/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "ID", "ID");
            return View();
        }

        // POST: Cashier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TransCatId,SUM,DATE,EmployeeId,BankAccId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "ID", "ID", transaction.EmployeeId);
            return View(transaction);
        }

        // GET: Cashier/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "ID", "ID", transaction.EmployeeId);
            return View(transaction);
        }

        // POST: Cashier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TransCatId,SUM,DATE,EmployeeId,BankAccId")] Transaction transaction)
        {
            if (id != transaction.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.ID))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "ID", "ID", transaction.EmployeeId);
            return View(transaction);
        }

        // GET: Cashier/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .Include(t => t.employee)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Cashier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transaction.FindAsync(id);
            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transaction.Any(e => e.ID == id);
        }
    }
}
