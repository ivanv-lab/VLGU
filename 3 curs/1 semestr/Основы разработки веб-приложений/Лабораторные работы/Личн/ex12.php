<?php ?><!doctype html><meta charset="UTF-8"><title>Задание 12</title><h1>Задание 12</h1>
<canvas id="canvas" width="640" height="240" style="border:1px solid #999"></canvas>
<script>
const canvas = document.getElementById('canvas'), ctx = canvas.getContext('2d');
for (let x1 = 0; x1 <= 319; x1++) { const y1 = 100, x2 = 120 + 100 * Math.sin(x1 / 30), y2 = 90 + 100 * Math.cos(x1 / 30); ctx.beginPath(); ctx.strokeStyle = `hsl(${Math.random() * 360}, 80%, 45%)`; ctx.moveTo(x1, y1); ctx.lineTo(x2, y2); ctx.stroke(); }
</script>
