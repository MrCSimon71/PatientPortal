using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PDDS.PatientData.Core.Helpers
{
    public class ExpressionBuilder
    {
        public static Expression<Func<T, bool>> BuildExpression<T>(List<SearchCriteria> criteria)
        {
            if (criteria.Count == 0)
            {
                return null;
            }

            var expressionList = new List<Expression>();
            ParameterExpression pxe = Expression.Parameter(typeof(T), "t");

            foreach (var c in criteria)
            {
                Expression exp = null;
                Expression prop = null;

                if (c.FieldName.Contains("."))
                {
                    var subObject = c.FieldName.Split('.');

                    Expression subProperty = Expression.Property(pxe, subObject[0]);
                    prop = Expression.Property(subProperty, subObject[1]);
                    exp = Expression.Equal(prop, BuildConstantExpression(c.LookupValue, prop.Type));
                }
                else
                {
                    prop = Expression.Property(pxe, c.FieldName);
                }

                switch (c.ComparisonOperator)
                {
                    case "eq":
                        var cexp = BuildConstantExpression(c.LookupValue, prop.Type);
                        exp = Expression.Equal(prop, cexp);
                        break;
                    case "ct":
                        MethodInfo containMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
                        Expression containsExpression = Expression.Constant(c.LookupValue, typeof(string));
                        exp = Expression.Call(prop, containMethod, containsExpression);
                        break;
                    case "sw":
                        MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
                        Expression startsWithExpression = Expression.Constant(c.LookupValue, typeof(string));
                        exp = Expression.Call(prop, startsWithMethod, startsWithExpression);
                        break;
                    case "ew":
                        MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
                        Expression endsWithExpression = Expression.Constant(c.LookupValue, typeof(string));
                        exp = Expression.Call(prop, endsWithMethod, endsWithExpression);
                        break;
                    default:
                        throw new NotSupportedException();
                }

                expressionList.Add(exp);
            }

            if (expressionList.Count == 1)
            {
                return Expression.Lambda<Func<T, bool>>(expressionList[0], pxe);
            }
            else
            {
                BinaryExpression combinedExpressions = Expression.AndAlso(expressionList[0], expressionList[1]);

                for (var i = 2; i < expressionList.Count; i++)
                {
                    combinedExpressions = Expression.AndAlso(combinedExpressions, expressionList[i]);
                }

                var lambda = Expression.Lambda<Func<T, bool>>(combinedExpressions, pxe);

                return lambda;
            }
        }

        public static Expression<Func<T, bool>> BuildAndAppendWhereDeletedClause<T>(Expression<Func<T, bool>> existingWhereClause = null)
        {
            ParameterExpression pxe = Expression.Parameter(typeof(T), "t");
            Expression deletedProperty = Expression.Property(pxe, "Deleted");

            Expression leftSide = Expression.Parameter(typeof(string), "t.Deleted");
            Expression rightSide = Expression.Convert(Expression.Constant("false"), typeof(string));

            //Expression deletedExpression = Expression.Equal(deletedProperty, BuildConstantExpression(false, deletedProperty.Type));
            Expression deletedExpression = Expression.Equal(leftSide, rightSide);

            if (existingWhereClause != null)
            {
                BinaryExpression combinedExpressions = Expression.AndAlso(existingWhereClause, deletedExpression);
                return Expression.Lambda<Func<T, bool>>(combinedExpressions, pxe);
            }

            return Expression.Lambda<Func<T, bool>>(deletedExpression, pxe);
        }

        private static UnaryExpression BuildConstantExpression(dynamic value, Type propertyType)
        {
            if (propertyType.IsClass && propertyType.Namespace == "CentralValleyBikes.Domain.Entities")
            {
                return Expression.Convert(Expression.Constant(value.ToString()), typeof(string));
            }
            else if (propertyType.BaseType == typeof(object) || propertyType.BaseType == typeof(ValueType))
            {
                switch (propertyType.Name.ToLower())
                {
                    case "string":
                        return Expression.Convert(Expression.Constant(value.ToString()), typeof(string));
                    case "number":
                        return Expression.Convert(Expression.Constant(Convert.ToInt16(value.GetString())), typeof(int));
                    case "int16":
                        return Expression.Convert(Expression.Constant(Convert.ToInt16(value.ToString())), typeof(Int16));
                    case "int32":
                        return Expression.Convert(Expression.Constant(Convert.ToInt32(value.ToString())), typeof(Int32));
                    case "boolean":
                        return Expression.Convert(Expression.Constant(Convert.ToBoolean(value.GetString())), typeof(bool));
                    case "nullable`1":
                        var filter1 = Expression.Constant(Convert.ChangeType(value, propertyType.GetGenericArguments()[0]));
                        return Expression.Convert(filter1, propertyType);
                }
            }
            else if (propertyType.BaseType == typeof(Enum))
            {
                return Expression.Convert(Expression.Constant(Enum.Parse(propertyType, value)), propertyType);
            }

            return null;
        }
    }
}
