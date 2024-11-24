using System.Globalization;

namespace ASTCalc.Lexer;

public class Lexer(string input)
{
	private readonly string _input = input.ToLower();
	private int _position = 0;

	private readonly Dictionary<string, (TokenType Type, MathFunction? Function)> _keywords = new()
	{
		{"sin", (TokenType.Function, MathFunction.Sin)},
		{"cos", (TokenType.Function, MathFunction.Cos)},
		{"tan", (TokenType.Function, MathFunction.Tan)},
		{"ctan", (TokenType.Function, MathFunction.Ctan)},
		{"sind", (TokenType.Function, MathFunction.SinDeg)},
		{"cosd", (TokenType.Function, MathFunction.CosDeg)},
		{"tand", (TokenType.Function, MathFunction.TanDeg)},
		{"asin", (TokenType.Function, MathFunction.Asin)},
		{"acos", (TokenType.Function, MathFunction.Acos)},
		{"atan", (TokenType.Function, MathFunction.Atan)},
		{"actan", (TokenType.Function, MathFunction.Actan)},
		{"sqrt", (TokenType.Function, MathFunction.Sqrt)},
		{"log", (TokenType.Function, MathFunction.Log)},
		{"ln", (TokenType.Function, MathFunction.Ln)},
		{"abs", (TokenType.Function, MathFunction.Abs)},
		{"exp", (TokenType.Function, MathFunction.Exp)},
		{"sign", (TokenType.Function, MathFunction.Sign)},
		{"round", (TokenType.Function, MathFunction.Round)},
		{"ceil", (TokenType.Function, MathFunction.Ceil)},
		{"floor", (TokenType.Function, MathFunction.Floor)},
		{"deg", (TokenType.Function, MathFunction.RadToDeg)},
		{"rad", (TokenType.Function, MathFunction.DegToRad)},
		{"pi", (TokenType.Identifier, null)},
		{"e", (TokenType.Identifier, null)},
		{"tau", (TokenType.Identifier, null)},
		{"bin", (TokenType.Function, MathFunction.Bin)},
		{"frombin", (TokenType.Function, MathFunction.FromBin)},
		{"oct", (TokenType.Function, MathFunction.Oct)},
		{"hex", (TokenType.Function, MathFunction.Hex)},
		{"trunc", (TokenType.Function, MathFunction.Trunc)}
	};

	private readonly Dictionary<string, double> _constants = new()
	{
		{ "pi", Math.PI },
		{ "e", Math.E },
		{ "tau", Math.Tau }
	};
	private char CurrentChar => _position < _input.Length ? _input[_position] : '\0';

	public List<Token> Tokenize()
	{
		var tokens = new List<Token>();

		while (_position < _input.Length)
		{
			if (char.IsWhiteSpace(CurrentChar))
			{
				Advance();
				continue;
			}

			var token = char.IsLetter(CurrentChar) ? ParseIdentifier() :
					   char.IsDigit(CurrentChar) ? ParseNumber() :
					   ParseOperator();

			if (token != null)
			{
				tokens.Add(token);
			}
		}

		tokens.Add(new Token(TokenType.EOF));
		return tokens;
	}

	private void Advance() => _position++;

	private Token ParseIdentifier()
	{
		string identifier = "";
		while (_position < _input.Length && char.IsLetter(CurrentChar))
		{
			identifier += CurrentChar;
			Advance();
		}

		if (_keywords.TryGetValue(identifier, out var keyword))
		{
			if (keyword.Type == TokenType.Function)
				return new Token(TokenType.Function, identifier, keyword.Function);
			if (keyword.Type == TokenType.Identifier)
				return new Token(TokenType.Number, GetConstantValue(identifier).ToString(CultureInfo.InvariantCulture));
		}

		throw new Exception($"Неизвестный идентификатор: {identifier}");
	}

	private double GetConstantValue(string identifier)
	{
		if (_constants.TryGetValue(identifier, out double value))
			return value;
		throw new Exception($"Неизвестная константа: {identifier}");
	}

	private Token ParseNumber()
	{
		string number = "";
		while (_position < _input.Length && (char.IsDigit(CurrentChar) || CurrentChar == '.'))
		{
			number += CurrentChar;
			Advance();
		}
		return new Token(TokenType.Number, number);
	}

	private Token ParseOperator()
	{
		var token = CurrentChar switch
		{
			'+' => new Token(TokenType.Plus),
			'-' => new Token(TokenType.Minus),
			'*' => new Token(TokenType.Multiply),
			'/' => new Token(TokenType.Divide),
			'^' => new Token(TokenType.Power),
			'(' => new Token(TokenType.LeftParen),
			')' => new Token(TokenType.RightParen),
			',' => new Token(TokenType.Comma),
			'%' => new Token(TokenType.Percent),
			'!' => new Token(TokenType.Factorial),
			_ => throw new Exception($"Неизвестный символ: {CurrentChar}")
		};
		
		Advance();
		return token;
	}
}
