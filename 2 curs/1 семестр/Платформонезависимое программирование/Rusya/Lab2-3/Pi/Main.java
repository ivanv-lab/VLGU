import java.util.ArrayList;
import java.util.List;
import java.util.Random;
import java.util.concurrent.*;

public class Main {
    public static void main(String[] args) throws ExecutionException, InterruptedException {
        System.out.println(calculatePi(1_000_000_000));
        System.out.println(calculatePiParallel(1_000_000_000,6));
    }

    public static double calculatePi(long totalPoints) {
        long insideCircle = 0;
        Random random = new Random();

        for (long i = 0; i < totalPoints; i++) {
            double x = random.nextDouble();
            double y = random.nextDouble();

            double distance = Math.pow(x - 0.5, 2) + Math.pow(y - 0.5, 2);
            if (distance <= 0.25) {
                insideCircle++;
            }
        }

        return 4.0 * insideCircle / totalPoints;
    }

    public static double calculatePiParallel(long totalPoints, int numThreads)
            throws InterruptedException, ExecutionException {

        ExecutorService executor = Executors.newFixedThreadPool(numThreads);
        long pointsPerThread = totalPoints / numThreads;

        List<Future<Long>> futures = new ArrayList<>();

        for (int i = 0; i < numThreads; i++) {
            final long points = (i == numThreads - 1) ?
                    totalPoints - pointsPerThread * (numThreads - 1) : pointsPerThread;

            Callable<Long> task = () -> {
                long localInside = 0;
                Random localRandom = new Random(Thread.currentThread().getId());

                for (long j = 0; j < points; j++) {
                    double x = localRandom.nextDouble();
                    double y = localRandom.nextDouble();

                    double distance = Math.pow(x - 0.5, 2) + Math.pow(y - 0.5, 2);
                    if (distance <= 0.25) {
                        localInside++;
                    }
                }
                return localInside;
            };

            futures.add(executor.submit(task));
        }

        long totalInside = 0;
        for (Future<Long> future : futures) {
            totalInside += future.get();
        }

        executor.shutdown();
        return 4.0 * totalInside / totalPoints;
    }
}
