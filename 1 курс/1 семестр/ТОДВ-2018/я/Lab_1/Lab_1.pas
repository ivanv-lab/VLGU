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
        if ((sqr(x/50) + sqr(y/50)-4*x <= 0) 
        or ((sqr(x/50) + sqr(y/50)+4*x <= 0) 
        xor (abs(x / 50) <= 2))) 
        and(abs(y/50)<=2) 
        then
            PutPixel(x,y,clSalmon);
   Line(-w,0,2*w,0,clSlateGray); 
   Line(0,-h,0,2*h,clSlateGray);
   TextOut(1,1,0); 
   TextOut(1,h-15,'λ');
   TextOut(w-10,1,'x');
end.