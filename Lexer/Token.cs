namespace ASTCalc.Lexer;

public class Token
{
	public TokenType Type { get; }
	public string Value { get; }
	public MathFunction? Function { get; }

	public Token(TokenType type, string value = "", MathFunction? function = null)
	{
		Type = type;
		Value = value;
		Function = function;
	}
}
