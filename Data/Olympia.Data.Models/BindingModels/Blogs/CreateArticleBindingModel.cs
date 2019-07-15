namespace Olympia.Data.Models.BindingModels.Blogs
{
    using Microsoft.AspNetCore.Http;
    using Olympia.Common;
    using System.ComponentModel.DataAnnotations;

    public class CreateArticleBindingModel
    {
        private const int TitleMaxLength = 255;
        private const int TitleMinLength = 3;


        [Required]
        [Display(Name = GlobalConstants.DisplayTitle)]
        [DataType(DataType.Text)]
        [StringLength(TitleMaxLength, ErrorMessage = GlobalConstants.ErrorInputMessage, MinimumLength = TitleMinLength)]
        public string Title { get; set; }

        [Required]
        [Display(Name = GlobalConstants.DisplayContent)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Display(Name = GlobalConstants.DisplayImg)]
        [DataType(DataType.Upload)]       
        public IFormFile ImgUrl { get; set; }
    }
}
