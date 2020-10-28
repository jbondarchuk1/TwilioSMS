using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace TwilioSMS
{
    public class TwilioSMS
    {
        // sends text message and returns the message resource. The message resource is optional but lets you return information in async
        public MessageResource sendSMS(string messageBody, string fromPhone, string toPhone, string accountSid, string authToken)
        {
            string userPhoneNumber = toPhone;

            // save these inside of an environmental variable
            string myAccountSid = accountSid;
            string myAuthToken = authToken;

            TwilioClient.Init(myAccountSid, myAuthToken);

            var message = MessageResource.Create(
                body: messageBody,
                // "+18507538791"
                from: new Twilio.Types.PhoneNumber(fromPhone),
                to: new Twilio.Types.PhoneNumber(userPhoneNumber)
            );

            Console.WriteLine(message.Sid);
            return message;
        }

        public Task sendSMSAsync(string messageBody, string fromPhone, string toPhone, string accountSid, string authToken)
        {
            MessageResource message = sendSMS(messageBody, fromPhone, toPhone, accountSid, authToken);
            // Status is one of Queued, Sending, Sent, Failed or null if the number is not valid
            Trace.TraceInformation(message.Status.ToString());
            // Twilio doesn't currently have an async API, so return success.
            return Task.FromResult(0);
        }
    }
}

