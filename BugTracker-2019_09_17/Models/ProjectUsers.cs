using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker_2019_09_17.Models
{
    public class ProjectUsers
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}