using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public static class _PassEncription
    {
        private static Random rand;
        private static int seed;
        public static int Seed
        {
            set
            {
                if(value % 23 == 0)
                {
                    seed = value;
                }
            }
        }

        public static int Encript(string pass)
        {
            char[] input = pass.ToCharArray();
            int output = 0;
            rand = new Random(seed);
            foreach (char letter in input)
            {
                int num = Convert.ToInt32(letter);
                num *= rand.Next(0,rand.Next());
                output += num;
            }
            return output;
        }
    }
}
