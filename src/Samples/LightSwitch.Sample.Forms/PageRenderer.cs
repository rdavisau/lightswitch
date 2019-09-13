using LightSwitch.Sample.Forms;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(Xappy.iOS.Renderers.PageRenderer))]

// thanks @daveortinau and @xamxappy
namespace Xappy.iOS.Renderers
{
    public class PageRenderer : Xamarin.Forms.Platform.iOS.PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            SetAppTheme();
        }

        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            base.TraitCollectionDidChange(previousTraitCollection);

            if (TraitCollection.HasDifferentColorAppearanceComparedTo(previousTraitCollection))
                SetAppTheme();
        }

        void SetAppTheme()
        {
            Xamarin.Forms.Application.Current.Resources =
                TraitCollection.UserInterfaceStyle == UIUserInterfaceStyle.Dark
                ? Themes.Dark
                : Themes.Light;
        }
    }
}