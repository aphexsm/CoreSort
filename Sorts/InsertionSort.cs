using System.Collections.Generic;
using System.Threading;
using CoreSort.Sorts;
using SFML.Graphics;

namespace CoreSort
{
    public sealed class InsertionSort : GenericSort, ISort
    {
        public InsertionSort(RenderWindow _window,
                List<RectangleShape> _shapes,
                CancellationToken _cancellationToken)
                : base(_window, _shapes, _cancellationToken)
        {

        }

        public override void Sort()
        {
            if (shapes.Count == 0)
            {
                return;
            }

            for (int i = 1; i < shapes.Count; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                if (GetColor(shapes[i]) > GetColor(shapes[i - 1]))
                {
                    SwapColors(shapes[i], shapes[i - 1]);
                    Render();

                    for (int j = i - 1; j >= 1; j--)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            return;
                        }

                        if (GetColor(shapes[j]) > GetColor(shapes[j - 1]))
                        {
                            SwapColors(shapes[j], shapes[j - 1]);
                            Render();
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
