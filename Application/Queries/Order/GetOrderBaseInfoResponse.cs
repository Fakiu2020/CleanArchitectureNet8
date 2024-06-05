using Application.Command.Order.Create;
using AutoMapper;
using Common.Entities.Interfaces;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Order
{
    public class GetOrderBaseInfoResponse
    {
        public int OrderId { get; set; }
        public int AccountId { get; private set; }
        public string AssetName { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public char Operation { get; private set; }
        public string Status { get; private set; }
        public decimal TotalAmount { get; set; }


    }
}
