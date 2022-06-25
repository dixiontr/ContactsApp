using System.Reflection;
using ContactsApp.Core.Interfaces.DTO;
using ContactsApp.Core.Interfaces.Entity;

namespace ContactsApp.Core.Mappers
{
    public static class AutoMapper
    {
        public static T AsDto<T>(this IEntity source, IDto target) where T : IDto
        {
            PropertyInfo[] sourceProperties = typeof(IEntity).GetProperties();
            PropertyInfo[] targetProperties = typeof(T).GetProperties();

            foreach (PropertyInfo property in targetProperties)
            {
                if (sourceProperties.Contains(property))
                {
                    var sourceValue = property.GetValue(source);
                    property.SetValue(target,sourceValue);
                }
            }
            return (T)target;
        }
        
        public static T AsEntity<T>(this IDto source, IEntity target) where T : IEntity
        {
            PropertyInfo[] sourceProperties = typeof(IDto).GetProperties();
            PropertyInfo[] targetProperties = typeof(T).GetProperties();

            foreach (PropertyInfo property in targetProperties)
            {
                if (sourceProperties.Contains(property))
                {
                    var sourceValue = property.GetValue(source);
                    property.SetValue(target,sourceValue);
                }
            }
            return (T)target;
        }
    }
}