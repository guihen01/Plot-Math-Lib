using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace PlotLib
{
    public class Class1
    {
    }

    public class FormatArgument
    {

        // arguments en entree : 
        //      b : String qui contient le shell Python a executer
        //      a  : tableau des donées (type : décimal) a ploter
        // argument en sortie :
        //      String argument : chaine de string formatée a passer comme arguments au shell Python
        public static string FormerArguments(decimal[] a, string b)
        {
            //fabrication de la chaine d arguments 
            //---------------------------------------------------------------
            string[] arg = new string[a.Length];

            //  la variable b contient le shell python a executer
            //  par exemple : "C:\\C#\\MathsLib\\Custom_2D_Plot-1.py";
            //---------------------------------------------------------------

            //---------------------------------------------------------------
            string specifier;
            CultureInfo culture;
            // Use standard numeric format specifiers.
            specifier = "G";
            culture = CultureInfo.CreateSpecificCulture("eu-ES");

            for (int i = 1; i < a.Length; i++)
            {
                arg[i] = a[i].ToString(specifier, CultureInfo.InvariantCulture);
            }

            //formattage de la ligne d'arguments a passer au shell python   
            //---------------------------------------------------------------
            string argument;

            argument = b;
            for (int i = 1; i < a.Length; i++)
            { argument = argument + arg[i] + " "; }

            //Console.WriteLine("argument =" + argument);

            return argument;
        }

        // arguments en entree : 
        //      b : String qui contient le shell Python a executer
        //      X et Y  : tableau des données (type : décimal) a ploter
        //     X[]  pour l<Axe des X 
        //     Y[] pour l<axe des Y
        // argument en sortie :
        //      String argument : chaine de string formatée a passer comme arguments au shell Python
        public static string FormerArgXY(decimal[] X, decimal[] Y, string b)
        {
            //---------------------------------------------------------------
            string[] arg = new string[X.Length];
            string[] arg1 = new string[Y.Length];

            //---------------------------------------------------------------
            string specifier;
            CultureInfo culture;
            // Use standard numeric format specifiers.
            specifier = "G";
            culture = CultureInfo.CreateSpecificCulture("eu-ES");

            for (int i = 0; i < X.Length; i++)
            {  arg[i] = X[i].ToString(specifier, CultureInfo.InvariantCulture);
            }
            for (int i = 0; i < Y.Length; i++)
            {  arg1[i] = Y[i].ToString(specifier, CultureInfo.InvariantCulture);
            }

            //formattage de la ligne d'arguments a passer au shell python   
            //---------------------------------------------------------------
            // 1 premier argument             : pathname concatene au nom du shell python
            // 2 deuxieme argument            : nombre de points du tableau X (identique a Y)
            // 3 a XLength                    : X[] arguments suivants de 3 a  X.Length
            // XLength + 1 a (2*Xlength +1)   : Y[] arguments suivants  

            string argument;

            argument = b + " ";
            argument = argument + X.Length + " ";
            for (int i = 0; i < X.Length; i++)
            { argument = argument + arg[i] + " "; }

            for (int i = 0; i < Y.Length; i++)
            { argument = argument + arg1[i] + " "; }

            return argument;
        }



        // arguments en entree : 
        //      Si : String qui contient le shell Python a executer
        //      Ti  : tableau des donées (type : décimal) a ploter
        //      axisX : commentaire , titre de l'axe des X
        //      axisY : commentaire , titre de l'axe des Y
        // argument en sortie :
        //      String argument : chaine de string formatée a passer comme arguments au shell Python
        // conditions de fonctionnement :
        //    pas de blanc dans les strings des commentaires a mettre dans l<Axe ds X et Y
        //    les blancs seront remplaces par des - 
        //    exemple : "axe des x" pourrait etre remplacé par "axe-des-X"
        public static String FormAxis( decimal[] Ti, String axisX, String axisY, String Si)
        {
             if (axisX.Contains(" "))
             {
                Console.WriteLine("axisX ne doit pas contenir d'espace");
                Console.WriteLine("Saisissez a nouveau : titre de l'axe des X ");
                axisX = Console.ReadLine();
             }
            if (axisY.Contains(" "))
            {
                Console.WriteLine("axisY ne doit pas contenir d'espace");
                Console.WriteLine("Saisissez a nouveau : titre de l'axe des Y ");
                axisY = Console.ReadLine();
            }

            //fabrication de la chaine d arguments 
            //---------------------------------------------------------------
            string[] arg = new string[Ti.Length];

            //  la variable Argument contient le shell python a executer
            //  par exemple : "C:\\C#\\MathsLib\\Custom_2D_Plot-1.py";
            //---------------------------------------------------------------

            // la variable Ti contient le Tableau de points a tracer 
            //-------------------------------------------------------------
            string specifier;
            CultureInfo culture;
            // Use standard numeric format specifiers.
            specifier = "G";
            culture = CultureInfo.CreateSpecificCulture("eu-ES");

            for (int i = 0; i < Ti.Length; i++)
            {
                arg[i] = Ti[i].ToString(specifier, CultureInfo.InvariantCulture);
            }

            //formattage de la ligne d'arguments a passer au shell python   
            //---------------------------------------------------------------
            string argument; 
            argument = Si;  // nom du shell python a executer
            argument = argument + " " + axisX + " " + axisY + " ";
            for (int i = 0; i < Ti.Length; i++)
            { argument = argument + arg[i] + " "; }

            return argument;
        }

    }

    public class PlotPython
    {

        // arguments en entree : 
        //      String argument : nom du shell Phyton a executer
        //      Ti  : tableau des donées (type : décimal) a ploter
        static public void Plot1(decimal[] Ti, String argument)
        {
            // Mise en forme de string du tableau de donnees : TI
            //--------------------------------------------------
            argument = FormatArgument.FormerArguments(Ti, argument);

            // impression du tableau de donnees : TI 
            //----------------------------------------------------
            PlotPython.Plot2D1(argument);
        }

        //
        // arguments en entree : 
        //          Ti[] Tableau de données (type : décimal) a ploter 
        //          axisX : commentaire , titre de l'axe des X
        //          axisY : commentaire , titre de l'axe des Y
        //          argument :  nom du shell python a executer
        static public void Plot1WithAxis(decimal[] Ti, String axisX, String axisY, String argument)
        {
            argument = FormatArgument.FormAxis(Ti, axisX, axisY, argument);

            // impression du tableau de donnees : TI 
            //----------------------------------------------------
            PlotPython.Plot2D1(argument);
        }

        // impression d'une partie (extraction) d'un tableau de points
        // arguments en entree :  
        //        Ti : tableau de données calculés, dont on veut extraire une partie (a compter du début du tableau) 
        //        Nbpoints : nombre de points du tableau que l'on veut ploter
        //        argument : shell python a executer
        static public void Plot2(decimal[] Ti, int Nbpoints, String argument)
        {
            //extraction de Nbpoints du tableau Ti
            decimal[] Tr = new decimal[Nbpoints];
            for (int i = 0; i < Nbpoints; i++)
            {   Tr[i] = Ti[i];
            }
            // Mise en forme de string du tableau de donnees : Tr
            //--------------------------------------------------
            argument = FormatArgument.FormerArguments(Tr, argument);

            // impression du tableau de donnees : Tr 
            //----------------------------------------------------
            PlotPython.Plot2D1(argument);
        }

        // impression tableau de points sur axe X et axe Y
        // arguments en entree :  
        //        Xi[] : tableau de données pour l<Axe des X
        //        Yi[] : tableau de données pour l<Axe des Y
        //        Nbpoints : nombre de points du tableau Y[] que l'on veut ploter
        //        argument : shell python a executer
        // On doit toujours avoir Nbpoints <= Xi.Length  et <= Yi.Length
        // Xi et Yi doivent avoir la meme dimension ( le meme nombre de points)
        static public void PlotXY(decimal[] Xi, decimal[] Yi, int Nbpoints, String argument)
        {
            while ((Nbpoints > Xi.Length) || (Nbpoints > Yi.Length))
            {
                Console.Write("Nbpoints doit etre < Xi.Length et < Yi.length");
                Console.Write("entrez le nombre points a ploter");
                string rep;
                rep = Console.ReadLine();
                Nbpoints = Convert.ToInt32(rep);
            }
            if (Xi.Length != Yi.Length)
            {
                Console.Write(" Xi[i] et Yi[] nont pas la meme dimension ");
                Console.Write(" Xi.Length est differend de Yi.Length");
                System.Environment.Exit(0);
            }    

            //extraction de Nbpoints du tableau Xi
            decimal[] Xs = new decimal[Nbpoints];
            for (int i = 0; i < Nbpoints; i++)
            {   Xs[i] = Xi[i];
            }
            //extraction de Nbpoints du tableau Yi
            decimal[] Ys = new decimal[Nbpoints];
            for (int i = 0; i < Nbpoints; i++)
            {   Ys[i] = Yi[i];
            }

            // Mise en forme de string des tableaux de données
            //--------------------------------------------------
            argument = FormatArgument.FormerArgXY(Xs,Ys, argument);

            // impression des données
            //----------------------------------------------------
            PlotPython.Plot2D1(argument);
        }

        // impression  d'un fichier de données qui contient une DSP (Densité Spectrale de puissance) 
        // arument en entree : 
        //     string Name : nom du fichier qui contient les données a ploter
        //
        static public void Plot5DSP(string Name)
        {
            String Argument;
            Argument = "Plot5-DSP.py" + " " + Name; 
            PlotPython.Plot2D1(Argument);
        }

        // impression  d'un fichier de données qui contient le signal éhantillonné a la fréquence Fe (en Hz) 
        // arument en entree : 
        //     string Name : nom du fichier qui contient les données a ploter
        //
        static public void Plot5signal(string Name)
        {
            String Argument;
            Argument = "Plot5-signal.py" + " " + Name;
            PlotPython.Plot2D1(Argument);
        }

        public static void Plot2D1(string argument)

        {
            //-------------------------------------------------------
            //Passing arguements from C# to python
            // we plot an array of points with a 2D ploting in console
            //-------------------------------------------------------
            // the array of point are calculated in c# with fftilb library (Henri Guillot Library) and then passed as arguments to a python shell code
            //----------------------------------------------------------------------------------------------------------------

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "C:\\Program Files (x86)\\Microsoft Visual Studio\\Shared\\Python37_64\\python.exe";
            Console.Write(argument.Length);

            // arg[0] = Path to your python script (example : "C:\\add_them.py")
            // arg[1] = first arguement taken from  C#'s main method's args variable (here i'm passing a number : 5)
            // arg[2] = second arguement taken from  C#'s main method's args variable ( here i'm passing a number : 6)
            // pass these to your Arguements property of your ProcessStartInfo instance


            start.Arguments = argument;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    // this prints the array of points in 2D ploting
                    Console.Write(result);

                }
            }
            Console.Read();
        }
    }
}
