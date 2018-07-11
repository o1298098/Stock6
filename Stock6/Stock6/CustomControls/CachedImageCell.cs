using FFImageLoading.Forms;
using Stock6.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Stock6.CustomControls
{
    public class CachedImageCell:ViewCell
    {
        readonly CachedImage cachedImage = null;

        public CachedImageCell()
        {
            Grid stack = new Grid();
            cachedImage = new CachedImage {
                HeightRequest = 180,
                Aspect = Aspect.AspectFill,
                DownsampleToViewSize = true,
            };
            bool ischeck = false;
            BoxView box = new BoxView
            {
                Opacity = 0.5,
                Color = Color.Black,
            };
            Image selectpic = new Image
            {
                Source = "select_green.png",
                Margin = new Thickness(0, 5, 5, 0),
                WidthRequest = 20,
                HeightRequest = 20,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Start
            };
            TapGestureRecognizer recognizer = new TapGestureRecognizer();
            recognizer.Tapped += (sender, args) =>
            {
                if (ischeck)
                {
                    ischeck = !ischeck;                  
                    stack.Children.Remove(selectpic);
                    stack.Children.Remove(box);
                }
                else
                {
                    ischeck = !ischeck;
                    stack.Children.Add(box);
                    stack.Children.Add(selectpic);
                }

            };
            box.GestureRecognizers.Add(recognizer);
            cachedImage.GestureRecognizers.Add(recognizer);
            stack.Children.Add(cachedImage);
            View = stack;
        }

        protected override void OnBindingContextChanged()
        {
            cachedImage.Source = null;
            var item = BindingContext as ImageModel;

            if (item == null)
            {
                return;
            }

            cachedImage.Source = item.Uri;

            base.OnBindingContextChanged();
        }
    }
}
