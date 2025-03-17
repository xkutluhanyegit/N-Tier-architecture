using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using FluentValidation;

namespace CrossCutting.Validation
{
    public class ValidateAttribute : AbstractInterceptorAttribute
    {
        private readonly IValidatorFactory _validatorFactory;

        // Dependency injection için validator factory'yi constructor'a enjekte ediyoruz.
        public ValidateAttribute(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            // Eğer methodda [Validate] attribute'u varsa doğrulama yap
            var parameters = context.Parameters; // Parametreleri al

            // Parametrelerin doğrulamasını yap
            foreach (var param in parameters)
            {
                var validator = _validatorFactory.GetValidator(param.GetType());
                if (validator != null)
                {
                    var validationContext = new ValidationContext<object>(param);
                    var validationResult = await validator.ValidateAsync(validationContext);
                    if (!validationResult.IsValid)
                    {
                        // Hata varsa, ValidationException fırlat
                        throw new ValidationException(validationResult.Errors);
                    }
                }
            }
            // Methodu çalıştır
            await next(context);
        }
    }
}