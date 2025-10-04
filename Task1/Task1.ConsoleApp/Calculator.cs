namespace Task1.ConsoleApp;

public class Calculator
{
    public static void Run()
    {
        bool exitFlag = false;
        while (!exitFlag)
        {
            var number1 = ReadOperand();
            var op = ReadOperator();
            var number2 = ReadOperand(false);

            try
            {
                var result = Calculate(number1, number2, op);
                Console.WriteLine($"Result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            exitFlag = IsExit();
        }
    }

    private static double ReadOperand(bool isFirst = true)
    {
        while (true)
        {
            Console.WriteLine("Input " + (isFirst ? "first" : "second") + " number:");
            var input = Console.ReadLine();
            if (double.TryParse(input, out var number))
            {
                return number;
            }

            Console.WriteLine($"{input} is not a number. Try again.");
        }
    }

    private static OperatorEnum ReadOperator()
    {
        while (true)
        {
            Console.WriteLine("Input operator: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "+":
                    return OperatorEnum.Add;
                case "-":
                    return OperatorEnum.Subtract;
                case "*":
                    return OperatorEnum.Multiply;
                case "/":
                    return OperatorEnum.Divide;
                default:
                    Console.WriteLine($"{input} is not a valid operator. Try again.");
                    continue;
            }
        }
    }

    private static double Calculate(double number1, double number2, OperatorEnum op)
    {
        switch (op)
        {
            case OperatorEnum.Add:
                return number1 + number2;
            case OperatorEnum.Subtract:
                return number1 - number2;
            case OperatorEnum.Multiply:
                return number1 * number2;
            case OperatorEnum.Divide:
                if (number2 == 0)
                {
                    throw new Exception("Division by zero is not allowed.");
                }
                return number1 / number2;
            default:
                throw new Exception("Invalid operator.");
        }
    }

    private static bool IsExit()
    {
        char exitCommand = 'e';
        Console.WriteLine($"""Enter "{exitCommand}" to exit or press any key to continue""");

        var input = Console.ReadKey(true);

        return input.KeyChar == exitCommand;
    }
}
