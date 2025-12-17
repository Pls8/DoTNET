using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasksAPI.Context;
using TasksAPI.DTO;
using TasksAPI.Models;

namespace TasksAPI.Controllers
{
    //[Route("api/[controller]")] //// - already in BaseController
    //[ApiController] ////  - already in BaseController
    public class CategoryController : BaseController
    {
        public CategoryController(AppDbContext db) : base(db)
        {// Pass db to BaseController
        }

        //private readonly AppDbContext _db;
        //public CategoryController(AppDbContext db)
        //{
        //    _db = db;
        //}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<List<TaskCategoryClass>>> GetCategories()
        //{
        //    var catg = await _db.Categories.ToListAsync();
        //    if (catg.Count > 0)
        //    {
        //        return Ok(catg);
        //    }
        //    return NotFound("No Categories found.");
        //}
        //// Alternative version returning DTOs___________________________
        public async Task<ActionResult<List<CategoryDTO>>> GetCategories()
        {
            var categories = await _db.Categories.ToListAsync();
            // Convert each TaskCategoryClass to CategoryDTO
            var categoryDTOs = categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            return Ok(categoryDTOs);
        }
        //// Alternative version returning DTOs___________________________



        // POST: api/Category
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateCategory(TaskCategoryClass category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();

            return Ok("Category created.");
        }

        // PUT: api/Category/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateCategory(int id, TaskCategoryClass category)
        {
            var existing = await _db.Categories.FindAsync(id);

            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = category.Name;
            // Add other properties if needed

            await _db.SaveChangesAsync();

            return Ok("Category updated.");
        }

        // DELETE: api/Category/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _db.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();

            return Ok("Category deleted.");
        }
    }
}