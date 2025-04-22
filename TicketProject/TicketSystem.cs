namespace TicketProject;

public class TicketSystem
{
    // De la base datos
    private readonly List<Ticket> _tickets = new List<Ticket>();
    private readonly List<TeamMember> _teamMembers = new List<TeamMember>();

    // Ticket Management
    public void AddTicket(Ticket ticket)
    {
        _tickets.Add(ticket);
    }

    public List<Ticket> GetAllTickets()
    {
        return _tickets.OrderByDescending(t => t.CreatedDate).ToList();
    }

    public Ticket? GetTicketById(int id)
    {
        return _tickets.FirstOrDefault(t => t.Id == id);
    }

    public void UpdateTicketStatus(int ticketId, TicketStatus newStatus)
    {
        var ticket = GetTicketById(ticketId);
        if (ticket != null)
        {
            ticket.UpdateStatus(newStatus);
        }
        else
        {
            throw new KeyNotFoundException($"Ticket with ID {ticketId} not found");
        }
    }

    public void AssignTicket(int ticketId, TeamMember assignee)
    {
        var ticket = GetTicketById(ticketId);
        if (ticket != null)
        {
            ticket.AssignTo(assignee);
            if (!assignee.AssignedTickets.Contains(ticket)) //
            {
                assignee.AssignedTickets.Add(ticket);
            }
        }
        else
        {
            throw new KeyNotFoundException($"Ticket with ID {ticketId} not found");
        }
    }

    public void AddCommentToTicket(int ticketId, string author, string text)
    {
        var ticket = GetTicketById(ticketId);
        if (ticket != null)
        {
            var comment = new Comment(author, text);
            ticket.AddComment(comment);
        }
        else
        {
            throw new KeyNotFoundException($"Ticket with ID {ticketId} not found");
        }
    }

    // Team Management
    public void AddTeamMember(TeamMember member)
    {
        _teamMembers.Add(member);
    }

    public List<TeamMember> GetTeamMembers()
    {
        return _teamMembers;
    }

    // Search and Filtering
    public List<Ticket> SearchTickets(string keyword)
    {
        keyword = keyword.ToLower();
        return _tickets.Where(t =>
            t.Title.ToLower().Contains(keyword) || t.Description.ToLower().Contains(keyword)).ToList();
    }

    public List<Ticket> GetTicketsByStatus(TicketStatus status)
    {
        return _tickets.Where(t => t.Status == status).ToList();
    }

    public List<Ticket> GetTicketsByPriority(Priority priority)
    {
        return _tickets.Where(t => t.Priority == priority).ToList();
    }

    public List<Ticket> GetTicketsByAssignee(string assigneeName)
    {
        return _tickets.Where(t =>
            t.AssignedTo != null &&
            t.AssignedTo.Name.ToLower().Contains(assigneeName.ToLower())).ToList();
    }

    // Dashboard and Analytics
    public DashboardStats GetDashboardStats()
    {
        var stats = new DashboardStats
        {
            TotalTickets = _tickets.Count,
            OpenTickets = _tickets.Count(t => t.Status == TicketStatus.Open),
            InProgressTickets = _tickets.Count(t => t.Status == TicketStatus.InProgress),
            ResolvedTickets = _tickets.Count(t => t.Status == TicketStatus.Resolved),
            ClosedTickets = _tickets.Count(t => t.Status == TicketStatus.Closed),
            RecentTickets = _tickets.OrderByDescending(t => t.CreatedDate).Take(5).ToList()
        };

        // Count tickets by priority
        foreach (Priority priority in Enum.GetValues<Priority>())
        {
            stats.TicketsByPriority[priority] = _tickets.Count(t => t.Priority == priority);
        }

        // Count tickets by category
        foreach (TicketCategory category in Enum.GetValues<TicketCategory>())
        {
            stats.TicketsByCategory[category] = _tickets.Count(t => t.Category == category);
        }

        return stats;
    }

    // Export
    public string ExportTickets(ITicketExporter exporter)
    {
        return exporter.Export(_tickets);
    }
}