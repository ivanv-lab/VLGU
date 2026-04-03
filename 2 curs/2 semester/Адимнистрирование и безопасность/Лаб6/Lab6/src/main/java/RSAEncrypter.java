import java.util.Random;

public class RSAEncrypter {
    private final String message;
    private final int p;
    private final int q;

    private int n;
    private int e;
    private long d;
    private final Random random;

    public RSAEncrypter(String message, int p, int q) {
        this.message = message;
        this.p = p;
        this.q = q;
        this.random = new Random();
        initKeys();
    }

    private void initKeys() {
        n = p * q;
        int fi = (p - 1) * (q - 1);
        e = random.nextInt(2, fi - 1);

        while (gcd(e, fi) != 1) {
            e = random.nextInt(2, fi - 1);
            d = modInverse(e, fi);
        }
    }

    private int gcd(int a, int b) {
        a = Math.abs(a);
        b = Math.abs(b);

        while (b != 0) {
            int temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    private int modInverse(int a, int m) {
        int[] gxy = extendedGcd(a, m);
        if (gxy[0] != 1) return 0;
        return gxy[1] % m;
    }

    private int[] extendedGcd(int a, int b) {
        int[] gyx;
        if (a == 0)
            return new int[]{b, 0, 1};
        else gyx = extendedGcd(b % a, a);
        return new int[]{gyx[0], gyx[2] - (b / a) * gyx[1], gyx[1]};
    }

    private long modPow(long base, long exp, long mod){
        if(mod==1) return 0;

        long result=1;
        base=base%mod;

        while (exp>0){
            if((exp & 1)==1)
                result=(result*base)%mod;

            base=(base*base)%mod;
            exp>>=1;
        }

        return result;
    }

    public String encrypt() {
        if (message.isEmpty())
            throw new RuntimeException("Сообщение пустое");

        StringBuilder encrypted = new StringBuilder();
        for (int i = 0; i < message.length(); i++) {
            int m = message.charAt(i);
            long c = modPow(m,e,n);
            encrypted.append(c);
        }

        return encrypted.toString();
    }

    public String decrypt(String encrypted) {
        StringBuilder decrypted=new StringBuilder();
        for(int i=0;i<encrypted.length();i++){
            char ch=encrypted.charAt(i);
            char m = (char) modPow(ch,d,n);
            decrypted.append(m);
        }

        return decrypted.toString();
    }
}
