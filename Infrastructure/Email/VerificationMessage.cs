using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Email;

public class VerificationMessage
{
    public MailboxAddress To { get; set; }
    public string Subject { get; set; }
    public string Body { get; private set; }
    public VerificationMessage(string to, string verificationCode)
    {
        To = new MailboxAddress("Verification", to);
        Subject = "Verify your email.";
        Body = $""""
            <p>Dear User,</p>
        <p>Thank you for registering with our service. Please click the link below to verify your email:</p>
        <p>{verificationCode}</p>
        <p>If you did not sign up for an account, you can safely ignore this email.</p>
        <br>
        <p>Best regards,</p>
        <p>Your App Team</p>
        

        """";
    }

}
