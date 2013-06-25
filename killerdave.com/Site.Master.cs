using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using RSCWebApp.code.models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace RSCWebApp
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    SetMenu("Admin");
                }
                else
                {
                    SetMenu("Default");
                }
            }
        }

        private void SetMenu(string role)
        {
            SqlConnection SqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["RSCDBConnectionString"].ToString());
            SqlCommand SqlCmd = new SqlCommand("GetMenuLevels", SqlCon);
            SqlCmd.Parameters.Add(new SqlParameter("@Role", role));
            SqlCmd.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();

            SqlCon.Open();
            SqlDataReader SqlReader = SqlCmd.ExecuteReader();
            dt.Load(SqlReader);
            SqlCon.Close();

            DataRow[] drowpar = dt.Select("ParentItem=" + 0);

            foreach (DataRow dr in drowpar)
            {
                NavigationMenu.Items.Add(new MenuItem(dr["MenuLabel"].ToString(),
                        dr["RowID"].ToString(), "",
                        dr["MenuURL"].ToString()));
            }

            foreach (DataRow dr in dt.Select("ParentItem >" + 0))
            {
                MenuItem mnu = new MenuItem(dr["MenuLabel"].ToString(),
                                dr["RowID"].ToString(),
                                "", dr["MenuURL"].ToString());
                NavigationMenu.FindItem(dr["ParentItem"].ToString()).ChildItems.Add(mnu);
            }
        
        }
    }
}


//string[,] menuitems = { { "Students", "~/Account/studentlookup.aspx" }, { "New User", "~/Account/Register.aspx" }, { "Change Password", "~/Account/ChangePassword.aspx" } };
//if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
//{
//    for (int i = 0; i < menuitems.GetLength(0); i++)
//    {
//        MenuItem m = new MenuItem();
//        m.Text = menuitems[i, 0];
//        m.NavigateUrl = menuitems[i, 1];
//        NavigationMenu.Items.Add(m);
//    }
//}