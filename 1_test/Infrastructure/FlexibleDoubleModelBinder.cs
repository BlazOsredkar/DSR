using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace _1_test.Infrastructure;

public class FlexibleDoubleModelBinder : IModelBinder
{
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

        var trimmed = rawValue.Trim();
        if (double.TryParse(trimmed, NumberStyles.Any, CultureInfo.CurrentCulture, out var currentCultureResult) ||
            double.TryParse(trimmed, NumberStyles.Any, CultureInfo.InvariantCulture, out currentCultureResult))
        {
            bindingContext.Result = ModelBindingResult.Success(currentCultureResult);
            return Task.CompletedTask;
        }

        bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Neveljavno decimalno stevilo.");
        return Task.CompletedTask;
    }
}

public class FlexibleDoubleModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(double) || context.Metadata.ModelType == typeof(double?))
        {
            return new BinderTypeModelBinder(typeof(FlexibleDoubleModelBinder));
        }

        return null;
    }
}
