import numpy as np
import matplotlib.pyplot as plt 
import sys
import matplotlib.patches as mpatches
from mpl_axes_aligner import align

#M = np.loadtxt('DFT.txt')
#M

print("this is the name of the script ", sys.argv[0])
print("number of arguments ",len(sys.argv))
print("the arguments are ", str(sys.argv))
print("list of arguments " + str(sys.argv))
for i in range (0,2):
      print(sys.argv[i])

#ouverture fichier
#f = open("DFT.txt", "r")
#t = f.readline()
#while t:
#    print(t)
#    # utilisez readline() pour lire la ligne suivante
#    t = f.readline()
#    f.close()

#D/claration d<un tableau
Y =  []
# init du tableau 
for i in range(0,2048):
    Y.append(0.0)
# autre m/thode pour initialiser
Y1 = []
Y1 = [0.0] * 2048

# Autre m/thodes de lecture de fichier
# Ouvrir le fichier des Data en lecture seule
#--------------------------------------------------------
i=0
f = open(sys.argv[1], "r")
for line in f:
    #print(line)
    str1= line.strip();
    str1 = str1.replace(',','.')
    #print(str1)
    #Y.append(float(str1))
    Y[i] = float(str1)
    i = i+1
    Len = i-2   # nbechant : nombre d'échantillons a ploter
    
Fe = Y[0]        # Frequence d<echantillonnage (en HZ)
print("Fe = ",Fe)

nbechant = Y[1]  #nombre d</chantillons nbechant
print("nbechant = ", nbechant)
print(" Len  = ", Len)

f.close()

#calcul du pas de frequence
Pas = Fe/nbechant
PasFreq = str(Pas)    # converti en string
print("pas = ", Pas)

#D/claration d<un tableau pour l<axe des Y
#---------------------------------------------------
Y1 =  []
# init du tableau 
for i in range(0,Len):
    Y1.append(0.0)   

# Remplissage du tableau de points a afficher ( en fonction du tableau lu dans le fichier)
for i in range(0,Len):
    Y1[i] = Y[i+2]      

#verification
#for i in range (0,Len):
#    print(Y1[i])
#

#tableau de l<Axe des X 
#-----------------------------------------
#Declaration du tableau
Xaxe1 =  []
# init du tableau 
Xaxe1 = np.tile(0,Len)

# Remplissage du tableau de points a afficher sur l<Axe principal des X
for j in range(0,Len):
  Xaxe1[j] = j
  #print(Xaxe1[j])

# Multiplication des abcisses par le pas frequentiel pour afficher la valeur reelle en Hz des X sur un second axe
#-----------------------------------------------------------------------------------------------------------------
Xaxe2 = []
# init du tableau
for i in range(0,Len):
   Xaxe2.append(0.0)
 
for j in range(0,Len):
  Xaxe2[j] = (float)(j*Pas)
  #Xaxe2[j] = j
  #print(Xaxe2[j])

#Affichage de la DSP (Densité Spectrale de Puissance)
#------------------------------------------------------
plt.plot(Xaxe1, Y1)

# naming the x axis   
Axis = 'x - axis pas de fequence = ' + PasFreq + ' Hz'
Axe1 = plt.gca()
#Axe1 = plt.xlabel(Axis)
Axe1.set_xlabel(Axis)  
Axe1.set_xticks(Xaxe1)

# instantiate a second axes that shares the same x-axis
#------------------------------------------------------
Axe2 = Axe1.twiny() 
Axe2.set_xticks(Xaxe2)
Axe2.set_xlabel('Frequence (en HZ) ')

# naming the y axis 
Axis1 =  'y - DSP (Densité Spectrale de Puissance '
Axe1.set_ylabel(Axis1)
#plt.ylabel('y - DSP (Densité Spectrale de Puissance ')   
  
# giving a title to my graph   
red_patch = mpatches.Patch(color='blue', label='|X(f)|*|X(f)|/N')
plt.legend(handles=[red_patch])

#Ajustement et alignement des deux axes x ---- on affiche 10 points
#------------------------------------------------------------------
Axe1.set_xticks(np.linspace(0, Xaxe1[Len-1], 10))
Axe2.set_xticks(np.linspace(0, Xaxe2[Len-1], 10))

# Adjust the plotting range of two X axes
org1 = 0  # Origin of first axis
org2 = 0  # Origin of second axis
pos = 0.1  # Position the two origins are aligned
align.xaxes(Axe1, org1, Axe2, org2, pos)

#plt.tight_layout()
# function to show the plot   
plt.show()
