using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Treehouse.FitnessFrog.Data;
using Treehouse.FitnessFrog.Models;

namespace Treehouse.FitnessFrog.Controllers
{
  public class EntriesController : Controller
  {
    private EntriesRepository _entriesRepository = null;

    public EntriesController()
    {
      _entriesRepository = new EntriesRepository();
    }

    public ActionResult Index()
    {
      List<Entry> entries = _entriesRepository.GetEntries();

      // Calculate the total activity.
      double totalActivity = entries
          .Where(e => e.Exclude == false)
          .Sum(e => e.Duration);

      // Determine the number of days that have entries.
      int numberOfActiveDays = entries
          .Select(e => e.Date)
          .Distinct()
          .Count();

      ViewBag.TotalActivity = totalActivity;
      ViewBag.AverageDailyActivity = (totalActivity / (double)numberOfActiveDays);

      return View(entries);
    }

    public ActionResult Add()
    {
      var entry = new Entry()
      {
        Date = DateTime.Today
      };
      return View(entry);
    }

    [HttpPost]
    public ActionResult Add(Entry entry)
    //public ActionResult Add(DateTime? date, int? activityId, double? duration,
    //                            Entry.IntensityLevel? intensity, bool? exclude, string notes)
    {
      //string date = Request.Form["Date"];
      //This line above will pull the date value from the form, but instead we are using
      //parameters in the method signiture to pull the values from the form.

      //The following can be used to preserve data, but the Html helper methods
      //used in Add.cshtml internally use ModelState so they are no longer needed here

      //ViewBag.Date = ModelState["Date"].Value.AttemptedValue;
      //ViewBag.ActivityId = ModelState["ActivityId"].Value.AttemptedValue;
      //ViewBag.Duration = ModelState["Duration"].Value.AttemptedValue;
      //ViewBag.Intensity = ModelState["Intensity"].Value.AttemptedValue;
      //ViewBag.Exclude = ModelState["Exclude"].Value.AttemptedValue;
      //ViewBag.Notes = ModelState["Notes"].Value.AttemptedValue;

      if (ModelState.IsValid)
      {
        _entriesRepository.AddEntry(entry);

        return RedirectToAction("Index");
      }
      return View(entry);
    }

    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      return View();
    }

    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      return View();
    }
  }
}