using InternLog.Domain.Entities.Base;
using System.Reflection;

namespace InternLog.Data.Extensions
{
	public static class EntityExtensions
	{
		public static List<Type> GetEntitiesFromAssembly(this Assembly assembly)
		{
			return assembly.ExportedTypes
					.Where(type => !type.IsAbstract
								&& !type.IsInterface
								&& type.IsAssignableTo(typeof(IEntity))).ToList();
		}
	}
}