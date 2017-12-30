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


        public void createMSFTStyleAcrylicArea(Panel rootOfContent)
        {
            transparentBox = new RelativePanel();
            addTransparentBoxToGrid(rootOfContent, transparentBox);
            applyMSFTAcrylicEffect(transparentBox, rootOfContent);
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


        private void applyMSFTAcrylicEffect(RelativePanel transparentBox, Panel rootOfContent)
        {
            _compositor = ElementCompositionPreview.GetElementVisual(rootOfContent).Compositor;
            _hostSprite = _compositor.CreateSpriteVisual();
            _hostSprite.Size = new Vector2((float)transparentBox.ActualWidth, (float)transparentBox.ActualHeight);
            _hostSprite.Opacity = 0.8f;

            //CompositionSurfaceBrush noiseBrush = _compositor.CreateSurfaceBrush();
            //LoadedImageSurface loadedSurface = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///Assets/acrylicNoise.png"));
            //noiseBrush.Surface = loadedSurface;






            _hostSprite.Brush = _compositor.CreateHostBackdropBrush();

            ContainerVisual containerVisual = _compositor.CreateContainerVisual();
            containerVisual.Children.InsertAtTop(_hostSprite);
            containerVisual.Size = new Vector2((float)transparentBox.ActualWidth, (float)transparentBox.ActualHeight);
            containerVisual.Opacity = 0.8f;
            containerVisual.IsVisible = true;
           

            //SpriteVisual hostBackdropVisual = CreateHostBackdropVisual();
            //hostBackdropVisual.Size = new Vector2((float)transparentBox.ActualWidth, (float)transparentBox.ActualHeight);
            //containerVisual.Children.InsertAtTop(hostBackdropVisual);


            //SpriteVisual gaussianBlurVisual = CreateGaussianBlurVisual();
            //gaussianBlurVisual.Size = new Vector2((float)transparentBox.ActualWidth, (float)transparentBox.ActualHeight);
            //containerVisual.Children.InsertAbove(gaussianBlurVisual, hostBackdropVisual);


            ////SpriteVisual exclusionBlendVisual = CreateExclusionBlendVisual();
            ////exclusionBlendVisual.Size = new Vector2((float)transparentBox.ActualWidth, (float)transparentBox.ActualHeight);
            ////containerVisual.Children.InsertAbove(exclusionBlendVisual, gaussianBlurVisual);

            //SpriteVisual colorTintVisual = CreateColorTintOverlay();
            //colorTintVisual.Size = new Vector2((float)transparentBox.ActualWidth, (float)transparentBox.ActualHeight);
            ////containerVisual.Children.InsertAbove(colorTintVisual, exclusionBlendVisual);
            //containerVisual.Children.InsertAbove(colorTintVisual, gaussianBlurVisual);

            //SpriteVisual acrylicNoiseVisual = CreateAcrylicNoiseVisual();
            //acrylicNoiseVisual.Size = new Vector2((float)transparentBox.ActualWidth, (float)transparentBox.ActualHeight);
            //containerVisual.Children.InsertAbove(acrylicNoiseVisual, colorTintVisual);

            //containerVisual.Opacity = 0.8f;
            //containerVisual.Size = new Vector2((float)transparentBox.ActualWidth, (float)transparentBox.ActualHeight);

            //Last thing to do!!!


            ElementCompositionPreview.SetElementChildVisual(rootOfContent, containerVisual);

        }

        private SpriteVisual CreateAcrylicNoiseVisual()
        {
            CompositionSurfaceBrush noiseBrush = _compositor.CreateSurfaceBrush();
            LoadedImageSurface loadedSurface = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///Assets/acrylicNoise.png"));
            noiseBrush.Surface = loadedSurface;

            SpriteVisual noiseVisual = _compositor.CreateSpriteVisual();
            return noiseVisual;
        }

        private SpriteVisual CreateColorTintOverlay()
        {
            SpriteVisual colorVisual = _compositor.CreateSpriteVisual();
            colorVisual.Opacity = 1f;

            colorVisual.Brush  = _compositor.CreateColorBrush(Colors.Blue);

            return colorVisual;
            
        }

        private SpriteVisual CreateExclusionBlendVisual()
        {
            SpriteVisual blendVisual = _compositor.CreateSpriteVisual();
            blendVisual.Opacity = 1f;

            IGraphicsEffect exclusionBlendEffect = new Microsoft.Graphics.Canvas.Effects.BlendEffect
            {
                Mode = BlendEffectMode.Exclusion

            };

            CompositionEffectFactory exclusionBlendFactory = _compositor.CreateEffectFactory(exclusionBlendEffect);
            CompositionEffectBrush exlcusionBrush = exclusionBlendFactory.CreateBrush();

            blendVisual.Brush = exlcusionBrush;

            return blendVisual;
            

        }

        private SpriteVisual CreateGaussianBlurVisual()
        {
            GaussianBlurEffect blurEffect = new GaussianBlurEffect()
            {
                Name = "Blur",
                BlurAmount = 20.0f,
                BorderMode = EffectBorderMode.Hard,
                Source = new CompositionEffectSourceParameter("BlurSource")
            };

            CompositionEffectFactory blurEffectFactory = _compositor.CreateEffectFactory(blurEffect);
            CompositionEffectBrush blurBrush = blurEffectFactory.CreateBrush();

            SpriteVisual blurSpriteVisual = _compositor.CreateSpriteVisual();
            blurSpriteVisual.Brush = blurBrush;
            blurSpriteVisual.Opacity = 1f;

            return blurSpriteVisual;
        }

        private SpriteVisual CreateHostBackdropVisual()
        {
            var hostBackdropSprite = _compositor.CreateSpriteVisual();
            hostBackdropSprite.Opacity = 1f;
            return hostBackdropSprite;
        }
    }
}
