using System.Linq.Expressions;
using DynamicExpression.Enums;
using DynamicExpression.Models;
using DynamicExpression.Dtos;
using DynamicExpression.Utils;

namespace DynamicExpression.QueryCondition;

public class GreaterThanOrEqualsComparer<T> : ComparerBase<T> where T : Entity
{
    public override Condition Condition
    {
        get; set;
    } = Condition.GreaterThanOrEquals;

    public override IQueryable<T> DoComparer(QueryConditionArgs queryCondition, IQueryable<T> query, string propertyName, Type propertyType)
    {
        if (!string.IsNullOrWhiteSpace(queryCondition.Value1?.ToString()))
        {

            var expr = Expression.Parameter(typeof(T));
            var memberAccess = Expression.PropertyOrField(expr, propertyName);

            var value = TypeHelper.ChangeType(queryCondition.Value1, propertyType);

            var exprRight = Expression.Constant(value);
            var equalExpr = ExpressionHelper.GreaterThanOrEqual(memberAccess, exprRight);

            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(equalExpr, expr);

            query = query.Where(lambda);
        }

        return query;
    }
}