namespace Lab1
{
    internal class Program
    {
        /// <summary> 
        /// The main entry point for the application. 
        /// </summary> 
        [STAThread]
        static void Main(string[] args)
        {
            int ArraySize;  //размер будущего вектора (массива) 
            int[] BinaryArray; //массив элементов принадлежащих множеству {0, 1} 

            Console.WriteLine("Введите число элементов вектора: ");
            /*Т.к. Функция ReadLine возвращает строку символов введенную с клавиатуры, 
             *то необходимо ее преобразовать к целочисленному типу (к типу int). 
             *Метод Parse выполняет это преобразование. Int32 в данном случае это 
             *целочисленный тип данных, поддерживаемый платформой .NET Framework, 
             *который является аналогом типа int, применяемого в C#*/
            ArraySize = Int32.Parse(Console.ReadLine());
            BinaryArray = new int[ArraySize];

            /* Инициализируем массив значениями 0 или 1!!! 
             * Никаких проверок не выполняем!!!*/
            for (int i = 0; i < ArraySize; i++)
            {
                Console.Write("Введите " + i.ToString() + " элемент ");
                BinaryArray[i] = Int32.Parse(Console.ReadLine());
            }

            // Начинаем обрабатывать наш массив в соответствии с заданием. 
            int j = 0,  // индекс для обработки массива 
            OnesCounter = 0; // счетчик единиц 
            while (j < ArraySize && BinaryArray[j] != 1)
                j++;

            while (j < ArraySize && BinaryArray[j] == 1)
            {
                j++;
                OnesCounter++;
            }
            Console.WriteLine("Количество единиц: " + OnesCounter.ToString());
            Console.ReadLine();
        }
    }
}
