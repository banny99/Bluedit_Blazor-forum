namespace Entities.Models;

public class Vote
{
    private User Voter { get; set; }

    public Vote(User voter)
    {
        Voter = voter;
    }
}