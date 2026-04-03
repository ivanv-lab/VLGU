import java.util.Scanner;

public class Main {

    static Scanner in=new Scanner(System.in);

    public static void main(String[] args) {
//        System.out.print("Введите сообщение: ");
//        String message=in.nextLine();
//
//        System.out.print("Введите значение p: ");
//        int p=in.nextInt();
//        System.out.print("Введите значение q: ");
//        int q=in.nextInt();

        String message="Новое сообщение";
        int p=59;
        int q=11;
        RSAEncrypter encrypter=new RSAEncrypter(message, p, q);
        String encrypted=encrypter.encrypt();

        System.out.printf(
                "Зашифрованное сообщение: %s%n",
                encrypted);

        System.out.printf("Дешифрованное сообщение: %s",
                encrypter.decrypt(encrypted));
    }
}
