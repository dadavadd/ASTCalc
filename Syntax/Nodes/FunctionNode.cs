using ASTCalc.Lexer;

namespace ASTCalc.Syntax.Nodes;

public class FunctionNode : AstNode
{
	private readonly MathFunction _function;
	private readonly AstNode _argument;

	public FunctionNode(MathFunction function, AstNode argument)
	{
		_function = function;
		_argument = argument;
	}

	public override double Evaluate()
	{
		var arg = _argument.Evaluate();

		return _function switch
		{
			MathFunction.Sin => Math.Sin(arg),
			MathFunction.Cos => Math.Cos(arg),
			MathFunction.Tan => Math.Tan(arg),
			MathFunction.Ctan => 1 / Math.Tan(arg),
			MathFunction.Asin => Math.Asin(arg),
			MathFunction.Acos => Math.Acos(arg),
			MathFunction.Atan => Math.Atan(arg),
			MathFunction.Actan => Math.PI / 2 - Math.Atan(arg),
			MathFunction.Sqrt => Math.Sqrt(arg),
			MathFunction.Log => Math.Log10(arg),
			MathFunction.Ln => Math.Log(arg),
			MathFunction.Abs => Math.Abs(arg),
			MathFunction.Round => Math.Round(arg),
			MathFunction.Ceil => Math.Ceiling(arg),
			MathFunction.Floor => Math.Floor(arg),
			MathFunction.SinDeg => Math.Sin(arg * Math.PI / 180),
			MathFunction.CosDeg => Math.Cos(arg * Math.PI / 180),
			MathFunction.TanDeg => Math.Tan(arg * Math.PI / 180),
			MathFunction.RadToDeg => arg * 180 / Math.PI,
			MathFunction.DegToRad => arg * Math.PI / 180,
			MathFunction.Exp => Math.Exp(arg),
			MathFunction.Sign => Math.Sign(arg),
			MathFunction.Bin => Convert.ToInt32(arg),
			MathFunction.FromBin => Convert.ToInt32(arg.ToString("F0"), 2),
			MathFunction.Oct => Convert.ToInt32(arg),
			MathFunction.Hex => Convert.ToInt32(arg),
			MathFunction.Trunc => Math.Truncate(arg),
			_ => throw new Exception($"Неизвестная функция: {_function}")
		};
	}
}
