using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker_2019_09_17.Models;
using Microsoft.AspNet.Identity;
using static BugTracker_2019_09_17.Models.TicketStatus;

namespace BugTracker_2019_09_17.Controllers
{
    
    public class TicketsController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tickets

            //only authenticated users can view the tickets page
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        //This is to make sure that the only people who can create tickets are Submitters
        [Authorize(Roles = "Submitter, Admin")]
        public ActionResult Create()
        {
            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "UserName");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.TicketPriority = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketType = new SelectList(db.TicketTypes, "Id", "Name");


            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,CreatedDate,UpdatedDate,ProjectId,TicketTypeId,TicketPriorityId,OwnerUserId,AssignedToUserId")]
        Ticket ticket, int ProjectId, int TicketPriority, int TicketType)
        {
            ticket.OwnerUserId = User.Identity.GetUserId();
            ticket.CreatedDate = DateTime.Now;
            ticket.ProjectId = ProjectId;
            ticket.TicketPriorityId = TicketPriority;
            ticket.TicketTypeId = TicketType;
            

            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticket);
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles="ProjectManager, Developer, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,CreatedDate,UpdatedDate,ProjectId,TicketTypeId,TicketPriorityId,OwnerUserId,AssignedToUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        [Authorize(Roles="Admin, ProjectManager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // project managers should be able to see all tickets belonging to projects which they are assigned.

        [Authorize(Roles="ProjectManager, Admin")]
        public ActionResult AllManagerTickets()
        {
            var userID = User.Identity.GetUserId();
            var ticketlist = db.Tickets.Where(t => t.AssignedToUserId == userID);
            ViewBag.TicketList = ticketlist;
            return View(ticketlist);
        }

        [Authorize(Roles="ProjectManager, Developer, Admin")]
        public ActionResult AllTicketsByProject()
        {
            var userID = User.Identity.GetUserId();
            var usersprojects = db.ProjectUsers.Where(pu => pu.ApplicationUserId == userID);
            var ticketlist = db.Tickets.Where(t => usersprojects.Any(pu => pu.ProjectId == t.ProjectId)).ToList();
            ViewBag.TicketList = ticketlist;
            return View(ticketlist);
        }

        [Authorize(Roles="Developer, Admin")]
        public ActionResult AllTicketsYoureAssignedTo()
        {
            var userID = User.Identity.GetUserId();
            var ticketsList = db.Tickets.Where(t => t.AssignedToUserId == userID).ToList();
            ViewBag.Tickets = ticketsList;

            return View(ticketsList);
        }

        [Authorize(Roles="Submitter, Admin")]
        public ActionResult AllTicketsYouSubmitted()
        {
            var userID = User.Identity.GetUserId();
            var ticketsList = db.Tickets.Where(t => t.OwnerUserId == userID);
            ViewBag.Tickets = ticketsList;

            var newstatus = Enum.GetValues(typeof(StatusName)).ToString().StartsWith("High");

            //var firstTicketEnum = db.Tickets.First().TicketStatus;

            //Enum.Parse(typeof(StatusName), "");

            return View(ticketsList);
        }

        


    }
}
