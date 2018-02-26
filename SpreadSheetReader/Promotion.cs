﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadSheetReader
{
    public class Promotion
    {

        public Promotion(DateTime startdate, DateTime endDate, double price, string sku, string store, double volume)
        {
            Id = Guid.NewGuid();
            StartDate = startdate;
            EndDate = endDate;
            Sku = sku;
            InitialPrice = price;
            DaysRaninEachStore = new Dictionary<string, StoreVolume>() { { store, new StoreVolume(1, volume) } };
        }

        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Sku { get; set; }
        public double InitialPrice { get; set; }
        public Dictionary<string, StoreVolume> DaysRaninEachStore { get; set; }

        public void IncrementStoreCountAndVolume(string store, double volume)
        {
            IncrementStoreDate(store, volume);
        }
        private void IncrementStoreDate(string store, double volumeOnDay)
        {
            if (DaysRaninEachStore.TryGetValue(store, out var storeCount))
            {
                DaysRaninEachStore[store].DaysRanInStore = storeCount.DaysRanInStore + 1;
                DaysRaninEachStore[store].TotalVolume = storeCount.TotalVolume + volumeOnDay;
            }
            else
            {
                DaysRaninEachStore.Add(store, new StoreVolume(1, volumeOnDay));
            }

        }
    }

    public class StoreVolume
    {
        public StoreVolume(int initialDays, double dayOneVolume)
        {
            DaysRanInStore = initialDays;
            TotalVolume = dayOneVolume;
        }
        public int DaysRanInStore { get; set; }
        public double TotalVolume { get; set; }
    }
}