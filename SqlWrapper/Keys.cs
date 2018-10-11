using System;
namespace SqlWrapper {
    public static class OperatorKeys {

        static public string equals = "=";
        static public string doesNotEqual = "!=";
        static public string greaterThan = ">";
        static public string lessThan = "<";
        static public string greaterThanOrEqual = ">=";
        static public string lessThanOrEqual = "<=";
        static public string between = "BETWEEN";
        static public string Like = "LIKE";
        static public string In = "IN";
        static public string AND = "AND";
        static public string OR = "OR";
        static public string NOT = "NOT";

    }

    public enum EOperatorType{

        none = 1,
        Equals = 2,
        DoesNotEqual = 3,
        GreaterThan = 4,
        LessThan = 5,
        GreaterThanOrEqual = 6,
        LessThanOrEqual = 7,
        Between = 8,
        Like = 9,
        In = 10,
        And = 11,
        Or = 12,
        Not = 13,
    }

    public enum EIsNullType {
        none = 0,
        IsNull = 1,
        IsNotNull = 2,

    }

    public enum ERenderType {


        Paramed = 1,
        NonParamed = 2,
    }

    public enum EOrderType {
        none = 0,
        Asc = 1,
        Desc = 2,
    }

    public enum EAndOrType {

        Paramable = 1,
        NonParamable = 2,
    }

    public enum EMathsType {

        MAX = 1,
        MIN = 2,
        COUNT = 3,
        AVG = 4,
        SUM = 5,
    }

    public enum EJoinType{

        Inner = 1,
        Left = 2,
        Right = 3,
        Full = 4,
        Self = 5,
        Join = 6

    }








}
