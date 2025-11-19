using System;
using System.Text;

namespace Lab_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите кол-во чисел:");
            int count = Convert.ToInt32(Console.ReadLine());

            Random random = new Random();
            int[] mas = new int[count];

            using (FileStream fstream = new FileStream("f0.txt", FileMode.Create))
            {
                for (int i = 0; i < count; i++)
                {
                    byte[] buffer =
                        Encoding.Default.GetBytes(random.Next(0, 100).ToString() + "\n");

                    fstream.Write(buffer, 0, buffer.Length);
                }
            }

            string[] file = File.ReadAllLines("f0.txt");

            for (int i = 0; i < count; i++)
            {
                mas[i] = Convert.ToInt32(file[i]);
            }

            SortArrayByCounting(mas);
            SortArrayByBubble(mas);
            SortArrayBySelect(mas);
            SortArrayByMerge(mas);
        }
        public static void SortArrayByMerge(int[] array)
        {
            if (array.Length <= 1) return;
            int mid = array.Length / 2;
            int[] left = new int[mid];
            int[] right = new int[array.Length - mid];
            for (int i = 0; i < mid; i++)
            {
                left[i] = array[i];
            }
            for(int i = mid; i < array.Length; i++)
            {
                right[i-mid]=array[i];
            }
            static void Merge(int[] arr, int[] left, int[] right)
            {
                int i = 0, j = 0, k = 0;
                while (i < left.Length && j < right.Length)
                {
                    if (left[i] <= right[j]) arr[k++] = left[i++];
                    else arr[k++] = right[j++];
                }
                while (i < left.Length) arr[k++] = left[i++];
                while (j < right.Length) arr[k++] = right[j++];
            }

            SortArrayByMerge(left);
            SortArrayByMerge(right);
            Merge(array,left,right);

            using (FileStream fstream = new FileStream("f4.txt", FileMode.Create))
            {
                for (int i = 0; i < array.Length; i++)
                {
                    byte[] buffer =
                        Encoding.Default.GetBytes(array[i].ToString() + "\n");

                    fstream.Write(buffer, 0, buffer.Length);
                }
            }
        }


        public static void SortArrayBySelect(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int min = i;

                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[min])
                    {
                        min = j;
                    }
                }
                int temp = array[min];
                array[min] = array[i];
                array[i] = temp;
            }

            using (FileStream fstream = new FileStream("f3.txt", FileMode.Create))
            {
                for (int i = 0; i < array.Length; i++)
                {
                    byte[] buffer =
                        Encoding.Default.GetBytes(array[i].ToString() + "\n");

                    fstream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        public static void SortArrayByBubble(int[] array)
        {
            int temp = 0;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        temp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = temp;
                    }
                }
            }

            array = array.Reverse().ToArray();

            using (FileStream fstream = new FileStream("f2.txt", FileMode.Create))
            {
                for (int i = 0; i < array.Length; i++)
                {
                    byte[] buffer =
                        Encoding.Default.GetBytes(array[i].ToString() + "\n");

                    fstream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        public static void SortArrayByCounting(int[] array)
        {
            int max = array.Max();

            int[] temp = new int[max + 1];

            foreach (int i in array)
            {
                temp[i]++;
            }

            int b = 0;
            for (int i = 0; i < max + 1; ++i)
            {
                for (int j = 0; j < temp[i]; ++j)
                {
                    array[b++] = i;
                }
            }

            array = array.Reverse().ToArray();

            using (FileStream fstream = new FileStream("f1.txt", FileMode.Create))
            {
                for (int i = 0; i < array.Length; i++)
                {
                    byte[] buffer =
                        Encoding.Default.GetBytes(array[i].ToString() + "\n");

                    fstream.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
