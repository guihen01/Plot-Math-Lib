using System;
using System.Numerics;
using FFTlib;
using PlotLib;


namespace Test_Math_Plot_Lib
{
    class Test4
    {

        static void Main(string[] args)
        {

            String argument;

            // Test sur nbechant
            //----------------------------------------------------------
            // FFT et DSP de sinx avec nbechant points  y = 3.sin(2.pi.50.x)   
            //----------------------------------------------------------
            int nbechant;

            //nombre de points a echantillonner 
            // sur creneau de 5.12s   pas temporel :  T = 1/F = 1/50  = 0.02 s 
            // on echantillone toutes les 0.02s au niveau temporel le signal de test
            // on calcule la FFT et DSP, sur 256 points 
            // 5.12/0.02 = 256 points 
            // pas frequentiels de la FFT : F = 1/T = 50 Hz

            nbechant = SiTest.Nbechant(50, 5.12);  //crenau de 5.12 secondes (5120 milisecondes) et frequence de 50 Hz

            //Form a signal containing a 50 Hz sinusoid of amplitude 3
            //S = 3*sin(2*pi*50*t)  

            decimal[] S1 = new decimal[nbechant];
            S1 = SiTest.GenSin(nbechant, 50, 3);

            // resultat de la DFT (Discret Fourier Transform) dans le tableau de complexe : Ts[]
            //----------------------------------------------------------------------------------
            Complex[] Ts = new Complex[nbechant];
            Ts = DFT.DFTv2(S1);

            //Densite spectrale de puissance du signal 
            //---------------------------------------------------------------------------
            decimal[] Module1 = new decimal[nbechant];

            Module1 = DSP.Dspdeci(Ts);

            //plot signal de test : Tableau S[]
            //-------------------------------------------------------------------
            argument = "C:\\C#\\MathsLib\\Test_sysarg-v4.py" + " ";
            //PlotPython.Plot1(S1, argument);

            //plot Densite spectrale de puissance du sgignal
            //------------------------------------------------------------------
            String st1, st2;
            st1 = " axe-frequentiel ";   // axe des X
            st2 = " Apmplitude-spectrum-of-X(t) ";      // axe des Y
            argument = "C:\\C#\\MathsLib\\Sysarg-v5.py" + " ";  
            PlotPython.Plot1WithAxis(Module1,st1, st2, argument);


        }

    }
}

