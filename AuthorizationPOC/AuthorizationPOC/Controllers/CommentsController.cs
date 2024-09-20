using AuthorizationPOC.Models;
using AuthorizationPOC.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace AuthorizationPOC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        private readonly NewsDbContext _context;

        public CommentsController(NewsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var comments = await _context.Comments.ToListAsync();
            return Ok(comments);
        }

        [HttpPost]
        [Authorize(Policy = "CommentOnArticle")]
        public async Task<IActionResult> Create([FromBody] Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return Ok(comment);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "EditComment")]
        public async Task<IActionResult> Edit(int id, [FromBody] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(comment);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteComment")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
