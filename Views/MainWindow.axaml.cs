using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using System.Linq;

namespace Maps.Views;

public partial class MainWindow : Window
{
    private Point? lastMousePosition;
    private double scale = 1.0;
    private ScaleTransform scaleTransform;
    private TranslateTransform translateTransform;


    public MainWindow()
    {
        InitializeComponent();
        AttachTransforms();
    }

    private void AttachTransforms()
    {
        scaleTransform = this.Find<ScaleTransform>("scaleTransform");
        translateTransform = this.Find<TranslateTransform>("translateTransform");
    }

    //private void OnPointerWheelChanged(object sender, PointerWheelEventArgs e)
    //{
    //    var image = (Image)sender;

    //    var position = e.GetPosition(image);
    //    double scaleChange = e.Delta.Y > 0 ? 1.1 : 0.9;

    //    scale *= scaleChange;

    //    var scaleTransform = image.RenderTransform as ScaleTransform;
    //    scaleTransform.ScaleX = scale;
    //    scaleTransform.ScaleY = scale;

    //    var offsetX = (position.X - image.Bounds.Width / 2) * (1 - scaleChange);
    //    var offsetY = (position.Y - image.Bounds.Height / 2) * (1 - scaleChange);

    //    image.Margin = new Thickness(offsetX, offsetY);

    //    e.Handled = true;
    //}

    //private void OnPointerWheelChanged(object sender, PointerWheelEventArgs e)
    //{
    //    var image = (Image)sender;

    //    var position = e.GetPosition(image);
    //    double scaleChange = e.Delta.Y > 0 ? 1.1 : 0.9;

    //    scale *= scaleChange;

    //    var scaleTransform = image.RenderTransform as ScaleTransform;
    //    scaleTransform.ScaleX = scale;
    //    scaleTransform.ScaleY = scale;

    //    var offsetX = (position.X - image.Bounds.Width / 2) * (1 - scaleChange);
    //    var offsetY = (position.Y - image.Bounds.Height / 2) * (1 - scaleChange);

    //    translateTransform.X += offsetX;
    //    translateTransform.Y += offsetY;

    //    e.Handled = true;
    //}

    private void OnPointerWheelChanged(object sender, PointerWheelEventArgs e)
    {
        var image = (Image)sender;

        var position = e.GetPosition(image);
        double scaleChange = e.Delta.Y > 0 ? 1.1 : 0.9;

        scale *= scaleChange;

        var scaleTransform = FindScaleTransform(image);
        if (scaleTransform != null)
        {
            scaleTransform.ScaleX = scale;
            scaleTransform.ScaleY = scale;
        }

        var translateTransform = FindTranslateTransform(image);
        if (translateTransform != null)
        {
            var offsetX = (position.X - image.Bounds.Width / 2) * (1 - scaleChange);
            var offsetY = (position.Y - image.Bounds.Height / 2) * (1 - scaleChange);

            translateTransform.X += offsetX;
            translateTransform.Y += offsetY;
        }

        e.Handled = true;
    }

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


    private void OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        lastMousePosition = e.GetPosition(null);
    }

    private void OnPointerReleased(object sender, PointerReleasedEventArgs e)
    {
        lastMousePosition = null;
    }

    //private void OnPointerMoved(object sender, PointerEventArgs e)
    //{
    //    var image = (Image)sender;

    //    if (lastMousePosition.HasValue)
    //    {
    //        var position = e.GetPosition(null);
    //        var transform = image.RenderTransform as ScaleTransform;

    //        var offsetX = image.Margin.Left + position.X - lastMousePosition.Value.X;
    //        var offsetY = image.Margin.Top + position.Y - lastMousePosition.Value.Y;

    //        image.Margin = new Thickness(offsetX, offsetY);

    //        lastMousePosition = position;
    //    }
    //}

    private void OnPointerMoved(object sender, PointerEventArgs e)
    {
        if (lastMousePosition.HasValue)
        {
            var image = (Image)sender;
            var translateTransform = FindTranslateTransform(image);

            var position = e.GetPosition(null);
            var deltaX = position.X - lastMousePosition.Value.X;
            var deltaY = position.Y - lastMousePosition.Value.Y;

            translateTransform.X += deltaX;
            translateTransform.Y += deltaY;

            lastMousePosition = position;
        }
    }
}