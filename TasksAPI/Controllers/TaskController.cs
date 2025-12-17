using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TasksAPI.Context;
using TasksAPI.DTO;
using TasksAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TasksAPI.Controllers
{
    //// POST → Create
    //// PUT → Update  
    //// DELETE → Delete

    ////:BaseController, no need to repeat code and db context here
    //[Route("api/[controller]")] 
    //[ApiController]
    public class TaskController : BaseController
    {
        public TaskController(AppDbContext db) : base(db)
        {
        }

        //private readonly AppDbContext _db;
        //public TaskController(AppDbContext db) {_db = db;}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<TaskClass>>> GetTasks()  // Changed return type
        {
            var tasks = await _db.Tasks.ToListAsync();  // Added "await" and "Async"
            if (tasks.Count > 0)
            {
                return Ok(tasks);
            }
            return NotFound("No tasks found.");
        }
        //// Alternative version returning DTOs___________________________
        //public async Task<ActionResult<List<TaskDTO>>> GetTasks()
        //{
        //    var tasks = await _db.Tasks.ToListAsync();
        //    // Convert each TaskClass to TaskDTO
        //    var taskDTOs = tasks.Select(t => new TaskDTO
        //    {
        //        Id = t.Id,
        //        Name = t.Name,
        //        DeadLine = t.DeadLine
        //    }).ToList();
        //    return Ok(taskDTOs);
        //}
        //// Alternative version returning DTOs___________________________

        // POST: api/Task
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateTask(TaskClass task)
        {
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();  // Changed to Async

            return Ok("Task created.");
        }


        //PUT = Replace ENTIRE object
        //PUT: api/Task/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateTask(int id, TaskClass task)
        {
            var existing = await _db.Tasks.FindAsync(id);  // Changed to Async

            if (existing == null)
            {
                return NotFound();
            }
            existing.Name = task.Name;

            await _db.SaveChangesAsync();  // Changed to Async

            return Ok("Task updated.");
        }
        //1.PATCH = Update ONLY specific fields
        [HttpPatch("{id}/complete")]
        public async Task<ActionResult> MarkTaskComplete(int id)
        {
            var task = await _db.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();
            task.Name = "[Completed] " + task.Name;
            await _db.SaveChangesAsync();

            return Ok("Task marked as complete");
        }
        ////2.Generic PATCH with JSON Patch
        ////Microsoft.AspNetCore.JsonPatch
        ////Microsoft.AspNetCore.Mvc.NewtonsoftJson
        //[HttpPatch("{id}")]
        //public async Task<IActionResult> PatchTask(int id, 
        //    [FromBody] JsonPatchDocument<TaskClass> patchDoc)
        //{
        //    var task = await _db.Tasks.FindAsync(id);
        //    if (task == null)
        //        return NotFound(); 
        //    // Apply the patch (only updates specified fields)
        //    patchDoc.ApplyTo(task, ModelState); 
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);           
        //    await _db.SaveChangesAsync();     
        //    return Ok("Task updated partially");
        //}


        // DELETE: api/Task/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteTask(int id)
        {
            var task = await _db.Tasks.FindAsync(id);  // Changed to Async

            if (task == null)
            {
                return NotFound();
            }

            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();  // Changed to Async

            return Ok("Task deleted.");
        }
    }
}