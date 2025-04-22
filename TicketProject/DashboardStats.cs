namespace TicketProject;

public class DashboardStats
{
    public int TotalTickets { get; set; }
    public int OpenTickets { get; set; }
    public int InProgressTickets { get; set; }
    public int ResolvedTickets { get; set; }
    public int ClosedTickets { get; set; }
    public Dictionary<Priority, int> TicketsByPriority { get; set; } = new Dictionary<Priority, int>();
    public Dictionary<TicketCategory, int> TicketsByCategory { get; set; } = new Dictionary<TicketCategory, int>();
    public List<Ticket> RecentTickets { get; set; } = new List<Ticket>();
}