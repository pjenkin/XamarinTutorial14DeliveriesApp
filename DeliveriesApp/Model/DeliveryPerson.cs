using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveriesApp.Model
{
    public class DeliveryPerson
    {

        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public static async Task<DeliveryPerson> GetDeliveryPerson(string id)
        {
            DeliveryPerson person = new DeliveryPerson();

            person = (await AzureHelper.MobileService.GetTable<DeliveryPerson>().Where(dp => dp.Id == id).ToListAsync()).FirstOrDefault();
            // NB pattern for single instances (eg an individual person) - brackets and ().FirstOrDefault

            return person;
        }

        public async static Task<string> Login(string email, string password)
        {
            // bool result = false;
            string userId = string.Empty;


            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                // alert = UIAlertController.Create("Login incomplete", "Both email and password must be entered", UIAlertControllerStyle.Alert);
                // alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                // PresentViewController(alert, true, null);  // https://forums.xamarin.com/discussion/140298/uialertcontroller-not-showing
                // result = false;
                userId = string.Empty;
            }
            else
            {
                var deliveryPerson = (await AzureHelper.MobileService.GetTable<DeliveryPerson>().Where(dp => dp.Email == email).ToListAsync()).FirstOrDefault();      // null if no user in list
                if (deliveryPerson?.Password == password)     // PNJ null conditional added
                {
                    //Toast.MakeText(this, "Login successful", ToastLength.Long).Show();
                    // alert = UIAlertController.Create("Successful", "Welcome", UIAlertControllerStyle.Alert);
                    // alert.AddAction(UIAlertAction.Create("Thanks", UIAlertActionStyle.Default, null));
                    // PresentViewController(alert, true, null);       // PresentViewController to make alert show up

                    // result = true;
                    userId = deliveryPerson.Id;     // return ID of delivery person if successful sign-in

                    // navigate to home page
                }
                else
                {
                    // Toast.MakeText(this, "Incorrect user name or password", ToastLength.Long).Show();
                    // alert = UIAlertController.Create("Failure", "Email or password is incorrect", UIAlertControllerStyle.Alert);
                    // alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                    // PresentViewController(alert, true, null);

                    // result = false;
                    userId = string.Empty;

                    // navigate away if needed
                }
            }
            // return result;
            return userId ?? string.Empty;
        }


        /// <summary>
        /// Tryies to register a user with db table of User
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
        public static async Task<bool> Register(string email, string password, string confirmPassword)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(password))
            {
                if (password == confirmPassword)
                {
                    var deliveryPerson = new DeliveryPerson()
                    {
                        Email = email,
                        Password = password,
                    };

                    //await AzureHelper.MobileService.GetTable<User>().InsertAsync(user);        // insert record to Azure db table

                    // Use AzureHelper's bespoke generic method instead to write (in this case a record to User table) 18-152
                    //AzureHelper.Insert<User>(ref user);     // actually type (User) inferred from object instance (user)
                    //AzureHelper.Insert(ref user); // ref not used
                    AzureHelper.Insert(deliveryPerson);           // type still inferred


                    result = true;
                }
            }
            return result;
        }
    }
}
