using ASTCalc.Syntax;

namespace ASTCalc.Syntax.Nodes;
public class FactorialNode : AstNode
{
    private readonly AstNode _operand;

    public FactorialNode(AstNode operand)
    {
        _operand = operand;
    }

    public override double Evaluate()
    {
        var n = (int)_operand.Evaluate();
        if (n < 0) throw new Exception("Факториал отрицательного числа не определен");
        double result = 1;
        for (int i = 2; i <= n; i++)
            result *= i;
        return result;
    }
} 