﻿using System;
using System.Collections.Generic;

namespace Weather.Models.Decanat
{
    internal class Student
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public String Patrunymic { get; set; }

        public DateTime Birthday { get; set; }

        public double Rating { get; set; }

    }

    internal class Group
    {
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
