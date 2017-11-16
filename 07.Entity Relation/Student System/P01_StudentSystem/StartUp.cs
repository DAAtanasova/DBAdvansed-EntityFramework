using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem
{
    public class StartUp
    {
        static void Main()
        {
            var db = new StudentSystemContext();
            //db.Database.EnsureCreated();
            ResetDatabase(db);
        }

        private static void ResetDatabase(StudentSystemContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.Migrate();
            Seed(db);
        }

        private static void Seed(StudentSystemContext db)
        {
            var students = new List<Student>
            {
                new Student{ Name = "Pesho"},
                new Student{ Name = "Merry"},
                new Student{ Name = "Ivan", PhoneNumber = "0889876543"},
                new Student{ Name = "Nikola"},
                new Student{ Name = "Katq", PhoneNumber = "0886854333"}
            };
            db.Students.AddRange(students);

            var courses = new List<Course>
            {
                new Course{Name = "Basics", StartDate =DateTime.Now, EndDate = (DateTime.Parse("5/2/2018")), Price = 100.00m},
                new Course { Name = "Fundamentals",Price = 260.00m },
                new Course { Name = "Java", Price = 180.00m },
                new Course { Name = "Db Entity Framework", Price = 269.00m }
            };
            db.Courses.AddRange(courses);

            var resources = new List<Resource>
            {
                new Resource{Name = "Introduction", ResourceType = ResourceType.Document, Course = courses[0]},
                new Resource{Name = "Basic Functionality", ResourceType = ResourceType.Presentation, Course = courses[2]},
                new Resource{Name = "Exam Preparation", ResourceType = ResourceType.Document, Course = courses[1]},
                new Resource{Name = "Loops", ResourceType = ResourceType.Video, Course = courses[1]},
                new Resource{Name = "Entity Relation", ResourceType = ResourceType.Document, Course = courses[3]},
                new Resource{Name = "Entity Relation", ResourceType = ResourceType.Video, Course = courses[3]},
                new Resource{Name = "Install Visual Studio", ResourceType = ResourceType.Other, Course = courses[0]},
                new Resource{Name = "Introduction", ResourceType = ResourceType.Presentation, Course = courses[1]},
                new Resource{Name = "Exercise", ResourceType = ResourceType.Document, Course = courses[2]},
                new Resource{Name = "Condition Statements", ResourceType = ResourceType.Video, Course = courses[0]}
            };
            db.Resources.AddRange(resources);

            var hwsubmissions = new List<Homework>
            {
                new Homework{ContentType = ContentType.Pdf, Course = courses[0], Student = students[0]},
                new Homework{ContentType = ContentType.Application, Course = courses[2], Student = students[1]},
                new Homework{ContentType = ContentType.Zip, Course = courses[3], Student = students[4]},
                new Homework{ContentType = ContentType.Zip, Course = courses[1], Student = students[2]},
                new Homework{ContentType = ContentType.Application, Course = courses[3], Student = students[1]},
                new Homework{ContentType = ContentType.Pdf, Course = courses[0], Student = students[1]},
                new Homework{ContentType = ContentType.Pdf, Course = courses[0], Student = students[3]},
                new Homework{ContentType = ContentType.Zip, Course = courses[1], Student = students[0]},
                new Homework{ContentType = ContentType.Application, Course = courses[3], Student = students[4]}
            };
            db.HomeworkSubmissions.AddRange(hwsubmissions);

            var studentCourses = new List<StudentCourse>
            {
                new StudentCourse{Student = students[0], Course= courses[0]},
                new StudentCourse{Student = students[0], Course= courses[1]},
                new StudentCourse{Student = students[1], Course= courses[0]},
                new StudentCourse{Student = students[2], Course= courses[1]},
                new StudentCourse{Student = students[3], Course= courses[0]},
                new StudentCourse{Student = students[4], Course= courses[3]},
                new StudentCourse{Student = students[4], Course= courses[1]},
                new StudentCourse{Student = students[2], Course= courses[3]},
                new StudentCourse{Student = students[1], Course= courses[2]},
            };
            db.StudentCourses.AddRange(studentCourses);
            db.SaveChanges();
        }
    }
}
