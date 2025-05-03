using ReservaRestaurant.Models;

namespace ReservaRestaurant.Repositories;

public class ReservationRepository : IReservationRepository
{
    private List<Reservation> _reservations = new List<Reservation>();
    private int _nextId = 1;
    
    public List<Reservation> GetAll()
    {
        return _reservations;
    }

    public Reservation GetById(int id)
    {
        return _reservations.Find(x => x.Id == id);
    }

    public void Add(Reservation entity)
    {
        entity.Id = _nextId++;
        _reservations.Add(entity);
    }

    public void Update(Reservation entity)
    {
        var index = _reservations.FindIndex(x => x.Id == entity.Id);

        if (index != -1)
        {
            _reservations[index] = entity;
        }
    }

    public void Delete(Reservation entity)
    {
        _reservations.Remove(entity);
    }

    public List<Reservation> GetReservationsByDate(DateTime date)
    {
        return _reservations.Where(x => x.Date == date).ToList();
    }

    public List<Reservation> GetReservationsByCustomer(int customerId)
    {
        return _reservations.Where(x => x.CustomerId == customerId).ToList();
    }
}