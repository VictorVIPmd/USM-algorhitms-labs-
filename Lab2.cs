using System;
using System.Diagnostics;

class Finder
{
    public static int SearchLinear(int[] data, int value)
    {
        for (int idx = 0; idx < data.Length; idx++)
        {
            if (data[idx] == value)
                return idx;
        }
        return -1;
    }

    public static int SearchBinary(int[] input, int value)
    {
        int start = 0, end = input.Length - 1;

        while (start <= end)
        {
            int center = start + (end - start) / 2;

            if (input[center] == value)
                return center;
            if (input[center] < value)
                start = center + 1;
            else
                end = center - 1;
        }

        return -1;
    }

    public static int SearchInterpolation(int[] dataset, int targetVal)
    {
        int begin = 0, finish = dataset.Length - 1;

        while (begin <= finish && targetVal >= dataset[begin] && targetVal <= dataset[finish])
        {
            if (begin == finish)
                return dataset[begin] == targetVal ? begin : -1;

            int guess = begin + ((targetVal - dataset[begin]) * (finish - begin)) / (dataset[finish] - dataset[begin]);

            if (dataset[guess] == targetVal)
                return guess;
            if (dataset[guess] < targetVal)
                begin = guess + 1;
            else
                finish = guess - 1;
        }

        return -1;
    }

    static void Main()
    {
        int[] lengths = { 100, 1000, 10000 };
        int attempts = 5;
        Random generator = new Random();

        foreach (int length in lengths)
        {
            int[] ascArray = GenerateSequential(length);
            int[] descArray = GenerateDescending(length);
            int[] mixedArray = GenerateRandom(length, generator);

            Evaluate("Линейный поиск", ascArray, mixedArray, attempts, SearchLinear);
            Evaluate("Бинарный поиск", ascArray, ascArray, attempts, SearchBinary);
            Evaluate("Интерполяционный поиск", ascArray, ascArray, attempts, SearchInterpolation);
        }
    }

    static void Evaluate(string label, int[] searchBase, int[] testPool, int runs, Func<int[], int, int> strategy)
    {
        Stopwatch timer = new Stopwatch();
        long sumTicks = 0;
        Random rnd = new Random();

        for (int run = 0; run < runs; run++)
        {
            int value = testPool[rnd.Next(testPool.Length)];
            timer.Restart();
            strategy(searchBase, value);
            timer.Stop();
            sumTicks += timer.ElapsedTicks;
        }

        Console.WriteLine($"{label} на массиве из {searchBase.Length} элементов: {sumTicks / runs} тиков (среднее из {runs} запусков)");
    }

    static int[] GenerateSequential(int count)
    {
        int[] arr = new int[count];
        for (int i = 0; i < count; i++)
            arr[i] = i;
        return arr;
    }

    static int[] GenerateDescending(int count)
    {
        int[] arr = new int[count];
        for (int i = 0; i < count; i++)
            arr[i] = count - i - 1;
        return arr;
    }

    static int[] GenerateRandom(int count, Random rnd)
    {
        int[] arr = new int[count];
        for (int i = 0; i < count; i++)
            arr[i] = rnd.Next(count * 2);
        return arr;
    }
}
