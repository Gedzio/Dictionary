using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dictionary.Domain.Utils
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder RegisterEntitiesConfigurationFromAssemblyOfType<TType>(this ModelBuilder modelBuilder)
        {
            foreach (var configuration in GetConfigurations<TType>())
                modelBuilder.ApplyConfiguration(configuration);

            return modelBuilder;
        }

        private static IEnumerable<dynamic> GetConfigurations<TType>()
        {
            var assembly = typeof(TType).Assembly;

            foreach (var type in assembly.DefinedTypes)
            {
                if (!type.IsAbstract && type.GetConstructor(Type.EmptyTypes) != null && type.ImplementedInterfaces.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                {
                    yield return Activator.CreateInstance(type);
                }
            }
        }
    }
}
