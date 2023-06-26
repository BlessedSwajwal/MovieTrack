using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Command.EmailVerification;

public record EmailVerificationCommand(
        string token,
        string email
    ) : IRequest;
