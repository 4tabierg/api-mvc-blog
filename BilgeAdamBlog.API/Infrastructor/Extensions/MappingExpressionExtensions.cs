﻿using AutoMapper;
using System.Reflection;

namespace BilgeAdamBlog.API.Infrastructor.Extensions
{
    public static class MappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flags);
            foreach (var property in destinationProperties)
            {
                if (sourceType.GetProperty(property.Name,flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
        }
    }
}
