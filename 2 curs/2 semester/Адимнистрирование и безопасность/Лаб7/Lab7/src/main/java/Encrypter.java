public class Encrypter {
    private final int[][] matrix;
    private final String message;

    private final int rank;
    private final int matrixLength;

    private final String alphabet="АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

    public Encrypter(int[][] matrix, String message) {
        this.matrix = matrix;
        this.message = message.toUpperCase();
        this.rank= this.matrix.length;
        matrixLength=this.message.length()/rank+this.message.length()%rank;
    }

    public String encrypt(){
        int[][] vectors=getVectorsFromMessage();
        int[][] result=new int[matrixLength][rank];

        for(int i=0;i<matrixLength;i++){
            for(int j=0;j<rank;j++){
                int tempResult=0;
                for(int q=0;q<rank;q++){
                    tempResult+=matrix[j][q]*vectors[i][j];
                }

                result[i][j]=tempResult;
            }
        }

        StringBuilder encrypted=new StringBuilder();
        for(int i=0;i<matrixLength;i++){
            for(int j=0;j<rank;j++){
                encrypted.append(result[i][j]).append(",");
            }
        }

        return encrypted.substring(0,encrypted.length()-1);
    }

    private int[][] getVectorsFromMessage(){
        int[][] vectors=new int[matrixLength][rank];

        int charCounter=0;
        for(int i=0;i<matrixLength;i++){
            for(int j=0;j<rank;j++){
                int alphabetChar;
                if(charCounter<message.length())
                    alphabetChar=alphabet.indexOf(message.charAt(charCounter));
                else alphabetChar=0;
                vectors[i][j]=alphabetChar;
                charCounter++;
            }
        }

        return vectors;
    }
}
