using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker_2019_09_17.Models
{
    public class TicketStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public enum StatusName
        {
            Top,
            High,
            Middle, 
            Low
        }
    }
}