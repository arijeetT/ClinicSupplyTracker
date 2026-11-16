using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicSupplyTracker.Data;
using ClinicSupplyTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSupplyTracker.Pages;

public class IndexModel : PageModel
{
    private readonly ClinicContext _context;

    public IndexModel(ClinicContext context)
    {
        _context = context;
    }

    public List<SupplyRequest> Requests { get; set; }
        = new List<SupplyRequest>();

    public void OnGet(string? status)
    {
        var query = _context.SupplyRequests.AsQueryable();

        if (status == "pending")
        {
            query = query.Where(request => !request.IsFulfilled);
        }
        else if (status == "fulfilled")
        {
            query = query.Where(request => request.IsFulfilled);
        }

        Requests = query
            .OrderBy(request => request.Id)
            .ToList();
    }

    public IActionResult OnPostFulfill(int id)
    {
        SupplyRequest? request = _context.SupplyRequests.Find(id);

        if (request == null)
        {
            return NotFound();
        }

        request.IsFulfilled = true;
        _context.SaveChanges();

        return RedirectToPage("/Index");
    }
}