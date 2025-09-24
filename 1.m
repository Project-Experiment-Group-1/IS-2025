T0 = 2*pi;                     
t = linspace(0, 2*T0, 1000);   
f_approx = zeros(size(t));     

N = 1000;  

a0 = 0;   

for k = 1:N
    ak = 0;
    bk = (2 / (k * pi)) * (1 - (-1)^k);  
    f_approx = f_approx + ak * cos(k * t) + bk * sin(k * t);
end

f_approx = f_approx + a0;

f_original = @(x) (mod(x, 2*pi) < pi)*1 + (mod(x, 2*pi) >= pi)*(-1);

figure;
plot(t, f_approx, 'r', 'LineWidth', 2); hold on;
xlabel('角度(rad)'); 
title('k=1000');
grid on;