namespace Olympia.Data.Models.BindingModels.Blogs
{
    using Microsoft.AspNetCore.Http;
    using Olympia.Common;
    using System.ComponentModel.DataAnnotations;

    public class CreateArticleBindingModel
    {
        [Required]
        [Display(Name = DisplayModelConstatnts.DisplayTitle)]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [Display(Name = DisplayModelConstatnts.DisplayContent)]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        [Display(Name = DisplayModelConstatnts.DisplayImg)]
        [DataType(DataType.Upload)]
        public IFormFile ImgUrl { get; set; }
    }
}
