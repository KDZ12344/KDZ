using System;
namespace Study.Core
{
    public class Request
    {
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public bool Status { get; set; } // True(1) Sender sent to Receiver request; False(0) Requester accepted request (they're friends now)
    }
}
