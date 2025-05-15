using DynamicExpression.Models;
using DynamicExpression.Dtos;
using DynamicExpression.Utils;

namespace DynamicExpression.QueryCondition;

public static class QueryConditionExtensions<T> where T : Entity
{
    public static IQueryable<T> DoQuery(IQueryable<T> query, QueryConditionDto input)
    {
        var type = input.GetType();
        var props = Activator.CreateInstance(typeof(T)).GetType().GetProperties();

        foreach (var propertyInfo in type.GetProperties())
        {
            var queryConditionArgs = propertyInfo.GetValue(input, null) as QueryConditionArgs;
            if (queryConditionArgs != null)
            {
                var propertyType = props.FirstOrDefault(x => x.Name == propertyInfo.Name)?.PropertyType;
                
                if (propertyType is null) continue;
                
                query = ComparerExtensions<T>.DoComparer(queryConditionArgs, query, propertyInfo.Name, propertyType);
            }
        }
        
        return query;
    }
}