using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoBookStore.Data;
using DemoBookStore.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Cryptography;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace DemoBookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly DemoBookStoreContext _context;

        public AuthorController(DemoBookStoreContext context)
        {
            _context = context;
        }

        // GET: Author
        public async Task<IActionResult> Index()
        {
            return View(await _context.AuthorModel.ToListAsync());
        }

        // GET: Author/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorModel = await _context.AuthorModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorModel == null)
            {
                return NotFound();
            }

            return View(authorModel);
        }

        // GET: Author/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AverageScore,Id,Firstname,Lastname,Email,Password")] AuthorModel authorModel)
        {
            authorModel.Password = ProceedData(authorModel.Password);

            if (ModelState.IsValid && !CheckEmail(authorModel.Email))
            {
                _context.Add(authorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(authorModel);
        }

        private string ProceedData(string password)
        {
            // hash password
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashbytes = sha256.ComputeHash(inputBytes);

                string hashedPassword = Convert.ToHexString(hashbytes);
                password = hashedPassword;
            }

            return password;
        }

        private bool CheckEmail(string email)
        {
            List<AuthorModel> Authors = _context.AuthorModel.ToListAsync().Result;

            foreach (AuthorModel author in Authors)
            {
                if (author.Email == email)
                {
                    return true;
                }
            }
            return false;
        }

        // GET: Author/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorModel = await _context.AuthorModel.FindAsync(id);
            if (authorModel == null)
            {
                return NotFound();
            }
            return View(authorModel);
        }

        // POST: Author/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AverageScore,Id,Firstname,Lastname,Email,Password")] AuthorModel authorModel)
        {
            if (id != authorModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorModelExists(authorModel.Id))
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
            return View(authorModel);
        }

        // GET: Author/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorModel = await _context.AuthorModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorModel == null)
            {
                return NotFound();
            }

            return View(authorModel);
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorModel = await _context.AuthorModel.FindAsync(id);
            if (authorModel != null)
            {
                _context.AuthorModel.Remove(authorModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorModelExists(int id)
        {
            return _context.AuthorModel.Any(e => e.Id == id);
        }
    }
}
