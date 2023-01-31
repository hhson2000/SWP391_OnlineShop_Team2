using Microsoft.Build.Framework;

namespace NitStore.Models.DTO
{
    public class UserAddDTO
    {
        public string UserName { get; set; }

        public int Role { get; set; }

        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool NeedToChange { get; set; }

        public string Email { get; set; }
    }
}
