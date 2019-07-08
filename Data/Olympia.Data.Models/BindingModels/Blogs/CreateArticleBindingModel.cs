namespace Olympia.Data.Models.BindingModels.Blogs
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class CreateArticleBindingModel
    {
        [Required]
        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        [Display(Name = "Display Image")]
        [DataType(DataType.Upload)]
        public IFormFile ImgUrl { get; set; }
    }
}
