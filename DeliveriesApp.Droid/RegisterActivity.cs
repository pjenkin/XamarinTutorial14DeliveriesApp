using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {

        EditText emailEditText, passwordEditText, confirmPasswordEditText;      // private, so camel case
        Button registerButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.Register);       // (value of a dot-notation'd resource is actually an integer constant eg 2130837508)

            emailEditText = FindViewById<EditText>(Resource.Id.registerEmailEditText);
            passwordEditText = FindViewById<EditText>(Resource.Id.registerPasswordEditText);
            confirmPasswordEditText = FindViewById<EditText>(Resource.Id.registerConfirmPasswordEditText);
            registerButton = FindViewById<Button>(Resource.Id.registerButton);

            // ... and sort out an event handler
            registerButton.Click += RegisterButton_Click;

        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}