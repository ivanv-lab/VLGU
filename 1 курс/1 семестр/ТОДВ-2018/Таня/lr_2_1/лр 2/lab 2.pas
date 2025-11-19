function Сond(str: string): boolean;
 var i: integer; b: boolean;
begin
 b:= true;
  for i := 1 to Trunc(length(str) / 2) do
   b:= b and ((ord(str[i * 2]) >= 48) and (ord(str[i * 2]) <= 57));
   Сond := b;
end;

procedure rp(res:string; n : integer; var count:integer; abc: string; filet: text);
var i: byte;
begin
 if length(res) = n then
  begin
   if Сond(res) then
    begin
     WriteLn(FileT, res);
     count := count + 1;
    end;
  end
 else begin
  for i := 1 to length(abc) do
   rp( res + abc[i], n, count, abc, filet);
      end;
end;
 var ABC: string;
 N: integer;
 FileT: text;
 Count: integer;
begin
Writeln('Введите алфавит');
readln(ABC);
 if (length(ABC) < 2) or (length(ABC) > 8) then
  Writeln('Неверное количество символов в алфавите!')
 else begin
  Writeln('Введите длину слова');
  readln(N);
assign(FileT,'Result.txt');
rewrite(FileT);
rp('', N, Count, ABC, FileT);
WriteLn(FileT, '');
WriteLn(FileT, 'Количество получившихся слов = ' + Count);
close(FileT);
Writeln('Программа завершена корректно. Result.txt сгенерирован');
end;

end.
