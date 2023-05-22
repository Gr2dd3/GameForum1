using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameForum1.Data;
using GameForum1.Models.DbModels;

namespace GameForum1.Pages.Admin.SubCategoryAdmin
{
    public class EditModel : PageModel
    {
        /// <summary>
        /// TODO: EDIT FUnkar ej, rätt id kommer in men det sparas ej mot API
        /// </summary>
        private readonly GameForum1.Data.GameForum1Context _context;

        public EditModel(GameForum1.Data.GameForum1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public SubCategory SubCategory { get; set; }
        public string WarningForNonExistingDbSubCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SubCategories == null)
            {
                return NotFound();
            }

            SubCategory = await DAL.SubCategoryManager.GetOneSubCategory((int)id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || SubCategory is not null)
            {
                var existingDbSubCategory = _context.SubCategories.Where(x => x.Id == SubCategory.Id).FirstOrDefault();
                if (existingDbSubCategory is not null)
                {
                    await DAL.SubCategoryManager.UpdateSubCategory(SubCategory);
                    //_context.Update(existingDbSubCategory);
                    _context.Attach(existingDbSubCategory).State = EntityState.Modified;
                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (Exception) { }
                }
                else
                {
                    WarningForNonExistingDbSubCategory = "The SubCategory does not exist! Try adding one instead";
                }
                return Page();
            }


            //_context.Attach(DbSubCategory).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!DbSubCategoryExists(DbSubCategory.Id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            return RedirectToPage("./Index");
        }

        private bool DbSubCategoryExists(int id)
        {
            return (_context.SubCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
