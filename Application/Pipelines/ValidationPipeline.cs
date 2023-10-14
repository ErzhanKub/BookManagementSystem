using Application.Extensions;

namespace Application.Pipelines
{
    public class ValidationPipeline<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : ResultBase<TResponse>, new()

    {

        private readonly IValidator<TRequest> _validator;

        public ValidationPipeline(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validResult = _validator.Validate(request);
            if (validResult != null && validResult.Errors.Any())
            {
                var errors = validResult.Errors.MapToErrors();
                var responseType = typeof(TResponse);

                TResponse? result;
                if (responseType.IsGenericType)
                {
                    var resultType = responseType.GetGenericArguments()[0];
                    var invalidResponseType = typeof(Result<>).MakeGenericType(resultType);

                    result = Activator.CreateInstance(invalidResponseType, null) as TResponse;
                }
                else
                {
                    result = new TResponse();
                }
                return result!.WithErrors(errors);
            }
            return await next();
        }
    }
}
