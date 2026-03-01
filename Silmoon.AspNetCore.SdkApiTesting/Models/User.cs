using Silmoon.Extensions.Enums;
using Silmoon.Extensions.Interfaces;

namespace Silmoon.AspNetCore.SdkApiTesting.Models
{
    public class User : IDefaultUserIdentity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public IdentityRole Role { get; set; }
        public IdentityStatus Status { get; set; }
        public DateTime created_at { get; set; }
    }
}
