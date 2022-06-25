using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ContactsApp.ContactService.Extensions
{

    public static class ModelStateDictionaryExtensions
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary model)
        {
            return model.Values
                .SelectMany(sm => sm.Errors)
                .Select(s=>s.ErrorMessage)
                .ToList();
        }

    }

}