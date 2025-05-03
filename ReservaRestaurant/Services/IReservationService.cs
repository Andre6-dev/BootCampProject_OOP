using ReservaRestaurant.Models;

namespace ReservaRestaurant.Services;

public interface IReservationService
{
    bool CreateReservation(Reservation reservation);
    bool UpdateReservation(Reservation reservation);
    bool CancelReservation(Reservation reservation);
    List<Reservation> GetReservationsByDate(DateTime date);
    Reservation GetReservationById(int id);
}