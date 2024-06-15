using System.Collections.Generic;

namespace ISPAN.Izakaya.Entities
{
    public class ReportChartEntity
    {
        /// <summary>
        /// Line Chart
        /// </summary>
        public class RootObject
        {
            public List<int> TotalAmount { get; set; }
            public List<int> NetAmount { get; set; }
            public List<int> Customers { get; set; }
            public List<string> Categories { get; set; }
        }

        public RootObject GetData(List<int> totalAmount, List<int> netAmount, List<int> customers, List<string> categories)
        {
            var data = new RootObject();

            data.TotalAmount = totalAmount;
            data.NetAmount = netAmount;
            data.Customers = customers;
            data.Categories = categories;

            return data;
        }
        /// <summary>
        /// Website Traffic
        /// </summary>
        public class PaymentWayReport
        {
            public int Cash { get; set; }
            public int Card { get; set; }
            public int Electronic { get; set; }
            public int Ticket { get; set; }
        }

    }
}
