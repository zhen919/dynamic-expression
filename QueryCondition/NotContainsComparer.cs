using System.Linq.Expressions;
using DynamicExpression.Enums;
using DynamicExpression.Models;
using DynamicExpression.Dtos;
using DynamicExpression.Utils;

namespace DynamicExpression.QueryCondition;

public class NotContainsComparer<T> : ComparerBase<T> where T : Entity
{
    public override Condition Condition
    {
        get; set;
    } = Condition.NotContains;

    public override IQueryable<T> DoComparer(QueryConditionArgs queryCondition, IQueryable<T> query, string propertyName, Type propertyType)
    {
        if (!string.IsNullOrWhiteSpace(queryCondition.Value1?.ToString()))
        {
            var value = TypeHelper.ChangeType(queryCondition.Value1, propertyType);

            var expr = Expression.Parameter(typeof(T));

            var exprRight = Expression.Constant(value);
            var method = propertyType.GetMethod(nameof(string.Contains), new[] { typeof(string) });

            var call = Expression.Not(Expression.Call(Expression.Property(expr, propertyName), method, exprRight));

            var lambda = Expression.Lambda<Func<T, bool>>(call, expr);
            query = query.Where(lambda);
        }

        return query;
    }
}