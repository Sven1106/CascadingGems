using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace CascadingGems
{
    class Program
    {

        static void Main(string[] args)
        {
            Start();

        }
        public static void Start()
        {
            Console.Clear();

            int rows = 5;
            int columns = 4;
            Field[,] allFields = new Field[rows, columns];

            var watchBefore = System.Diagnostics.Stopwatch.StartNew();
            Iterate2Darray(allFields, columns, rows, (list, r, c) =>
            {
                // CODEBLOCK
                list[r, c] = new Field
                {
                    Shape = GetRandomEnumValue<Shape>(),
                    Position = new Position { X = c, Y = r },
                    IsFull = RandomBool()
                };
                // CODEBLOCK
            });

            watchBefore.Stop();
            Console.WriteLine("Before");
            Iterate2Darray(allFields, columns, rows, (list, r, c) =>
            {
                // CODEBLOCK
                Console.Write(list[r, c].IsFull == true ? "|" + list[r, c].Shape + "|" : "| |");
                if (c == columns - 1)
                {
                    Console.WriteLine();
                }
                // CODEBLOCK
            });


            var elapsedTimeBefore = watchBefore.ElapsedTicks;
            Console.WriteLine($"ElapsedTicks: {elapsedTimeBefore}");
            Console.ReadKey();
            Console.Clear();
            var watchAfter = System.Diagnostics.Stopwatch.StartNew();
            Iterate2Darray(allFields, columns, rows, (list, r, c) =>
            {
                // CODEBLOCK
                if (list[r, c].IsFull == false)
                {
                    int nearestIsFullIndexOfY = GetRowOfNearestIsFull(list, r, c, rows);
                    if (nearestIsFullIndexOfY != -1)
                    {
                        SwapItemsIn2DArray(list, r, c, nearestIsFullIndexOfY, c);
                    }
                }
                // CODEBLOCK
            });
            watchAfter.Stop();
            Console.WriteLine("After");
            Iterate2Darray(allFields, columns, rows, (list, r, c) =>
            {
                // CODEBLOCK
                Console.Write(list[r, c].IsFull == true ? "|" + list[r, c].Shape + "|" : "| |");
                if (c == columns - 1)
                {
                    Console.WriteLine();
                }
                // CODEBLOCK
            });
            var elapsedTimeAfter = watchAfter.ElapsedTicks;
            Console.WriteLine($"ElapsedTicks: {elapsedTimeAfter}");
            Console.ReadKey();
            Start();
        }

        public static void Iterate2Darray(Field[,] _2DArr, int columns, int rows, Action<Field[,], int, int> method)
        {
            for (int r = 0; r != rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    method(_2DArr, r, c);
                }
            }
        }

        public static void SwapItemsIn2DArray(object[,] _2DArr, int y1, int x1, int y2, int x2)
        {
            object temp = _2DArr[y1, x1];
            _2DArr[y1, x1] = _2DArr[y2, x2];
            _2DArr[y2, x2] = temp;
        }


        public static int GetRowOfNearestIsFull(Field[,] _2DArr, int row, int col, int rows)
        { // based on  BREADTH-FIRST-SEARCH instead of recursion http://www8.cs.umu.se/kurser/TDBA36/VT02/lec5.html
            Queue queue = new Queue(); // https://www.guru99.com/c-sharp-queue.html
            queue.Enqueue(new object { });
            int currentRow = row;
            while (queue.Count > 0)
            {
                queue.Dequeue();
                if (_2DArr[currentRow, col].IsFull == true)
                {
                    break;
                }
                else
                {
                    currentRow++;
                    if (currentRow < rows)
                    {
                        queue.Enqueue(new object { });
                    }
                    else
                    {
                        currentRow = -1;
                    }
                }
            }
            return currentRow;
        }


        public static T GetRandomEnumValue<T>()
        {
            Random random = new Random();
            Array enumValues = Enum.GetValues(typeof(T));
            T randomEnumValue = (T)enumValues.GetValue(random.Next(enumValues.Length));
            return randomEnumValue;
        }
        public static bool RandomBool()
        {
            Random random = new Random();
            double probability = 0.5;
            bool randomBool = random.NextDouble() < probability;
            return randomBool;
        }
    }
}
