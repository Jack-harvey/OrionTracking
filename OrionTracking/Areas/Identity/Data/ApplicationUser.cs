using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using OrionTracking.Models;

namespace OrionTracking.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        //public ApplicationUser()
        //{
        //    AssetChangeTrackingNotes = new HashSet<AssetChangeTrackingNote>();
        //}
        //[InverseProperty("AspNetUser")]
        //public virtual ICollection<AssetChangeTrackingNote> AssetChangeTrackingNotes { get; set; }
    }
}