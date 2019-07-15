using Olympia.Data.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Olympia.Data.Models.BindingModels.Client
{
    public class ClientHeightWeightBindingModel
    {
        public double Weight { get; set; }

        public double Height { get; set; }

        public ActityLevel Activity { get; set; }
    }
}
