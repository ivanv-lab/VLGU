<?php ?><!doctype html><meta charset="UTF-8"><title>Задание 12</title><h1>Задание 12</h1>
<label>Частей: <select id="parts"><option value="4">4</option><option value="8">8</option></select></label> <button id="draw">Построить 1000 точек</button><br><canvas id="canvas" width="800" height="500" style="border:1px solid #999"></canvas>
<script>
const canvas = document.getElementById('canvas'), ctx = canvas.getContext('2d');
document.getElementById('draw').onclick = () => { ctx.clearRect(0, 0, canvas.width, canvas.height); const cx = canvas.width / 2, cy = canvas.height / 2, parts = Number(document.getElementById('parts').value), radius = Math.min(cx, cy) - 5; ctx.fillStyle = '#111';
  for (let i = 0; i < 1000; i++) { const x = Math.random() * cx, y = Math.random() * cy, angle = Math.random() * Math.PI / 2; for (let k = 0; k < parts; k++) { const a = angle + k * 2 * Math.PI / parts, px = cx + x * Math.cos(a) - y * Math.sin(a), py = cy + x * Math.sin(a) + y * Math.cos(a); if (px >= 0 && px <= canvas.width && py >= 0 && py <= canvas.height) ctx.fillRect(px, py, 1, 1); } }
};
</script>
