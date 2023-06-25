using AutoMapper;
using System.Reflection;

namespace Infrastructure.Extensions
{
    public static class AutoMapperExtension
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
            (this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flags);

            foreach (var prop in destinationProperties)
            {
                if (sourceType.GetProperty(prop.Name, flags) == null)
                    expression.ForMember(prop.Name, opt => opt.Ignore());
            }
            return expression;
        }
    }
}