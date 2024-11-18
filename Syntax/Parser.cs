using ASTCalc.Lexer;
using ASTCalc.Syntax.Nodes;
using System.Globalization;

namespace ASTCalc.Syntax;

public class Parser
{
	private readonly List<Token> _tokens;
	private int _position = 0;
	private static readonly CultureInfo ParsingCulture = CultureInfo.InvariantCulture;

	public Parser(List<Token> tokens)
	{
		_tokens = tokens;
	}

	private Token Current => _tokens[_position];

	private void Advance() => _position++;

	public AstNode Parse()
	{
		var result = Expression();
		if (Current.Type != TokenType.EOF)
			throw new Exception("Ожидался конец выражения");
		return result;
	}

	private AstNode Expression()
	{
		var node = Term();

		while (Current.Type == TokenType.Plus || Current.Type == TokenType.Minus)
		{
			var op = Current.Type;
			Advance();
			node = new BinaryOpNode(node, op, Term());
		}

		return node;
	}

	private AstNode Term()
	{
		var node = Factor();

		while (Current.Type == TokenType.Multiply || Current.Type == TokenType.Divide)
		{
			var op = Current.Type;
			Advance();
			node = new BinaryOpNode(node, op, Factor());
		}

		return node;
	}

	private AstNode Factor()
	{
		var node = Power();

		while (Current.Type == TokenType.Power)
		{
			var op = Current.Type;
			Advance();
			node = new BinaryOpNode(node, op, Power());
		}

		return node;
	}

	private AstNode Power()
	{
		if (Current.Type == TokenType.Function)
		{
			var functionName = Current.Value;
			Advance();

			if (Current.Type != TokenType.LeftParen)
				throw new Exception("Ожидалась открывающая скобка после имени функции");

			Advance();
			var argument = Expression();

			if (Current.Type != TokenType.RightParen)
				throw new Exception("Ожидалась закрывающая скобка");

			Advance();
			return new FunctionNode(functionName, argument);
		}

		var token = Current;

		if (token.Type == TokenType.Number)
		{
			Advance();
			if (!double.TryParse(token.Value, NumberStyles.Any, ParsingCulture, out double number))
				throw new Exception($"Не удалось преобразовать '{token.Value}' в число");
			return new NumberNode(number);
		}

		if (token.Type == TokenType.LeftParen)
		{
			Advance();
			var node = Expression();
			if (Current.Type != TokenType.RightParen)
				throw new Exception("Ожидалась закрывающая скобка");
			Advance();
			return node;
		}

		throw new Exception("Ожидалось число или открывающая скобка");
	}
}