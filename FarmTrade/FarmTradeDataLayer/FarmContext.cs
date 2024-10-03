using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading.Tasks;
using FarmTradeEntity;

namespace FarmTradeDataLayer
{
    public class FarmContext:DbContext
    {
        public FarmContext()
        {
        }

        public FarmContext(DbContextOptions<FarmContext> options) : base(options)
        {

        }

        // Entity class database:

        public DbSet<User> users { get; set; }
        public DbSet<ReviewsAndRatings> ratingsreview { get; set; }
        public DbSet<Product> product { get; set; }
        // SQL Connection:
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer("Server=IN-6YYZFY3;Database=agriproductsdbs;User Id=sa;Password=Praveen0077$$$$;Integrated Security = True;TrustServerCertificate=True;");
        }
    }
}
