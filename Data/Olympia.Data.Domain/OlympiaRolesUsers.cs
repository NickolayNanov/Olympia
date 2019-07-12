namespace Olympia.Data.Domain
{
    using Microsoft.AspNetCore.Identity;

    public class OlympiaRolesUsers : IdentityUserRole<string>
    {
        public virtual OlympiaUser User { get; set; }

        public virtual OlympiaUserRole Role { get; set; }
    }
}
