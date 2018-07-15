namespace Training.Persona.Api
{
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// The starting point for your program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The starting method.
        /// </summary>
        /// <param name="args">The args.</param>
        public static void Main(string[] args)
        {
            // FIX: Para que .Net core 2.0 cargue manualmente los assemblies de dlls referenciadas.
            System.AppDomain.CurrentDomain.AssemblyResolve += (sender, eventArgs) =>
            {
                /*
                Carga:
                Apollo.NetCore.Core.Data.dll
                MySql.Data.dll

                Omite:
                MySql.Data.resources.dll
                */
                Assembly ret = null;
                const RegexOptions flags = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;
                if (Regex.IsMatch(eventArgs.Name, "^(Apollo|MySQL)", flags))
                {
                    string name = eventArgs.Name.Split(',').First();
                    if (!name.EndsWith(".resources"))
                    {
                        string dllPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, $"{name}.dll");
                        ret = Assembly.LoadFile(dllPath);
                    }
                }

                return ret;
            };

            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Build the web host.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>A configurated IWebHost.</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
