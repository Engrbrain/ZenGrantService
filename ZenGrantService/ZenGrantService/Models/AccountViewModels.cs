using System;
using System.Collections.Generic;

namespace ZenGrantService.Models
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Email { get; set; }
        public string Userid { get; set; }
        public bool HasRegistered { get; set; }
        public string LoginProvider { get; set; }
    }

    public class completeUserInformation
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public enumManager.State State { get; set; }
        public enumManager.country Nationality { get; set; }
        public enumManager.Gender Gender { get; set; }
        public string UserSummary { get; set; }
        public string JobDesignation { get; set; }
        public byte[] UserImage { get; set; }
        public enumManager.Scope scope { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
