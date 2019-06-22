using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeliveriesApp
{
    public class AzureHelper
    {
        public static MobileServiceClient MobileService = new MobileServiceClient("https://pnj-deliveryapp.azurewebsites.net");  // static variable for Azure access - copy from Overview of (Web) App Service in Azure portal

        // generic task - for any class - so in AzureHelper
        //public static Task<bool> Insert<T>(ref T objectToInsert)
        // public static async Task <bool> Insert<T>(ref T objectToInsert)  // async methods cannot have ref in or out parameters, so drop ref
        public static async Task<bool> Insert<T>(T objectToInsert)
        {
            try
            {
                await MobileService.GetTable<T>().InsertAsync(objectToInsert);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
