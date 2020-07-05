using System.Collections.Generic;
using System.Threading;
using CoreSort.Sorts;
using SFML.Graphics;

namespace CoreSort
{
    public sealed class QuickSort : GenericSort, ISort
    {
        public QuickSort(RenderWindow _window,
                        List<RectangleShape> _shapes,
                        CancellationToken _cancellationToken)
            : base(_window, _shapes, _cancellationToken)
        {

        }

        private int QuickPartition(int left, int right)
        {
            uint v = GetColor(shapes[(left + right) / 2]);
            int i = left;
            int j = right;

            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return 0;
                }

                while (GetColor(shapes[i]) > v)
                {
                    i++;
                }

                while (GetColor(shapes[j]) < v)
                {
                    j--;
                }

                if (i >= j)
                {
                    break;
                }

                SwapColors(shapes[i++], shapes[j--]);
                Render();
            }

            return j;
        }

        private void QuickSubsort(int left, int right)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (left < right)
            {
                int q = QuickPartition(left, right);
                QuickSubsort(left, q);
                QuickSubsort(q + 1, right);
            }
        }

        public override void Sort()
        {
            if (shapes.Count == 0)
            {
                return;
            }

            QuickSubsort(0, shapes.Count - 1);
        }
    }
}
