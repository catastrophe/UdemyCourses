﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {

        public ApplicationDbContext _db;
        [TempData]
        public string Message { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]

        public Book Book { get; set; }
        public async Task OnGet(int id)
        {
            Book =await _db.Book.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var BookFromDb = await _db.Book.FindAsync(Book.Id);
                BookFromDb.Name = Book.Name;
                BookFromDb.Author = Book.Author;
                BookFromDb.ISBN = Book.ISBN;

                await _db.SaveChangesAsync();
                Message = "Book has been updated successfully";
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage();
            }

            
            

        }
    }
}