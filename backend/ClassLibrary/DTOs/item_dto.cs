namespace final_proj.DTO;
using System.ComponentModel.DataAnnotations;
public class CreateItemDto
{

    [Required]
    public string Name { get; set; } =null!;

    [Range(0,int.MaxValue)]
    public int Price { get; set; }

    [Range(0,double.MaxValue)]
    public decimal? Volume { get; set; }

}