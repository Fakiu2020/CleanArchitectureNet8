﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum OrderStatusTypeEnum
    {
        [Display(Name = "En Proceso")]
        InProcess = 0,
        [Display(Name = "Ejecutado")]
        Executed = 1,
        [Display(Name = "Cancelado")]
        Cancelled = 3
    }
}
