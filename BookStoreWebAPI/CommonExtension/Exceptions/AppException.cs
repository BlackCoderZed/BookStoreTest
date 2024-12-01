
namespace CommonExtension.Exceptions
{
    public class AppException : Exception
    {
        public const string MSG_FAIL = "Failed to create/update data.";

        public const string MSG_READ_FAIL = "Failed to read data.";

        public const string MSG_INVALID_LOGIN = "Invalid user id or password.";

        public const string MSG_INPUT_INVALID = "Input value is invalid";

        public const string MSG_PARAM_VALUE_INVALID = "{0} value is invalid";

        public const string MSG_ACCOUNT_TERMINATED = "Account is disabled or terminated";

        public const string MSG_INVALID_RANGE = "Invalid value range. [{0}]";

        public const string MSG_NOT_EXIST = "{0} information does not exist.";

        public const string MSG_NOT_ENOUGH = "Stock is not enough to fulfill the request.";

        public const string MSG_INVALID_OPERATION = "Invalid Operation!";

        public const string MSG_NOT_EXIST_CART_ID = "The following values are not existed in cart. {0}";

        public const string MSG_NOT_ENOUGH_WITH_ID = "Stock is not enough to fulfill the request. {0}";

        public AppException(string message) : base(message) { }
    }
}
