using ReservaRestaurant.Models;

namespace ReservaRestaurant.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private List<Customer> _customers = new List<Customer>();
    private int _nextId = 1;
    
    public List<Customer> GetAll()
    {
        return _customers;
    }

    public Customer GetById(int id)
    {
        return _customers.FirstOrDefault(x => x.Id == id);
    }

    public void Add(Customer entity)
    {
        entity.Id = _nextId++;
        _customers.Add(entity);
    }

    public void Update(Customer entity)
    {
        var index = _customers.FindIndex(x => x.Id == entity.Id);

        if (index != -1)
        {
            _customers[index] = entity;
        }
    }

    public void Delete(Customer entity)
    {
        _customers.Remove(entity);
    }

    public Customer GetByPhoneNumber(string phoneNumber)
    {
        return _customers.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
    }

    public Customer GetByEmail(string email)
    {
        return _customers.FirstOrDefault(x => x.Email == email);
    }
}