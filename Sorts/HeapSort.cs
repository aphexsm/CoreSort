using System.Collections.Generic;
using System.Threading;
using CoreSort.Sorts;
using SFML.Graphics;

namespace CoreSort
{
    public sealed class HeapSort : GenericSort, ISort
    {
        public HeapSort(RenderWindow _window, 
                        List<RectangleShape> _shapes, 
                        CancellationToken _cancellationToken) 
            : base (_window, _shapes, _cancellationToken)
        {

        }

        private void SiftDown(int root, int bottom)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            int max_child = root * 2 + 1;

            if (max_child < bottom)
            {
                if (GetColor(shapes[max_child + 1]) < GetColor(shapes[max_child]))
                    max_child++;
            }
            else if (max_child > bottom)
            {
                return;
            }

            if (GetColor(shapes[root]) <= GetColor(shapes[max_child]))
                return;

            SwapColors(shapes[root], shapes[max_child]);
            Render();

            SiftDown(max_child, bottom);
        }

        public override void Sort()
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (shapes.Count == 0)
            {
                return;
            }

            //building first heap
            for (int i = shapes.Count / 2; i >= 0; i--)
            {
                SiftDown(i, shapes.Count - 1);
            }

            //sorting
            for (int i = shapes.Count - 1; i > 0; i--)
            {
                SwapColors(shapes[0], shapes[i]);

                SiftDown(0, i - 1);
            }
        }
    }
}
