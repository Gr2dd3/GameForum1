using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GameForum1.Areas.Identity.Data;

// Add profile data for application users by adding properties to the GameForum1User class
public class GameForum1User : IdentityUser
{
    [PersonalData]
    public string FirstName { get; set; }

    [PersonalData]
    public string LastName { get; set; }

    [PersonalData]
    public string NickName { get; set; }

    [PersonalData]
    public int BirthYear { get; set; }
}

