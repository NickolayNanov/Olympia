﻿using Olympia.Common;
using Olympia.Data.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Olympia.Data.Models.ViewModels.Home
{
    public class UserProfile
    {
        public string UserName { get; set; }

        public string FullName { get; set; }

        [Range(0.0, 500.0, ErrorMessage = GlobalConstants.ErrorInputMessage)]
        public double? Weight { get; set; }

        [Range(0.0, 300.0 , ErrorMessage = GlobalConstants.ErrorInputMessage)]
        public double? Height { get; set; }

        public string Interests { get; set; }

        [Range(12, 65, ErrorMessage = GlobalConstants.ErrorInputMessage)]
        public int Age { get; set; }

        public ActityLevel Actity { get; set; }
    }
}