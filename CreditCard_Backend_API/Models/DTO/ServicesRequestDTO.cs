﻿namespace CreditCard_Backend_API.Models.DTO
{
    public class ServicesRequestDTO
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public int ServiceCharges { get; set; }
    }
}
