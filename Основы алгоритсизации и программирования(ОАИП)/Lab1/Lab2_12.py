x=float(input("Введите x:"));
y=float(input("Введите y:"));
z=float(input("Введите z:"));
v=-1;

if y>x and y>z:
    v=1;
if y<x and y<z:
    v=0;

print(v);
