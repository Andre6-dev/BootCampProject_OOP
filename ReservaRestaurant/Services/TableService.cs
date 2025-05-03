using ReservaRestaurant.Models;
using ReservaRestaurant.Repositories;

namespace ReservaRestaurant.Services;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;

    public TableService(ITableRepository tableRepository)
    {
        this._tableRepository = tableRepository;
    }

    public List<Table> GetAvailableTables(DateTime date, int partySize)
    {
        return _tableRepository.GetAvailableTables(date, partySize);
    }

    public Table GetTableById(int id)
    {
        return _tableRepository.GetById(id);
    }

    public List<Table> GetAllTables()
    {
        return _tableRepository.GetAll();
    }
}