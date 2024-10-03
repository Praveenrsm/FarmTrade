using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FarmTradeEntity
{
    public class ReviewsAndRatings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string comments { get; set; }
        public Rating rating { get; set; }

        [ForeignKey("Product")]
        public int productId { get; set; }

        public Product product { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }

        public User User { get; set; }
    }
}
