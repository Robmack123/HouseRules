namespace HouseRules.Models.DTOs;

public class ChoreCompletionDTO
{
    public int Id { get; set; }
    public int UserProfileId { get; set; }
    public int ChoreId { get; set; }
    public DateTime CompletedOn { get; set; }

    // Related data
    public string UserName { get; set; } // Optional: Include user name for clarity
    public string ChoreName { get; set; } // Optional: Include chore name for clarity
}
