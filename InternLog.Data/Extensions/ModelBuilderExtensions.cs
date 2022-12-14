using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InternLog.Data.Extensions
{
	public static class ModelBuilderExtensions
	{
		public static void RegisterEntitiesFromAssembly(this ModelBuilder builder, Assembly assembly)
		{
			var entityTypes = assembly.GetEntitiesFromAssembly();

			entityTypes.ForEach(entity => builder.Entity(entity)
												 .ToTable(entity.Name.Pluralize(inputIsKnownToBeSingular: false)));
		}
	}
}