﻿using MimeKit;
using MailKit.Net.Smtp;

namespace tupenca_back.Utilities.EmailService
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("user",x)));
            Subject = subject;
            Content = content;
        }
    }
}
