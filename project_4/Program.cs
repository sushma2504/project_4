using System;
using System.Collections.Generic;

// Singleton design pattern
public class SchoolData
{
    private static SchoolData instance;
    public List<Student> Students { get; private set; }
    public List<Teacher> Teachers { get; private set; }
    public List<Subject> Subjects { get; private set; }

    private SchoolData()
    {
        Students = new List<Student>();
        Teachers = new List<Teacher>();
        Subjects = new List<Subject>();
    }

    public static SchoolData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SchoolData();
            }
            return instance;
        }
    }
}

// Factory design pattern
public static class SchoolFactory
{
    public static Student CreateStudent(string name, string classSection)
    {
        return new Student(name, classSection);
    }

    public static Teacher CreateTeacher(string name, string classSection)
    {
        return new Teacher(name, classSection);
    }

    public static Subject CreateSubject(string name, string subjectCode, Teacher teacher)
    {
        return new Subject(name, subjectCode, teacher);
    }
}

public class Student
{
    public string Name { get; private set; }
    public string ClassSection { get; private set; }

    public Student(string name, string classSection)
    {
        Name = name;
        ClassSection = classSection;
    }
}

public class Teacher
{
    public string Name { get; private set; }
    public string ClassSection { get; private set; }

    public Teacher(string name, string classSection)
    {
        Name = name;
        ClassSection = classSection;
    }
}

public class Subject
{
    public string Name { get; private set; }
    public string SubjectCode { get; private set; }
    public Teacher Teacher { get; private set; }

    public Subject(string name, string subjectCode, Teacher teacher)
    {
        Name = name;
        SubjectCode = subjectCode;
        Teacher = teacher;
    }
}

class Program
{
    static void Main()
    {
        // Filling up the lists with data
        FillData();

        // Displaying students in a class
        DisplayStudentsInClass("10A");

        // Displaying subjects taught by a teacher
        DisplaySubjectsTaughtByTeacher("Smitha Dash");
    }

    static void FillData()
    {
        SchoolData schoolData = SchoolData.Instance;

        // Create students
        schoolData.Students.Add(SchoolFactory.CreateStudent("Nupur Verma", "10A"));
        schoolData.Students.Add(SchoolFactory.CreateStudent("Sushma Mahato", "10A"));
        schoolData.Students.Add(SchoolFactory.CreateStudent("Nutan Verma", "11B"));

        // Create teachers
        schoolData.Teachers.Add(SchoolFactory.CreateTeacher("Shreya Lal", "10A"));
        schoolData.Teachers.Add(SchoolFactory.CreateTeacher("Smitha Dash", "11B"));

        // Create subjects
        schoolData.Subjects.Add(SchoolFactory.CreateSubject("Math", "MATH101", schoolData.Teachers[0]));
        schoolData.Subjects.Add(SchoolFactory.CreateSubject("Science", "SCI202", schoolData.Teachers[1]));
    }

    static void DisplayStudentsInClass(string classSection)
    {
        Console.WriteLine($"Students in Class {classSection}:\n");

        foreach (var student in SchoolData.Instance.Students)
        {
            if (student.ClassSection.Equals(classSection, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Name: {student.Name}");
            }
        }

        Console.WriteLine();
    }

    static void DisplaySubjectsTaughtByTeacher(string teacherName)
    {
        Console.WriteLine($"Subjects taught by {teacherName}:\n");

        Teacher teacher = SchoolData.Instance.Teachers.Find(t => t.Name.Equals(teacherName, StringComparison.OrdinalIgnoreCase));

        if (teacher != null)
        {
            foreach (var subject in SchoolData.Instance.Subjects)
            {
                if (subject.Teacher == teacher)
                {
                    Console.WriteLine($"Subject: {subject.Name}, Subject Code: {subject.SubjectCode}");
                }
            }
        }
        else
        {
            Console.WriteLine("Teacher not found.");
        }

        Console.ReadLine();

    }
}