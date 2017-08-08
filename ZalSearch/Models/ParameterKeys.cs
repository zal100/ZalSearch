using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ZalSearch.Models
{
    [Table("ParameterKeys")]
    public class ParameterKeys
    {
        [Key]
        public int ParameterId { get; set; }
        public string TermValue { get; set; }
    }
}