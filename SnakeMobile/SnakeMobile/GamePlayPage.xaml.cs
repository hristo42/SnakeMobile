using System.IO;
using System.Runtime.CompilerServices;
using SnakeMobile.ViewModels;

namespace SnakeMobile;

public partial class GamePlayPage : ContentPage
{
    private const int rows = 20;
    private const int columns = 40;
    private double _cellSize;
    private readonly Wall _wall = new Wall(rows,columns);
    private readonly Food _food = new Food(rows, columns);
    private Snake _snake = new Snake();
    private IDispatcherTimer _gameTimer;
    private int _score;
    private bool _isGameOver = true;
    public GamePlayPage()
    {
        InitializeComponent();
        InitializeGrid();
        PrintWall();
        PrintSnake();
        PrintFood();
        StartGameLoop();
    }

    // Метод за променяне на размера на клетките според размера на екрана
    private void SetCellSize()
    {
        var window = Application.Current.MainPage?.Window;
        if (window != null)
        {
            double windowWidth = window.Width;
            double windowHeight = window.Height;

            if (0.9 * windowWidth / columns > 0.9 * windowHeight / (1.5 * rows))
            {
                _cellSize = 0.9 * windowHeight / (1.5 * rows);
            }
            else
            {
                _cellSize = 0.9 * windowWidth / columns;
            }

            GameGrid.WidthRequest = columns * _cellSize;
            GameGrid.HeightRequest = rows * _cellSize;
        }
    }

    // Създаване на координатната система според зададените размери
    private void InitializeGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            GameGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        }
        for (int j = 0; j < columns; j++)
        {
            GameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }

        SetCellSize();
    }

    // Метод за поставяне на елемент (бутон) на конкретни координати
    private void PlaceElement(CoordinatePoint point, Color color)
    {
        Button element = new Button()
        {
            BackgroundColor = color
        };

        GameGrid.Children.Add(element);

        Grid.SetColumn(element, point.X);
        Grid.SetRow(element, point.Y);
        
    }

    //Принтиране на елементите на координатната система
    private void PrintWall()
    {
        foreach (var point in _wall.Body)
        {
            PlaceElement(point, Colors.Black);
        }
    }

    private void PrintSnake()
    {
        foreach (var point in _snake.Body)
        {
            PlaceElement(point, _snake.SnakeColor);
        }
    }

    private void PrintFood()
    {
        PlaceElement(_food.Location, Colors.Red);
    }

    // Инициализиране на _gameTimer
    private void StartGameLoop()
    {
        _gameTimer = Dispatcher.CreateTimer();
        _gameTimer.Interval = TimeSpan.FromMilliseconds(100);
        _gameTimer.Tick += OnGameTick;
        _gameTimer.Start();
    }

    private void OnGameTick(object sender, EventArgs e)
    {
        // Проверка дали върви игра
        if (!_isGameOver)
        {

            // Смяна на координатите на змията
            _snake.Update();

            // Ядене на храна
            if (_snake.Head.X == _food.Location.X && _snake.Head.Y == _food.Location.Y)
            {
                _snake.Grow();
                _score += 100;
                ClearGridByColor(Colors.Red);
                _food.Spawn(rows,columns);
                PrintFood();
                StartBtn.Text = "Score: " + _score;
            }

            // Сблъсък със стена
            if (_wall.IsCollidingWithWall(_snake.Head))
            {
                _isGameOver = true;
            }

            // Захапване на опашката
            if (_snake.IsCollidingWithTail())
            {
                _isGameOver = true;
            }

            // Принтиране на змията на новата позиция
            ClearGridByColor(Colors.Blue);
            PrintSnake();

            // При край на играта
            if (_isGameOver)
            {
                UpBtn.IsEnabled = false;
                DownBtn.IsEnabled = false;
                LeftBtn.IsEnabled = false;
                RightBtn.IsEnabled = false;
                StartBtn.IsEnabled = true;
                StartBtn.Text = "Final Score: " + _score + ". Try Again?";
            }
        }

        // Промяна на размера на клетките при промяна на размера на екрана
        SetCellSize();

    }

    // Метод за изтриване на конкретни елементи спред цвят
    private void ClearGridByColor(Color targetColor)
    {
        var childrenToRemove = GameGrid.Children
            .Where(child => child is Button button && button.BackgroundColor.Equals(targetColor))
            .ToList();

        foreach (var child in childrenToRemove)
        {
            GameGrid.Children.Remove(child);
        }
    }

    // Бутони
    private void OnStartClicked(object sender, EventArgs e)
    {
        UpBtn.IsEnabled = true;
        DownBtn.IsEnabled = true;
        LeftBtn.IsEnabled = true;
        RightBtn.IsEnabled = true;
        StartBtn.IsEnabled = false;
        _isGameOver = false;
        _score = 0;
        StartBtn.Text = "Score: 0";
        _snake = new Snake();
    }

    private void OnUpClicked(object sender, EventArgs e)
    {
        _snake.ChangeDirectionUp();
    }

    private void OnDownClicked(object sender, EventArgs e)
    {
        _snake.ChangeDirectionDown();
    }

    private void OnLeftClicked(object sender, EventArgs e)
    {
        _snake.ChangeDirectionLeft();
    }

    private void OnRightClicked(object sender, EventArgs e)
    {
        _snake.ChangeDirectionRight();
    }

}