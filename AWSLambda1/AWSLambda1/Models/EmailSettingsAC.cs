using System;
using System.Collections.Generic;
using System.Text;

namespace AWSLambda1.Models
{
    class EmailSettingsAC
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string SMTPHost { get; set; }
        public string SMTPPortNo { get; set; }
        public string SMTPUserName { get; set; }
        public string SMTPPassword { get; set; }
    }
}
