namespace final_proj.Models;

public class OrderWorkflowState
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string Status { get; set; } = "CREATED";

    public DateTime UpdatedAt { get; set; }

    public CustomerOrder? Order { get; set; }
}
