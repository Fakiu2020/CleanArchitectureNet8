using Common.Entities;
using Common.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command
{
    public abstract class CommandBase
    {
        public Guid Id { get ; set ; }
    }
}
