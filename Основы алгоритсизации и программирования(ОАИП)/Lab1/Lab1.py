from math import sqrt


a=float(input("Введите a:"));
b=float(input("Введите b:"));
c=(a+b)/2;
d=round(sqrt(abs(a)*abs(b)),2);

print(f"Среднее арифметическое чисел a и b: {c}");
print(f"Среднее геометрическое чисел a и b: {d}");