using ReservaRestaurant.Models;

namespace ReservaRestaurant.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    Customer GetByPhoneNumber(string phoneNumber);
    Customer GetByEmail(string email);
}