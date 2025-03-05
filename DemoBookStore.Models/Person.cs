using Microsoft.AspNetCore.Identity;

namespace DemoBookStore.Models
{
    public class Person:IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
