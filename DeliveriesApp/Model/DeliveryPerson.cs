using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveriesApp.Model
{
    class DeliveryPerson
    {

        public string Id { get; set; }

        public static async Task<DeliveryPerson> GetDeliveryPerson(string id)
        {
            DeliveryPerson person = new DeliveryPerson();

            person = (await AzureHelper.MobileService.GetTable<DeliveryPerson>().Where(dp => dp.Id == id).ToListAsync()).FirstOrDefault();
            // NB pattern for single instances (eg an individual person) - brackets and ().FirstOrDefault

            return person;
        }
    }
}
