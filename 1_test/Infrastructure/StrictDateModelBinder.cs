using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace _1_test.Infrastructure;

public class StrictDateModelBinder : IModelBinder
{
    private const string DateFormat = "dd.MM.yyyy";

    // Naloga: datum mora biti v formatu dd.MM.yyyy (navodilo: "Datum mora biti v pravilnem formatu").
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (value == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);
        var rawValue = value.FirstValue;

        if (string.IsNullOrWhiteSpace(rawValue))
        {
            return Task.CompletedTask;
        }

        if (DateTime.TryParseExact(rawValue.Trim(), DateFormat, CultureInfo.InvariantCulture,
            DateTimeStyles.None, out var result))
        {
            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }

        bindingContext.ModelState.TryAddModelError(bindingContext.ModelName,
            "Datum mora biti v formatu dd.MM.yyyy.");
        return Task.CompletedTask;
    }
}

public class StrictDateModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(DateTime) || context.Metadata.ModelType == typeof(DateTime?))
        {
            return new BinderTypeModelBinder(typeof(StrictDateModelBinder));
        }

        return null;
    }
}
