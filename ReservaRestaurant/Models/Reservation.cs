namespace ReservaRestaurant.Models;

public class Reservation
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int TableId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public int PartySize { get; set; }
    public string SpecialRequests { get; set; }
    public ReservationStatus Status { get; set; }
    
    public override string ToString()
    {
        return $"Reservation #{Id}: Date: {Date.ToShortDateString()}, Time: {Time}, Party Size: {PartySize}, Status: {Status}";
    }
}