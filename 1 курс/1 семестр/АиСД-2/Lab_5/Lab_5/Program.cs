namespace Lab_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 12, 11, 6, 2, 0, 4, 24, 1, 6, 6, 2, 54, 12 };
            int n = arr.Length;

            BinaryHeap binaryHeap = new BinaryHeap();
            binaryHeap.SortHeap(arr);

            for (int i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");
            Console.Read();

            Console.WriteLine();

            arr = binaryHeap.Add(arr, 7);
            n = arr.Length;
            Console.WriteLine("В дерево добавлен элемент");
            for (int i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");
            Console.Read();

            Console.WriteLine();

            binaryHeap.SortHeap(arr);
            Console.WriteLine("Дерево отсортировано");

            for (int i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");
            Console.Read();

            Console.WriteLine();

            arr = binaryHeap.Delete(arr, 4);
            n = arr.Length;
            Console.WriteLine("Из дерева удален элемент");
            for (int i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");
            Console.Read();

            Console.WriteLine();

            binaryHeap.SortHeap(arr);
            Console.WriteLine("Дерево отсортировано");
            for (int i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");
            Console.Read();

            Console.WriteLine();

            arr = binaryHeap.Merge(new int[] { 1, 4, 6, 2, 7 },
                new int[] { 6, 2, 34, 1, 0, 45 });
            n = arr.Length;
            Console.WriteLine("2 кучи объединены в 1");
            for (int i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");
            Console.Read();

            Console.WriteLine();

            binaryHeap.SortHeap(arr);
            Console.WriteLine("Дерево отсортировано");
            for (int i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");
            Console.Read();
        }

        public class BinaryHeap
        {
            //Сортировка
            public void SortHeap(int[] arr)
            {
                int n = arr.Length;
                for (int i = n / 2 - 1; i >= 0; i--)
                    Heap(arr, n, i);

                for (int i = n - 1; i >= 0; i--)
                {
                    int temp = arr[0];
                    arr[0] = arr[i];
                    arr[i] = temp;

                    Heap(arr, i, 0);
                }
            }

            //Создание
            void Heap(int[] arr, int n, int i)
            {
                int largest = i;
                
                int l = 2 * i + 1;
                int r = 2 * i + 2;

                if (l < n && arr[l] > arr[largest])
                    largest = l;

                if (r < n && arr[r] > arr[largest])
                    largest = r;

                if (largest != i)
                {
                    int swap = arr[i];
                    arr[i] = arr[largest];
                    arr[largest] = swap;

                    Heap(arr, n, largest);
                }
            }

            //Добавление
            public int[] Add(int[] arr,int val)
            {
                int[] temp = new int[arr.Length + 1];

                for(int i = 0; i < arr.Length; i++)
                {
                    temp[i] = arr[i];
                }
                temp[temp.Length - 1] = val;

                arr=new int[temp.Length];
                for(int i=0; i < temp.Length; i++)
                {
                    arr[i] = temp[i];
                }

                return arr;
            }

            //Удаление
            public int[] Delete(int[] arr,int valInd)
            {
                List<int> temp = arr.ToList();
                temp.Remove(arr[valInd]);
                arr=new int[temp.Count];
                for(int i = 0; i < temp.Count; i++)
                {
                    arr[i]=temp[i];
                }

                return arr;
            }

            //Объединение
            public int[] Merge(int[] arr1, int[] arr2)
            {
                int[] temp = new int[arr1.Length + arr2.Length];

                for (int i = 0; i < arr1.Length; i++)
                {
                    temp[i] = arr1[i];
                }
                int q = 0;
                for(int i=arr1.Length; i < arr2.Length+arr1.Length; i++)
                {
                    temp[i] = arr2[q];
                    q ++;
                }

                return temp;
            }
        }
    }
}
