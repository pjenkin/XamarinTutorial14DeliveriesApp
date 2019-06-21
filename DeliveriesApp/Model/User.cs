﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveriesApp.Model
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static async Task<bool> Login(string email, string password)
        {
            bool result = false;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                // alert = UIAlertController.Create("Login incomplete", "Both email and password must be entered", UIAlertControllerStyle.Alert);
                // alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                // PresentViewController(alert, true, null);  // https://forums.xamarin.com/discussion/140298/uialertcontroller-not-showing
                result = false;
            }
            else
            {
                var user = (await AzureHelper.MobileService.GetTable<User>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();      // null if no user in list
                if (user?.Password == password)     // PNJ null conditional added
                {
                    //Toast.MakeText(this, "Login successful", ToastLength.Long).Show();
                    // alert = UIAlertController.Create("Successful", "Welcome", UIAlertControllerStyle.Alert);
                    // alert.AddAction(UIAlertAction.Create("Thanks", UIAlertActionStyle.Default, null));
                    // PresentViewController(alert, true, null);       // PresentViewController to make alert show up

                    result = true;

                    // navigate to home page
                }
                else
                {
                    // Toast.MakeText(this, "Incorrect user name or password", ToastLength.Long).Show();
                    // alert = UIAlertController.Create("Failure", "Email or password is incorrect", UIAlertControllerStyle.Alert);
                    // alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                    // PresentViewController(alert, true, null);

                    result = false;

                    // navigate away if needed
                }
            }


            return result;
        }

    }
}
