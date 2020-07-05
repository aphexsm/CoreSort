using CoreSort.Sorts;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoreSort
{
    class Sorter
    {
        RenderWindow window;
        List<RectangleShape> shapes;
        readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        ISort sortAlgorithm;
        Task sortTask;

        public void Start()
        {
            uint width = 1920;
            uint height = 1080;
            uint side = 60;

            CreateShapes(width, height, side);

            CreateWindow(width, height);

            StartSortTask();

            while (window.IsOpen)
            {
                if (sortTask.IsCompleted)
                {
                    CreateShapes(width, height, side);
                    StartSortTask();
                }

                window.DispatchEvents();
            }
        }

        private void StartSortTask()
        {
            sortAlgorithm = SortFactory.GetRandomSortAlgorithm(window, shapes, tokenSource.Token);

            sortTask = new Task(sortAlgorithm.Sort, tokenSource.Token);

            sortTask.Start();
        }

        private void CreateShapes(uint width, uint height, uint side)
        {
            shapes = new List<RectangleShape>();

            var rnd = new Random();

            for (int i = 0; i < height / side; i++)
            {
                for (int j = 0; j < width / side; j++)
                {
                    byte red = (byte)rnd.Next(0, 50);
                    byte green = red;
                    byte blue = red;

                    var rect = new RectangleShape(new Vector2f(side, side))
                    {
                        FillColor = new Color(red, green, blue),
                        Position = new Vector2f(j * side, i * side)
                    };

                    shapes.Add(rect);
                }
            }
        }

        private void CreateWindow(uint width, uint height)
        {
            window = new RenderWindow(new VideoMode(width, height), "CoreSort", Styles.Fullscreen);

            window.Closed += Window_Closed;
            window.KeyPressed += Window_KeyPressed;
            window.MouseMoved += Window_MouseMoved;
            window.SetFramerateLimit(60);
            window.SetMouseCursorVisible(false);
            window.SetActive(false);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            tokenSource.Cancel();
            window.Close();
        }

        private void Window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            //window.Close();
        }

        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            tokenSource.Cancel();
            window.Close();
        }
    }
}
