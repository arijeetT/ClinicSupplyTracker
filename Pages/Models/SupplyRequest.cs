using System.ComponentModel.DataAnnotations;

namespace ClinicSupplyTracker.Models;

public class SupplyRequest
{
    public int Id { get; set; }

    [Required]
    public string ItemName { get; set; } = "";

    [Range(1, 1000)]
    public int Quantity { get; set; }

    [Required]
    public string Department { get; set; } = "";

    public bool IsFulfilled { get; set; }
}