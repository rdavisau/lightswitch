using Xamarin.Forms;
using Xappy.iOS.Renderers;

[assembly: ExportRenderer(typeof(ContentPage), typeof(PageRenderer))]
namespace Pages
{
    public class MyPage : ContentPage
    {
        public MyPage()
        {
            SetApplication();

            Content = new Label
            {
                Text = "Hello Forms",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            (Content as Label).SetDynamicResource(Label.TextColorProperty, Keys.TextColor);
            SetDynamicResource(BackgroundColorProperty, Keys.BackgroundColor);
        }

        private void SetApplication()
        {
            // Application gets cleared when using Native Forms, so set a new one
            if (Application.Current != null)
                return;

            Application.Current = new Application();
        }
    }
}