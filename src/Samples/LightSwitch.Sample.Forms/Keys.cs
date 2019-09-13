using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ContentPage), typeof(Xappy.iOS.Renderers.PageRenderer))]

public class Keys
{
    public const string BackgroundColor = nameof(BackgroundColor);
    public const string TextColor = nameof(TextColor);
}
