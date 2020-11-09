using Microsoft.EntityFrameworkCore;
using customercard.Models;

namespace customercard.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
        : base (options)
        {
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Token> Tokens { get; set; }
    }
}