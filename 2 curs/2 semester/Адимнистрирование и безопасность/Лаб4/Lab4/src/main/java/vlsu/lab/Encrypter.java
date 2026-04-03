package vlsu.lab;

public class Encrypter {
    private final String key;

    private String[] passwordMas;

    private final String alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

    public Encrypter(String key) {
        this.key = key.toUpperCase();

        passwordMas=new String[this.key.length()];
        for(int i=0;i<this.key.length();i++){
            char keyChar=this.key.charAt(i);
            int charIndex=alphabet.indexOf(keyChar);

            passwordMas[i]=alphabet.substring(charIndex)
                    +alphabet.substring(0,charIndex);
        }
    }

    public String encrypt(String text){
        text=text.trim().toUpperCase();
        StringBuilder result= new StringBuilder();

        int keyCharNumber=0;
        for(int i=0;i<text.length();i++){
            char currentChar=text.charAt(i);
            if(Character.isLetter(currentChar)) {
                result.append(encryptChar(currentChar, keyCharNumber));

                if(keyCharNumber<3) keyCharNumber++;
                else keyCharNumber=0;
            } else result.append(' ');
        }

        return result.toString();
    }

    private char encryptChar(char ch, int keyCharNumber){
        int charIndex=alphabet.indexOf(ch);
        return passwordMas[keyCharNumber].charAt(charIndex);
    }

    public String decrypt(String text){
        text=text.trim().toUpperCase();
        StringBuilder result=new StringBuilder();

        int keyCharNumber=0;
        for(int i=0;i<text.length();i++){
            char currentChar=text.charAt(i);
            if(Character.isLetter(currentChar)) {
                result.append(decryptChar(currentChar, keyCharNumber));

                if(keyCharNumber<3) keyCharNumber++;
                else keyCharNumber=0;
            } else result.append(' ');
        }

        return result.toString();
    }

    private char decryptChar(char ch, int keyCharNumber){
        int encryptedIndex = passwordMas[keyCharNumber].indexOf(ch);
        return alphabet.charAt(encryptedIndex);
    }
}
