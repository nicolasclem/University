﻿using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface IStudentsService
    {
        IEnumerable<Student> GetStudensWithCourses();
        IEnumerable<Student> GetStudensWithNoCourses();
        

    }
}
