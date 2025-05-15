using DynamicExpression.Enums;

namespace DynamicExpression.Models;

public class Student : Entity
{
    public string Name { get; set; }

    public Gender Gender { get; set; }
    
    public string StudentNumber { get; set; }
    
    public DateTime AdmissionTime { get; set; }

    public static List<Student> GetStudents()
    {
        return new List<Student>()
        {
            new Student()
            {
                Id = 1, Name = "张三", Gender = Gender.Male, StudentNumber = "202406012312322",
                AdmissionTime = new DateTime(2022, 6, 1)
            },
            new Student()
            {
                Id = 2, Name = "李四", Gender = Gender.Male, StudentNumber = "202306018980768",
                AdmissionTime = new DateTime(2023, 6, 2)
            },
            new Student()
            {
                Id = 3, Name = "王天天", Gender = Gender.Male, StudentNumber = "202409013556213",
                AdmissionTime = new DateTime(2024, 9, 1)
            },
            new Student()
            {
                Id = 4, Name = "梦天", Gender = Gender.Female, StudentNumber = "202409015643234",
                AdmissionTime = new DateTime(2024, 9, 1)
            },
            new Student()
            {
                Id = 5, Name = "宋石", Gender = Gender.Female, StudentNumber = "202409017895645",
                AdmissionTime = new DateTime(2024, 9, 1)
            },
            new Student()
            {
                Id = 6, Name = "唐烟", Gender = Gender.Secret, StudentNumber = "202406045677856",
                AdmissionTime = new DateTime(2024, 10, 1)
            },
        };
    }

}