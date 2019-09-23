using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker_2019_09_17.Models
{
    public class TicketAttachments
    {
        TicketAttachments()
        {
            Created = DateTime.Now;
        }

        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Filepath { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        public string FileUrl { get; set; }
    }
}