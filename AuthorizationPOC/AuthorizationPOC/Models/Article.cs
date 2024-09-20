using System.ComponentModel.DataAnnotations;

namespace AuthorizationPOC.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public User Author { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
