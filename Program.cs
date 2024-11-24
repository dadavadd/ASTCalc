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
			Console.WriteLine("  ^    Возведение в степень");
			Console.WriteLine("  !    Факториал");
			Console.WriteLine("  %    Процент\n");

			Console.WriteLine("Тригонометрические функции:");
			Console.WriteLine("  sin(x)     Синус x (в радианах)");
			Console.WriteLine("  cos(x)     Косинус x (в радианах)");
			Console.WriteLine("  tan(x)     Тангенс x (в радианах)");
			Console.WriteLine("  ctan(x)    Котангенс x (в радианах)");
			Console.WriteLine("  sind(x)    Синус x (в градусах)");
			Console.WriteLine("  cosd(x)    Косинус x (в градусах)");
			Console.WriteLine("  tand(x)    Тангенс x (в градусах)");
			Console.WriteLine("  deg(x)     Перевод радиан в градусы");
			Console.WriteLine("  rad(x)     Перевод градусов в радианы\n");

			Console.WriteLine("Обратные тригонометрические функции:");
			Console.WriteLine("  asin(x)    Арксинус x");
			Console.WriteLine("  acos(x)    Арккосинус x");
			Console.WriteLine("  atan(x)    Арктангенс x");
			Console.WriteLine("  actan(x)   Арккотангенс x\n");

			Console.WriteLine("Математические функции:");
			Console.WriteLine("  sqrt(x)    Квадратный корень из x");
			Console.WriteLine("  log(x)     Десятичный логарифм x");
			Console.WriteLine("  ln(x)      Натуральный логарифм x");
			Console.WriteLine("  abs(x)     Модуль числа x");
			Console.WriteLine("  exp(x)     Экспонента (e^x)");
			Console.WriteLine("  sign(x)    Знак числа (-1, 0, 1)");
			Console.WriteLine("  round(x)   Округление x до ближайшего целого");
			Console.WriteLine("  ceil(x)    Округление x вверх");
			Console.WriteLine("  floor(x)   Округление x вниз");
			Console.WriteLine("  trunc(x)   Отбрасывание дробной части\n");

			Console.WriteLine("Системы счисления:");
			Console.WriteLine("  bin(x)     Перевод числа x в двоичную систему (возвращает десятичное представление)");
			Console.WriteLine("  frombin(x) Перевод двоичного числа x в десятичную систему");
			Console.WriteLine("  oct(x)     Перевод числа x в восьмеричную систему (возвращает десятичное представление)");
			Console.WriteLine("  hex(x)     Перевод числа x в шестнадцатеричную систему (возвращает десятичное представление)\n");

			Console.WriteLine("Поддерживаемые константы:");
			Console.WriteLine("  pi    Число π (3.141592...)");
			Console.WriteLine("  e     Число e (2.718281...)");
			Console.WriteLine("  tau   Число τ (6.283185...)\n");

			Console.WriteLine("Примеры базовых операций:");
			Console.WriteLine("  2 + 2 * 2");
			Console.WriteLine("  sin(pi/2)");
			Console.WriteLine("  2^3 + sqrt(16)");
			Console.WriteLine("  log(100) + ln(e)\n");

			Console.WriteLine("Примеры новых возможностей:");
			Console.WriteLine("  sind(90)                  # синус 90 градусов = 1");
			Console.WriteLine("  deg(pi/2)                 # перевод π/2 радиан в градусы = 90");
			Console.WriteLine("  exp(2)                    # e в степени 2 ≈ 7.389");
			Console.WriteLine("  sign(-5) + abs(-5)        # знак числа и его модуль = -1 + 5 = 4");
			Console.WriteLine("  frombin(1111)             # 1111(2) в десятичную = 15");
			Console.WriteLine("  bin(15)                   # 15 в двоичную = 15 (1111 в двоичной)");
			Console.WriteLine("  5!                        # факториал числа 5 = 120");
			Console.WriteLine("  50% * 200                 # вычисление процента от числа = 100");
			
			return;
		}

		var calculator = new Calculator();

		while (true)
		{
			Console.Write("Введите выражение (или 'exit' для выхода): ");
			var input = Console.ReadLine()?.Trim();

			if (string.IsNullOrEmpty(input))
				continue;

			if (input.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
				break;

			try
			{
				var result = calculator.Calculate(input);
				Console.WriteLine($"Результат: {result}");
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
