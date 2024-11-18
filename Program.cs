namespace ASTCalc;

public static class Program
{
	static void Main(string[] args)
	{
		if (args.Length > 0 && (args[0] == "--help" || args[0] == "-h"))
		{
			Console.WriteLine("Калькулятор с поддержкой математических функций и констант\n");
			Console.WriteLine("Поддерживаемые операторы:");
			Console.WriteLine("  +    Сложение");
			Console.WriteLine("  -    Вычитание");
			Console.WriteLine("  *    Умножение");
			Console.WriteLine("  /    Деление");
			Console.WriteLine("  ^    Возведение в степень\n");

			Console.WriteLine("Поддерживаемые функции:");
			Console.WriteLine("  sin(x)   Синус x");
			Console.WriteLine("  cos(x)   Косинус x");
			Console.WriteLine("  tan(x)   Тангенс x");
			Console.WriteLine("  ctan(x)  Котангенс x");
			Console.WriteLine("  asin(x)  Арксинус x");
			Console.WriteLine("  acos(x)  Арккосинус x");
			Console.WriteLine("  atan(x)  Арктангенс x");
			Console.WriteLine("  actan(x) Арккотангенс x");
			Console.WriteLine("  sqrt(x)  Квадратный корень из x\n");

			Console.WriteLine("Поддерживаемые константы:");
			Console.WriteLine("  pi   Число π (3.141592...)");
			Console.WriteLine("  e    Число e (2.718281...)\n");

			Console.WriteLine("Примеры:");
			Console.WriteLine("  2 + 2 * 2");
			Console.WriteLine("  sin(pi/2)");
			Console.WriteLine("  2^3 + sqrt(16)");
			Console.WriteLine("  sin(0.5) + cos(2*pi)");
			return;
		}

		var calculator = new Calculator();

		while (true)
		{
			Console.Write("Введите выражение (или 'exit' для выхода): ");
			var input = Console.ReadLine()?.Trim();

			if (string.IsNullOrEmpty(input))
				continue;

			if (input.ToLower() == "exit")
				break;

			try
			{
				var result = calculator.Calculate(input);
				Console.WriteLine($"Результат: {result:F10}".TrimEnd('0').TrimEnd('.'));
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Ошибка: {ex.Message}");
				Console.ResetColor();
			}

			Console.WriteLine();
		}
	}
}
