using System.Reflection;
using ContactsApp.Core.Interfaces.DTO;
using ContactsApp.Core.Interfaces.Entity;

namespace ContactsApp.Core.Mappers
{
    public static class AutoMapper
    {
        public static T AsDto<T>(this IEntity source) where T : IDto, new()
        {
            PropertyInfo[] sourceProperties = typeof(IEntity).GetProperties();
            PropertyInfo[] targetProperties = typeof(T).GetProperties();
            T result = new T();

            foreach (PropertyInfo property in targetProperties)
            {
                if (sourceProperties.Contains(property))
                {
                    var sourceValue = property.GetValue(source);
                    property.SetValue(result,sourceValue);
                }
            }
            return result;
        }
        
        public static T AsEntity<T>(this IDto source) where T : IEntity, new()
        {
            PropertyInfo[] sourceProperties = typeof(IDto).GetProperties();
            PropertyInfo[] targetProperties = typeof(T).GetProperties();
            T result = new T();

            foreach (PropertyInfo property in targetProperties)
            {
                if (sourceProperties.Contains(property))
                {
                    var sourceValue = property.GetValue(source);
                    property.SetValue(result,sourceValue);
                }
            }
            return result;
        }
    }
}