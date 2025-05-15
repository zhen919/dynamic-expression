using System.Linq.Expressions;
using DynamicExpression.Enums;
using DynamicExpression.Models;
using DynamicExpression.Dtos;
using DynamicExpression.Utils;

namespace DynamicExpression.QueryCondition;

public class BetweenComparer<T> : ComparerBase<T> where T : Entity
{
    public override Condition Condition
    {
        get; set;
    } = Condition.Between;
    
    
    public override IQueryable<T> DoComparer(QueryConditionArgs queryCondition, IQueryable<T> query, string propertyName, Type propertyType)
        {
            if (!string.IsNullOrWhiteSpace(queryCondition.Value1?.ToString()))
            {
                object value2 = null;
                if (!string.IsNullOrWhiteSpace(queryCondition.Value2?.ToString()))
                {
                    value2 = TypeHelper.ChangeType(queryCondition.Value2, propertyType);
                }

                var value = TypeHelper.ChangeType(queryCondition.Value1, propertyType);

                var expr = Expression.Parameter(typeof(T));
                var memberAccess = Expression.PropertyOrField(expr, propertyName);

                if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                {
                    // 日期时间
                    DateTime tempValue1, tempValue2;
                    var bValue1 = DateTime.TryParse(value.ToString(), out tempValue1);
                    var bValue2 = DateTime.TryParse(value2.ToString(), out tempValue2);

                    if (!bValue1)
                        return query;

                    if (!bValue2)
                    {
                        var exprRight = Expression.Constant(tempValue1);
                        var equalExpr = ExpressionHelper.GreaterThanOrEqual(memberAccess, exprRight);

                        Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(equalExpr, expr);
                        query = query.Where(lambda);
                    }
                    else
                    {
                        var exprRight = Expression.Constant(tempValue1);
                        var exprRight2 = Expression.Constant(tempValue2);

                        if (DateTime.Compare(tempValue1, tempValue2) > 0)
                        {
                            var equalExpr = ExpressionHelper.LessThanOrEqual(memberAccess, exprRight);
                            var equalExpr2 = ExpressionHelper.GreaterThanOrEqual(memberAccess, exprRight2);

                            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(equalExpr, expr);
                            query = query.Where(lambda);
                            Expression<Func<T, bool>> lambda2 = Expression.Lambda<Func<T, bool>>(equalExpr2, expr);
                            query = query.Where(lambda2);
                        }
                        else
                        {
                            var equalExpr = ExpressionHelper.GreaterThanOrEqual(memberAccess, exprRight);
                            var equalExpr2 = ExpressionHelper.LessThanOrEqual(memberAccess, exprRight2);

                            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(equalExpr, expr);
                            query = query.Where(lambda);
                            Expression<Func<T, bool>> lambda2 = Expression.Lambda<Func<T, bool>>(equalExpr2, expr);
                            query = query.Where(lambda2);
                        }
                    }
                }
                else
                {
                    // 数值
                    decimal tempValue1, tempValue2 = 0;

                    var bValue1 = decimal.TryParse(value.ToString(), out tempValue1);
                    var bValue2 = decimal.TryParse(value2.ToString(), out tempValue2);

                    if (!bValue1)
                        return query;

                    if (!bValue2)
                    {
                        var exprRight = Expression.Constant(tempValue1);
                        var equalExpr = ExpressionHelper.Equal(memberAccess, exprRight);

                        Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(equalExpr, expr);
                        query = query.Where(lambda);
                    }
                    else
                    {
                        var exprRight = Expression.Constant(tempValue1);
                        var exprRight2 = Expression.Constant(tempValue2);

                        if (tempValue1 > tempValue2)
                        {
                            var equalExpr = ExpressionHelper.LessThanOrEqual(memberAccess, exprRight);
                            var equalExpr2 = ExpressionHelper.GreaterThanOrEqual(equalExpr, exprRight2);
                            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(equalExpr, expr);
                            query = query.Where(lambda);
                            Expression<Func<T, bool>> lambda2 = Expression.Lambda<Func<T, bool>>(equalExpr2, expr);
                            query = query.Where(lambda2);
                        }
                        else
                        {
                            var equalExpr = ExpressionHelper.GreaterThanOrEqual(memberAccess, exprRight);
                            var equalExpr2 = ExpressionHelper.LessThanOrEqual(equalExpr, exprRight2);

                            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(equalExpr, expr);
                            query = query.Where(lambda);
                            Expression<Func<T, bool>> lambda2 = Expression.Lambda<Func<T, bool>>(equalExpr2, expr);
                            
                            query = query.Where(lambda2);
                        }
                    }
                }
            }

            return query;
        }
    
}