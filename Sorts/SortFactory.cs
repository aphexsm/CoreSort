using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CoreSort.Sorts
{
    class SortFactory
    {
        public static ISort GetRandomSortAlgorithm(RenderWindow _window,
                                                   List<RectangleShape> _shapes,
                                                   CancellationToken _cancellationToken)
        {
            var listOfAlgorithms = new List<ISort>
            {
                new HeapSort(_window, _shapes, _cancellationToken),
                new InsertionSort(_window, _shapes, _cancellationToken),
                new QuickSort(_window, _shapes, _cancellationToken)
            };

            var rnd = new Random().Next(listOfAlgorithms.Count);

            return listOfAlgorithms[rnd];
        }
    }
}
