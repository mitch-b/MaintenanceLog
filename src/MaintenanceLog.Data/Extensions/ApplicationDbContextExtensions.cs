using MaintenanceLog.Data.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MaintenanceLog.Data.Extensions
{
    public static class SqlDefaultValueAttributeConvention
    {
        public static void Apply(ModelBuilder builder)
        {
            ConventionBehaviors
                .SetSqlValueForPropertiesWithAttribute<SqlDefaultValueAttribute>(builder, x => x.DefaultValue);
        }
    }

    /// <summary>
    /// https://stackoverflow.com/a/66257107
    /// </summary>
    public static class ConventionBehaviors
    {
        public static void SetSqlValueForPropertiesWithAttribute<TAttribute>(ModelBuilder builder, Func<TAttribute, string> lambda) where TAttribute : class
        {
            SetPropertyValue<TAttribute>(builder).ForEach((x) =>
            {
                x.Item1.SetDefaultValueSql(lambda(x.Item2));
            });
        }

        private static List<Tuple<IMutableProperty, TAttribute>> SetPropertyValue<TAttribute>(ModelBuilder builder) where TAttribute : class
        {
            var propsToModify = new List<Tuple<IMutableProperty, TAttribute>>();
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties();
                foreach (var property in properties)
                {
                    if (property is null) continue;
                    var attribute = property.PropertyInfo?
                        .GetCustomAttributes(typeof(TAttribute), false)
                        .FirstOrDefault() as TAttribute;
                    if (attribute is not null)
                    {
                        propsToModify.Add(new Tuple<IMutableProperty, TAttribute>(property, attribute));
                    }
                }
            }
            return propsToModify;
        }
    }
}
