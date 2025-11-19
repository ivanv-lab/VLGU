const n = 100; 
type ss = array of real; 
var 
a: ss; 
i: integer; 
f: text; 
min,max,mid,st,summ:real; 
k1,k2,k3,k5:integer; 
p1,p2,p3,p5:real; 
function d(var arr: ss; var mid:real): real;//дисперсия 
var 
i: integer; 
mo: real; 
begin 
mo:=0; 
for i:=0 to n-1 do 
begin 
mo:=mo+(sqr(arr[i]-mid)/(n-1)); 
end; 
result:=mo; 
end; 
begin 
assign(f, 'data.txt'); 
reset(f); 
setlength(a,n); 
i:=0; 
summ:=0; 
while not eof(f) do begin 
readln(f,st); 
summ:=summ+st; 
a[i]:=st; 
inc(i); 
end; 
mid:=summ/n; 
min:=a[0]; 
max:=a[0]; 
for i:=0 to n-1 do 
begin 
if a[i] < min then min := a[i]; 
if a[i] > max then max := a[i]; 
if (a[i]>-5) and (a[i]<5) then inc(k5);  
if (a[i]>-3) and (a[i]<3) then inc(k3);  
if (a[i]>-2) and (a[i]<2) then inc(k2);  
if (a[i]>-1) and (a[i]<1) then inc(k1);  
end; 
writeln('Минимальное значение: ',min); 
writeln('Максимальное значение: ',max); 
writeln('Среднее значение: ',mid); 
writeln('Дисперсия: ',d(a,mid)); 
writeln('СКО: ',sqrt(d(a,mid))); 
writeln; 
writeln('Количество значений попадающих в [-5..5]: ',k5, '     Вероятность: ', k5/n); 
writeln('Количество значений попадающих в [-3..3]: ',k3, '     Вероятность: ', k3/n); 
writeln('Количество значений попадающих в [-2..2]: ',k2, '     Вероятность: ', k2/n); 
writeln('Количество значений попадающих в [-1..1]: ',k1, '     Вероятность: ', k1/n); 
end.