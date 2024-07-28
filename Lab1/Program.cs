using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размерность массива");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]> (func1,n);

            Func<Task<int[]>, int[]> func2 = new Func<Task<int[]>, int[]> (PrintArray);
            Task<int[]> task2 = task1.ContinueWith(func2);

            
            Action<Task<int[]>> action2 = new Action<Task<int[]>>(GetSum);
            Task task3 = task2.ContinueWith(action2);

            
            Action<Task<int[]>> action3 = new Action<Task<int[]>>(GetMax);
            Task task4 = task2.ContinueWith(action3);

            task1.Start();
           
            Console.ReadKey(); 
        }

        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++) 
            {
                array[i] = random.Next(0, 100);            
            } 
            return array;
        }

        static void GetSum(Task<int[]> task)
        {
            int[] array = task.Result;
            int sum = array.Sum();
            Console.WriteLine($"\nСумма массива: {sum}");
            
        }

        static void GetMax(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = array.Max();
            Console.WriteLine($"Максимальное число: {max}");
            
        }

        static int[] PrintArray(Task<int[]> task)
        {
            int[] array = task.Result;
            for (int i = 0;i < array.Count();i++) 
            {
                Console.Write($"{array[i]} ");
            }
            return array;
        }
    }
}
