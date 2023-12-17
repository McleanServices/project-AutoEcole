using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace AutoEcoleRouteTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pythonExecutable = @"C:\Users\tyrec\PycharmProjects\driving school app\venv\Scripts\python.exe";
            string script = @"C:\Users\tyrec\PycharmProjects\driving school app\main.py";

            // Préparer la liste des communes et de leurs coordonnées
            var guadeloupeLocations = new List<string>
            {
                "abymes,16.271360,-61.505000",
                "point-a-pitre,16.240569,-61.532246",
                "jarry,16.254842,-61.562107",
                "josh,16.254842,-61.562107"
                // ... ajoutez plus de communes ici, chacune sous forme de chaîne séparée par des virgules
            };

            // Joindre tous les lieux en une seule chaîne d'arguments, séparés par des espaces
            string locationsArgument = string.Join(" ", guadeloupeLocations);

            // Démarrer le script Python avec les emplacements comme arguments
            var psi = new ProcessStartInfo
            {
                FileName = pythonExecutable,
                Arguments = $"\"{script}\" {locationsArgument}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (var process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    Console.WriteLine("Sortie du script Python :");
                    Console.WriteLine(output);
                }
                else
                {
                    Console.WriteLine("Erreur du script Python :");
                    Console.WriteLine(error);
                }
            }
        }
    }
}
