using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Stock6.Droid.Services;
using Stock6.Services;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(KeyboardHelper))]
namespace Stock6.Droid.Services
{
    public class KeyboardHelper : IKeyboardHelper
    {
        static Context _context;
        public static void Init(Context context)
        {
            _context = context;
        }
        public void ShowKeyboard()
        {
            var context = Forms.Context;
            var inputMethodManager = _context.GetSystemService(Context.InputMethodService) as InputMethodManager;

            if (inputMethodManager != null && _context is Activity)
            {
                var activity = _context as Activity;
                var token = activity.CurrentFocus?.WindowToken;
                inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
            }
        }

        public void HideKeyboard()
        {
            
            var imm = _context.GetSystemService(Context.InputMethodService) as InputMethodManager;

            if (imm != null && _context is Activity)
            {
                var activity = _context as Activity;
                var token = activity.CurrentFocus?.WindowToken;
                imm.HideSoftInputFromWindow(activity.Window.DecorView.WindowToken, 0);
                activity.Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);
                activity.Window.AddFlags(WindowManagerFlags.KeepScreenOn);
                activity.Window.DecorView.ClearFocus();
            }
        }
    }
}