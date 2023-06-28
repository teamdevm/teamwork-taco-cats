using System.Reflection;
//using System.Drawing;
using Avalonia.Data.Converters;
using Avalonia.Platform;
using Avalonia;
using System.Globalization;
using System;
using Maps.Services;
using Nito.AsyncEx;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Maps.Models;
using Maps.Models;
using System.IO;
using System.Linq;
using Avalonia.Media.Imaging;
using Avalonia.Controls;
using Avalonia.Media;
using Maps.Views;
using DynamicData;

namespace Maps.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    #region Windows
    //public virtual int Width { get; set; } = 1024;
    //public virtual int Height { get; set; } = 768;
    public WindowState WindwoState { get; set; } = WindowState.Maximized;


    #endregion

    private Data _data;
    public virtual Data DataModel
    {
        get => _data;
        set
        {
            _data = value; OnPropertyChanged();
        }

    }

    private PathGeometry _shapeGeometry = new PathGeometry();

    public PathGeometry ShapeGeometry
    {
        get { return _shapeGeometry; }
        set
        {
            _shapeGeometry = value;
            OnPropertyChanged(nameof(ShapeGeometry));
        }
    }

    private MapService _mapService;

    public MainWindowViewModel()
    {
        InitializationNotifier = NotifyTaskCompletion.Create(InitializeAsync());
    }

    public INotifyTaskCompletion InitializationNotifier { get; private set; }
    private CollectionOfMap _testmaps;
    private async Task InitializeAsync()
    {
        DataModel = new Data();
        await DataModel.FillTestDataAsync();
        
        //_mapService = new MapService();
        //var data = await _mapService.GetMap(0);
        //await Task.Delay(1000);
        //MyMaps = new ObservableCollection<Map>(data);
    }


    #region Buttons

    public void OpenTestMap()
    {
        //CreateVectorLayerVer3();
    }

    public void TestObjectWithMove()
    {

    }


    #endregion


    #region Methods

    private void CreateVectorLayerVer1()
    {
        // Создание объекта Canvas для размещения векторного слоя
        var canvas = new Canvas();

        // Определение размеров векторного слоя
        double canvasWidth = 800;
        double canvasHeight = 600;

        // Определение границ географической области, в которой располагается фигура
        double minLongitude = 73.9926;  // Минимальная долгота
        double maxLongitude = 73.9879;  // Максимальная долгота
        double minLatitude = 40.7406;   // Минимальная широта
        double maxLatitude = 40.7482;   // Максимальная широта

        // Вычисление масштабирования и смещения
        double scaleX = canvasWidth / (maxLongitude - minLongitude);
        double scaleY = canvasHeight / (maxLatitude - minLatitude);
        double offsetX = -minLongitude * scaleX;
        double offsetY = -minLatitude * scaleY;

        // Создание объекта Path для отображения фигуры
        //var path = new Path();
        //path.Fill = Brushes.Red; // Установка цвета заливки
        //path.Stroke = Brushes.Black; // Установка цвета обводки
        //path.StrokeThickness = 2; // Установка толщины обводки

        // Создание объекта PathFigure для представления фигуры
        var pathFigure = new PathFigure();

        // Установка начальной точки фигуры в пиксельных координатах
        double startX = (73.9926 * scaleX) + offsetX;
        double startY = (40.7414 * scaleY) + offsetY;
        pathFigure.StartPoint = new Point(startX, startY);

        // Добавление сегментов в PathFigure, указывая пиксельные координаты для каждого угла фигуры
        //pathFigure.Segments.Add(new LineSegment(new Point((73.9857 * scaleX) + offsetX, (40.7406 * scaleY) + offsetY), true));
        //pathFigure.Segments.Add(new LineSegment(new Point((73.9879 * scaleX) + offsetX, (40.7482 * scaleY) + offsetY), true));

        //// Добавление PathFigure в Path.Data
        //path.Data = new PathGeometry { Figures = new PathFigures { pathFigure } };

        //// Добавление Path на векторный слой (Canvas)
        //canvas.Children.Add(path);

        //// Размещение Canvas на главном окне
        //Content = canvas;
    }
    private void CreateVectorLayerVer2()
    {
        // Создание объекта Canvas для размещения векторного слоя
        var canvas = new Avalonia.Controls.Canvas();

        // Определение размеров векторного слоя
        double canvasWidth = 800;
        double canvasHeight = 600;

        // Определение границ географической области, в которой располагается фигура
        double minLongitude = double.MaxValue;
        double maxLongitude = double.MinValue;
        double minLatitude = double.MaxValue;
        double maxLatitude = double.MinValue;

        // Задание координат широты и долготы для 4 точек
        double[] latitudes = { 58.074246, 57.629111, 57.629111, 58.071335 };
        double[] longitudes = { 54.664838, 54.645612, 55.549237, 55.565717 };

        // Нахождение минимальных и максимальных значений широты и долготы
        for (int i = 0; i < latitudes.Length; i++)
        {
            if (longitudes[i] < minLongitude)
                minLongitude = longitudes[i];
            if (longitudes[i] > maxLongitude)
                maxLongitude = longitudes[i];
            if (latitudes[i] < minLatitude)
                minLatitude = latitudes[i];
            if (latitudes[i] > maxLatitude)
                maxLatitude = latitudes[i];
        }

        // Вычисление масштабирования и смещения
        double scaleX = canvasWidth / (maxLongitude - minLongitude);
        double scaleY = canvasHeight / (maxLatitude - minLatitude);
        double offsetX = -minLongitude * scaleX;
        double offsetY = -minLatitude * scaleY;

        // Создание объекта Path для отображения фигуры
        var path = new Avalonia.Controls.Shapes.Path();
        path.Fill = Brushes.Red; // Установка цвета заливки
        path.Stroke = Brushes.Black; // Установка цвета обводки
        path.StrokeThickness = 2; // Установка толщины обводки

        // Создание объекта PathFigure для представления фигуры
        Avalonia.Media.PathFigure pathFigure = new Avalonia.Media.PathFigure();

        // Добавление сегментов в PathFigure, указывая пиксельные координаты для каждой точки фигуры
        for (int i = 0; i < latitudes.Length; i++)
        {
            double x = (longitudes[i] * scaleX) + offsetX;
            double y = (latitudes[i] * scaleY) + offsetY;
            if (i == 0)
            {
                // Установка начальной точки фигуры в пиксельных координатах
                pathFigure.StartPoint = new Avalonia.Point(x, y);
            }
            else
            {
                // Добавление линейного сегмента к предыдущей точке фигуры
                //pathFigure.Segments.Add(new Avalonia.Media.LineSegment(new Point(x, y), true));
                Avalonia.Point point = new Point(x, y);
                Avalonia.Media.LineSegment lineSegment = new LineSegment();
                lineSegment.Point = point;

                pathFigure.Segments.Add(lineSegment);
            }
        }

        // Замыкание фигуры путем добавления последнего линейного сегмента к первой точке
        pathFigure.IsClosed = true;

        // Добавление PathFigure в Path.Data
        path.Data = new Avalonia.Media.PathGeometry { Figures = new PathFigures { pathFigure } };

        // Добавление Path на векторный слой (Canvas)
        canvas.Children.Add(path);

        // Размещение Canvas на главном окне
        //PathGeometry2 = path;
    }
    private void CreateVectorLayerVer3()
    {
        if (DataModel == null)
            return;

        // Определение размеров векторного слоя
        double canvasWidth = DataModel.SelectedMap.Image.PixelSize.Width;
        double canvasHeight = DataModel.SelectedMap.Image.PixelSize.Height;
        //double canvasWidth = 800;
        //double canvasHeight = 600;

        // Определение границ географической области, в которой располагается фигура
        double minLongitude = double.MaxValue;
        double maxLongitude = double.MinValue;
        double minLatitude = double.MaxValue;
        double maxLatitude = double.MinValue;

        // Задание координат широты и долготы для 4 точек
        double[] latitudes = { 58.074246, 57.629111, 57.629111, 58.071335 };
        double[] longitudes = { 54.664838, 54.645612, 55.549237, 55.565717 };

        // Нахождение минимальных и максимальных значений широты и долготы
        for (int i = 0; i < latitudes.Length; i++)
        {
            if (longitudes[i] < minLongitude)
                minLongitude = longitudes[i];
            if (longitudes[i] > maxLongitude)
                maxLongitude = longitudes[i];
            if (latitudes[i] < minLatitude)
                minLatitude = latitudes[i];
            if (latitudes[i] > maxLatitude)
                maxLatitude = latitudes[i];
        }

        // Вычисление масштабирования и смещения
        double scaleX = canvasWidth / (maxLongitude - minLongitude);
        double scaleY = canvasHeight / (maxLatitude - minLatitude);
        double offsetX = -minLongitude * scaleX;
        double offsetY = -minLatitude * scaleY;


        // Создание объекта PathFigure для представления фигуры
        Avalonia.Media.PathFigure pathFigure = new Avalonia.Media.PathFigure();

        // Добавление сегментов в PathFigure, указывая пиксельные координаты для каждой точки фигуры
        for (int i = 0; i < latitudes.Length; i++)
        {
            double x = (longitudes[i] * scaleX) + offsetX;
            double y = (latitudes[i] * scaleY) + offsetY;
            if (i == 0)
            {
                // Установка начальной точки фигуры в пиксельных координатах
                pathFigure.StartPoint = new Avalonia.Point(x, y);
            }
            else
            {
                // Добавление линейного сегмента к предыдущей точке фигуры
                //pathFigure.Segments.Add(new Avalonia.Media.LineSegment(new Point(x, y), true));
                Avalonia.Point point = new Point(x, y);
                Avalonia.Media.LineSegment lineSegment = new LineSegment();
                lineSegment.Point = point;

                pathFigure.Segments.Add(lineSegment);
            }
        }

        // Замыкание фигуры путем добавления последнего линейного сегмента к первой точке
        pathFigure.IsClosed = true;


        ShapeGeometry.Figures.Add(pathFigure);
    }

    #endregion



    
}



