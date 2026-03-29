import java.util.Scanner;

public class Main {
    static Scanner in = new Scanner(System.in);

    public static void main(String[] args) {
        System.out.println("Введите матрицу 4x4:");
        System.out.println();

        int[][] matrix = new int[4][4];

        String[] lines = new String[4];
        lines[0] = in.nextLine();
        lines[1] = in.nextLine();
        lines[2] = in.nextLine();
        lines[3] = in.nextLine();

//        int[][] matrix = {
//                {1, 2, 5, 4},
//                {3, 13, 9, 7},
//                {2, 3, 5, 11},
//                {5, 6, -5, 29}};
//
//        String message="Помехоустойчивое кодирование - это кодирование " +
//                "с возможностью восстановления потерянных или ошибочно " +
//                "принятых данных";

        for (int i = 0; i < matrix.length; i++) {
            for (int j = 0; j < matrix.length; j++) {
                matrix[i][j] =
                        Integer.parseInt(lines[i].split(" ")[j]);
            }
        }

        System.out.println("Введите сообщение: ");
        String message=in.nextLine();

        Encrypter encrypter=new Encrypter(matrix, message);

        System.out.println("Зашифрованное сообщение: \n"+encrypter.encrypt());
    }
}
