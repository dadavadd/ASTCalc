namespace ASTCalc.Syntax.Nodes;

public class NumberNode : AstNode
{
	private readonly double _value;

	public NumberNode(double value)
	{
		_value = value;
	}

	public override double Evaluate() => _value;
}
