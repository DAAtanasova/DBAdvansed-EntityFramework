﻿using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    public class Course
    {
        public Course()
        {
        }
        public Course(string name)
        {
            this.Name = name;
        }
        public Course(string name, decimal price)
        {
            this.Name = name;
            this.Price = price;
        }
        public Course(string name, DateTime startDate, DateTime endDate, decimal price)
        {
            this.Name = name;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Price = price;
        }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Price { get; set; }

        public ICollection<StudentCourse> StudentsEnrolled { get; set; }
        public ICollection<Resource> Resources { get; set; }
        public ICollection<Homework> HomeworkSubmissions { get; set; }

    }
}
