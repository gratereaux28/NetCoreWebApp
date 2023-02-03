using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Extensions
{
    public static class ObjectExtension
    {
        public static T Cast<T>(this Object myobj)
        {
            Type objectType = myobj.GetType();
            Type target = typeof(T);
            var x = Activator.CreateInstance(target, false);
            var z = from source in objectType.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            var d = from source in target.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            List<MemberInfo> members = d.Where(memberInfo => d.Select(c => c.Name)
               .ToList().Contains(memberInfo.Name)).ToList();
            PropertyInfo propertyInfo;
            object value;
            foreach (var memberInfo in members)
            {
                propertyInfo = typeof(T).GetProperty(memberInfo.Name);
                var prop = myobj.GetType().GetProperty(memberInfo.Name);
                var currentType = memberInfo.GetType();

                if (prop != null)
                {
                    value = prop.GetValue(myobj, null);
                    propertyInfo.SetValue(x, value, null);
                }
            }
            return (T)x;
        }

        public static T CopyTo<T>(this Object myobj, T copyObj)
        {
            Type objectType = myobj.GetType();
            Type target = typeof(T);
            var x = copyObj;
            var z = from source in objectType.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            var d = from source in target.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            List<MemberInfo> members = d.Where(memberInfo => d.Select(c => c.Name)
               .ToList().Contains(memberInfo.Name)).ToList();
            PropertyInfo propertyInfo;
            object value;
            foreach (var memberInfo in members)
            {
                propertyInfo = typeof(T).GetProperty(memberInfo.Name);
                var prop = myobj.GetType().GetProperty(memberInfo.Name);
                if (prop != null)
                {
                    value = prop.GetValue(myobj, null);
                    if (value != null)
                    {
                        propertyInfo.SetValue(x, value, null);
                    }
                }
            }
            return (T)x;
        }

        public static List<T> ConvertDataTable<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }


        public static List<T> DtToModel<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.PropertyType == typeof(Int32))
                        if (pro.Name == column.ColumnName)
                            pro.SetValue(obj, Convert.ToInt32(dr[column.ColumnName]), null);
                        else
                            continue;

                    else if (pro.PropertyType == typeof(bool))
                        if (pro.Name == column.ColumnName)
                            pro.SetValue(obj, Convert.ToBoolean(dr[column.ColumnName]), null);
                        else
                            continue;

                    else if (pro.PropertyType == typeof(DateTime))
                        if (pro.Name == column.ColumnName)
                            pro.SetValue(obj, Convert.ToDateTime(dr[column.ColumnName]), null);
                        else
                            continue;

                    else
                        if (pro.Name == column.ColumnName)
                        {
                            var value = dr[column.ColumnName];

                            if (value.GetType() != typeof(System.DBNull))
                                pro.SetValue(obj, value, null);
                            else
                                pro.SetValue(obj, null, null);
                        }
                    else
                        continue;
                }
            }
            return obj;
        }

        //public static string[] UpdateColumnsName(this Type objectType, string[] columnsName)
        //{
        //    var z = objectType.GetMembers().ToList();

        //    for (int i = 0; i < columnsName.Length; i++)
        //    {
        //        var info = z.FirstOrDefault(m => m.Name.Replace("get_", "").ToLower() == columnsName[i].ToLower());
        //        if (info != null)
        //        {
        //            columnsName[i] = info.Name.Replace("get_", "");
        //        }
        //        else if (columnsName[i].Contains("."))
        //        {
        //            Type currentType = objectType;
        //            var spllitedName = columnsName[i].Split(".");
        //            List<string> newProp = new List<string>();
        //            for (int j = 0; j < spllitedName.Length; j++)
        //            {
        //                var item = spllitedName[j];
        //                info = z.FirstOrDefault(m => m.Name.Replace("get_", "").ToLower() == item.ToLower());
        //                if (info != null)
        //                {
        //                    newProp.Add(info.Name.Replace("get_", ""));
        //                    currentType = info.DeclaringType;
        //                }

        //            }
        //            columnsName[i] = newProp.Join(".");
        //        }
        //    }

        //    return columnsName;
        //}

        public static string GetValue(this Object myobj, string property)
        {
            PropertyInfo propertyInfo;
            object value = myobj;
            Type currentType = myobj.GetType();
            foreach (string field in property.Split('.'))
            {
                propertyInfo = currentType.GetProperty(field);
                var prop = value.GetType().GetProperty(propertyInfo.Name);
                value = prop.GetValue(myobj, null);
                currentType = currentType.GetType();
            }
            return value?.ToString();
        }

        public static string ToJson(this Object obj)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(obj, serializerSettings);
        }

        public static Expression<Func<T, bool>> AndAlso<T>( this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            // need to detect whether they use the same
            // parameter instance; if not, they need fixing
            ParameterExpression param = expr1.Parameters[0];
            if (ReferenceEquals(param, expr2.Parameters[0]))
            {
                // simple version
                return Expression.Lambda<Func<T, bool>>(
                    Expression.AndAlso(expr1.Body, expr2.Body), param);
            }
            // otherwise, keep expr1 "as is" and invoke expr2
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    expr1.Body,
                    Expression.Invoke(expr2, param)), param);
        }

        public static string GetFullErrorMessage(this ModelStateDictionary modelState)
        {
            var messages = new List<string>();

            foreach (var entry in modelState)
            {
                foreach (var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" | ", messages);
        }


        public static DataTable ConvertXmlElementToDataTable(this System.Xml.XmlElement xmlElement, string tagName)
        {
            System.Xml.XmlNodeList xmlNodeList = xmlElement.GetElementsByTagName(tagName);

            DataTable dt = new DataTable();
            int TempColumn = 0;
            foreach (System.Xml.XmlNode node in xmlNodeList.Item(0).ChildNodes)
            {
                TempColumn++;
                DataColumn dc = new DataColumn(node.Name, System.Type.GetType("System.String"));
                if (dt.Columns.Contains(node.Name))
                {
                    dt.Columns.Add(dc.ColumnName = dc.ColumnName + TempColumn.ToString());
                }
                else
                {
                    dt.Columns.Add(dc);
                }
            }
            int ColumnsCount = dt.Columns.Count;
            for (int i = 0; i < xmlNodeList.Count; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < ColumnsCount; j++)
                {
                    if (xmlNodeList.Item(i).ChildNodes[j] != null)
                        dr[j] = xmlNodeList.Item(i).ChildNodes[j].InnerText;
                    else
                        dr[j] = "";
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
