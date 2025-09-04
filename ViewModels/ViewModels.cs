using System.ComponentModel.DataAnnotations;

namespace news.ViewModels
{
    public class AddNewsViewModel
    {
        [Required]
        [Display(Name = "News Headline")]
        public string? Head_news { get; set; }

        [Display(Name = "Upload your Headline pictures")]
        public IFormFile? NewsHeadlinePics { get; set; }

        [Required]
        [Display(Name = "News Details")]
        public string? comments { get; set; }

        [Display(Name = "Upload your other pictures")]
        public List<IFormFile>? otherPics { get; set; }
    }
}