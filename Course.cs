﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Course
    {
        public int CourseID { get; set; }  
        public string? CourseName { get; set; }

        public List<Student> Students { get; set; } = new();

    }
}
