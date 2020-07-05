using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CoreSort.Sorts
{
    public class SortFactory
    {
        public static ISort GetRandomSortAlgorithm(RenderWindow window,
                                                   List<RectangleShape> shapes,
                                                   CancellationToken cancellationToken)
        {
            var listOfAlgorithms = new List<ISort>
            {
                new HeapSort(window, shapes, cancellationToken),
                new InsertionSort(window, shapes, cancellationToken),
                new QuickSort(window, shapes, cancellationToken)
            };

            var rnd = new Random().Next(listOfAlgorithms.Count);

            return listOfAlgorithms[rnd];
        }
    }
}
