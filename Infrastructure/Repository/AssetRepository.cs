using Common.Entities;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Infrastructure.Repository
{
    public class AssetRepository : Repository<Asset>, IAssetRepository  
    {

        public AssetRepository(DataContext context)
            : base(context)
        {
        }

    }
}
