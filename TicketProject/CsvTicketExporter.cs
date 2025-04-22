namespace TicketProject;

public class CsvTicketExporter : ITicketExporter
{
    public string Export(List<Ticket> tickets)
    {
        // Create CSV header
        var csv = "ID,Title,Description,Status,Priority,Category,ReportedBy,CreatedDate,AssignedTo\n";
            
        // Add each ticket as a CSV row
        foreach (var ticket in tickets)
        {
            string assignedTo = ticket.AssignedTo != null ? ticket.AssignedTo.Name : "";
                
            // Clean up fields for CSV format (escape commas, quotes, etc.)
            string title = EscapeCsvField(ticket.Title);
            string description = EscapeCsvField(ticket.Description);
            string reportedBy = EscapeCsvField(ticket.ReportedBy);
                
            csv += $"{ticket.Id},\"{title}\",\"{description}\",{ticket.Status},{ticket.Priority},{ticket.Category},\"{reportedBy}\",{ticket.CreatedDate:yyyy-MM-dd},\"{assignedTo}\"\n";
        }
            
        return csv;
    }
        
    private string EscapeCsvField(string field)
    {
        // Replace quotes with double quotes (CSV standard)
        return field.Replace("\"", "\"\"");
    }
}