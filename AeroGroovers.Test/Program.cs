using System;
using AeroGroovers.Model;

namespace AeroGroovers.Test
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Score score = new Score("ov3rflow.toml");
            Console.WriteLine(score.Title);
        }
    }
}
