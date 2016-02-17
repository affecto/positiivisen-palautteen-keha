﻿using System;
using System.Collections.Generic;

namespace Affecto.PositiveFeedback.Api
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public IEnumerable<string> TextFeedback { get; set; }
        public bool HasPicture { get; set; }
    }
}