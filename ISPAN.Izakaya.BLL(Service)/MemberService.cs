using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Linq;

namespace ISPAN.Izakaya.BLL_Service_
{
    public class MemberService
    {
        private readonly IMemberRepository _memberRepository;
        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public IEnumerable<MemberDto> Search()
        {
            return _memberRepository.Search().Select(x => x.ToDto());
        }
        public MemberDto Get(int id)
        {
            return _memberRepository.Get(id).ToDto();
        }
        public void Create(MemberDto dto)
        {
            var entity = dto.ToEntity();
            _memberRepository.Create(entity);
        }
        public void Edit(MemberDto dto)
        {
            var entity = dto.ToEntity();
            _memberRepository.Edit(entity);
        }
        public void Delete(int id)
        {
            _memberRepository.Delete(id);
        }
    }
}
