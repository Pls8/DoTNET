using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TaskApp.Models.AuthModel
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Navigation Property - ONE User has MANY TodoTasks
        public virtual ICollection<TaskClass> TaskClasses { get; set; }
        //       ^-- no need!


        //form does NOT bind directly to AppUser
        //[IdentityUser] already contains: Email UserName PasswordHash Id PhoneNumber
        //Password is NEVER a property on AppUser
        //Register.cshtml
        //        ↓
        //RegisterViewModel
        //        ↓
        //AccountController
        //        ↓
        //      AppUser
        //        ↓
        //      Database

    }
}
