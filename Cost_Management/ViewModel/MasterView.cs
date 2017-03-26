using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Cost_Management.Models;
using Cost_Management.Services;
using System.ComponentModel.DataAnnotations;
namespace Cost_Management.ViewModel
{
    public class MasterView
    {

        //顯示資料陣列
        [DisplayName("搜尋")]
        public string Search { get; set; }
        [DataType(DataType.Date)]
        public string DateSearch { get; set; }
        public string yeatSearch { get; set; }
        public string MonthSearch { get; set; }
        public string projectSearch { get; set; }
        public ForPaging Paging { get; set; }
        public List<expense_form> DataList { get; set; }
        public List<AspNetUsers> Data1List { get; set; }
        public double NTamount = 0;
        public double taxamount = 0;
        public string sortOrder { get; set; }
    }
}