using System.Collections.Generic;
using System.Threading;
using CoreSort.Sorts;
using SFML.Graphics;

namespace CoreSort
{
    sealed class MergeSort : GenericSort, ISort
    {
        public MergeSort(RenderWindow _window,
                        List<RectangleShape> _shapes,
                        CancellationToken _cancellationToken)
            : base(_window, _shapes, _cancellationToken)
        {

        }
        
        void Merge(int left, int mid, int right)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            var temp = new List<RectangleShape>();

            int i = left;
            int j = mid + 1;

            Render();

            while ((i <= mid) && (j <= right))
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                if (GetColor(shapes[i]) > GetColor(shapes[j]))
                {
                    temp.Add(shapes[i]);
                    i++;
                }
                else
                {
                    temp.Add(shapes[j]);
                    j++;
                }
            }

            while (i <= mid)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                temp.Add(shapes[i]);
                i++;
            }

            while (j <= right)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                temp.Add(shapes[j]);
                j++;
            }

            for (int index = 0; index < temp.Count; index++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                shapes[left + index].FillColor = temp[index].FillColor;
                Render();
            }
        }

        void MergeSubsort(int left, int right)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (left < right)
            {
                int mid = (left + right) / 2;

                MergeSubsort(left, mid);
                MergeSubsort(mid + 1, right);
                Merge(left, mid, right);
            }
        }

        public override void Sort()
        {
            if (shapes.Count == 0)
            {
                return;
            }

            MergeSubsort(0, shapes.Count - 1);
        }
    }
}
