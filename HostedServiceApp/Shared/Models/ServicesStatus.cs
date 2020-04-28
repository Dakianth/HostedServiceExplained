using System;
using System.Collections.Generic;
using System.Text;

namespace HostedServiceApp.Models
{
    public class ServicesStatus
    {
        public ServicesStatus(string id, int status)
        {
            Id = id;
            Status = (Status)status;
        }

        public string Id { get; set; }
        public List<string> Events { get; set; }
        public Status Status { get; set; }

        public string Style => Status switch
        {
            Status.Started => "alert-success",
            Status.Running => "alert-primary",
            _ => "alert-danger"
        };
    }
}
