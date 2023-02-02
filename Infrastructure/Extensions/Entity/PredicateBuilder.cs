using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Extensions
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> FromString<T>(this Expression<Func<T, bool>> predicate, string dynamicQuery)
        {
            JArray Query = JArray.Parse(dynamicQuery);
            foreach (JToken root in Query)
            {
                string column = "";
                object value = "";
                object[] values = { };
                string method = "";
                string condition = "";
                foreach (JProperty inner in root)
                {
                    switch (inner.Name)
                    {
                        case "column":
                            column = inner.Value.ToString();
                            break;
                        case "value":
                            if (inner.Value is JArray)
                                values = ((JArray)inner.Value).ToObject<object[]>();
                            else
                                value = inner.Value.ToObject<object>();
                            break;
                        case "method":
                            method = inner.Value.ToString();
                            break;
                        case "condition":
                            condition = inner.Value.ToString();
                            break;
                    }
                }

                var currentPredicate = False<T>();

                switch (method.Trim())
                {
                    case "=":
                    case "==":
                    case "Equals":
                        if (values.Length > 0)
                            currentPredicate = currentPredicate.In(column, values);
                        else
                            currentPredicate = currentPredicate.Equal(column, value?.ToString());
                        break;
                    case "greaterthanorequal":
                    case ">=":
                        currentPredicate = currentPredicate.GreaterThanOrEqual(column, value);
                        break;
                    case ">":
                    case "greaterthan":
                        currentPredicate = currentPredicate.GreaterThan(column, value);
                        break;
                    case "<=":
                    case "lessthanorequal":
                        currentPredicate = currentPredicate.LessThanOrEqual(column, value);
                        break;
                    case "<":
                    case "lessthan":
                        currentPredicate = currentPredicate.LessThan(column, value);
                        break;
                    case "contains":
                        if (values.Length > 0)
                            currentPredicate = currentPredicate.In(column, values);
                        else
                            currentPredicate = currentPredicate.Contains(column, value.ToString());
                        break;
                    case "in":
                        currentPredicate = currentPredicate.In(column, values);
                        break;
                }

                switch (condition.ToLower())
                {
                    case "and":
                        predicate = predicate.And(currentPredicate);
                        break;
                    case "or":
                        predicate = predicate.Or(currentPredicate);
                        break;
                    default:
                        predicate = currentPredicate;
                        break;
                }
            }
            return predicate;
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> GenericPredicate<T>(this Expression<Func<T, bool>> expr1, string memberName, string searchValue, string methodName)
        {
            var parameter = Expression.Parameter(typeof(T), "m");
            var member = Expression.PropertyOrField(parameter, memberName);
            var body = Expression.Call(
                member,
                methodName,
                Type.EmptyTypes, // no generic type arguments
                Expression.Constant(searchValue)
            );
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public static Expression<Func<T, bool>> GenericPredicate<T>(this Expression<Func<T, bool>> expr1, string memberName, string searchValue, MethodInfo method)
        {
            var parameter = Expression.Parameter(typeof(T), "m");
            Expression member = parameter;

            foreach (string field in memberName.Split('.'))
                member = Expression.PropertyOrField(member, field);

            if (member.Type == typeof(bool) || member.Type == typeof(bool?))
            {
                member = Expression.Coalesce(member, Expression.Constant(false));
                bool value = false;
                bool.TryParse(searchValue, out value);
                if (!value)
                    member = Expression.Not(member);
                return Expression.Lambda<Func<T, bool>>(member, parameter);
            }

            else if (member.Type != typeof(string))
                member = Expression.Call(member, typeof(object).GetMethod("ToString"));

            var body = Expression.Call(
                member,
                method,
                Expression.Constant(Convert.ChangeType(searchValue, typeof(string)))
            );
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }


        static LambdaExpression CreateExpression(Type type, string propertyName)
        {
            var param = Expression.Parameter(type, "x");
            Expression body = param;
            foreach (var member in propertyName.Split('.'))
            {
                body = Expression.PropertyOrField(body, member);
            }
            return Expression.Lambda(body, param);
        }

        public static Expression<Func<T, bool>> GreaterThan<T>(this Expression<Func<T, bool>> expr1, string memberName, object searchValue)
        {
            var parameter = Expression.Parameter(typeof(T), "m");
            var member = Expression.PropertyOrField(parameter, memberName);
            searchValue = ChangeType(searchValue, member.Type);
            var body = Expression.GreaterThan(
               member,
               Expression.Constant(searchValue, member.Type)
           );
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public static Expression<Func<T, bool>> GreaterThanOrEqual<T>(this Expression<Func<T, bool>> expr1, string memberName, object searchValue)
        {
            var parameter = Expression.Parameter(typeof(T), "m");
            var member = Expression.PropertyOrField(parameter, memberName);
            searchValue = ChangeType(searchValue, member.Type);
            var body = Expression.GreaterThanOrEqual(
               member,
               Expression.Constant(searchValue, member.Type)
           );
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
        public static Expression<Func<T, bool>> LessThan<T>(this Expression<Func<T, bool>> expr1, string memberName, object searchValue)
        {
            var parameter = Expression.Parameter(typeof(T), "m");
            var member = Expression.PropertyOrField(parameter, memberName);
            searchValue = ChangeType(searchValue, member.Type);
            var body = Expression.LessThan(
               member,
               Expression.Constant(searchValue, member.Type)
           );
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public static Expression<Func<T, bool>> LessThanOrEqual<T>(this Expression<Func<T, bool>> expr1, string memberName, object searchValue)
        {
            var parameter = Expression.Parameter(typeof(T), "m");
            var member = Expression.PropertyOrField(parameter, memberName);
            searchValue = ChangeType(searchValue, member.Type);
            var body = Expression.LessThanOrEqual(
               member,
               Expression.Constant(searchValue, member.Type)
           );
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public static Expression<Func<T, bool>> In<T>(this Expression<Func<T, bool>> expr1, string memberName, object searchValue)
        {
            var parameter = Expression.Parameter(typeof(T));
            Expression member = parameter;
            foreach (string field in memberName.Split('.'))
                member = Expression.PropertyOrField(member, field);

            var ce = Expression.Constant(searchValue);
            var call = Expression.Call(typeof(Enumerable), "Contains", new[] { typeof(object) }, ce, member);
            return Expression.Lambda<Func<T, bool>>(call, parameter);
        }

        public static Expression<Func<T, bool>> Equal<T>(this Expression<Func<T, bool>> expr1, string memberName, string searchValue)
        {
            MethodInfo currentMethod = typeof(string).GetMethod("Equals", new[] { typeof(string) });
            return GenericPredicate(expr1, memberName, searchValue, currentMethod);
        }

        public static Expression<Func<T, bool>> Contains<T>(this Expression<Func<T, bool>> expr1, string memberName, string searchValue)
        {
            MethodInfo currentMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            return GenericPredicate(expr1, memberName, searchValue, currentMethod);
        }


        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return Convert.ChangeType(value, t);
        }
    }
}