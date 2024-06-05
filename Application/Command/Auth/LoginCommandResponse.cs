﻿using Domain.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Auth
{
    public class LoginCommandResponse
    {
        public required string Token { get; init; }

    }
}
