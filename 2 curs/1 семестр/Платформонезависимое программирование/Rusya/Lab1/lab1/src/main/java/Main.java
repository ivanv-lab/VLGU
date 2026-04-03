import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);

        int n = in.nextInt();

        if(n==0){
            System.out.print("NO SOLUTION");
            return;
        }

        for(int i=0;i<n;i++){
            String line = in.nextLine().trim();
            if (line.isEmpty()) {
                i--;
                continue;
            }

            String[] numbers = line.split(" ");
            long currentNod = Long.parseLong(numbers[0]);
            long currentNok = Long.parseLong(numbers[0]);

            for (int j = 1; j < numbers.length; j++) {
                long num = Long.parseLong(numbers[j]);
                currentNod = nod(currentNod, num);
                currentNok = nok(currentNok, num);
            }

            System.out.println(currentNod + " " + currentNok);
        }
    }

    public static long nod(long a, long b) {
        while (b != 0) {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public static long nok(long a, long b) {
        return a/nod(a,b)*b;
    }
}