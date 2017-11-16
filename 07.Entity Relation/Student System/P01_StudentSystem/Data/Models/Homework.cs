using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    public class Homework
    {
        public Homework()
        {
        }
        public Homework(ContentType type, Course course, Student student)
        {
            this.ContentType = type;
            this.Course = course;
            this.Student = student;
        }
        public Homework(ContentType type, Course course, Student student, DateTime time)
        {
            this.ContentType = type;
            this.Course = course;
            this.Student = student;
            this.SubmissionTime = time;
        }
        public int HomeworkId { get; set; }
        public string Content { get; set; }
        public ContentType ContentType { get; set; }
        public DateTime? SubmissionTime { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
