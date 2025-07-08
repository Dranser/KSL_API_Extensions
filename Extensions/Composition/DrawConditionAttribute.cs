using System;

namespace KSL.API.Extensions
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DrawConditionAttribute : Attribute
    {
        public DrawConditionType Condition { get; }

        public DrawConditionAttribute(DrawConditionType condition)
        {
            Condition = condition;
        }
    }
}
