using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Stock6.CustomControls;
using Stock6.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PasswordEntryCell), typeof(PasswordEntryCellRenderer))]
namespace Stock6.iOS.Renderer
{
    
   public class PasswordEntryCellRenderer: EntryCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var entryCell = (PasswordEntryCell)item;
            var cell = base.GetCell(item, reusableCell, tv);
            if (cell != null)
            {
                var textField = (UITextField)cell.ContentView.Subviews[0];
                textField.SecureTextEntry = entryCell.IsPassword;
            }
            return cell;
        }
    }
}