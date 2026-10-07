using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {

        List<string> history = new List<string>();

        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\n=== Калькулятор виразів ===");
            Console.WriteLine("0. Вихід");
            Console.WriteLine("1. Обчислити вираз (наприклад: 12 + 7)");
            Console.WriteLine("2. Показати історію");

            Console.Write("Ваш вибір: ");
            string? input = Console.ReadLine();

           isRunning = input switch
            {
                "1" => HandleCalculate(history),
                "2" => HandleShowHistory(history),
                "0" => false,
                _ => HandleInvalidInput()
            };
        }

        Console.WriteLine("Роботу завершено.");
    }

static bool HandleInvalidInput()
{
    Console.WriteLine("Некоректне значення. Введіть 0, 1 або 2.");
    return true;
}


static bool HandleCalculate(List<string> history)
    {
        Console.Write("Введіть вираз (формат: число оператор число): ");
        string? input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input))
        {
            ProcessExpression(input, history);
        }

        return true;
    }

    static bool HandleShowHistory(List<string> history)
    {
        if (history.Count == 0)
        {
            Console.WriteLine("Історія порожня.");
            return true;
        }

        Console.WriteLine("\n--- Історія обчислень за сеанс ---");
        foreach (string record in history)
        {
            Console.WriteLine(record);
        }
        Console.WriteLine("\n");

        return true;
    }


    static void ProcessExpression(string input, List<string> history)
    {

        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 3)
        {
            Console.WriteLine("Помилка: введіть вираз строго у форматі 'число оператор число' (через пробіли).");
            return;
        }


        if (!double.TryParse(parts[0], out double left) || !double.TryParse(parts[2], out double right))
        {
            Console.WriteLine("Помилка: не вдалося розпізнати числа.");
            return;
        }


        string opString = parts[1];
        string allowedOperators = "+-*/";
        if (opString.Length != 1 || !allowedOperators.Contains(opString))
        {
            Console.WriteLine($"Помилка: непідтримуваний оператор '{opString}'. Доступні оператори: +, -, *, /.");
            return;
        }

        char op = opString[0];


        if (op == '/' && right == 0)
        {
            Console.WriteLine("Помилка: ділення на нуль неможливе!");
            return;
        }

       
        double result = Calculate(left, right, op);

        
        string historyEntry = $"{left} {op} {right} = {result}";
        Console.WriteLine($"\nРезультат: {historyEntry}");
        history.Add(historyEntry);
    }

 
    static double Calculate(double left, double right, char op)
    {
        double result = op switch
        {
            '+' => left + right,
            '-' => left - right,
            '*' => left * right,
            '/' => left / right,
            _ => throw new InvalidOperationException("Невідомий оператор") 
        };

        return result;
    }
}