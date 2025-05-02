using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ceng382_25_26_202011037.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ceng382_25_26_202011037.Pages
{
    public class IndexModel : PageModel
    {
        private static List<ClassInformationModel> _classList = new();
        private static int _nextId = 1;

        [BindProperty]
        public ClassInformationModel Input { get; set; } = new ClassInformationModel();

        public List<ClassInformationModel> ClassList => _classList;

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
                return RedirectToPage("/Login");
            Input ??= new ClassInformationModel();
            return Page();
        }

        public IActionResult OnGetEdit(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
                return RedirectToPage("/Login");

            var item = _classList.FirstOrDefault(x => x.Id == id);
            if (item != null)
                Input = new ClassInformationModel
                {
                    Id           = item.Id,
                    ClassName    = item.ClassName,
                    StudentCount = item.StudentCount,
                    Description  = item.Description
                };

            return Page();
        }

        public IActionResult OnPostAdd()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
                return RedirectToPage("/Login");
            if (!ModelState.IsValid)
                return Page();

            Input.Id = _nextId++;
            _classList.Add(Input);
            return RedirectToPage();
        }

        public IActionResult OnPostEdit()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
                return RedirectToPage("/Login");
            if (!ModelState.IsValid)
                return Page();

            var item = _classList.First(x => x.Id == Input.Id);
            item.ClassName    = Input.ClassName;
            item.StudentCount = Input.StudentCount;
            item.Description  = Input.Description;

            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
                return RedirectToPage("/Login");

            var item = _classList.FirstOrDefault(x => x.Id == id);
            if (item != null) _classList.Remove(item);
            return RedirectToPage();
        }

        public IActionResult OnPostClear()
        {
            return RedirectToPage();
        }
    }
}
