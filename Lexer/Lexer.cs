using System.Globalization;

namespace ASTCalc.Lexer;

public class Lexer
{
	private readonly string _input;
	private int _position = 0;

	private readonly Dictionary<string, TokenType> _functions = new()
	{
		{"sin", TokenType.Function},
		{"cos", TokenType.Function},
		{"tan", TokenType.Function},
		{"ctan", TokenType.Function},
		{"asin", TokenType.Function},
		{"acos", TokenType.Function},
		{"atan", TokenType.Function},
		{"actan", TokenType.Function},
		{"sqrt", TokenType.Function}
	};

	private readonly Dictionary<string, double> _constants = new()
	{
		{"pi", Math.PI},
		{"e", Math.E}
	};

	public Lexer(string input)
	{
		_input = input.ToLower();
	}

	private char CurrentChar => _position < _input.Length ? _input[_position] : '\0';

	public List<Token> Tokenize()
	{
		var tokens = new List<Token>();

		while (_position < _input.Length)
		{
			if (char.IsWhiteSpace(CurrentChar))
			{
				_position++;
				continue;
			}

			if (char.IsLetter(CurrentChar))
			{
				string identifier = "";
				while (_position < _input.Length && char.IsLetter(CurrentChar))
				{
					identifier += CurrentChar;
					_position++;
				}

				if (_functions.ContainsKey(identifier))
				{
					tokens.Add(new Token(TokenType.Function, identifier));
				}
				else if (_constants.ContainsKey(identifier))
				{
					tokens.Add(new Token(TokenType.Number,
						_constants[identifier].ToString(CultureInfo.InvariantCulture)));
				}
				else
				{
					throw new Exception($"Неизвестный идентификатор: {identifier}");
				}
				continue;
			}

			if (char.IsDigit(CurrentChar))
			{
				string number = "";
				while (_position < _input.Length && (char.IsDigit(CurrentChar) || CurrentChar == '.'))
				{
					number += CurrentChar;
					_position++;
				}
				tokens.Add(new Token(TokenType.Number, number));
				continue;
			}


			switch (CurrentChar)
			{
				case '+':
					tokens.Add(new Token(TokenType.Plus));
					break;
				case '-':
					tokens.Add(new Token(TokenType.Minus));
					break;
				case '*':
					tokens.Add(new Token(TokenType.Multiply));
					break;
				case '/':
					tokens.Add(new Token(TokenType.Divide));
					break;
				case '^':
					tokens.Add(new Token(TokenType.Power));
					break;
				case '(':
					tokens.Add(new Token(TokenType.LeftParen));
					break;
				case ')':
					tokens.Add(new Token(TokenType.RightParen));
					break;
				case ',':
					tokens.Add(new Token(TokenType.Comma));
					break;
				default:
					throw new Exception($"Неизвестный символ: {CurrentChar}");
			}
			_position++;
		}

		tokens.Add(new Token(TokenType.EOF));
		return tokens;
	}
}
