using System;

class Program
{
    static void Main()
    {
        int K = int.Parse(Console.ReadLine());
        int L = int.Parse(Console.ReadLine());
        int M = int.Parse(Console.ReadLine());
        int N = int.Parse(Console.ReadLine());

        int validChangesCount = 0;

        for (int firstDigit1 = K; firstDigit1 <= 8; firstDigit1++)
        {
            for (int secondDigit1 = 9; secondDigit1 <= L; secondDigit1++)
            {
                for (int firstDigit2 = M; firstDigit2 <= 8; firstDigit2++)
                {
                    for (int secondDigit2 = 9; secondDigit2 <= N; secondDigit2++)
                    {
                        bool isValidChange = IsChangeValid(firstDigit1, secondDigit1, firstDigit2, secondDigit2);
                        if (isValidChange)
                        {
                            Console.WriteLine($"{firstDigit1}{secondDigit1} - {firstDigit2}{secondDigit2}");
                            validChangesCount++;
                        }

                        if (validChangesCount >= 6)
                            return;
                    }
                }
            }
        }

        if (validChangesCount == 0)
            Console.WriteLine("Cannot change the same player.");
    }

    static bool IsChangeValid(int firstDigit1, int secondDigit1, int firstDigit2, int secondDigit2)
    {
        return (firstDigit1 % 2 == 0) && (secondDigit1 % 2 != 0) &&
               (firstDigit2 % 2 == 0) && (secondDigit2 % 2 != 0) &&
               (firstDigit1 != firstDigit2) && (secondDigit1 != secondDigit2);
    }
}
