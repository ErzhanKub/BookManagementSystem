namespace WebApi.Dtos
{/// <summary>
 /// The ErrorDto class is a simple data transfer object (DTO) that is used to standardize the structure of error messages returned by an API.
 /// </summary>
    public class ErrorDto
    {
        /// <summary>
        /// Gets or sets the HTTP status code of the error.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets a human-readable explanation of the error. This property can be null.
        /// </summary>
        public string? Message { get; set; }
    }

}
