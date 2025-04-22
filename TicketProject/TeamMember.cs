namespace TicketProject;

public class TeamMember
{
    private static int _nextId = 1;
        
    public int Id { get; private set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public List<Ticket> AssignedTickets { get; private set; } = new List<Ticket>(); // Lista
        
    public TeamMember()
    {
        Id = _nextId++;
    }
}