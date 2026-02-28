using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class ReverseFunction : JmesPathFunction
    {
        public ReverseFunction()
            : base("reverse", 1)
        {

        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();
            var arg = args[0].Token;
            var tokenType = arg.GetTokenType();
            if (tokenType != "string" && tokenType != "array")
                throw new System.Exception($"Error: invalid-type, function {Name} accepts either an array or a string.");
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var token = args[0].Token;
            switch (token.GetTokenType())
            {
                case "string":
                    {
                        var text = (Text)token.Value<string>();
                        var reversed = new Text(System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Reverse(text.CodePoints)));
                        return new JValue((string)reversed);
                    }
                case "array":
                    {
                        var items = System.Linq.Enumerable.Reverse((JArray)token);
                        return new JArray().AddRange(items);
                    }
                default:
                    return null;
            }
        }
    }
}