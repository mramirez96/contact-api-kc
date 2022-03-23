using FluentValidation;
using System.Text;

namespace Infraestructure.Services
{
    public abstract class BaseService
    {
        protected void Validate<T>(AbstractValidator<T> validator, T item)
        {
            var results = validator.Validate(item);
                
            if (!results.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var failure in results.Errors)
                {
                    sb.AppendLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                throw new Domain.Exceptions.ValidationException(sb.ToString());
            }
        }
    }
}
