using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace news.Models
{
    public class infoNews
    {
        public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Content { get; set; }
        public string ImgTitle { get; set; }
        public DateTime PublishedDate { get; set; }
        public List<NewsImage> Images { get; set; } // One-to-many relationship

    }
    public class NewsImage
    {
        public int Id { get; set; }
        
        public int NewsId { get; set; }           // Foreign key 
        public string ImagePath { get; set; }     // or byte[] if storing in DB

        [JsonIgnore]
        public infoNews? infoNews { get; set; }
    }
}
