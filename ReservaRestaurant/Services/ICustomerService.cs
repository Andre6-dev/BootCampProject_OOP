using ReservaRestaurant.Models;

namespace ReservaRestaurant.Services;

public interface ICustomerService
{
    Customer GetOrCreateCustomer(string name, string phone, string email);
    Customer GetCustomerById(int id);
    List<Customer> GetCustomers();
    Customer GetCustomerByEmail(string email);
    List<Reservation> GetCustomerReservations(int customerId);
}