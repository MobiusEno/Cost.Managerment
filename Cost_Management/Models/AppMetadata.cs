using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cost_Management.Models
{

    [MetadataType(typeof(expense_formMetadata))]
    public partial class expense_form
    {

        private class expense_formMetadata
        {

            [DisplayName("*表單代碼:")]
            [Required(ErrorMessage = "請輸入表單代碼")]
            public string formID { get; set; }

            [DisplayName("*表單名稱:")]
            [Required(ErrorMessage = "請輸入表單名稱")]
            public string formName { get; set; }

            [DisplayName("*表單序號:")]
            [Required(ErrorMessage = "請輸入表單序號")]
            public string formNumber { get; set; }

            [DisplayName("*請購單號:")]
            [Required(ErrorMessage = "請輸入請購單號")]
            public string RequireNumber { get; set; }

            [DisplayName("*群組編號:")]
            [Required(ErrorMessage = "請輸入群序號")]
            public string circleID { get; set; }

            [DisplayName("*費用說明:")]
            [Required(ErrorMessage = "請輸入費用說明")]
            public string description { get; set; }

            [DisplayName("*國內/國外:")]
            [Required(ErrorMessage = "請輸入國內/國外")]
            public string nation { get; set; }

            [DisplayName("*消費日期:")]
            [DataType(DataType.Date)]
            [Required(ErrorMessage = "請輸入消費日期")]
            public string exp_date { get; set; }

            [DisplayName("消費城市:")]
            
            public string location { get; set; }

            [DisplayName("*姓名:")]
            [Required(ErrorMessage = "請輸入姓名")]
            public string userID { get; set; }

            [DisplayName("工號:")]
            
            public string userName { get; set; }

            [DisplayName("*費用類別:")]
            [Required(ErrorMessage = "請輸入費用類別")]
            public string exp_type { get; set; }

            [DisplayName("*費用屬性:")]
            [Required(ErrorMessage = "請輸入費用屬性")]
            public string exp_attr { get; set; }

            [DisplayName("*幣別:")]
            [Required(ErrorMessage = "請輸入幣別")]
            public string currency { get; set; }

            [DisplayName("*數量:")]
            [Required(ErrorMessage = "請輸入數量")]
            public double QTY { get; set; }

            [DisplayName("原幣總價:")]
            public string price { get; set; }

            [DisplayName("*台幣總價:")]
            [Required(ErrorMessage = "請輸入台幣總價")]
            public double amount { get; set; }

            [DisplayName("稅額:")]
            public string tax { get; set; }

            [DisplayName("發票號碼:")]
            public string invoice { get; set; }

            [DisplayName("專案代號:")]
            public string projectCode { get; set; }

            [DisplayName("填單時間:")]
            public string lmtime { get; set; }

            [DisplayName("填單人:")]
            public string lmuser { get; set; }

            [DisplayName("簽核時間:")]
            public System.DateTime signtime { get; set; }

            [DisplayName("簽核人:")]
            public string signuser { get; set; }

            [DisplayName("結案時間:")]
            public System.DateTime closetime { get; set; }

            [DisplayName("承辦人:")]
            public string closeuser { get; set; }

            [DisplayName("附註:")]
            public string note { get; set; }

            [DisplayName("單據序號:")]
            public string DocumentsNumber { get; set; }

            [DisplayName("單據圖片:")]
            public string picture { get; set; }

            [DisplayName("單子狀態:")]
            public bool SignStatus { get; set; }

            //db更新時手動加入
            [DisplayName("單據圖片:")]
            public HttpPostedFileBase ItemImage0 { get; set; }
            public HttpPostedFileBase ItemImage1 { get; set; }
            public HttpPostedFileBase ItemImage2 { get; set; }

        }

    }
    public class AppMetadata
    {
        public string UserName { get; set; }
        public string CircleID { get; set; }
        public string FormID { get; set; }
        public string FormName { get; set; }
        public string FormNumber { get; set; }
        public string TimeStamp { get; set; }
        public string userID { get; set; }
        public string userName { get; set; }
        public string nation { get; set; }
        public string location { get; set; }
        public string exp_date { get; set; }
        public string exp_type { get; set; }
        public string exp_attr { get; set; }
        public string currency { get; set; }
        public string QTY { get; set; }

        public string price { get; set; }
        public string amout { get; set; }
        public string tax { get; set; }
        public string invoice { get; set; }
        public string projectCode { get; set; }
        public string note { get; set; }
        public string picture { get; set; }
        public string FormDescription { get; set; }
        

        public List<Data_1> Data { get; set; }

    }
    public class AppRespones
    {
        public string AppStatus { get; set; }
        public string ErrorContent { get; set; }

    }
    public class Data_1
    {
        public string SubFormName { get; set; }
        public string Type { get; set; }
        public List<SubData_1> SubData { get; set; }
        public string SubFormDescription { get; set; }

    }
    public class SubData_1
    {
        public string Content { get; set; }
        public string Hint { get; set; }
        public string Status { get; set; }

    }
}
