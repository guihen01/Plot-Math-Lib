import matplotlib.pyplot as plt 
import sys
import numpy as np

print("this is the name of the script ", sys.argv[0])
print("number of arguments ",len(sys.argv))
print("the arguments are ", str(sys.argv))
#print("list of arguments " + str(sys.argv))
#print("the arguments are ", str(sys.argv[0]))
#print("the arguments are ", str(sys.argv[1])
#print("the arguments are ", str(sys.argv[2])
for i in range (0,3):
      print(sys.argv[i])

#  y axis values
#y = list(range(1,len(sys.argv)-1))

a = range(3,len(sys.argv))
y = np.asarray(a, dtype = float) 

for i in range (0,len(sys.argv)-3):
      y[i] = float(sys.argv[i+3])

#verification
#for i in range (0,len(sys.argv)-3):
#     print(y[i])

# corresponding y axis values 
# y = [sys.argv[1],sys.argv[2],sys.argv[3],sys.argv[4],sys.argv[5],sys.argv[6]]

# x axis value
#initialisation du tableau par des 0
x = np.tile(0,len(sys.argv)-3 )
 
 
for i  in range(0,len(sys.argv)-3):
 x[i] = i
 print(x[i])


# plotting the points   
#plt.plot(x, y, color='green', linestyle='dashed', linewidth = 3,   
#marker='o', markerfacecolor='blue', markersize=12)   
      
plt.plot(x, y) 
   
# setting x and y axis range   
#plt.ylim(1,len(sys.argv)-1)   
#plt.xlim(1,len(sys.argv)-1)  

#plt.ylim(1,10)   
#plt.xlim(1,10)   
  
# naming the x axis   
#plt.xlabel('x - axis')
plt.xlabel(str(sys.argv[1]))  

# naming the y axis   
#plt.ylabel('y - axis')   
plt.ylabel(str(sys.argv[2]))

# giving a title to my graph   
plt.title(' DSP (Signal Spectral Density')   
  
# function to show the plot   
plt.show()   




 
