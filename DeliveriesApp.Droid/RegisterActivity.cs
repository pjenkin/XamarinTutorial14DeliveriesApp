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
            registerButton = FindViewById<Button>(Resource.Id.registerUserButton);

            // ... and sort out an event handler
            registerButton.Click += RegisterButton_Click;

            string email = Intent.GetStringExtra("email");      // receive PutExtra'd data
            emailEditText.Text = email;                         // write any data which may've been typed in previous screen
        }

        private async void RegisterButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordEditText.Text))
            {
                if (passwordEditText.Text == confirmPasswordEditText.Text)
                {
                    var user = new User()
                    {
                        Email = emailEditText.Text,
                        Password = passwordEditText.Text,
                    };

                    await MainActivity.MobileService.GetTable<User>().InsertAsync(user);        // insert record to Azure db table
                    Toast.MakeText(this, "User record successfully added", ToastLength.Long).Show();
                    return;
                }
                Toast.MakeText(this, "Passwords don't match", ToastLength.Long).Show();
            }
            Toast.MakeText(this, "Password must be enetered", ToastLength.Long).Show();
        }
    }
}