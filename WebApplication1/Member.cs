namespace WebApplication1
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MembershipId { get; set; }
        public string RoleOfMem { get; set; }


        public Member(string name, string email, string phoneNumber, string membershipId, string roleOfMem)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            MembershipId = membershipId;
            RoleOfMem = roleOfMem;
        }
    }
}
