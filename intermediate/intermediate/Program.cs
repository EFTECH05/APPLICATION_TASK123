using System.Threading.Tasks.Sources;

namespace intermediate
{
    internal class Program
    {
        static void Main(string[] args)
        {
          Console.WriteLine("starting");
            goto middle;
            Console.WriteLine("this will be skipped");
        middle:
            Console.WriteLine("this will be printed");


        }

    }
}
