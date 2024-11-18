using ASTCalc.Syntax;

namespace ASTCalc;

public class Calculator
{
	public double Calculate(string expression)
	{
		var lexer = new Lexer.Lexer(expression);
		var tokens = lexer.Tokenize();
		var parser = new Parser(tokens);
		var ast = parser.Parse();
		return ast.Evaluate();
	}
}
