namespace TicketProject;

public class Ticket
{
    private static int _nextId = 1;
        
    public int Id { get; private set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TicketStatus Status { get; set; } = TicketStatus.Open;
    public Priority Priority { get; set; }
    public TicketCategory Category { get; set; }
    public string ReportedBy { get; set; }
    public DateTime CreatedDate { get; private set; } = DateTime.Now;
    public DateTime? LastUpdated { get; set; }
    public TeamMember? AssignedTo { get; set; }
    public List<Comment> Comments { get; private set; } = new List<Comment>();
        
    public Ticket()
    {
        Id = _nextId++;
    }
        
    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
        LastUpdated = DateTime.Now;
    }
        
    public void UpdateStatus(TicketStatus newStatus)
    {
        Status = newStatus;
        LastUpdated = DateTime.Now;
    }
        
    public void AssignTo(TeamMember member)
    {
        AssignedTo = member;
        LastUpdated = DateTime.Now;
    }
}