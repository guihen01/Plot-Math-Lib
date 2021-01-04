using System;
using System.Numerics;
using System.IO;
using FFTlib;
using PlotLib;


namespace Test14
{
     //--------------------------------------------------------------------------------------------
     // test avec génération de signaux 
     // superposition de deux signaux sinusoidaux de fréquences très proches 
     // afin de tester la détection fine des fréquences de base des 2 signaux par la fenetre de Hamming
     // 1) Génération des signaux
     // 2) écritutre signaux dans un fichier 
     // 3) ouverture fichier 
     // 4) traitement du signal avec fenetrage Hamming
     // 5) calcul DFT sur le signal
     // 6) Ploting (affichage) a l'écran
     //------------------------------------------------------------------------------------------------
    
    class Program
    {

        //stockage des données de calcul d'une DFT dans un ficher au format binaire.
        //-----------------------------------------------------------------------
        static void CreFicD(decimal[] Xi)
        {
            //LEcture et ecriture d<un  fichier binaire, remplis avec des valeurs decimales

            if (File.Exists("DFT.txt"))
            {   File.Delete("DFT.txt");
            }

            BinaryWriter bw;
            bw = new BinaryWriter(File.Open("DFT.txt", FileMode.Create));
            for (int i = 0; i < Xi.Length; i++)
            {
              bw.Write(Xi[i]);
            }
            bw.Close();
        }

        //stockage des données de calcul d'une DFT dans un ficher au format texte.
        // tableau DFT[] au format decimal
        // argument en entree : 
        //        decimal Xi[] :  donnees DFT
        //        string Name  : nom du fichier de donnes
        //        double Fe    : frequence dechantillonnage (en Hz)
        //        int nbechant : nombre d<Echantillons
        //-----------------------------------------------------------------------
        static void StoficSign(decimal[] Xi, String Name, double Fe, int nbechant)    //Write decimal array to a text file 
        {
            // re-initialisation du fichier de test
            if (File.Exists(Name))
            {   File.Delete(Name);
            }

            var outf = new StreamWriter(Name);     //creation du fichier texte
            outf.WriteLine(" {0} ", Fe);         // informations pour stockage des données de calcul d'une DFT dans un ficher au format texte.
            outf.WriteLine(" {0} ", nbechant);
            for (int i = 0; i < Xi.Length; i++)
                outf.WriteLine(" {0} ", Xi[i]);

            outf.Close();

        }

         
        static void Main(string[] args)
        {

            int nbechant;
            double Fe;     //Frequence d echantllonnage
            double Duree;  // Duree du signal (en secondes)
            double Te;     //pas temporel d echantillonnage : Te

            Console.WriteLine(" nombre d'échantillons ? ");
            string rep;
            rep = Console.ReadLine();
            nbechant = Convert.ToInt32(rep);

            //String reppad = "0";
			//if ( nbechant <= 128) 
			//{  Console.WriteLine("-----------------------------------------------------------------");
            //   Console.WriteLine("nombre echantillons bas . voulez vous du 0 padding ? ");
			//   Console.WriteLine(" oui (1)  non (0) ? ");
			//   Console.WriteLine("-----------------------------------------------------------------");
			//   reppad = Console.ReadLine();
			//}
			
            Console.WriteLine(" durée d<echantillonnage (en secondes) ? ");
            rep = Console.ReadLine();
            Duree = Convert.ToDouble(rep);

            Te = Duree / nbechant;
            Fe = 1 / Te;  // frequence d echantillonnage : Fe
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("Frequence d echantillonnage : Fe = {0} Hz ", Fe);

            //pas frequentiel 
            double pas;
            pas = Fe / (double)nbechant;
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("pas de Frequence (resolution frequentielle) (axe des X pour la DFT");
            Console.WriteLine("un point de mesure de DFT touts les : {0} Hz  pas = {0} Hz ", pas, pas);
            Console.WriteLine("---------------------------------------------------------------------");

            decimal[] S2 = new decimal[nbechant];
            decimal[] S3 = new decimal[nbechant];
            decimal[] Si = new decimal[nbechant];
            decimal[] Sign = new decimal[nbechant];
            decimal[] H = new decimal[nbechant];

            // Generation du signal de test suivant : avec les parametres : 
            //     frequence dechantillonnage Fe
            //     nombre dechantillons : nbechant
            //     frequence snusoidale : 2 Khz et 2.35 Khz
            //----------------------------------------------------------
            // S = 1 * sin(2 * pi * Freq1 * t) + 1*sin(2 * pi * Freq2 * t)
            //---------------------------------------------------------

            double Freq1, Freq2;
            Console.WriteLine(" frequence du signal 1 (en Hz) : 1*sin(2*pi*Freq1*t) ? ");
            rep = Console.ReadLine();
            Freq1 = Convert.ToDouble(rep);

            Console.WriteLine(" frequence du signal 2 (en Hz): 1*sin(2*pi*Freq2*t)  ? ");
            rep = Console.ReadLine();
            Freq2 = Convert.ToDouble(rep);

            S2 = SiTest.GenSin2piF(nbechant, Freq1, 1, Fe);
            S3 = SiTest.GenSin2piF(nbechant, Freq2, 1, Fe);

            // signal resultant "Addition des deux signaux sinusoidaux
            for (int i = 0; i < nbechant; i++)
            {   Si[i] = S2[i] + S3[i];
            }

            
            Console.WriteLine(" Fenetre de Hamming (1) ou fenetre de Hanning (2) ? ");
            rep = Console.ReadLine();
            if (rep == "1")
            {  //----------------------------------------------------
                // Fenetre de Hamming
                //--------------------------------------------------
                H = Fenetre.HammingD(nbechant);
            }
            else if (rep == "2")
            {  //----------------------------------------------------
               // Fenetre de Hanning
               //--------------------------------------------------
               H = Fenetre.HanningD(nbechant);
            }
                

            // Mutiplication du signal par la fenetre choisie 
            //---------------------------------------------------------
            for (int i = 0; i < nbechant; i++)
            {   Sign[i] = Si[i] * H[i];
            }

            //Sauvegarde dans fichier pour v/rifications ou traitement ulterieur
            //-------------------------------------------------------------------
             StoficSign(Sign, "Signal.txt", Fe, nbechant);
             
            //affichage du signal de test
            //--------------------------------------------------------------------------------
             PlotPython.Plot5signal("Signal.txt");

            // resultat de la DFT (Discret Fourier Transform)  
            //----------------------------------------------------------------------------------
            Complex[] Ts = new Complex[nbechant];
            Ts = DFT.DFTv2(Sign);

            //----------------------------------------------------------------------------
            //Densite spectrale de puissance du signal 
            //---------------------------------------------------------------------------
            decimal[] Module1 = new decimal[nbechant];
            Module1 = DSP.Dspdeci(Ts);

            //Sauvegarde dans fichier pour v/rifications ou traitement ulterieur
            //-------------------------------------------------------------------
            StoficSign(Module1,"DFT.txt", Fe,nbechant);
              

            //plot Densite spectrale de puissance du sgignal 
            //------------------------------------------------------------------
            PlotPython.Plot5DSP("DFT.txt");

            




        }


    }
}