using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ZalSearch.Models
{
    [Table("Page")]
    public class PageModels
    {
        [Key]
        public int PageId { get; set; }
        [Url]
        public string ITunesPageURL { get; set; }
        public int Clicks { get; set; }
    }
}