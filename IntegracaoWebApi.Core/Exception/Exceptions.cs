namespace IntegracaoWebApi.Core.Exception
{
    public class Exceptions
    {
        public class NotFoundException : System.Exception
        {
            public NotFoundException(string message) : base(message) { }
        }

        public class ExternalApiException : System.Exception
        {
            public ExternalApiException(string message) : base(message) { }
        }

        public class UnauthorizedException : System.Exception
        {
            public UnauthorizedException(string message) : base(message) { }
        }
    }
}
