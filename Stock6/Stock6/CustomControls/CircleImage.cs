using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Stock6.CustomControls
{
    public class CircleImage:Image
    {
        BindableProperty RadiusProperty = BindableProperty.Create("Radius", typeof(float), typeof(CircleImage), (float)12);
        public float Radius
        {
            get
            {
                return (float)base.GetValue(RadiusProperty);
            }
            set
            {
                SetValue(RadiusProperty, value);
            }
        }
    }
}
