using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class BranchService
    {
        private readonly IBranchRepository _repository;
        public BranchService(IBranchRepository repo)
        {
            //決定用 EF 或 Dapper
            _repository = repo;
        }

        public void Create(BranchDto branch)
        {
            _repository.Create(branch.ToEntity());
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public BranchDto Get(int id)
        {
            return _repository.Get(id).ToDto();
        }

        public List<BranchDto> GetAll()
        {
            return _repository.GetAll().Select(x => x.ToDto()).ToList();
        }

        public void Update(BranchDto branch)
        {
            _repository.Update(branch.ToEntity());
        }

        public void UpdateCloseTime(DateTime closeTime, int branchId)
        {
            _repository.UpdateCloseTime(closeTime, branchId);
        }
    }
}