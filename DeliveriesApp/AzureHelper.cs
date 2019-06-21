using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveriesApp
{
    public class AzureHelper
    {
        public static MobileServiceClient MobileService = new MobileServiceClient("https://pnj-deliveryapp.azurewebsites.net");  // static variable for Azure access - copy from Overview of (Web) App Service in Azure portal
    }
}
