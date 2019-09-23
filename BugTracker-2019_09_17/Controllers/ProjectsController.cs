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

namespace BugTracker_2019_09_17.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationUserManager um = HelperClass.MakeUserManager();

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Projects
        [Authorize(Roles="Admin, ProjectManager")]
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        [Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult AllProjects()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles="Admin, ProjectManager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles="ProjectManager, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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


        //anyone can see the projects that they're assigned to, or are associated with
        public ActionResult MyProjects()
        {
            var userId = User.Identity.GetUserId();
            var userprojs = db.ProjectUsers.Where(up => up.ApplicationUserId == userId);
            var projectsList = new List<Project>();

            foreach(var up in userprojs)
            {
                projectsList.Add(db.Projects.Find(up.ProjectId));
            }

            return View(projectsList);
        }

        [Authorize(Roles="Admin, ProjectManager")]
        public ActionResult AddUserToProject()
        {
            ViewBag.projectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.userId = new SelectList(db.Users, "Id", "Username");
            ViewBag.addOrRemove = new SelectList(new List<string> { "add", "remove" });


            return View();
        }

        [Authorize(Roles = "Admin, ProjectManager")]
        [HttpPost]
        public ActionResult AddUserToProject(string userId, int projectId, string addOrRemove)
        {
            ViewBag.Message = "Something went wrong.";

            if(addOrRemove == "add")
            {
                if (!db.ProjectUsers.Any(pu => pu.ApplicationUserId == userId && pu.ProjectId == projectId))
                {
                    var project = db.Projects.Find(projectId);

                    db.ProjectUsers.Add(new ProjectUsers { ProjectId = projectId, ApplicationUserId = userId, Project = project });
                    ViewBag.Message = "User added to project";
                    db.SaveChanges();
                }
            }else
            {
                if (db.ProjectUsers.Any(pu => pu.ApplicationUserId == userId && pu.ProjectId == projectId))
                {
                    var userproj = db.ProjectUsers.FirstOrDefault(pu => pu.ApplicationUserId == userId && pu.ProjectId == projectId);
                    db.ProjectUsers.Remove(userproj);
                    ViewBag.Message = "User removed from project";

                    db.SaveChanges();
                }
            }
            return RedirectToAction("MessagePage", new { ViewBag.Message });
        }

        public ActionResult MessagePage(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }


    }
}
