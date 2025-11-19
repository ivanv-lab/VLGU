var img=[];
img[0]='url("wall/cars.jpg")';
img[1]='url("wall/planets.jpg")';
img[2]='url("wall/abstract.jpg")';
img[3]='url("wall/vector.jpg")';

function changeBG(){
    var i=Math.floor(Math.random()*img.length);
    var elem=document.getElementsByTagName("header");
    elem[0].style.backgroundImage=img[i];
}