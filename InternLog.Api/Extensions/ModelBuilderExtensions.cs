using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InternLog.Api.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void RegisterEntitiesFromAssembly(this ModelBuilder builder, Assembly assembly)
        {
            var entityTypes = EntityExtensions.GetEntitiesFromAssembly(assembly);

            entityTypes.ForEach(entity => builder.Entity(entity)
                                                 .ToTable(entity.Name.Pluralize(inputIsKnownToBeSingular: false)));
        }
    }
}
