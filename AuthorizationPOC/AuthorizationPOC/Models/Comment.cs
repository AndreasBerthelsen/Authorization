using System.ComponentModel.DataAnnotations;

namespace AuthorizationPOC.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int ArticleId { get; set; }

        public Article Article { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
