using ISPAN.Izakaya.Entities;
using System;
using System.Collections.Generic;

namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface IBranchRepository
    {
        void Create(BranchEntity branch);
        void Delete(int id);
        List<BranchEntity> GetAll();
        BranchEntity Get(int id);

        void Update(BranchEntity branch);
        void UpdateCloseTime(DateTime closeTime, int branchId);
    }
}
