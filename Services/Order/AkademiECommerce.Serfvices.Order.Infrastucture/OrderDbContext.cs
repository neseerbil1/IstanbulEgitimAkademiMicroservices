using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkademiECommerce.Serfvices.Order.Infrastucture
{
    public class OrderDbContext:DbContext
    {
        public const string default_schema = "ordering";
        public OrderDbContext(DbContextOptions<OrderDbContext>options):base(options)
        {

        }
       

      
    }
}
