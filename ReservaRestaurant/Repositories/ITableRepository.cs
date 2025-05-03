using ReservaRestaurant.Models;

namespace ReservaRestaurant.Repositories;

public interface ITableRepository : IRepository<Table>
{
    List<Table> GetAvailableTables(DateTime date, int partySize);
}