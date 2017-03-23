using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cost_Management.Models;

namespace Cost_Management.Services
{
    public class MasterServices
    {
        public static string status = "";
        int i = 0;
        //實作db模型
        public Cost_Management.Models.Cost_Management1Entities db = new Models.Cost_Management1Entities();
        //根據分頁以及搜尋來取得資料陣列的方法
        public List<expense_form> GetDataList(ForPaging Paging, string Search , string DateSearch,string sortOrder)
        {
            
            //宣告要接受全部搜尋資料的物件
            IQueryable<expense_form> SearchData;
            //判斷搜尋是否為空或Null，用於決定要呼叫取得搜尋資料
            if (String.IsNullOrEmpty(Search) && String.IsNullOrEmpty(DateSearch))
            {
                SearchData = GetAllDataList(Paging);
            }
            else
            {
                SearchData = GetAllDataList(Paging, Search,DateSearch);
            }
            //先排序再根據分頁來回傳所需部分的資料陣列
            switch (sortOrder)
            {
                case "formName_desc":
                    return SearchData.OrderByDescending(p => p.formName)
          .Skip((Paging.NowPage - 1) *
              Paging.ItemNum).Take(Paging.ItemNum).ToList();
                    break;

                case "projectcode_desc":
                    return SearchData.OrderByDescending(p => p.projectCode)
            .Skip((Paging.NowPage - 1) *
                Paging.ItemNum).Take(Paging.ItemNum).ToList();
                    break;

                case "lmuser_desc":
                    return SearchData.OrderByDescending(p => p.lmuser)
               .Skip((Paging.NowPage - 1) *
                   Paging.ItemNum).Take(Paging.ItemNum).ToList();
                    break;

                case "lmtime_desc":
                    return SearchData.OrderByDescending(p => p.lmtime)
          .Skip((Paging.NowPage - 1) *
              Paging.ItemNum).Take(Paging.ItemNum).ToList();
                    break;

                default:
                    return SearchData.OrderByDescending(p => p.lmtime)
              .Skip((Paging.NowPage - 1) *
                  Paging.ItemNum).Take(Paging.ItemNum).ToList();
                    break;
            }
        }
        //無搜尋值的搜尋資料方法
        public IQueryable<expense_form> GetAllDataList(ForPaging Paging)
        {
            //宣告要回傳的搜尋資料為資料庫中的Guestbooks資料表
            IQueryable<expense_form> Data = db.expense_form;
            //計算所需的總頁數
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            //重新設定正確的頁數，避免有不正確值傳入
            Paging.SetRightPage();
            //回傳搜尋資料
            return Data;
        }

        //包含搜尋值的搜尋資料方法
        public IQueryable<expense_form> GetAllDataList(ForPaging Paging, string Search, string DateSearch)
        {
            if(!String.IsNullOrEmpty(DateSearch)&& !String.IsNullOrEmpty(Search))
            {
                IQueryable<expense_form> Data = db.expense_form
               .Where(p => p.formName.Contains(Search)&& p.exp_date.Contains(DateSearch) || p.userID.Contains(Search) && p.exp_date.Contains(DateSearch));
                //計算所需的總頁數
                Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
                //重新設定正確的頁數，避免有不正確值傳入
                Paging.SetRightPage();
                //回傳搜尋資料
                return Data;
            }
            else if(!String.IsNullOrEmpty(DateSearch) && String.IsNullOrEmpty(Search))
            {
                //根據搜尋值來搜尋資料
                IQueryable<expense_form> Data = db.expense_form
                    .Where(p => p.exp_date.Contains(DateSearch));
                //計算所需的總頁數
                Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
                //重新設定正確的頁數，避免有不正確值傳入
                Paging.SetRightPage();
                //回傳搜尋資料
                return Data;
            }
            else
            {
                IQueryable<expense_form> Data = db.expense_form
              .Where(p => p.formName.Contains(Search)  || p.userID.Contains(Search));
                //計算所需的總頁數
                Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
                //重新設定正確的頁數，避免有不正確值傳入
                Paging.SetRightPage();
                //回傳搜尋資料
                return Data;
            }
           
        }
        public AspNetUsers GetAutrorityById(string Id)
        {
            //回傳根據標號所取得的資料
            return db.AspNetUsers.Find(Id);
        }
        //藉由標號取得單筆資料的方法
        public expense_form GetDataById(int ID)
        {
            //回傳根據標號所取得的資料
            return db.expense_form.Find(ID);
        }
        //修改表單
        public void Edit(expense_form UpdateData)
        {
            //取得要修改的資料
            expense_form OldData = db.expense_form.Find(UpdateData.ID);
            //修改資料庫裡的值
            OldData.formID = UpdateData.formID;
            OldData.formName = UpdateData.formName;
            OldData.formNumber = UpdateData.formNumber;
            OldData.RequireNumber = UpdateData.RequireNumber;
            OldData.circleID = UpdateData.circleID;
            OldData.description = UpdateData.description;
            OldData.nation = UpdateData.nation;
            OldData.exp_date = UpdateData.exp_date;
            OldData.location = UpdateData.location;
            OldData.userID = UpdateData.userID;
            OldData.userName = UpdateData.userName;
            OldData.exp_type = UpdateData.exp_type;
            OldData.exp_attr = UpdateData.exp_attr;
            OldData.currency = UpdateData.currency;
            OldData.QTY = UpdateData.QTY;
            OldData.price = UpdateData.price;
            OldData.amount = UpdateData.amount;
            OldData.tax = UpdateData.tax;
            OldData.invoice = UpdateData.invoice;
            OldData.projectCode = UpdateData.projectCode;
            OldData.signuser = UpdateData.signuser;
            OldData.closeuser = UpdateData.closeuser;
            OldData.signtime = UpdateData.signtime;
            OldData.closetime = UpdateData.closetime;
            OldData.SignStatus = UpdateData.SignStatus;
            OldData.note = UpdateData.note;
            OldData.DocumentsNumber = UpdateData.DocumentsNumber;
            OldData.picture = UpdateData.picture;


            //儲存資料庫變更
            db.SaveChanges();
        }
        public void AuthorityEdit(AspNetUsers UpdateData)
        {
            //取得要修改的資料
            AspNetUsers OldData = db.AspNetUsers.Find(UpdateData.Id);
            //修改資料庫裡的值
            OldData.Email = UpdateData.Email;
            OldData.PasswordHash = UpdateData.PasswordHash;
            OldData.SecurityStamp = UpdateData.SecurityStamp;
            OldData.LockoutEnabled = UpdateData.LockoutEnabled;
            OldData.AccessFailedCount = UpdateData.AccessFailedCount;
            OldData.UserName = UpdateData.UserName;
           
            //儲存資料庫變更
            db.SaveChanges();
        }
        //刪除表單
        public void Delete(int ID)
        {
            expense_form DeleteData = db.expense_form.Find(ID);
            db.expense_form.Remove(DeleteData);
            db.SaveChanges();
        }
        //新增表單
        public void DBCreate(string RequireNumber, string circleID, string description, string nation, string exp_date
                                              , string location, string userID, string userName, string exp_type, string exp_attr, string currency, float QTY
                                              , string price, float amount, string tax, string invoice, string projectCode, string lmuser, string note,string DocumentsNumber, string picturename0, string picturename1, string picturename2)
        {

            expense_form NewData = new expense_form();

            NewData.formID = "45";//一定值 待定
            NewData.formName = "費用報銷表單";
            
            NewData.RequireNumber = RequireNumber;
            NewData.circleID = circleID;
            NewData.description = description;
            NewData.nation = nation;
            NewData.exp_date = exp_date;
            NewData.location = location;
            NewData.userID = userID;
            NewData.userName = userName;
            NewData.exp_type = exp_type;
            NewData.exp_attr = exp_attr;
            NewData.currency = currency;
            NewData.QTY = QTY;
            NewData.price = price;
            NewData.amount = amount;
            NewData.tax = tax;
            NewData.invoice = invoice;
            NewData.projectCode = projectCode;
            NewData.lmtime = (Convert.ToInt32(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds)).ToString();
            NewData.formNumber = NewData.formID+ NewData.lmtime;
            NewData.lmuser = lmuser.Substring(0, (lmuser.IndexOf("@", 2)));
            NewData.signtime = DateTime.Now;
            NewData.signuser = "nosign@";
            NewData.closetime = DateTime.Now;
            NewData.closeuser = "nosign@";
            NewData.note = note;
            NewData.picture = picturename0;
            NewData.picture1 = picturename1;
            NewData.picture2 = picturename2;
            NewData.DocumentsNumber = DocumentsNumber;

            db.expense_form.Add(NewData);

            db.SaveChanges();
        }
      
        public List<AspNetUsers> Data1List()
        {
            return db.AspNetUsers.ToList();
        }


    }
}