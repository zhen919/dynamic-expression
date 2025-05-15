// See https://aka.ms/new-console-template for more information

using System;
using DynamicExpression.Models;
using DynamicExpression.QueryCondition;
using DynamicExpression.Dtos;
using DynamicExpression.Enums;

namespace DynamicExpression
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Test Data
            var list = Student.GetStudents();

            var input = new QueryConditionDto()
            {
                AdmissionTime = new QueryConditionArgs()
                {
                    Condition = Condition.Between,
                    Value1 = new DateTime(2022, 6, 1),
                    Value2 = new DateTime(2023, 12, 1)
                },
                Gender = new QueryConditionArgs()
                {
                    Condition = Condition.Equals,
                    Value1 = Gender.Male
                },
                StudentNumber = new QueryConditionArgs()
                {
                    Condition = Condition.Contains,
                    Value1 = "2023"
                }
            };
            
            var query = list.AsQueryable();
            
            query = QueryConditionExtensions<Student>.DoQuery(query, input);

            Console.WriteLine(query.Expression);
        }
    }
    
}