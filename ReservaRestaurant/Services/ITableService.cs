using ReservaRestaurant.Models;

namespace ReservaRestaurant.Services;

public interface ITableService
{
    List<Table> GetAvailableTables(DateTime date, int partySize);
    Table GetTableById(int id);
    List<Table> GetAllTables();
}