using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Stock6.CustomControls
{
     public class CustomListView:ListView
    {
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return base.OnMeasure(widthConstraint, heightConstraint);
        }
    }
}
