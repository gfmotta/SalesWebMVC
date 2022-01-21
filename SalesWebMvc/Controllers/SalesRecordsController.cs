using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? fromDate, DateTime? untilDate)
        {
            if (!fromDate.HasValue)
            {
                fromDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!untilDate.HasValue)
            {
                untilDate = DateTime.Now;
            }

            ViewData["fromDate"] = fromDate.Value.ToString("yyyy-MM-dd");
            ViewData["untilDate"] = untilDate.Value.ToString("yyyy-MM-dd");

            var result = await _salesRecordService.FindByDateAsync(fromDate, untilDate);

            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? fromDate, DateTime? untilDate)
        {
            if (!fromDate.HasValue)
            {
                fromDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!untilDate.HasValue)
            {
                untilDate = DateTime.Now;
            }

            ViewData["fromDate"] = fromDate.Value.ToString("yyyy-MM-dd");
            ViewData["untilDate"] = untilDate.Value.ToString("yyyy-MM-dd");

            var result = await _salesRecordService.FindByDateGroupingAsync(fromDate, untilDate);

            return View(result);
        }
    }
}
