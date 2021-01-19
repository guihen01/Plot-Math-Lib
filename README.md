
<center>
    <img src="https://github.com/guihen01/Plot-Math-Lib/blob/main/Test3/Figure_2.png
" alt="icon" width="256" height="256"/>
</center>


# Plot-Math-Lib
Librairie de routines gtraphiques 
Développées en C# et Python

# Dependencies
none required

# cs project 
Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp3.1</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <Reference Include="FFTlib">
      <HintPath>..\..\Math-FFT-Lib\FFTlib\bin\Debug\netstandard2.0\FFTlib.dll</HintPath>
    </Reference>
    <Reference Include="PlotLib">
      <HintPath>..\..\Math-PLOT-Lib\PlotLib\bin\Debug\netstandard2.0\PlotLib.dll</HintPath>
    </Reference>
  </ItemGroup>

</Project>

# Building
 in C# import (include) the library DLL : Plotlib.dll in your project
 add the instruction : using PlotLib; at the top of your code source, where and when needed
 ie : 
using System;
using System.Numerics;
using FFTlib;
using PlotLib;

# How to use : 

See directories Test3, Test4 in this repository 

https://github.com/guihen01/Plot-Math-Lib/tree/main/Test3


# screenshots
https://github.com/guihen01/Plot-Math-Lib/blob/main/Test3/Figure_2.png

# screenshot signal 

https://github.com/guihen01/Plot-Math-Lib/blob/main/Capture%20signal.PNG
![alt text]( https://github.com/guihen01/Plot-Math-Lib/blob/main/Capture%20signal.PNG "Logo Title Text 1")

# screenshot DFT (Discrete Fourier Transform)

Here a simple DFT :

![alt text]( https://github.com/guihen01/Plot-Math-Lib/blob/main/Capture%20DFT.PNG  "Logo Title Text 1")
