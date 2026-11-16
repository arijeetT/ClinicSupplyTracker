using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicSupplyTracker.Data;
using ClinicSupplyTracker.Models;

namespace ClinicSupplyTracker.Pages;

public class EditModel : PageModel
{
    private readonly ClinicContext _context;

    public EditModel(ClinicContext context)
    {
        _context = context;
    }

    [BindProperty]
    public SupplyRequest EditedRequest { get; set; }
        = new SupplyRequest();

    public IActionResult OnGet(int id)
    {
        SupplyRequest? request = _context.SupplyRequests.Find(id);

        if (request == null)
        {
            return NotFound();
        }

        EditedRequest = request;
        return Page();
    }

    public IActionResult OnPost(int id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        SupplyRequest? request = _context.SupplyRequests.Find(id);

        if (request == null)
        {
            return NotFound();
        }

        request.ItemName = EditedRequest.ItemName;
        request.Quantity = EditedRequest.Quantity;
        request.Department = EditedRequest.Department;

        _context.SaveChanges();

        return RedirectToPage("/Index");
    }
}