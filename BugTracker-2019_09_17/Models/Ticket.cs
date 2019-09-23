using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static BugTracker_2019_09_17.Models.TicketStatus;

namespace BugTracker_2019_09_17.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; } 
        public DateTime? UpdatedDate { get; set; }
        public int ProjectId { get; set; }
        public int TicketTypeId { get; set; }
        public int TicketPriorityId { get; set; }
        public string OwnerUserId { get; set; }
        public string AssignedToUserId { get; set; }
        public StatusName Status { get; set; }
    }
}