using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cost_Management.Services;
using Cost_Management.ViewModel;
using Cost_Management.Models;
using System.IO;
using Microsoft.AspNet.Identity;
namespace Cost_Management.Controllers
{
    public class MasterController : Controller
    {
       
        //最高權限管理者
        public string user = "Manager@va7.com.tw";
        public static string[] picturexist = null;
        MasterServices data = new MasterServices();
        
        // GET: Master
        public ActionResult Master()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Master( string RequireNumber, string circleID, string description, string nation, string exp_date
                                              , string location, string userID, string userName, string exp_type, string exp_attr, string currency, float QTY
                                              , string price, float amount, string tax, string invoice, string projectCode, string note, string DocumentsNumber,
                                    IEnumerable<HttpPostedFileBase> ItemImage0, IEnumerable<HttpPostedFileBase> ItemImage1, IEnumerable<HttpPostedFileBase> ItemImage2)
        {

            string picturename0 = null;
            string picturename1 = null;
            string picturename2 = null;

            foreach (var ItemImages in ItemImage0)
            {
                if (ItemImages != null && ItemImages.ContentLength > 0)
                {
                    using (Stream inputStream = ItemImages.InputStream)
                    {
                        MemoryStream memorystream = inputStream as MemoryStream;
                        if (memorystream == null)
                        {
                            memorystream = new MemoryStream();
                            inputStream.CopyTo(memorystream);
                        }
                        picturename0 = Convert.ToBase64String(memorystream.ToArray());
                    }

                }
            }
            foreach (var ItemImages in ItemImage1)
            {
                if (ItemImages != null && ItemImages.ContentLength > 0)
                {
                    using (Stream inputStream = ItemImages.InputStream)
                    {
                        MemoryStream memorystream = inputStream as MemoryStream;
                        if (memorystream == null)
                        {
                            memorystream = new MemoryStream();
                            inputStream.CopyTo(memorystream);
                        }
                        picturename1 = Convert.ToBase64String(memorystream.ToArray());
                    }

                }
            }
            foreach (var ItemImages in ItemImage2)
            {
                if (ItemImages != null && ItemImages.ContentLength > 0)
                {
                    using (Stream inputStream = ItemImages.InputStream)
                    {
                        MemoryStream memorystream = inputStream as MemoryStream;
                        if (memorystream == null)
                        {
                            memorystream = new MemoryStream();
                            inputStream.CopyTo(memorystream);
                        }
                        picturename2 = Convert.ToBase64String(memorystream.ToArray());
                    }

                }
            }
            string lmuser = User.Identity.Name;
            data.DBCreate( RequireNumber, circleID, description, nation, exp_date
                                                 , location, userID, userName, exp_type, exp_attr, currency, QTY
                                                 , price, amount, tax, invoice, projectCode, lmuser, note, DocumentsNumber,picturename0, picturename1, picturename2);
            return RedirectToAction("Index", "Home");
        }

        //搜尋表
        public ActionResult Demand(string sortOrder,string Search, string DateSearch,string filterStatus,int Page= 0)
        {
            
            MasterView Data = new MasterView();
            //將傳入值Search(搜尋)放入頁面模型中
            if (!String.IsNullOrEmpty(DateSearch))
            {
                Data.DateSearch = DateSearch;
            }
           else
            {
                Data.DateSearch = DateSearch;
            }
            Data.Search = Search;
            Data.sortOrder = sortOrder;
            //新增頁面模型中的分頁
            if (Page==0)
            {
                Data.Paging = new ForPaging();
            }
            else
            {
                Data.Paging = new ForPaging(Page);
            }
            //從Service中取得頁面所需陣列資料
            Data.DataList = data.GetDataList(Data.Paging, Data.Search, Data.DateSearch, sortOrder);
            
            //將頁面資料傳入View中
            return View(Data);
        }
        //統計表
        public ActionResult Statistics_table(string sortOrder,string yearSearch,string MonthSearch, string Search, string DateSearch, string projectSearch,string filterStatus, int Page = 0)
        {
            
            string year_month = yearSearch + MonthSearch;
            MasterView Data = new MasterView();
            //將傳入值Search(搜尋)放入頁面模型中
            if(!string.IsNullOrEmpty(DateSearch))
            {
                Data.DateSearch = DateSearch;
            }
            
            else 
            {
                Data.DateSearch = year_month;
            }
          
            Data.projectSearch = projectSearch;
            Data.Search = Search;
            Data.sortOrder = sortOrder;
            //新增頁面模型中的分頁
            if (Page == 0)
            {
                Data.Paging = new ForPaging();
            }
            else
            {
                Data.Paging = new ForPaging(Page);
            }
            //從Service中取得頁面所需陣列資料
            Data.DataList = data.GetData1List(Data.Paging, Data.Search, Data.DateSearch, Data.projectSearch,sortOrder);
            
            //將頁面資料傳入View中
            return View(Data);
        }
        public ActionResult Edit(int ID)
        {
            //取得頁面所需資料，藉由Service取得
            expense_form Data = data.GetDataById(ID);
            //將資料傳入View中
            return View(Data);
        }

        //修改留言傳入資料時的Action
        [HttpPost] //設定此Action只接受頁面POST資料傳入
        //使用Bind的Inculde來定義只接受的欄位，用來避免傳入其他不相干值
        public ActionResult Edit(int ID, expense_form UpdateData)
        {
            //將編號設定至修改資料中
            UpdateData.ID = ID;
                UpdateData.signuser = "nosign@";
                UpdateData.closeuser = "nosign@";
                UpdateData.signtime = DateTime.Now;
                UpdateData.closetime = DateTime.Now;
                UpdateData.SignStatus = false;
            //使用Service來修改資料
            data.Edit(UpdateData);
            //重新導向頁面至開始頁面
            return RedirectToAction("Demand", "Master");
        }
        public ActionResult SignEdit(int ID)
        {
            //取得頁面所需資料，藉由Service取得
            expense_form Data = data.GetDataById(ID);
            //將資料傳入View中
            return View(Data);
        }
        [HttpPost] //設定此Action只接受頁面POST資料傳入
        //使用Bind的Inculde來定義只接受的欄位，用來避免傳入其他不相干值
        public ActionResult SignEdit(int ID, expense_form UpdateData)
        {
            //將編號設定至修改資料中
            UpdateData.ID = ID;
            
                UpdateData.signuser = User.Identity.GetUserName();
                UpdateData.closeuser = User.Identity.GetUserName();
            UpdateData.signtime = DateTime.Now;
                UpdateData.closetime = DateTime.Now;
                UpdateData.SignStatus = true;
           
            //使用Service來修改資料
            data.Edit(UpdateData);
            //重新導向頁面至開始頁面
            return RedirectToAction("Demand", "Master");
        }
        //刪除表單
        public ActionResult Delete(int ID)
        {
            data.Delete(ID);
            return RedirectToAction("Demand", "Master");
        }
        public ActionResult DetailView(int ID)
        {

            //取得頁面所需資料，藉由Service取得
            expense_form Data = data.GetDataById(ID);
            picturexist = new string[3];
            string picture = Data.picture;
            string picture1 = Data.picture1;
            string picture2 = Data.picture2;
            picturexist[0] = picture;
            picturexist[1] = picture1;
            picturexist[2] = picture2;
            Data.picture = "data:image/png;base64," + picture;
            Data.picture1 = "data:image/png;base64," + picture1;
            Data.picture2 = "data:image/png;base64," + picture2;
            
            //將資料傳入View中
            return View(Data);
        }
       

    }
}

