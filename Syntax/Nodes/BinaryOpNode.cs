using ASTCalc.Lexer;

namespace ASTCalc.Syntax.Nodes;

public class BinaryOpNode : AstNode
{
	private readonly AstNode _left;
	private readonly AstNode _right;
	private readonly TokenType _operator;

	public BinaryOpNode(AstNode left, TokenType op, AstNode right)
	{
		_left = left;
		_operator = op;
		_right = right;
	}

	public override double Evaluate()
	{
		var left = _left.Evaluate();
		var right = _right.Evaluate();

		return _operator switch
		{
			TokenType.Plus => left + right,
			TokenType.Minus => left - right,
			TokenType.Multiply => left * right,
			TokenType.Divide => right != 0 ? left / right : throw new DivideByZeroException(),
			TokenType.Power => Math.Pow(left, right),
			_ => throw new Exception("Неизвестный оператор")
		};
	}
}
