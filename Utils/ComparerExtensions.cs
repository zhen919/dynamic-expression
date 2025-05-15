using DynamicExpression.Enums;
using DynamicExpression.Models;
using DynamicExpression.QueryCondition;
using DynamicExpression.Dtos;

namespace DynamicExpression.Utils;

public static class ComparerExtensions<T> where T : Entity
{
    
    private static readonly Dictionary<Condition, ComparerBase<T>> Comparers = new ()
    {
        { Condition.Equals, new EqualsComparer<T>() },
        { Condition.Between, new BetweenComparer<T>() },
        { Condition.NotEquals, new NotEqualsComparer<T>() },
        { Condition.Contains, new ContainsComparer<T>() },
        { Condition.NotContains, new NotContainsComparer<T>() },
        { Condition.GreaterThan, new GreaterThanComparer<T>() },
        { Condition.LessThan, new LessThanComparer<T>() },
        { Condition.GreaterThanOrEquals, new GreaterThanOrEqualsComparer<T>() },
        { Condition.LessThanOrEquals, new LessThanOrEqualsComparer<T>() }
    };


    public static IQueryable<T> DoComparer(QueryConditionArgs queryConditionArgs, IQueryable<T> query, string propertyName, Type propertyType)
    {
        if (Comparers.ContainsKey(queryConditionArgs.Condition))
        {
            query = Comparers[queryConditionArgs.Condition].DoComparer(queryConditionArgs, query, propertyName, propertyType);
        }

        return query;
    }
    
}