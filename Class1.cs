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

            // la variable a contient le Tableau de points a tracer 
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

            Console.WriteLine("argument =" + argument);

            return argument;
        }


        // arguments en entree : 
        //      Si : String qui contient le shell Python a executer
        //      Ti  : tableau des donées (type : décimal) a ploter
        //      axisX : commentaire , titre de l'axe des X
        //      axisY : commentaire , titre de l'axe des Y
        // argument en sortie :
        //      String argument : chaine de string formatée a passer comme arguments au shell Python
        public static String FormAxis( decimal[] Ti, String axisX, String axisY, String Si)
        {
           
            //fabrication de la chaine d arguments 
            //---------------------------------------------------------------
            string[] arg = new string[Ti.Length];

            //  la variable Argument contient le shell python a executer
            //  par exemple : "C:\\C#\\MathsLib\\Custom_2D_Plot-1.py";
            //---------------------------------------------------------------

            // la variable Ti contient le Tableau de points a tracer 
            //-------------------------------------------------------------
            //---------------------------------------------------------------
            string specifier;
            CultureInfo culture;
            // Use standard numeric format specifiers.
            specifier = "G";
            culture = CultureInfo.CreateSpecificCulture("eu-ES");

            for (int i = 1; i < Ti.Length; i++)
            {
                arg[i] = Ti[i].ToString(specifier, CultureInfo.InvariantCulture);
            }

            //formattage de la ligne d'arguments a passer au shell python   
            //---------------------------------------------------------------
            string argument; 
            argument = Si;  // nom du shell python a executer

            for (int i = 1; i < Ti.Length; i++)
            { argument = argument + " " + axisX + " " + axisY + " " + arg[i] + " "; }

            Console.WriteLine("argument =" + argument);

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
