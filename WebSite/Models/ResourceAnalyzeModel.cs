using Microsoft.AspNetCore.Identity;
using RoslynLibrary.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace WebSite.Models
{
    public class ResourceAnalyzeModel : AnalyzeBaseModel
    {
        [Key]
        public Guid Id { get; set; }

        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
