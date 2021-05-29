﻿using System;
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
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AccountUser> _userManager;

        public ClientController(ApplicationDbContext context, UserManager<AccountUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Client
        public async Task<IActionResult> Index()
        {
            var user = _userManager.GetUserAsync(User).Result;

            var currentClient = _context.Client.Where(p => p.UserId == user.Id).FirstOrDefault();

            var clientBankAcc = _context.BankAccount.Where(c => c.ClientId == currentClient.ID).ToList();

            List<BankAccount> legitBankAccounts = new List<BankAccount>();

            foreach (BankAccount item in clientBankAcc)
            {
                var temp = item;
                temp.currency = _context.Currency.Where(v => v.ID == item.CurrencyId).FirstOrDefault();
                temp.acctype = _context.AccountType.Where(v => v.ID == item.AccTypeId).FirstOrDefault();
                legitBankAccounts.Add(temp);
           
            }

            return View(legitBankAccounts);
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(c => c.user)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "ID", "ID");
            ViewData["AccTypeId"] = new SelectList(_context.AccountType, "ID", "ID");
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EGN,NAME,address,PN,UserId")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client); //zapisva v bazata danni
                await _context.SaveChangesAsync(); //zapisva v context
                //return RedirectToAction(nameof(Index)); //otiva v stranica index na klienta
                return RedirectToAction("CreateBankAcc", "Banker", new { clientID = client.ID });
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", client.UserId);
            return View(client);// vrushta sushtata stranica, no s id-tata na klientite zaredeni veche kato spisak
            //tova e return-a pri error
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", client.UserId);
            return View(client);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EGN,NAME,address,PN,UserId")] Client client)
        {
            if (id != client.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ID))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", client.UserId);
            return View(client);
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(c => c.user)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Client.FindAsync(id);
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.ID == id);
        }
    }
}
