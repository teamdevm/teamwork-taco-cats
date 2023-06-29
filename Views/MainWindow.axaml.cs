using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using System.Linq;
using Avalonia.Controls.Shapes;
using Maps.ViewModels;
using Microsoft.Win32.SafeHandles;
using System.Threading;

namespace Maps.Views;

public partial class MainWindow : Window
{
    private Point? lastMousePosition;
    private bool isLeftMouseButtonPressed;
    private double scale = 1.0;
    private ScaleTransform scaleTransform;
    private TranslateTransform translateTransform;


    public MainWindow()
    {
        InitializeComponent();
        AttachTransforms();
        AttachMouseMoveEvent();
    }

    private void AttachTransforms()
    {
        scaleTransform = this.Find<ScaleTransform>("scaleTransform");
        translateTransform = this.Find<TranslateTransform>("translateTransform");
    }

    private void OnPointerWheelChanged(object sender, PointerWheelEventArgs e)
    {
        #region Canvas
        
        var position = e.GetPosition(canvas);
        double scaleChange = e.Delta.Y > 0 ? 1.1 : 0.9;

        scale *= scaleChange;

        var scaleTransform = FindScaleTransform(canvas);
        if (scaleTransform != null)
        {
            scaleTransform.ScaleX = scale;
            scaleTransform.ScaleY = scale;
        }

        var translateTransform = FindTranslateTransform(canvas);
        if (translateTransform != null)
        {
            var offsetX = (position.X - canvas.Bounds.Width / 2) * (1 - scaleChange);
            var offsetY = (position.Y - canvas.Bounds.Height / 2) * (1 - scaleChange);

            translateTransform.X += offsetX;
            translateTransform.Y += offsetY;//
        }

        #endregion


        //var image = Image;

        //var position = e.GetPosition(image);
        //double scaleChange = e.Delta.Y > 0 ? 1.1 : 0.9;

        //scale *= scaleChange;

        //var scaleTransform = FindScaleTransform(image);
        //if (scaleTransform != null)
        //{
        //    scaleTransform.ScaleX = scale;
        //    scaleTransform.ScaleY = scale;
        //}

        //var translateTransform = FindTranslateTransform(image);
        //if (translateTransform != null)
        //{
        //    var offsetX = (position.X - image.Bounds.Width / 2) * (1 - scaleChange);
        //    var offsetY = (position.Y - image.Bounds.Height / 2) * (1 - scaleChange);

        //    translateTransform.X += offsetX;
        //    translateTransform.Y += offsetY;//
        //}




        //var scaleTransformPath = FindScaleTransform(Path);
        //if (scaleTransformPath != null)
        //{
        //    scaleTransformPath.ScaleX = scale;
        //    scaleTransformPath.ScaleY = scale;
        //}

        //var translateTransformPath = FindTranslateTransform(Path);
        //if (translateTransformPath != null)
        //{
        //    var offsetX = (position.X - Path.Bounds.Width / 2) * (1 - scaleChange);
        //    var offsetY = (position.Y - Path.Bounds.Height / 2) * (1 - scaleChange);

        //    translateTransformPath.X += offsetX;
        //    translateTransformPath.Y += offsetY;//
        //}

        e.Handled = true;
    }

    //private void OnPointerWheelChanged(object sender, PointerWheelEventArgs e)
    //{
    //    var vm = DataContext as MainWindowViewModel;

    //    var image = Image;


    //    var scaleTransform = new ScaleTransform();
    //    var transformGroup = new TransformGroup();

    //    if (image.RenderTransform is TransformGroup existingTransformGroup)
    //    {
    //        // Копируем существующие трансформации
    //        foreach (var transform in existingTransformGroup.Children)
    //        {
    //            if (transform is ScaleTransform existingScaleTransform)
    //            {
    //                scaleTransform.ScaleX = existingScaleTransform.ScaleX;
    //                scaleTransform.ScaleY = existingScaleTransform.ScaleY;
    //            }
    //            else
    //            {
    //                transformGroup.Children.Add(transform);
    //            }
    //        }
    //    }

    //    double scaleFactor = 0.1; // Измените значение масштабирования по вашему желанию
    //    if (e.Delta.Y > 0)
    //    {
    //        // Увеличение масштаба
    //        scaleTransform.ScaleX += scaleFactor;
    //        scaleTransform.ScaleY += scaleFactor;
    //    }
    //    else if (e.Delta.Y < 0)
    //    {
    //        // Уменьшение масштаба
    //        if (scaleTransform.ScaleX > scaleFactor)
    //            scaleTransform.ScaleX -= scaleFactor;
    //        if (scaleTransform.ScaleY > scaleFactor)
    //            scaleTransform.ScaleY -= scaleFactor;
    //    }

    //    // Добавляем обновленную ScaleTransform в TransformGroup
    //    transformGroup.Children.Add(scaleTransform);

    //    // Присваиваем TransformGroup свойству Image.RenderTransform
    //    Image.RenderTransform = transformGroup;
    //    Path.RenderTransform = transformGroup;

    //    e.Handled = true;


    //}


    private ScaleTransform FindScaleTransform(Image image)
    {
        var renderTransform = image.RenderTransform;
        if (renderTransform is TransformGroup transformGroup)
        {
            return transformGroup.Children.OfType<ScaleTransform>().FirstOrDefault();
        }
        return null;
    }

    private TranslateTransform FindTranslateTransform(Image image)
    {
        var renderTransform = image.RenderTransform;
        if (renderTransform is TransformGroup transformGroup)
        {
            return transformGroup.Children.OfType<TranslateTransform>().FirstOrDefault();
        }
        return null;
    }

    private ScaleTransform FindScaleTransform(Path image)
    {
        var renderTransform = image.RenderTransform;
        if (renderTransform is TransformGroup transformGroup)
        {
            return transformGroup.Children.OfType<ScaleTransform>().FirstOrDefault();
        }
        return null;
    }

    private TranslateTransform FindTranslateTransform(Path image)
    {
        var renderTransform = image.RenderTransform;
        if (renderTransform is TransformGroup transformGroup)
        {
            return transformGroup.Children.OfType<TranslateTransform>().FirstOrDefault();
        }
        return null;
    }

    private ScaleTransform FindScaleTransform(Canvas image)
    {
        var renderTransform = image.RenderTransform;
        if (renderTransform is TransformGroup transformGroup)
        {
            return transformGroup.Children.OfType<ScaleTransform>().FirstOrDefault();
        }
        return null;
    }

    private TranslateTransform FindTranslateTransform(Canvas image)
    {
        var renderTransform = image.RenderTransform;
        if (renderTransform is TransformGroup transformGroup)
        {
            return transformGroup.Children.OfType<TranslateTransform>().FirstOrDefault();
        }
        return null;
    }


    //private void OnPointerPressed(object sender, PointerPressedEventArgs e)
    //{
    //    lastMousePosition = e.GetPosition(null);
    //}
    private void OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(null).Properties.PointerUpdateKind == PointerUpdateKind.LeftButtonPressed)
        {
            isLeftMouseButtonPressed = true;
            lastMousePosition = e.GetPosition(null);
        }
    }

    private void OnPointerReleased(object sender, PointerReleasedEventArgs e)
    {
        lastMousePosition = null;
    }

    private void OnPointerMoved(object sender, PointerEventArgs e)
    {
        MousePosition = e.GetPosition(this);

       
        // Рассчитываем коэффициенты масштабирования
        double scaleX = 360.0 / (DataContext as MainWindowViewModel).DataModel.SelectedMap.Image.PixelSize.Width;
        double scaleY = 180.0 / (DataContext as MainWindowViewModel).DataModel.SelectedMap.Image.PixelSize.Height;

        // Вычисляем относительные координаты
        double relX = MousePosition.X / (DataContext as MainWindowViewModel).DataModel.SelectedMap.Image.PixelSize.Width;
        double relY = MousePosition.Y / (DataContext as MainWindowViewModel).DataModel.SelectedMap.Image.PixelSize.Height;

        // Вычисляем широту и долготу
        double lat = (DataContext as MainWindowViewModel).DataModel.SelectedMap.Coordinates[0][0] + (relY - 0.5) * (-2 * scaleY);
        double lon = (DataContext as MainWindowViewModel).DataModel.SelectedMap.Coordinates[0][1] + (relX - 0.5) * (2 * scaleX);

       


        //PositionTextBlock.Text = $"X: {MousePosition.X}, Y: {MousePosition.Y}";
        PositionTextBlock.Text = $"Ш: {Math.Round(lat,5)}, Д: {Math.Round(lon,5)}";


        var positionMouse= e.GetPosition(this);
        _positionTextBlock.Text = $"X: {positionMouse.X}, Y: {positionMouse.Y}";
        
        

        if (lastMousePosition.HasValue && isLeftMouseButtonPressed)
        {
            
            var translateTransform = FindTranslateTransform(canvas);

            var position = e.GetPosition(null);
            var deltaX = position.X - lastMousePosition.Value.X;
            var deltaY = position.Y - lastMousePosition.Value.Y;

            translateTransform.X += deltaX;
            translateTransform.Y += deltaY;

            lastMousePosition = position;
        }
    }
    private void AttachMouseMoveEvent()
    {
        this.PointerMoved += OnPointerMoved;
    }
    public TextBlock _positionTextBlock = new TextBlock();


    public Point MousePosition { get; set; }


    //private void OnPointerMoved(object sender, PointerEventArgs e)
    //{
    //    if (lastMousePosition.HasValue)
    //    {
    //        var image = (Image)sender;
    //        var translateTransform = FindTranslateTransform(image);

    //        var position = e.GetPosition(null);
    //        var deltaX = position.X - lastMousePosition.Value.X;
    //        var deltaY = position.Y - lastMousePosition.Value.Y;

    //        translateTransform.X += deltaX;
    //        translateTransform.Y += deltaY;

    //        lastMousePosition = position;
    //    }
    //}

    //private void ZoomIn_Click(object sender, RoutedEventArgs e)
    //{
    //    scale *= 1.1;
    //    ApplyScaleAndTranslate(sender, e);
    //}

    //private void ZoomOut_Click(object sender, RoutedEventArgs e)
    //{
    //    scale *= 0.9;
    //    ApplyScaleAndTranslate(sender, e);
    //}

    //private void ApplyScaleAndTranslate(object sender, RoutedEventArgs e)
    //{


    //    var scaleTransform = FindScaleTransform(image);
    //    var translateTransform = FindTranslateTransform(image);

    //    scaleTransform.ScaleX = scale;
    //    scaleTransform.ScaleY = scale;

    //    // Применить текущие значения translateTransform.X и translateTransform.Y

    //    e.Handled = true;
    //}

}