using Application.Common.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Order.GetAll
{
    public class GetByIdOrderQuery : IRequest<Result<GetByIdOrderResponse>>
    {
        public int OrderId { get; set; }
    }
}
