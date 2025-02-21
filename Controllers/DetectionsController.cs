// Controllers/DetectionsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObjectDetection.Data;
using ObjectDetection.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ObjectDetection.Controllers
{
    public class DetectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Detections
        public async Task<IActionResult> Index()
        {
            return View(await _context.Detections.ToListAsync());
        }

        // GET: Detections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detection = await _context.Detections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detection == null)
            {
                return NotFound();
            }

            return View(detection);
        }

        // GET: Detections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Detections/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ImageName,VideoName,ClassName,Latitude,Longitude,Status,CriticalLevel,DateTimeDetection")] Detection detection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detection);
        }

        // GET: Detections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detection = await _context.Detections.FindAsync(id);
            if (detection == null)
            {
                return NotFound();
            }
            return View(detection);
        }

        // POST: Detections/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ImageName,VideoName,ClassName,Latitude,Longitude,Status,CriticalLevel,DateTimeDetection")] Detection detection)
        {
            if (id != detection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetectionExists(detection.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(detection);
        }

        // GET: Detections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detection = await _context.Detections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detection == null)
            {
                return NotFound();
            }

            return View(detection);
        }

        // POST: Detections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detection = await _context.Detections.FindAsync(id);
            _context.Detections.Remove(detection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetectionExists(int id)
        {
            return _context.Detections.Any(e => e.Id == id);
        }

        // GET: Detections/GroupByVideoName
        [HttpGet]
        public async Task<IActionResult> GroupByVideoName()
        {
            var groupedData = await _context.Detections
                .GroupBy(d => d.VideoName)
                .Select(g => new { VideoName = g.Key, Count = g.Count() })
                .ToListAsync();
            return Json(groupedData);
        }

   
        [HttpGet]
        public async Task<IActionResult> GroupByTitle()
        {
            var groupedData = await _context.Detections
                .GroupBy(d => d.Title)
                .Select(g => new { Title = g.Key, Count = g.Count() })
                .ToListAsync();
            return Json(groupedData);
        }

        // GET: Detections/GroupByClassName
        [HttpGet]
        public async Task<IActionResult> GroupByClassName()
        {
            var groupedData = await _context.Detections
                .GroupBy(d => d.ClassName)
                .Select(g => new { ClassName = g.Key, Count = g.Count() })
                .ToListAsync();
            return Json(groupedData);
        }

        // GET: Detections/GroupByStatus
        [HttpGet]
        public async Task<IActionResult> GroupByStatus()
        {
            var groupedData = await _context.Detections
                .GroupBy(d => d.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();
            return Json(groupedData);
        }

        // GET: Detections/GroupByCriticalLevel
        [HttpGet]
        public async Task<IActionResult> GroupByCriticalLevel()
        {
            var groupedData = await _context.Detections
                .GroupBy(d => d.CriticalLevel)
                .Select(g => new { CriticalLevel = g.Key, Count = g.Count() })
                .ToListAsync();
            return Json(groupedData);
        }

       // GET: Detections/ReportList
        public async Task<IActionResult> ReportList()
        {
            Console.WriteLine("ReportList()");
            // var groupedData = await _context.Detections
            //     .GroupBy(d => d.VideoName)
            //     .Select(g => new { VideoName = g.Key, Count = g.Count() })
            //     .ToListAsync();
            // ViewBag.VideoNames = groupedData;
            return View();
        }

        // POST: Detections/ReportList
        [HttpPost]
        public async Task<IActionResult> ReportList([FromBody] string selectedVideoName)
        {
            Console.WriteLine($"Received selectedVideoName: {selectedVideoName}");
            var filteredData = await _context.Detections
                .Where(d => d.VideoName == selectedVideoName)
                .ToListAsync();
            return Json(filteredData);
        }

      // GET: Detections/ReportResult
        public async Task<IActionResult> ReportResult()
        {
            var groupedData = await _context.Detections
                .GroupBy(d => new { d.Title, d.ClassName })
                .Select(g => new { Title = g.Key.Title, ClassName = g.Key.ClassName, RecordCount = g.Count() })
                .ToListAsync();
            return View(groupedData);
        }

        // POST: Detections/ReportResult
        [HttpPost]
        public async Task<IActionResult> ReportResult(string newSelectedVideoName)
        {
            var filteredData = await _context.Detections
                .Where(d => d.VideoName == newSelectedVideoName)
                .ToListAsync();
            return Json(filteredData);
        }
    }
}
