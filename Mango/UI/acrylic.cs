using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Effects;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;

namespace Mango.UI
{
    public sealed partial class UIEffects
    {
        Compositor _compositor;
        SpriteVisual _hostSprite;
        /// <summary>
        /// Panel where effect will be show in.
        /// </summary>
        public RelativePanel transparentBox { get; set; }

        /// <summary>
        /// Create see through window effect inside a panel control.
        /// </summary>
        /// <param name="rootOfContent"></param>
        public void createTransparentArea(Panel rootOfContent)
        {
            transparentBox = new RelativePanel();
            addTransparentBoxToGrid(rootOfContent, transparentBox);
            applyTransparentEffectToBox(transparentBox, rootOfContent);

        }


       

        private void applyTransparentEffectToBox(RelativePanel transparentBox, Panel rootOfContent)
        {
            _compositor = ElementCompositionPreview.GetElementVisual(rootOfContent).Compositor;
            _hostSprite = _compositor.CreateSpriteVisual();
            _hostSprite.Size = new Vector2((float)transparentBox.ActualWidth, (float)transparentBox.ActualHeight);
            _hostSprite.Opacity = 0.6f;
            ElementCompositionPreview.SetElementChildVisual(transparentBox, _hostSprite);
            _hostSprite.Brush = _compositor.CreateHostBackdropBrush();
        }


        private void addTransparentBoxToGrid(Panel rootOfContent, RelativePanel transparentBox)
        {
            int panelCount = rootOfContent.Children.Count;
            rootOfContent.Children.Insert(0, transparentBox);
        }


        /// <summary>
        /// Resizes the UIEffect when the app window has been resized. Recommended to use in a Page's SizeChanged event.
        /// </summary>
        /// <param name="acrylicGlass"></param>
        /// <param name="newPageSize"></param>
        public void updateAcrylicGlassSize(UIEffects acrylicGlass, Size newPageSize)
        {
            if (acrylicGlass._hostSprite != null)
            {
                acrylicGlass._hostSprite.Size = newPageSize.ToVector2();

            }

            
        }


 
    }
}
