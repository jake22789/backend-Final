using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.DTOs;

public class CreatePurchaseOrderDTO
{
    [Required(ErrorMessage = "Date ordered is required")]
    public DateTime DateOrdered { get; set; }
}
