program abc;
uses graphABC;
var w,h:integer;

begin
     MaximizeWindow; 
   ClearWindow(clGhostWhite); 
   coordinate.SetMathematic;
   w:=WindowWidth div 2;
   h:=WindowHeight div 2;
   SetCoordinateOrigin(w, h);
   for var x:=-w to 2*w do
      for var y:=-h to 2*h do
         if ((sqr(y/50) + sqr(x/50)-9 <= 0) xor ((abs(y/50)<=4) and (-6 <= x / 50) and (x / 50 <= 1))) and not (y/50<0) then
            PutPixel(x,y,clSalmon);
   Line(-w,0,2*w,0,clSlateGray); 
   Line(0,-h,0,2*h,clSlateGray);
   TextOut(1,1,0); 
   TextOut(1,h-15,'λ');
   TextOut(w-10,1,'x');
end.