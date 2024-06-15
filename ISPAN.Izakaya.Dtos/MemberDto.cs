using System;

namespace ISPAN.Izakaya.Dtos
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Points { get; set; }
        public string AuthenticatioCode { get; set; }
        public DateTime Birthday { get; set; }
    }
}
