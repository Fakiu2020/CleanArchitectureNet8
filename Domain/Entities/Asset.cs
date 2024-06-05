using Common.Entities;
using Domain.Enums;

namespace Domain.Entities
{
    public class Asset : Entity
    {
       
        public string Ticker { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public AssetTypeEnum Type { get; private set; }
        public decimal UnitPrice { get; private set; } = 0;
        protected Asset() { }

        public Asset(int id, string ticker, string name, AssetTypeEnum type, decimal unitPrice)
        {
            if (string.IsNullOrWhiteSpace(ticker))
                throw new ArgumentException("Ticker cannot be empty", nameof(ticker));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));

            if (unitPrice <= 0)
                throw new ArgumentException("Unit price must be greater than zero", nameof(unitPrice));
            Id= id;
            Ticker = ticker;
            Name = name;
            Type = type;
            UnitPrice = unitPrice;
        }
    }
}
