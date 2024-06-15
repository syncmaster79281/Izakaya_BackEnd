using System;
using System.Collections.Generic;

namespace ISPAN.Izakaya.Entities
{
    public class PurchaseRecordEntity
    {
        public int Id { get; set; }

        public int BranchId { get; set; }

        public DateTime OrderDate { get; set; }

        public int TotalCost { get; set; }
        public virtual IEnumerable<PurchaseOrderEntity> PurchaseOrders { get; set; }
    }
}
