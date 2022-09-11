using Microsoft.AspNetCore.Identity;

namespace ECommersionAPI.Domain.Entities.Identity
{
    public class AppUser:IdentityUser<string>
    {
        public string NameSurname { get; set; }
    }
}
