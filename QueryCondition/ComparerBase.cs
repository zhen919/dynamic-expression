using DynamicExpression.Enums;
using DynamicExpression.Models;
using DynamicExpression.Dtos;

namespace DynamicExpression.QueryCondition;

public abstract class ComparerBase<T> where T : Entity
{
    public abstract Condition Condition
    {
        get; set;
    }
    
    public abstract IQueryable<T> DoComparer(QueryConditionArgs queryConditionArgs, IQueryable<T> query, string propertyName, Type propertyType);
}