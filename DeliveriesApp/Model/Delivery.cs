﻿using System;
using System.Collections.Generic;
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
    }
}
