using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Service;

public interface IEmailService
{
    Task SendVerificationEmailAsync(string recipientEmail, string verificationCode);
}
