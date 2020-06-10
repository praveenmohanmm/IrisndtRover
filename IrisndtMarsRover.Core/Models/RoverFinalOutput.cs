﻿using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IrisndtMarsRover.Core.Models
{


    public partial class RoverFinalPoints
    {
        [JsonProperty("FinalPoints")]
        public string FinalPoints { get; set; }

        [JsonProperty("FlowPath")]
        public FlowPath[] FlowPath { get; set; }
    }

    public partial class FlowPath
    {
        [JsonProperty("XPos")]
        public long XPos { get; set; }

        [JsonProperty("YPos")]
        public long YPos { get; set; }
    }
}