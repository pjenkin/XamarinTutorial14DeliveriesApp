using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveriesApp.Model
{
    public class Delivery
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double OriginLatitude { get; set; }

        public double OriginLongitude { get; set; }

        public double DestinationLatitude { get; set; }

        public double DestinationLongitude { get; set; }

        /// <summary>
        /// Status variable (could use an enum here)
        /// 0 = awaiting delivery person
        /// 1 = being delivered
        /// 2 = delivered
        /// </summary>
        public int Status { get; set; }

        public string DeliveryPersonId { get; set; }

        public static async Task<List<Delivery>> GetDeliveries()
        {
            List<Delivery> deliveries = new List<Delivery>();

            //deliveries = await AzureHelper.MobileService.GetTable<Delivery>().ToListAsync();

            // Get only certain deliveries - where Status is not 2 (delivered already)

            deliveries = await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.Status != 2).ToListAsync();

            return deliveries;
        }

        public static async Task<List<Delivery>> GetDelivered()
        {
            List<Delivery> deliveries = new List<Delivery>();

            //deliveries = await AzureHelper.MobileService.GetTable<Delivery>().ToListAsync();

            // Get only certain deliveries - where Status is 2 (delivered already)

            deliveries = await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.Status == 2).ToListAsync();

            return deliveries;
        }

        /// <summary>
        /// Overload for GetDelivered for a specific delivery person
        /// </summary>
        /// <param name="">deliveryPersonID for delivery arranged</param>
        /// <returns></returns>
        public static async Task<List<Delivery>> GetDelivered(string deliveryPersonID)
        {
            List<Delivery> deliveries = new List<Delivery>();

            //deliveries = await AzureHelper.MobileService.GetTable<Delivery>().ToListAsync();

            // Get only certain deliveries - where Status is 2 (delivered already)

            deliveries = await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.Status == 2 && d.DeliveryPersonId == deliveryPersonID).ToListAsync();

            return deliveries;
        }


        public static async Task<List<Delivery>> GetBeingDelivered(string deliveryPersonId)
        {
            List<Delivery> deliveries = new List<Delivery>();

            //deliveries = await AzureHelper.MobileService.GetTable<Delivery>().ToListAsync();

            // Get only certain deliveries - where Status is 1 (being delivered) and where delivery person is the one identified

            deliveries = await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.Status == 1 && d.DeliveryPersonId == deliveryPersonId).ToListAsync();

            return deliveries;
        }

        public static async Task<List<Delivery>> GetWaiting()
        {
            List<Delivery> deliveries = new List<Delivery>();

            //deliveries = await AzureHelper.MobileService.GetTable<Delivery>().ToListAsync();

            // Get only certain deliveries - where Status is 0 (waiting for pickup by delivery person)

            deliveries = await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.Status == 0).ToListAsync();

            return deliveries;
        }



        public static async Task<bool> InsertDelivery(Delivery delivery)
        {
            return await AzureHelper.Insert<Delivery>(delivery);       // use bespoke generic method from helper to insert a new delivery record
        }

        /// <summary>
        /// overrride for ToString so as to display delivery data text nicely e.g. for in a list
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // return base.ToString();
            return $"{Name} - {Status}";        
        }

        /// <summary>
        /// Change status to show package picked up, and record by which deliveryperson
        /// </summary>
        /// <param name="deliveryPerson"></param>
        /// <returns>True if successful</returns>
        public static async Task<bool> MarkAsPickedUp(Delivery delivery, string deliveryPersonId)
        {
            try
            {
                delivery.Status = 1;                            // interesting no rollback here
                delivery.DeliveryPersonId = deliveryPersonId;   // interesting no rollback here
                await AzureHelper.MobileService.GetTable<Delivery>().UpdateAsync(delivery);
                // Update the Azure database using our bespoke AzureHelper with generic method (as written in 18-151) 25-181
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        /// <summary>
        /// Change status to show package delivered
        /// </summary>
        /// <param name="deliveryPerson"></param>
        /// <returns>True if successful</returns>
        public static async Task<bool> MarkAsDelivered(Delivery delivery)
        {
            try
            {
                delivery.Status = 2;                            // interesting no rollback here
                await AzureHelper.MobileService.GetTable<Delivery>().UpdateAsync(delivery);
                // Update the Azure database using our bespoke AzureHelper with generic method (as written in 18-151) 25-181
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        /// <summary>
        /// Change status to show package picked up, and record by which delivery person  (overload requiring only delivery ID)
        /// </summary>
        /// <param name="deliveryPerson"></param>
        /// <returns>True if successful</returns>
        public static async Task<bool> MarkAsPickedUp(string deliveryId, string deliveryPersonId)
        {
            try
            {
                var delivery = (await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.Id == deliveryId).ToListAsync()).FirstOrDefault();

                delivery.Status = 1;                            // interesting no rollback here
                delivery.DeliveryPersonId = deliveryPersonId;   // interesting no rollback here
                await AzureHelper.MobileService.GetTable<Delivery>().UpdateAsync(delivery);
                // Update the Azure database using our bespoke AzureHelper with generic method (as written in 18-151) 25-181
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        /// <summary>
        /// Change status to show package delivered (overload requiring only delivery ID)
        /// </summary>
        /// <param name="deliveryPerson"></param>
        /// <returns>True if successful</returns>
        public static async Task<bool> MarkAsDelivered(string deliveryId)
        {
            try
            {
                var delivery = (await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.Id == deliveryId).ToListAsync()).FirstOrDefault();

                delivery.Status = 2;                            // interesting no rollback here
                await AzureHelper.MobileService.GetTable<Delivery>().UpdateAsync(delivery);
                // Update the Azure database using our bespoke AzureHelper with generic method (as written in 18-151) 25-181
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }
    }
}
