using DataInsights.API.Data;
using DataInsights.API.Models;
using DataInsights.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataInsights.API.Repository
{
    /// <summary>
    /// Commented PostgreSQL database to test SQL Server functionality
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly SQLDatabase _context;
       // private readonly PostgreSQLDb _postgreSQL;
       // private readonly string _activeDb;
        public ProductRepository(SQLDatabase context)
        { 
           _context = context;
           // _postgreSQL = postgreSQL;
           // _activeDb = config["ActiveDatabase"];
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var sqlProducts = await _context.Products.ToListAsync();
            //  var pgProducts = await _postgreSQL.Products.ToListAsync();
            return sqlProducts; //.Concat(pgProducts).ToList();
        }
        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            var _sqlProductId = await _context.Products.FindAsync(productId);
            //var _pgProductId = await _postgreSQL.Products.FindAsync(productId);
            return _sqlProductId; //?? _pgProductId;
        }   

        public async Task AddProductAsync(Product product)
        {
               _context.Products.Add(product);
               await _context.SaveChangesAsync();

        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            //_postgreSQL.Products.Update(product);
            await _context.SaveChangesAsync();
            //await _postgreSQL.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _context.Products.Include(p => p.Sales).FirstOrDefaultAsync(p => p.Product_ID == productId);
            // var productPg = await _postgreSQL.Products.FindAsync(productId);
            if (product.Sales.Any())
            {
                _context.Sales.RemoveRange(product.Sales);
            }
                _context.Products.Remove(product);
            /// _postgreSQL.Products.Remove(productPg);
            await _context.SaveChangesAsync();
            // await _postgreSQL.SaveChangesAsync();
        }
    }
}
