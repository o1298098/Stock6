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
        public void ShowKeyboard()
        {
            var context = Forms.Context;
            var inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;

            if (inputMethodManager != null && context is Activity)
            {
                var activity = context as Activity;
                var token = activity.CurrentFocus?.WindowToken;
                inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
            }
        }

        public void HideKeyboard()
        {
            var context = Forms.Context;
            var imm = context.GetSystemService(Context.InputMethodService) as InputMethodManager;

            if (imm != null && context is Activity)
            {
                var activity = context as Activity;
                var token = activity.CurrentFocus?.WindowToken;
                imm.HideSoftInputFromWindow(token,0);
                imm.HideSoftInputFromInputMethod(token, 0);
                imm.ToggleSoftInput(0,0);
                activity.Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);
                activity.Window.AddFlags(WindowManagerFlags.KeepScreenOn);
                activity.Window.DecorView.ClearFocus();
            }
        }
    }
}