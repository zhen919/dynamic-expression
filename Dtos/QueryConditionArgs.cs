using DynamicExpression.Enums;

namespace DynamicExpression.Dtos;

public class QueryConditionArgs
{
    public Condition Condition
    {
        get; set;
    }

    public object Value1
    {
        get; set;
    }

    public object Value2
    {
        get; set;
    }
}