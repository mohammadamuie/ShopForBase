using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.MenuItem
{
    public class UpsertMenuItem
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Url1 { get; set; }
        public int? ParentId { get; set; }
    }
}
