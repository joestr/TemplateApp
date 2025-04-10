namespace TemplateApp.Presentation.Web.Misc
{
    public class WebAppJavaScriptFunctionCalls
    {
        public string FunctionCalls = "javascript:";

        public WebAppJavaScriptFunctionCalls()
        {
            FunctionCalls = "undefined;";
        }

        public WebAppJavaScriptFunctionCalls(params WebAppJavaScriptFunctionCall[] webAppJavaScriptFunctionCalls)
        {
            foreach (var webAppJavaScriptFunctionCall in webAppJavaScriptFunctionCalls)
            {
                var javaScriptUriLength = "javascript:".Length;
                var functionCallToAdd = webAppJavaScriptFunctionCall.FunctionCall;
                FunctionCalls += functionCallToAdd.Substring(javaScriptUriLength, functionCallToAdd.Length - javaScriptUriLength);
            }
        }
    }
}
