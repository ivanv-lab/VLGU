package vlsu.lab;

import java.util.Scanner;

public class Main {

    private static Scanner in = new Scanner(System.in);

    public static void main(String[] args) {
        System.out.print("Text: ");
        String text = in.nextLine();
        System.out.print("Key: ");
        String key = in.nextLine();

        Encrypter encrypter=new Encrypter(key);

        String encrypted = encrypter.encrypt(text);
        System.out.println("Encrypted text: " + encrypted);
        System.out.println("Decrypted text: "+ encrypter.decrypt(encrypted));
    }
}