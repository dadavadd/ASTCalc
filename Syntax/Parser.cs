using ASTCalc.Lexer;
using ASTCalc.Syntax.Nodes;
using System.Globalization; 

namespace ASTCalc.Syntax;

public class Parser(List<Token> tokens)
{
	private readonly List<Token> _tokens = tokens;
	private int _position = 0;
	private static readonly CultureInfo ParsingCulture = CultureInfo.InvariantCulture;

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
			var function = Current.Function ?? 
				throw new Exception("Внутренняя ошибка: функция не определена");
			Advance();

			if (Current.Type != TokenType.LeftParen)
				throw new Exception("Ожидалась открывающая скобка после имени функции");

			Advance();
			var argument = Expression();

			if (Current.Type != TokenType.RightParen)
				throw new Exception("Ожидалась закрывающая скобка");

			Advance();
			return new FunctionNode(function, argument);
		}

		var node = Primary();

		while (Current.Type == TokenType.Factorial || Current.Type == TokenType.Percent)
		{
			if (Current.Type == TokenType.Factorial)
			{
				Advance();
				node = new FactorialNode(node);
			}
			else if (Current.Type == TokenType.Percent)
			{
				Advance();
				node = new BinaryOpNode(node, TokenType.Multiply, new NumberNode(0.01));
			}
		}

		return node;
	}

	private AstNode Primary()
	{
		var token = Current;

		if (token.Type == TokenType.Number)
		{
			Advance();
			if (!double.TryParse(token.Value, NumberStyles.Any, ParsingCulture, out var number))
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

		if (token.Type == TokenType.Minus)
		{
			Advance();
			return new BinaryOpNode(new NumberNode(0), TokenType.Minus, Primary());
		}

		throw new Exception("Ожидалось число или открывающая скобка");
	}
}