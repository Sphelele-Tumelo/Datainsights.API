using DataInsights.API.Data;
using DataInsights.API.Models;
using DataInsights.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataInsights.API.Repository
{
    public class SalesRepository : ISalesRepository
    {
        /// <summary>
        /// Commented PostgreSQL database to test SQL Server functionality first.
        /// </summary>
        private readonly SQLDatabase _context;
        //private readonly PostgreSQLDb _postgreSQL;
        public SalesRepository(SQLDatabase context )//, PostgreSQLDb postgreSQL)
        {
            _context = context;
           // _postgreSQL = postgreSQL;
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            var sqlList = await _context.Sales.ToListAsync();
            // var pgList = await _postgreSQL.Sales.ToListAsync();

            return sqlList;//.Concat(pgList).ToList();
        }

        public async Task<Sale?> GetSaleByIdAsync(int id)
        {
            var sqlSalesId = await _context.Sales.FindAsync(id);
           // var pgSalesId = await _postgreSQL.Sales.FindAsync(id);

            return sqlSalesId; 
        }   

        public async Task<Sale> AddSaleAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            //_postgreSQL.Sales.Add(sale);
            await _context.SaveChangesAsync();
            //await _postgreSQL.SaveChangesAsync();
            return sale;
        }

        public async Task<Sale?> UpdateSaleAsync(int id, Sale sale)
        {
            var existingSale = await _context.Sales.FindAsync(id);
            //var exisitingSalePostgre = await _postgreSQL.Sales.FindAsync(id);
            if (existingSale == null)// || exisitingSalePostgre == null)
            {
                return null;
            }
            existingSale.Customer_ID = sale.Customer_ID;
            existingSale.Quantity = sale.Quantity;
            existingSale.SaleDate = sale.SaleDate;

           // exisitingSalePostgre.Customer_ID = sale.Customer_ID;
          //  exisitingSalePostgre.Quantity = sale.Quantity;
           // exisitingSalePostgre.SaleDate = sale.SaleDate;

            _context.Sales.Update(existingSale);
           // _postgreSQL.Sales.Update(exisitingSalePostgre);
            await _context.SaveChangesAsync();
          //  await _postgreSQL.SaveChangesAsync();
            return existingSale;
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
           // var salePg = await _postgreSQL.Sales.FindAsync(id);
            if (sale == null) //|| salePg == null)
            {
                return false;
            }
            _context.Sales.Remove(sale);
            //_postgreSQL.Sales.Remove(salePg);
            await _context.SaveChangesAsync();
           // await _postgreSQL.SaveChangesAsync();
            return true;
        }
    }
}
