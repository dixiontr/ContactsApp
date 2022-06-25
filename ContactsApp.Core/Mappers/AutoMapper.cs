using System.Reflection;
using ContactsApp.Core.Interfaces.DTO;
using ContactsApp.Core.Interfaces.Entity;

namespace ContactsApp.Core.Mappers
{
    public static class AutoMapper
    {
        public static T AsDto<T>(this IEntity source, IDto target) where T : IDto
        {
            PropertyInfo[] entityProperties = source.GetType().GetProperties();
            PropertyInfo[] dtoProperties = typeof(T).GetProperties();

            foreach (PropertyInfo dtoProperty in dtoProperties)
            {
                var entityProperty = entityProperties.FirstOrDefault(x => x.Name.Equals(dtoProperty.Name));
                if (entityProperty != null)
                {
                    
                    var sourceValue = entityProperty.GetValue(source);
                    dtoProperty.SetValue(target,sourceValue);
                }
            }
            return (T)target;
        }
        
        public static T AsEntity<T>(this IDto source, IEntity target ) where T : IEntity
        {
            PropertyInfo[] dtoProperties = source.GetType().GetProperties();
            PropertyInfo[] entityProperties = typeof(T).GetProperties();

            foreach (PropertyInfo dtoProperty in dtoProperties)
            {
                var entityProperty = entityProperties.FirstOrDefault(x => x.Name.Equals(dtoProperty.Name));
                if (entityProperty != null)
                {
                    var sourceValue = dtoProperty.GetValue(source);
                    entityProperty.SetValue(target,sourceValue);
                }
            }
            return (T)target;
        }
    }
}