using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicSupplyTracker.Data;
using ClinicSupplyTracker.Models;

namespace ClinicSupplyTracker.Pages;

public class DeleteModel : PageModel
{
    private readonly ClinicContext _context;

    public DeleteModel(ClinicContext context)
    {
        _context = context;
    }

    public SupplyRequest RequestItem { get; set; }
        = new SupplyRequest();

    public IActionResult OnGet(int id)
    {
        SupplyRequest? request = _context.SupplyRequests.Find(id);

        if (request == null)
        {
            return NotFound();
        }

        RequestItem = request;
        return Page();
    }

    public IActionResult OnPost(int id)
    {
        SupplyRequest? request = _context.SupplyRequests.Find(id);

        if (request == null)
        {
            return NotFound();
        }

        _context.SupplyRequests.Remove(request);
        _context.SaveChanges();

        return RedirectToPage("/Index");
    }
}