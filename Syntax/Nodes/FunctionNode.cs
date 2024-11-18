

namespace ASTCalc.Syntax.Nodes;

public class FunctionNode : AstNode
{
	private readonly string _functionName;
	private readonly AstNode _argument;

	public FunctionNode(string functionName, AstNode argument)
	{
		_functionName = functionName;
		_argument = argument;
	}

	public override double Evaluate()
	{
		var arg = _argument.Evaluate();

		return _functionName switch
		{
			"sin" => Math.Sin(arg),
			"cos" => Math.Cos(arg),
			"tan" => Math.Tan(arg),
			"ctan" => 1 / Math.Tan(arg),
			"asin" => Math.Asin(arg),
			"acos" => Math.Acos(arg),
			"atan" => Math.Atan(arg),
			"actan" => Math.PI / 2 - Math.Atan(arg),
			"sqrt" => Math.Sqrt(arg),
			_ => throw new Exception($"Неизвестная функция: {_functionName}")
		};
	}
}
