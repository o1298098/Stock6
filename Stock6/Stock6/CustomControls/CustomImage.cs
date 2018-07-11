using FFImageLoading.Forms;
using Stock6.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Stock6.CustomForms
{
    public class CustomImage : Grid
    {
        BindableProperty SourceProperty = BindableProperty.Create("Source", typeof(float), typeof(CustomImage));
        public ImageSource Source
        {
            get
            {
                return (ImageSource)base.GetValue(SourceProperty);
            }
            set
            {
                SetValue(SourceProperty, value);
            }
        }
        public CustomImage()
        {
            int imageDimension = Device.RuntimePlatform == Device.iOS ||
                                  Device.RuntimePlatform == Device.Android ? 120 : 60;
            Grid stack = new Grid()
            {
                WidthRequest = imageDimension,
                HeightRequest = imageDimension,
            };

            CachedImage cachedImage = new CachedImage
            {
                Source = Source,
                WidthRequest = imageDimension,
                HeightRequest = imageDimension,
                Aspect = Aspect.AspectFill,
                DownsampleToViewSize = true,
                LoadingPlaceholder = "xiaobin.jpg"
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
            recognizer.Tapped +=    (sender, args) =>
            {
                if (ischeck)
                {
                    ischeck = !ischeck;
                    stack.Children.Remove(selectpic);
                    stack.Children.Remove(box);
                }
                else
                {
                        stack.Children.Add(box);
                        stack.Children.Add(selectpic);

                }

            };
            box.GestureRecognizers.Add(recognizer);
            cachedImage.GestureRecognizers.Add(recognizer);
            stack.Children.Add(cachedImage);
        }


     
    }  
    
}
