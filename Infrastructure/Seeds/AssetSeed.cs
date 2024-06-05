using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds
{
    public static class AssetSeed
    {
        public static IList<Asset> GetAssets()
        {
            return
            [
                new (1,"AAPL", "Apple", AssetTypeEnum.Stock, 177.97m),
                new (2,"GOOGL", "Alphabet Inc", AssetTypeEnum.Stock, 138.21m),
                new (3,"MSFT", "Microsoft", AssetTypeEnum.Stock, 329.04m),
                new (4,"KO", "Coca Cola", AssetTypeEnum.Stock, 58.3m),
                new (5,"WMT", "Walmart", AssetTypeEnum.Stock, 163.42m),
                new (6,"AL30", "BONOS ARGENTINA USD 2030 L.A", AssetTypeEnum.Bond, 307.4m),
                new (7,"GD30", "Bonos Globales Argentina USD Step Up 2030", AssetTypeEnum.Bond, 336.1m),
                new (8,"Delta.Pesos", "Delta Pesos Clase A", AssetTypeEnum.FCI, 0.0181m),
                new (9,"Fima.Premium", "Fima Premium Clase A", AssetTypeEnum.FCI, 0.0317m)
            ];
        }
    }
}
