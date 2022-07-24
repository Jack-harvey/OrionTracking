﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrionTracking.Models
{
    public partial class DocumentType
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; } = null!;
        [StringLength(260)]
        public string RootPath { get; set; } = null!;
        [StringLength(260)]
        public string? FolderName { get; set; } = null!;
    }
}