using DataInsights.API.Data;
using DataInsights.API.Models;
using DataInsights.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataInsights.API.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SQLDatabase _context;
        //private readonly PostgreSQLDb _postgreSQL;
        public CustomerRepository( SQLDatabase context) //PostgreSQLDb postgreSQL)
        {
            _context = context;
            //_postgreSQL = postgreSQL;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
        {
            var sqlProducts =  await _context.Customers.ToListAsync();
            //var pgProducts = await _postgreSQL.Customers.ToListAsync();
            return sqlProducts;//.Concat(pgProducts).ToList();
        }

        public async Task<CustomerDTO?> GetCustomerByIdAsync(int id)
        {
            var sqlID =  await _context.Customers.FindAsync(id);
            //var pgID = await _postgreSQL.Customers.FindAsync(id);

            return sqlID; //?? pgID;
        }   

        public async Task<CustomerDTO> AddCustomerAsync(CustomerDTO customer)
        {
            _context.Customers.Add(customer);
           /// _postgreSQL.Customers.Add(customer);
            await _context.SaveChangesAsync();
           // await _postgreSQL.SaveChangesAsync();
            return customer;
        }

        public async Task<CustomerDTO?> UpdateCustomerAsync(int id, CustomerDTO customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(id);
           // var existingCustomerPg = await _postgreSQL.Customers.FindAsync(id);
            if (existingCustomer == null )// || existingCustomerPg == null)
            {
                return null;
            }
            existingCustomer._customerName = customer._customerName;
            existingCustomer._customerEmail = customer._customerEmail;
            _context.Customers.Update(existingCustomer);
            //_postgreSQL.Customers.Update(existingCustomer);
            await _context.SaveChangesAsync();
            //await _postgreSQL.SaveChangesAsync();
            return existingCustomer;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
          var product = await _context.Customers.Include(p => p._sales).FirstOrDefaultAsync(p => p._customerID == id);
            // var productPg = await _postgreSQL.Customers.FindAsync(id);
             if (product._sales.Any())
             {
                 _context.Sales.RemoveRange(product._sales);
             }
                 _context.Customers.Remove(product);
             /// _postgreSQL.Customers.Remove(productPg);
             await _context.SaveChangesAsync();
            return true;
        }
    }
}
