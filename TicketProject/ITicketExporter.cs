namespace TicketProject;

public interface ITicketExporter
{
    string Export(List<Ticket> tickets);
}