using System;
using System.Collections.Generic;
using ConsoleApp1;

namespace ConsoleApp1
{
    class Program
    {


        static void Main(string[] args)
        {

            using var context = new StudentContext();
            var storage = new StudentDBStorage(context);
            Group g1 = new Group { GroupName = "ФИИТ" };
            Group g2 = new Group { GroupName = "МОАИС" };
            Group g3 = new Group { GroupName = "ПМИ" };

            Student s1 = new Student() { StudentName = "Ioan", StudentSurname = "Surreal", Age = 18, Group = g1 };
            Student s2 = new Student() { StudentName = "John", StudentSurname = "Doe", Age = 17, Group = g2 };
            Student s3 = new Student() { StudentName = "Alex", StudentSurname = "Diaz", Age = 17, Group = g3 };

            storage.AddStudent(s1);
            storage.AddStudent(s2);
            storage.AddStudent(s3);

            bool go = true;
            while (go)
            {
                var groups = storage.GetGroups();
                foreach (var group in groups)
                {
                    Console.WriteLine($"{group.GroupName} ({group.Students.Count})");
                    foreach (var student in group.Students.OrderBy(s => s.StudentSurname))
                    {
                        Console.WriteLine($"{student.StudentId} - {student.StudentName} {student.StudentSurname}");
                    }
                    Console.WriteLine();
                }
                
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - добавить студента");
                Console.WriteLine("2 - удалить студента");
                Console.WriteLine("3 - завершение работы");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddStudent(storage);
                        break;
                    case "2":
                        RemoveStudent(storage);
                        break;
                    case "3":
                        go = false;
                        break;
                    default:
                        Console.WriteLine("Некорректный ввод");
                        break;
                }
            }
        }

        static void AddStudent(StudentDBStorage storage)
        {
            Console.WriteLine("Выберите номер группы:");
            var groups = storage.GetGroups();
            foreach (var group in groups)
            {
                Console.WriteLine($"{group.GroupId} - {group.GroupName}");
            }
            int groupId = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите имя и фамилию студента:");
            string studentName = Console.ReadLine();

            var student = new Student
            {
                StudentName = studentName,
                GroupId = groupId
            };

            storage.AddStudent(student);
            Console.WriteLine("Студент успешно добавлен.");

            Console.WriteLine();
            
        }

        static void RemoveStudent(StudentDBStorage storage)
        {
            Console.WriteLine("Выберите номер группы:");
            var groups = storage.GetGroups();
            foreach (var Group in groups)
            {
                Console.WriteLine($"{Group.GroupId} - {Group.GroupName}");
            }
            int groupId = int.Parse(Console.ReadLine());

            var group = groups.FirstOrDefault(g => g.GroupId == groupId);

            if (group != null)
            {
                Console.WriteLine("Выберите номер студента:");
                foreach (var student in group.Students)
                {
                    Console.WriteLine($"{student.StudentId} - {student.StudentName} {student.StudentSurname}");
                }
                int studentId = int.Parse(Console.ReadLine());

                storage.removeStudent(studentId);
                Console.WriteLine("Студент успешно удален.");
            }
            else
            {
                Console.WriteLine("Группа не найдена.");
            }

            Console.WriteLine();
            
        }
    }
}
