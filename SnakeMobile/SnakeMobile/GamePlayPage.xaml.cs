using SnakeMobileApp.ViewModels;
using System.IO;
using System.Runtime.CompilerServices;
using Point = SnakeMobileApp.ViewModels.Point;

namespace SnakeMobile;

public partial class GamePlayPage : ContentPage
{
    private const int Rows = 20;
    private const int Columns = 40;
    private double CellSize;
    List<Point> WallBody = new List<Point>();
    private Snake Snake = new Snake();
    private Point CurrentFoodLocation;
    private IDispatcherTimer GameTimer;
    private bool FoodEaten=true;
    private int Score;
    private bool IsGameOver;
    public GamePlayPage()
    {
        InitializeComponent();
        InitializeGrid();
        CreateWall();
        PrintWall();
        PrintSnake();
        PrintFood();
    }

    private void SetCellSize()
    {
        var window = Application.Current.MainPage.Window;
        double windowWidth = window.Width;
        double windowHeight = window.Height;

        if (0.9 * windowWidth / Columns > 0.9 * windowHeight / (1.5 * Rows))
        {
            CellSize = 0.9 * windowHeight / (1.5 * Rows);
        }
        else
        {
            CellSize = 0.9 * windowWidth / Columns;
        }

        GameGrid.WidthRequest = Columns * CellSize;
        GameGrid.HeightRequest = Rows * CellSize;
    }

    private void InitializeGrid()
    {
        // Add rows and columns
        for (int i = 0; i < Rows; i++)
        {
            GameGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        }
        for (int j = 0; j < Columns; j++)
        {
            GameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }

        SetCellSize();
        
    }

    private void PlaceElement(Point point, Color color)
    {
        Button element = new Button()
        {
            BackgroundColor = color
        };

        // Add the element to the Grid
        GameGrid.Children.Add(element);

        // Set its position in the Grid
        Grid.SetColumn(element, point.X);
        Grid.SetRow(element, point.Y);
        
    }

    private void CreateWall()
    {
        for (int i = 0; i < Rows; i++)
        {
            WallBody.Add(new Point(0, i));
            WallBody.Add(new Point(Columns - 1, i));
        }
        for (int i = 0; i < Columns; i++)
        {
            WallBody.Add(new Point(i, 0));
            WallBody.Add(new Point(i, Rows - 1));
        }
    }

    private void PrintWall()
    {
        foreach (var point in WallBody)
        {
            PlaceElement(point,Colors.Black);
        }
    }

    private void PrintSnake()
    {
        foreach (var point in Snake.Body)
        {
            PlaceElement(point, Colors.Blue);
        }
    }

    private void PrintFood()
    {
        if (FoodEaten)
        {
            Random random = new Random();
            int x = random.Next(1, Columns - 1);
            int y = random.Next(1, Rows - 1);
            CurrentFoodLocation = new Point(x, y);
            FoodEaten = false;
        }

        PlaceElement(CurrentFoodLocation, Colors.Red);
    }

    private void StartGameLoop()
    {
        GameTimer = Dispatcher.CreateTimer();
        GameTimer.Interval = TimeSpan.FromMilliseconds(100);
        GameTimer.Tick += OnGameTick;
        GameTimer.Start();
    }

    private void OnGameTick(object sender, EventArgs e)
    {
        // Преместване на змията
        Snake.Update();

        // Ядене на храна
        if (Snake.Head.X == CurrentFoodLocation.X && Snake.Head.Y == CurrentFoodLocation.Y)
        {
            Snake.Grow();
            FoodEaten = true;
            Score += 100;
            ClearGridByColor(Colors.Red);
            PrintFood();
            StartBtn.Text = "Score: " + Score;
        }

        // Сблъсък със стена
        if (IsCollidingWithWall(Snake.Head))
        {
            IsGameOver = true;
        }

        // Захапване на опашката
        if (Snake.IsCollidingWithTail())
        {
            IsGameOver = true;
        }

        ClearGridByColor(Colors.Blue);
        PrintSnake();

        if (IsGameOver)
        {
            UpBtn.IsEnabled = false;
            DownBtn.IsEnabled = false;
            LeftBtn.IsEnabled = false;
            RightBtn.IsEnabled = false;
            StartBtn.IsEnabled = true;
            StartBtn.Text = "Score: " + Score + ". Try Again?";
            GameTimer.Stop();
            GameTimer.Tick -= OnGameTick;
        }

    }

    private void ClearGridByColor(Color targetColor)
    {
        // Find all children matching the specified color
        var childrenToRemove = GameGrid.Children
            .Where(child => child is Button button && button.BackgroundColor.Equals(targetColor))
            .ToList();

        // Remove each matching child from the grid
        foreach (var child in childrenToRemove)
        {
            GameGrid.Children.Remove(child);
        }
    }

    private void OnStartClicked(object sender, EventArgs e)
    {
        UpBtn.IsEnabled = true;
        DownBtn.IsEnabled = true;
        LeftBtn.IsEnabled = true;
        RightBtn.IsEnabled = true;
        StartBtn.IsEnabled = false;
        IsGameOver = false;
        Score = 0;
        StartBtn.Text = "Score: 0";
        Snake = new Snake();
        StartGameLoop();
    }

    private void OnUpClicked(object sender, EventArgs e)
    {
        Snake.ChangeDirectionUp();
    }

    private void OnDownClicked(object sender, EventArgs e)
    {
        Snake.ChangeDirectionDown();
    }

    private void OnLeftClicked(object sender, EventArgs e)
    {
        Snake.ChangeDirectionLeft();
    }

    private void OnRightClicked(object sender, EventArgs e)
    {
        Snake.ChangeDirectionRight();
    }

    private bool IsCollidingWithWall(Point snakePoint)
    {
        foreach (var wallPoint in WallBody)
        {
            if (snakePoint.X == wallPoint.X && snakePoint.Y == wallPoint.Y)
            {
                return true;
            }
        }
        return false;
    }
}