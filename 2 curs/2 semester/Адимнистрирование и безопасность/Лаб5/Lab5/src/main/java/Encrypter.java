import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

public class Encrypter {
    private final String text;
    private final String key;

    private char[] keyArr;
    private int[] keyIndexArr;
    private char[][] textArr;

    private final String alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

    public Encrypter(String text, String key) {
        this.key = key.trim().toUpperCase();
        this.text = text.trim().toUpperCase();

        int keyLength = this.key.length();
        keyArr = new char[keyLength];
        keyIndexArr = new int[keyLength];

        textArr = new char[this.text.length() / keyLength + 1][keyLength];

        for (int i = 0; i < keyLength; i++) {
            keyArr[i] = this.key.charAt(i);
        }

        int[] charIndexes = new int[keyLength];
        for (int i = 0; i < keyLength; i++)
            charIndexes[i] = alphabet.indexOf(this.key.charAt(i));

        keyIndexArr = getIndexed(charIndexes);

        int charCounter = 0;
        for (int i = 0; i < this.text.length() / keyLength + 1; i++) {
            for (int j = 0; j < keyLength; j++) {
                textArr[i][j] = charCounter < this.text.length()
                        ? this.text.charAt(charCounter)
                        : '-';
                charCounter++;
            }
        }
    }

    private int[] getIndexed(int[] array) {
        int[] sorted = array.clone();
        Arrays.sort(sorted);

        Map<Integer, Integer> nextNumber = new HashMap<>();
        Map<Integer, Integer> currentCount = new HashMap<>();

        int current = 0;
        for (int i = 0; i < sorted.length; i++) {
            if (i == 0 || sorted[i] != sorted[i - 1]) {
                nextNumber.put(sorted[i], current);
                currentCount.put(sorted[i], 0);
            }
            current++;
        }

        int[] result = new int[array.length];
        for (int i = 0; i < array.length; i++) {
            int val = array[i];
            int offset = currentCount.get(val);
            result[i] = nextNumber.get(val) + offset;
            currentCount.put(val, offset + 1);
        }

        return result;
    }

    public String encrypt() {
        for (int i = 0; i < key.length(); i++)
            System.out.print(keyArr[i] + " ");
        System.out.println();

        for (int i = 0; i < keyIndexArr.length; i++)
            System.out.print(keyIndexArr[i] + " ");
        System.out.println();

        for (int i = 0; i < text.length() / key.length() + 1; i++) {
            for (int j = 0; j < key.length(); j++) {
                System.out.print(textArr[i][j] + " ");
            }
            System.out.println();
        }
        System.out.println();

        int indexCounter = 0;
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < keyIndexArr.length; i++) {
            int index = 0;
            for (int j = 0; j < keyIndexArr.length; j++) {
                if (keyIndexArr[j] == indexCounter)
                    index = j;
            }

            for (int q = 0; q < text.length() / key.length() + 1; q++) {
                builder.append(textArr[q][index] == '-'
                        ? ""
                        : textArr[q][index]);
            }
            builder.append(" ");

            indexCounter++;
        }

        return builder.toString();
    }
}
