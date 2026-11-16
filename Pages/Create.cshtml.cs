using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicSupplyTracker.Data;
using ClinicSupplyTracker.Models;

namespace ClinicSupplyTracker.Pages;

public class CreateModel : PageModel
{
    private readonly ClinicContext _context;

    public CreateModel(ClinicContext context)
    {
        _context = context;
    }

    [BindProperty]
    public SupplyRequest NewRequest { get; set; }
        = new SupplyRequest();

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        NewRequest.Id = 0;
        NewRequest.IsFulfilled = false;

        _context.SupplyRequests.Add(NewRequest);
        _context.SaveChanges();

        return RedirectToPage("/Index");
    }
}