using Discord.CoD.Blops.App_Start;

namespace Discord.CoD.Blops
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Initialize().GetAwaiter().GetResult();
        } 
    }
}
