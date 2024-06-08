using System.Diagnostics;
using System.IO;

namespace FinalExam_OS_Session2
{
    internal class Program
    {
        static Semaphore semaphore = new Semaphore(3, 3);

        static void Main(string[] args)
        {
            // Find Number //
            string txt = File.ReadAllText(@"C:\Users\themohsen\Desktop\hh.txt");
            string[] lines = txt.Split("\n");
            string[] arr = new string[lines.Length];
            int i = 0;

            int j = 0;

            foreach (string line in lines)
            {
                try
                {
                    j = Convert.ToInt32(line);
                    break;
                }
                catch (Exception ex) 
                {
                    arr[i] = line;
                    i++;
                }
            }

            // Q1 //
            Thread q1 = new Thread(() =>
            {
                Fact(j);
            });
            q1.Start();


            // Q2 //
            Thread q2 = new Thread(() =>
            {
                Avr(arr, i);
            });
            q2.Start();

            // Q3 //
            Q3(j, arr, i);
            
            // Q4 //
            Process notepad = Process.Start("notepad", @"C:\Users\themohsen\Desktop\hs.txt");
            Thread.Sleep(10000);
            notepad.Kill();
        }

        static int Fact(int x)
        {
            Thread.Sleep(1000);
            if (x == 1)
            {
                Console.WriteLine($"Q1: {1}");
                return 1;
            }
            else
            {
                int temp = x * Fact(x - 1);
                Console.WriteLine($"Q1: {temp}");
                return temp;
            }
        }

        static float Avr(string[] arr, int i)
        {
            float len = 0;
            for (int x = 0; x < i; x++)
            {
                len += arr[x].Length;
                Thread.Sleep(1000);
                Console.WriteLine(len.ToString());
            }

            return (len / i);
        }

        static void Q3(int num, string[] arr, int i)
        {
            semaphore.WaitOne();
            Thread q1 = new Thread(() =>
            {
                Fact(num);
            });
            q1.Start();
            semaphore.Release();

            semaphore.WaitOne();
            Thread q2 = new Thread(() =>
            {
                Avr(arr, i);
            });
            q2.Start();
            semaphore.Release();

            semaphore.WaitOne();
            Thread write = new Thread(() =>
            {
                string text = "";
                for (int x = i - 1; x >= 0; x--)
                {
                    text += arr[x] + "\n";
                }
                File.WriteAllText(@"C:\Users\themohsen\Desktop\hs.txt", text);
            });
            write.Start();
            semaphore.Release();
        }

    }
}