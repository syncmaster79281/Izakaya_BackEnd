using ISPAN.Izakaya.Entities;
using System.Collections.Generic;


namespace ISPAN.Izakaya.IDAL_IRepo_
{
    public interface IMemberRepository
    {
        void Create(MemberEntity entity);
        void Delete(int id);
        MemberEntity Get(int id);
        void Edit(MemberEntity entity);
        IEnumerable<MemberEntity> Search();
    }

}
