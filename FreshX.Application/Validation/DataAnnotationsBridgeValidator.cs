using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace FreshX.Application.Validation;

public abstract class DataAnnotationsBridgeValidator<T> : AbstractValidator<T>
{
    protected DataAnnotationsBridgeValidator()
    {
        RuleFor(x => x).Custom((instance, context) =>
        {
            if (instance is null)
            {
                context.AddFailure(typeof(T).Name, "Request payload is required.");
                return;
            }

            var validationContext = new ValidationContext(instance);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(instance, validationContext, results, true))
            {
                return;
            }

            foreach (var result in results)
            {
                var members = result.MemberNames?.Any() == true
                    ? result.MemberNames
                    : new[] { typeof(T).Name };

                foreach (var member in members)
                {
                    context.AddFailure(member, result.ErrorMessage ?? "Validation failed.");
                }
            }
        });
    }
}
