using Discord.CoD.Blops.App_Start;
using System;

namespace Discord.CoD.Blops
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Configuration.Instance.GetConnectionString("MongoDB"));

            Application.Initialize().GetAwaiter().GetResult();
        } 
    }
}
