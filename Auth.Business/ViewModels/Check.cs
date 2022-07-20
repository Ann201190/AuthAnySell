using System;
using System.Collections.Generic;

namespace Auth.Business.ViewModels
{
    public class Check
    {
        public string Email { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string OrderNumber { get; set; }
        public string EmployeeFIO { get; set; }
        public ICollection<CheckProduct> ReservationProducts { get; set; } = new List<CheckProduct>();
        public DateTime OrderDate { get; set; }    
    }
}
