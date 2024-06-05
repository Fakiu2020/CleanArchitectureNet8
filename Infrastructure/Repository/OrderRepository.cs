using Common.Entities;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Infrastructure.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository  //  if we have a new DB provider must change to this : MySqlRepository<Order>, IOrderRepository 
    {

        public OrderRepository(DataContext context)
            : base(context)
        {
        }

       

    }
}
