using AuthorizationPOC.Models;
using AuthorizationPOC.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationPOC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : Controller
    {
        private readonly NewsDbContext _context;

        public ArticlesController(NewsDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var articles = await _context.Articles.ToListAsync();
            return Ok(articles);
        }
        [HttpPost]
        [Authorize(Policy = "EditArticle")]
        public async Task<IActionResult> Create([FromBody] Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return Ok(article);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "EditArticle")]
        public async Task<IActionResult> Edit(int id, [FromBody] Article article)
        {
            if (id != article.ArticleId)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(article);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteArticle")]
        public async Task<IActionResult> Delete(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
