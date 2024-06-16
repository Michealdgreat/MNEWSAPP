using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNEWSAPP.MVVM.Models
{
    public class ApiResponseModel
    {
        public string? Status { get; set; }
        public int TotalResults { get; set; }
        public ObservableCollection<ArticleModel>? Articles { get; set; }
    }
}
