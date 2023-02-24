require "lab"
require "nn"

dataset={};
function dataset:size() return 100 end -- 100 examples
for i=1,dataset:size() do 
	local input= lab.randn(2);     --normally distributed example in 2d
	local output= torch.Tensor(1);
	if input[1]*input[2]>0 then    --calculate label for XOR function
		output[1]=-1;
	else
		output[1]=1;
	end
	dataset[i] = {input, output};
end


criterion = nn.MSECriterion()  
mlp=nn.Sequential();  -- make a multi-layer perceptron
inputs=2; outputs=1; HUs=20;
mlp:add(nn.Linear(inputs,HUs))
mlp:add(nn.Tanh())
mlp:add(nn.Linear(HUs,outputs))

criterion = nn.MSECriterion()  
trainer = nn.StochasticGradient(mlp, criterion)
trainer.learningRate = 0.01
trainer:train(dataset)

for i = 1,2500 do
  -- random sample
  local input= lab.randn(2);     -- normally distributed example in 2d
  local output= torch.Tensor(1);
  if input[1]*input[2] > 0 then  -- calculate label for XOR function
    output[1] = -1
  else
    output[1] = 1
  end

  -- feed it to the neural network and the criterion
  prediction = mlp:forward(input)
  criterion:forward(prediction, output)

  -- train over this example in 3 steps

  -- (1) zero the accumulation of the gradients
  mlp:zeroGradParameters()

  -- (2) accumulate gradients
  criterion_gradient = criterion:backward(prediction, output)
  mlp:backward(input, criterion_gradient)

  -- (3) update parameters with a 0.01 learning rate
  mlp:updateParameters(0.01)
end