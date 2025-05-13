using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ceng382_25_26_202011037.Models;
using Ceng382_25_26_202011037.Helpers;

namespace Ceng382_25_26_202011037.Pages
{
    public class IndexModel : PageModel
    {
        private const int PageSize = 10;
        private static readonly List<ClassInformationModel> SeedData =
            Enumerable.Range(1, 100)
                      .Select(i => new ClassInformationModel {
                          Id = i,
                          ClassName = $"Sınıf {i}",
                          StudentCount = i * 5,
                          Description = $"Açıklama {i}"
                      }).ToList();

        // Render için
        public string? SearchTerm { get; private set; }
        public int Page         { get; private set; }
        public string[] SelectedColumns { get; private set; } = Array.Empty<string>();
        public List<ClassInformationModel> Items { get; private set; } = new();
        public int TotalPages   { get; private set; }

        // Parametresiz OnGet, tüm değerleri Query’den okuyoruz
        public IActionResult OnGet()
        {
            // 1) Login kontrol
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
                return RedirectToPage("/Login");

            var q = HttpContext.Request.Query;
            SearchTerm = q["searchTerm"].FirstOrDefault();
            if (!int.TryParse(q["page"].FirstOrDefault(), out var pg) || pg < 1)
                pg = 1;
            Page = pg;
            SelectedColumns = q["selectedColumns"].ToArray();

            // 2) Filtre
            var query = SeedData.AsQueryable();
            if (!string.IsNullOrWhiteSpace(SearchTerm))
                query = query.Where(x =>
                    !string.IsNullOrEmpty(x.ClassName) &&
                    x.ClassName.Contains(SearchTerm!, StringComparison.OrdinalIgnoreCase));

            // 3) Sayfalama hesapla
            var totalItems = query.Count();
            TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
            if (TotalPages < 1) TotalPages = 1;
            if (Page > TotalPages) Page = TotalPages;

            // 4) Veriyi getir
            Items = query
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return Page();
        }

        public IActionResult OnGetExportJson(bool filtered, string[]? selectedColumns)
        {
            var data = filtered ? Items : SeedData;
            var json = Utils.Instance.ExportToJson(data, selectedColumns);
            return File(
              System.Text.Encoding.UTF8.GetBytes(json),
              "application/json", "export.json"
            );
        }
    }
}
