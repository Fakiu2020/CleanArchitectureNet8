using Common.Entities;
using Domain.Constants;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order : Entity
    {
        public int AccountId { get; private set; }
        public string AssetName { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public char Operation { get; private set; }
        public OrderStatusTypeEnum Status { get; private set; }
        public decimal TotalAmount { get; set; }

        [NotMapped]
        public decimal Commission { get; set; }
        [NotMapped]
        public decimal Tax { get; set; }
        
      
        public Order(int accountId, string assetName, int quantity, decimal price, char operation)
        {          
            AccountId = accountId;
            AssetName = assetName;
            Quantity = quantity;
            Price = price;
            Operation = char.ToUpper(operation);
            Status = OrderStatusTypeEnum.InProcess;
        }
        private Order() { }
        public void CalculateTotalAmount(AssetTypeEnum assetType, decimal price)
        {      
            switch (assetType)
            {
                case AssetTypeEnum.Bond:
                    TotalAmount = Quantity * Price;
                    Commission = TotalAmount * 0.002m;
                    Tax = Commission * 0.21m;
                    TotalAmount += Commission + Tax;
                    break;
                case AssetTypeEnum.Stock:
                    Price = price; // price from db
                    TotalAmount = Price * Quantity;
                    Commission = TotalAmount * 0.006m;
                    Tax = Commission * 0.21m;
                    TotalAmount += Commission + Tax; 
                    break;
                case AssetTypeEnum.FCI:
                    TotalAmount = Quantity * Price;
                    break;
                default:
                    throw new ArgumentException("Unknown asset type", nameof(assetType));                    
            }
        }
        public void UpdateStatus(OrderStatusTypeEnum status)
        {
            Status = status;
        }
    }
}
