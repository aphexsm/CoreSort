using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoreSort.Sorts;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace CoreSort
{
    class Program
    {
        static void Main()
        {
            var sorter = new Sorter();

            sorter.Start();
        }
    }
}