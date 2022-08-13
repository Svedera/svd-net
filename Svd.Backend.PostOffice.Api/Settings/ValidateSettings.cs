using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using DataAnnotationsValidator;
using Svd.Backend.Application.Attributes;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Svd.Backend.PostOffice.Settings;

public class ValidatedSettings
{
    public T Validate<T>() where T : ValidatedSettings
    {
        ProcessAnnotations(this);
        var validationResults = new List<ValidationResult>();
        var validator = new DataAnnotationsValidator.DataAnnotationsValidator();
        if (validator.TryValidateObjectRecursive(this, validationResults))
        {
            return (T)this;
        }

        var message = "\n" + string.Join(
            "\n",
            validationResults.Select(result => result.ErrorMessage));
        throw new ValidationException(message);
    }

    private static void ProcessAnnotations<T>(T obj)
    {
        ProcessAnnotationsRecursively(obj, ProcessEnvVarNames);
        ProcessAnnotationsRecursively(obj, ProcessFallbackToFields);
    }

    private static void ProcessAnnotationsRecursively<T>(
        T obj,
        Action<PropertyInfo, object> action)
    {
        if (obj == null)
        {
            return;
        }

        foreach (var propertyInfo in obj.GetType().GetProperties())
        {
            var propertyValue = obj.GetPropertyValue(propertyInfo.Name);
            if (propertyInfo.PropertyType == typeof(string) ||
                propertyInfo.PropertyType.IsValueType)
            {
                action(propertyInfo, obj);
            }
            else
            {
                ProcessAnnotationsRecursively(propertyValue, action);
            }
        }
    }

    private static void ProcessEnvVarNames(PropertyInfo propertyInfo, object obj)
    {
        var value = propertyInfo.GetCustomAttribute<MapEnvironmentVariable>()?.GetVarValue();
        if (value == null)
        {
            return;
        }

        var converter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);
        var convertedObject = converter.ConvertFromString(value);
        propertyInfo.SetValue(obj, convertedObject);
    }

    private static void ProcessFallbackToFields(PropertyInfo propertyInfo, object obj)
    {
        var value = propertyInfo.GetCustomAttribute<FallbackToField>()?.Name;
        if (value == null)
        {
            return;
        }

        var newValue = obj.GetPropertyValue(value);

        propertyInfo.SetValue(obj, newValue);
    }
}
