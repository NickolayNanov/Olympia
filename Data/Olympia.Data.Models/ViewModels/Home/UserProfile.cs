namespace Olympia.Data.Models.ViewModels.Home
{
    using Microsoft.AspNetCore.Http;

    using Olympia.Common;
    using Olympia.Data.Domain.Enums;

    using System.ComponentModel.DataAnnotations;

    public class UserProfile
    {
        private const double WeightMinValue = 0.01;
        private const double WeightMaxValue = 500.0;

        private const double HeightMinValue = 0.01;
        private const double HeightMaxValue = 300.0;

        private const int AgeMinValue = 12;
        private const int AgeMaxValue = 65;

        private const int DescriptionMinLength = 5;
        private const int DescriptionMaxLength = 9999;

        private const int AdressMinLength = 3;
        private const int AdressMaxLength = 9999;

        public string UserName { get; set; }

        public string FullName { get; set; }

        [Range(WeightMinValue, WeightMaxValue, ErrorMessage = GlobalConstants.ErrorInputNumberMessage)]
        public double? Weight { get; set; }

        [Range(HeightMinValue, HeightMaxValue, ErrorMessage = GlobalConstants.ErrorInputNumberMessage)]
        public double? Height { get; set; }

        public string Interests { get; set; }

        [Range(AgeMinValue, AgeMaxValue, ErrorMessage = GlobalConstants.ErrorInputMessage)]
        public int Age { get; set; }

        [StringLength(DescriptionMaxLength, ErrorMessage = GlobalConstants.ErrorInputMessage, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        [StringLength(AdressMaxLength, ErrorMessage = GlobalConstants.ErrorInputMessage, MinimumLength = AdressMinLength)]
        public string Adress { get; set; }

        public ActityLevel Actity { get; set; }

        [Display(Name = GlobalConstants.DisplayProfilePic)]
        [DataType(DataType.Upload)]
        public IFormFile ProfilePictureUrl { get; set; }
    }
}
