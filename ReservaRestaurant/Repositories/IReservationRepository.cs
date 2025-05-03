using ReservaRestaurant.Models;

namespace ReservaRestaurant.Repositories;

public interface IReservationRepository : IRepository<Reservation>
{
    List<Reservation> GetReservationsByDate(DateTime date);
    List<Reservation> GetReservationsByCustomer(int customerId);
}