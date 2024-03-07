using System.Collections.Generic;
using CalculationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace TaxLabProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SalaryCalculateService _calculateService;

        public IndexModel(SalaryCalculateService calculateService)
        {
            _calculateService = calculateService;
        }

        [BindProperty]
        public decimal Salary { get; set; }

        [BindProperty]
        public required List<TaxRateBand> TaxRateBands { get; set; }

        [BindProperty]
        public decimal TotalTaxCollected { get; set; }



        public void OnGet()
        {

            PopulateTaxRateBands();

            // Calculate tax collected for each band and total tax collected
            _calculateService.CalculateTaxForBands(TaxRateBands, Salary);
            TotalTaxCollected = _calculateService.CalculateTotalTaxCollected(TaxRateBands);


        }
        public IActionResult OnPost()
        {
            TaxRateBands = _calculateService.GetInitialRateBand();
            // Calculate tax collected for each band and total tax collected when salary changes
            _calculateService.CalculateTaxForBands(TaxRateBands, Salary);
            TotalTaxCollected = _calculateService.CalculateTotalTaxCollected(TaxRateBands);
            return Page();
        }

        private void PopulateTaxRateBands()
        {
            // Initialize tax rate bands with initial values
            TaxRateBands = _calculateService.GetInitialRateBand();
        }
    }
}
