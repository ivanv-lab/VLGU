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
 var abc: string;
 n: integer;
 filet: text;
 count: integer;
begin
Writeln('Введите алфавит');
readln(abc);
 if (length(abc) < 2) or (length(abc) > 8) then
  Writeln('Неверное количество символов в алфавите!')
 else begin
  Writeln('Введите длину слова');
  readln(n);
assign(filet,'filet.txt');
rewrite(filet);
rp('', n, count, abc, filet);
WriteLn(filet, '');
WriteLn(filet, 'Количество получившихся слов = ' + count);
close(filet);
end;

end.
