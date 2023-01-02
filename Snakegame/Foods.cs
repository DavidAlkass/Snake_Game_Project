using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Snakegame
{
    public abstract class Foods
    {
        public int Value { get; set; };
        public string Colour { get; set; };
        public int X { get; set; }
        public int Y { get; set; }

        public Foods()
        {
            X = 0;
            Y = 0;
        }
    }

    class smallFood : Foods
    {

        public smallFood()
        {
            this.Value = 1;
            this.Colour = "Red";
        }
    }

    class bigFood : Foods
    {

        public bigFood()
        {
            this.Value = 2;
            this.Colour = "Purple";
            this.Shit = "brown";
        }
    }
}
