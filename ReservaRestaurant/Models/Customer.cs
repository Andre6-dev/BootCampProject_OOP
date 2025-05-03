namespace ReservaRestaurant.Models;

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public bool IsVIP { get; set; }
    public List<Reservation> ReservationHistory { get; set; } = new List<Reservation>();

    public override string ToString()
    {
        return $"Customer: {FullName}, Phone: {PhoneNumber}, VIP: {IsVIP}";
    }
}