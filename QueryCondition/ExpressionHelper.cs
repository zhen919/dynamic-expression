using System.Linq.Expressions;
using DynamicExpression.Utils;

namespace DynamicExpression.QueryCondition;

public static class ExpressionHelper
{
    public static Expression Equal(Expression e1, Expression e2)
    {
        ConvertNullableTypes(ref e1, ref e2);
        return Expression.Equal(e1, e2);
    }
    
    public static Expression NotEqual(Expression e1, Expression e2)
    {
        ConvertNullableTypes(ref e1, ref e2);
        return Expression.NotEqual(e1, e2);
    }
    
    public static Expression GreaterThan(Expression e1, Expression e2)
    {
        ConvertNullableTypes(ref e1, ref e2);
        return Expression.GreaterThan(e1, e2);
    }
    
    public static Expression LessThanOrEqual(Expression e1, Expression e2)
    {
        ConvertNullableTypes(ref e1, ref e2);
        return Expression.LessThanOrEqual(e1, e2);
    }
    
    public static Expression LessThan(Expression e1, Expression e2)
    {
        ConvertNullableTypes(ref e1, ref e2);
        return Expression.LessThan(e1, e2);
    }
    
    public static Expression GreaterThanOrEqual(Expression e1, Expression e2)
    {
        ConvertNullableTypes(ref e1, ref e2);
        return Expression.GreaterThanOrEqual(e1, e2);
    }
    
    
    private static void ConvertNullableTypes(ref Expression e1, ref Expression e2)
    {
        if (e1.Type.IsNullableType() && !e2.Type.IsNullableType())
        {
            e2 = Expression.Convert(e2, e1.Type);
        }
        else if (!e1.Type.IsNullableType() && e2.Type.IsNullableType())
        {
            e1 = Expression.Convert(e1, e2.Type);
        }
    }
}