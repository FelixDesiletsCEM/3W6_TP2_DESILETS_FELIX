using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JuliePro.Data;
using JuliePro.Models;
using JuliePro.Services;
using JuliePro.Utility;

namespace JuliePro.Controllers
{
    public class RecordController : Controller
    {

        private readonly ITrainerService _trainerService;
        private readonly IServiceBaseAsync<Discipline> _disciplineService;


        private readonly JulieProDbContext _context;

        public RecordController(JulieProDbContext context, ITrainerService trainerService, IServiceBaseAsync<Discipline> disciplineService)
        {
            _trainerService = trainerService;
            _disciplineService = disciplineService;
            _context = context;
        }

        // GET: Record
        public async Task<IActionResult> Index()
        {
            var julieProDbContext = _context.Records.Include(d => d.Discipline).Include(t => t.Trainer);
            return View(await julieProDbContext.ToListAsync());
        }

        // GET: Record/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @record = await _context.Records
                .Include(d => d.Discipline)
                .Include(t => t.Trainer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@record == null)
            {
                return NotFound();
            }

            return View(@record);
        }

        // GET: Record/Create
        public async Task<IActionResult> Create([FromServices] IImageFileManager fileManager)
        {

            ViewData["Discipline_Id"] = new SelectList(await _disciplineService.GetAllAsync(), "Id", "Name");
            ViewData["Trainer_Id"] = new SelectList(await _trainerService.GetAllAsync(), "Id", "FullName");
            return View();
        }

        // POST: Record/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Record @record)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@record);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Discipline_Id"] = new SelectList(await _disciplineService.GetAllAsync(), "Id", "Name");
            ViewData["Trainer_Id"] = new SelectList(await _trainerService.GetAllAsync(), "Id", "FullName");
            return View(@record);
        }

        // GET: Record/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @record = await _context.Records.FindAsync(id);
            if (@record == null)
            {
                return NotFound();
            }
            RecordViewModel recordVM = new RecordViewModel();
            recordVM.record = @record;
            recordVM.disciplines = new SelectList(await _disciplineService.GetAllAsync(), "Id", "Name");
            recordVM.trainers = new SelectList(await _trainerService.GetAllAsync(), "Id", "FullName", @record.Trainer_Id);

            //ViewData["Discipline_Id"] = new SelectList(await _disciplineService.GetAllAsync(), "Id", "Name");
            //ViewData["Trainer_Id"] = new SelectList(await _trainerService.GetAllAsync(), "Id", "FullName");
            return View(recordVM);
        }

        // POST: Record/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Record @record)
        {
            if (id != @record.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@record);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordExists(@record.Id))
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
            RecordViewModel recordVM = new RecordViewModel();
            recordVM.record = @record;
            recordVM.disciplines = new SelectList(await _disciplineService.GetAllAsync(), "Id", "Name");
            recordVM.trainers = new SelectList(await _trainerService.GetAllAsync(), "Id", "FullName", @record.Trainer_Id);

            //ViewData["Discipline_Id"] = new SelectList(await _disciplineService.GetAllAsync(), "Id", "Name");
            //ViewData["Trainer_Id"] = new SelectList(await _trainerService.GetAllAsync(), "Id", "FullName", @record.Trainer_Id);
            return View(recordVM);
        }

        // GET: Record/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @record = await _context.Records
                .Include(d => d.Discipline)
                .Include(t => t.Trainer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@record == null)
            {
                return NotFound();
            }

            return View(@record);
        }

        // POST: Record/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @record = await _context.Records.FindAsync(id);
            if (@record != null)
            {
                _context.Records.Remove(@record);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordExists(int id)
        {
            return _context.Records.Any(e => e.Id == id);
        }
    }
}
