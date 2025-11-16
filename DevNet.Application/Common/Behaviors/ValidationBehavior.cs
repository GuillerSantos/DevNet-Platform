using FluentValidation;
using MediatR;

namespace DevNet.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : ResultBase, new()
    {
        #region Fields

        private readonly IEnumerable<IValidator<TRequest>> _validators;

        #endregion Fields

        #region Public Constructors

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                var errors = string.Join(", ", failures.Select(f => $"{f.PropertyName}: {f.ErrorMessage}"));

                var resultType = typeof(TResponse).GetGenericArguments()[0];
                var failureResult = typeof(Result<>)
                    .MakeGenericType(resultType)
                    .GetMethod("Failure")!
                    .Invoke(null, new object[] { errors });

                return (TResponse) failureResult!;
            }

            return await next();
        }

        #endregion Public Methods
    }
}