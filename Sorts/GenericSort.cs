using SFML.Graphics;
using System.Collections.Generic;
using System.Threading;

namespace CoreSort.Sorts
{
    public abstract class GenericSort : ISort
    {
        protected RenderWindow window;
        protected List<RectangleShape> shapes;
        protected CancellationToken cancellationToken;

        public GenericSort(RenderWindow _window, List<RectangleShape> _shapes, CancellationToken _cancellationToken)
        {
            window = _window;
            shapes = _shapes;
            cancellationToken = _cancellationToken;
        }

        public void Render()
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            window.Clear();

            foreach (var shape in shapes)
            {
                window.Draw(shape);
            }

            window.Display();
        }

        public void SwapColors(RectangleShape a, RectangleShape b)
        {
            var tempColor = a.FillColor;
            a.FillColor = b.FillColor;
            b.FillColor = tempColor;
        }

        public uint GetColor(RectangleShape shape)
        {
            return shape.FillColor.R;
        }

        public virtual void Sort()
        {

        }
    }
}
