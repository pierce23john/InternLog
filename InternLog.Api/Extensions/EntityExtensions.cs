using InternLog.Api.Domain.Entities.Base;
using System.Reflection;

namespace InternLog.Api.Extensions
{
    public class EntityExtensions
    {
        public static List<Type> GetEntitiesFromAssembly(Assembly assembly)
        {
            return assembly.ExportedTypes
                    .Where(type => !type.IsAbstract
                                && !type.IsInterface
                                && type.IsAssignableTo(typeof(IEntity))).ToList();
        }
    }
}
