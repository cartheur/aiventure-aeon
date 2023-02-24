require "torch"
require "lab"
x=torch.Tensor(5,5)
for i=1,5 do 
 for j=1,5 do 
   x[i][j]=math.random();
 end
end
print(x)

x1=lab.rand(5,5)
x2=lab.sum(x1); 
print(x2) 