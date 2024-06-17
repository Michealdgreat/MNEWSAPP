using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNEWSAPP.MVVM.Models
{
    public class ResponseModel
    {
        public string? Status { get; set; }
        public int TotalResults { get; set; }
        public List<ArticleModel>? Articles { get; set; }
    }
}
