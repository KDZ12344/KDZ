using System;
namespace Study.Core
{
    public class Request
    {
        public User Sender { get; set; }
        public User Receiver { get; set; }

        //public Request(User sender, User receiver)
        //{
        //    Sender = sender;
        //    Receiver = receiver;
        //}
    }
}
