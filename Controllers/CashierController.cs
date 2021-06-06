using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bank.Data;
using Bank.Models;
using Bank.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Bank.Controllers
{
    public class CashierController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AccountUser> _userManager;

        public CashierController(ApplicationDbContext context, UserManager<AccountUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Cashier
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult ViewAll(string bankAccIDSpecial)
        {
            if (bankAccIDSpecial != null)
            { 
                ViewData["bankAccIDSpecial"] = bankAccIDSpecial; 
            }
            else
            {
                ViewData["BankAccId"] = new SelectList(_context.BankAccount, "ID", "ID");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ViewAll(string bankAccID, DateTime? transTimeFrom, DateTime? transTimeTo)
        {

            if (bankAccID != null)
            {
                IQueryable<Transaction> applicationDbContext;
                if (transTimeFrom != null) {
                    if (transTimeTo != null) {
                        applicationDbContext = _context.Transaction.Where(v => v.BankAccId == int.Parse(bankAccID) && v.DATE >= transTimeFrom && v.DATE <= transTimeTo);
                        return View(await applicationDbContext.ToListAsync());
                    }
                    applicationDbContext = _context.Transaction.Where(v => v.BankAccId == int.Parse(bankAccID) && v.DATE >= transTimeFrom );
                    return View(await applicationDbContext.ToListAsync());
                }

                applicationDbContext = _context.Transaction.Where(v => v.BankAccId == int.Parse(bankAccID));
                return View(await applicationDbContext.ToListAsync());
            }
            else {
                ViewData["error"] = "Some of the fields are empty!";
                ViewData["BankAccId"] = new SelectList(_context.BankAccount, "ID", "ID");
                return View();
            }
           
        }

        // GET: Cashier/Create
        public IActionResult Create()
        {
            var user = _userManager.GetUserAsync(User).Result;
            ViewData["EmployeeId"] = _context.Employee.Where(v => v.UserId == user.Id).FirstOrDefault().ID;
            ViewData["BankAccId"] = new SelectList(_context.BankAccount, "ID", "ID");
            ViewData["TransCatId"] = new SelectList(_context.TransactionCategory, "ID", "NAME");
            return View();
        }

        // POST: Cashier/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransCatId,SUM,EmployeeId,BankAccId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                DateTime dt = DateTime.Now;
                transaction.bankAccount = _context.BankAccount.Where(v => v.ID == transaction.BankAccId).FirstOrDefault();
                transaction.employee = _context.Employee.Where(v => v.ID == transaction.EmployeeId).FirstOrDefault();
                transaction.transactionCategory = _context.TransactionCategory.Where(v => v.ID == transaction.TransCatId).FirstOrDefault();
                transaction.DATE = dt;
                _context.Add(transaction);
                float sum = transaction.SUM * transaction.transactionCategory.COEF;//_context.TransactionCategory.Where(v => v.ID == transaction.TransCatId).FirstOrDefault().COEF;
                _context.BankAccount.Where(v => v.ID == transaction.BankAccId).FirstOrDefault().BALANCE += sum;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewAll));
            }
            var user = _userManager.GetUserAsync(User).Result;
            ViewData["EmployeeId"] = _context.Employee.Where(v => v.UserId == user.Id).FirstOrDefault().ID;
            ViewData["BankAccId"] = new SelectList(_context.BankAccount, "ID", "ID", transaction.EmployeeId);
            ViewData["TransCatId"] = new SelectList(_context.TransactionCategory, "ID", "NAME");
            return View(transaction);
        }

    }
}
