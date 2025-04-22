using System.Text.Json;

namespace TicketProject;

public class JsonTicketExporter : ITicketExporter
{
    public string Export(List<Ticket> tickets)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
            
        // Create simplified objects for export to avoid circular references
        var exportTickets = tickets.Select(t => new
        {
            t.Id,
            t.Title,
            t.Description,
            Status = t.Status.ToString(),
            Priority = t.Priority.ToString(),
            Category = t.Category.ToString(),
            t.ReportedBy,
            t.CreatedDate,
            t.LastUpdated,
            AssignedTo = t.AssignedTo?.Name,
            Comments = t.Comments.Select(c => new
            {
                c.Author,
                c.Text,
                c.Timestamp
            }).ToList()
        }).ToList();
            
        return JsonSerializer.Serialize(exportTickets, options);
    }
}