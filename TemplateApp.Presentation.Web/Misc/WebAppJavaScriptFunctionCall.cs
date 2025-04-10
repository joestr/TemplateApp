namespace TemplateApp.Presentation.Web.Misc
{
    public class WebAppJavaScriptFunctionCall
    {
        public string FunctionCall { get; set; } = "javascript:";

        public WebAppJavaScriptFunctionCall()
        {
            FunctionCall += "undefined;";
        }

        public WebAppJavaScriptFunctionCall(string functionCall)
        {
            if (string.IsNullOrEmpty(functionCall) || string.IsNullOrWhiteSpace(functionCall))
            {
                FunctionCall += "undefined;";
            }

            FunctionCall += functionCall;

            if (!FunctionCall.EndsWith(";"))
            {
                FunctionCall += ";";
            }
        }

        public WebAppJavaScriptFunctionCall(string className, string identifier, string methodName, params string[] methodArgs)
        {
            FunctionCall += $"{className}_{identifier}.{methodName}({string.Join(", ", methodArgs)});";
        }
    }
}
