using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;

namespace IndianArmyWeb.Infrastructure.Helpers.Menu
{
    public class MenuHelper
    {
        //==== Method to create dynamic Tree Chart
        public void generateDynamicMenu()
        {
            //HtmlGenericControl htmlGnrcContrl = new HtmlGenericControl();

            StringBuilder sbTree = new StringBuilder();

            //==== Created first ul element of the unordered list to generate menu also assigned id="menu" as our entire css is based on this id.
            sbTree.Append("<ul id=\"OrgTree\">");

            //==== Call recursive method to get all child elements according to parents.
            //=== We we pass 0 as argument as 0 is id of default parent.
            string childItems = getMenuItems(0);
            sbTree.Append(childItems);

            sbTree.Append(" </ul>");

            //=== Store stringbuilder html content in html control
            //htmlGnrcContrl.InnerHtml = sbTree.ToString();
        }

        //==== Recrusive method.
        StringBuilder sbTree = new StringBuilder();
        int childCount = 0;
        public string getMenuItems(int parentId)
        {
            int ser = 1;
            string fileurl = "";
            string query = "";
            query = "SELECT orgID, orgName, ParentId, orgtype FROM gen_org_struct where ParentId = " + parentId;
            DataTable dtDist = new DataTable();
            dtDist = objDbCon.getDataTable(query, "Query");

            foreach (DataRow cdr in dtDist.Rows)
            {
                string ColorText = "";
                //getColor(cdr["orgtype"].ToString(), out ColorText);

                query = "";
                query = "SELECT orgID, orgName, ParentId FROM gen_org_struct where ParentId = " + cdr["OrgId"].ToString();
                DataTable dtChildCt = new DataTable();
                dtChildCt = objDbCon.getDataTable(query, "Query");

                if (dtChildCt.Rows.Count > 0)
                {
                    try
                    {
                        string[] files = Directory.GetFiles(Server.MapPath("~/UserImage/" + cdr["OrgId"].ToString()));
                        foreach (string file in files)
                        {
                            fileurl = "../UserImage/" + cdr["OrgId"].ToString() + "/" + Path.GetFileName(file);
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    sbTree.Append("<li><img src=" + fileurl + " height=" + "40px" + " width=" + "40px" + " /></br><a href='#' style='background-color: " + ColorText + "; color:White;' >" + cdr["orgName"].ToString() + "</a><ul>");

                    getMenuItems(Convert.ToInt32(cdr["orgId"].ToString()));
                    sbTree.Append("</ul></li>");
                }
                else
                {
                    try
                    {
                        string[] files = Directory.GetFiles(Server.MapPath("~/UserImage/" + cdr["OrgId"].ToString()));
                        foreach (string file in files)
                        {
                            fileurl = "../UserImage/" + cdr["OrgId"].ToString() + "/" + Path.GetFileName(file);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    sbTree.Append("<li><img src=" + fileurl + " height=" + "40px" + " width=" + "40px" + "/></br><a href='#' style='background-color: " + ColorText + "; color:White;' >" + cdr["orgName"].ToString() + "</a>");
                    //sbTree.Append("<li><a href='#' style='background-color: " + ColorText + "; color:White;' >" + cdr["orgName"].ToString() + "</a></li>");

                }
                ser++;
            }
            //sbMenu.Append("</ul>");
            return sbTree.ToString();
        }
        public void getColor(string apptnmentType, out string ColorText)
        {
            //string ColorText1 = "";
            switch (apptnmentType)
            {
                case "DG":
                    ColorText = "#e83636";
                    break;
                case "ADG":
                    ColorText = "#fc2f03";
                    break;
                case "DDG":
                    ColorText = "#b7110b";
                    break;
                case "DIR":
                    ColorText = "#18800e";
                    break;
                case "G1":
                    ColorText = "#0f9ad8";
                    break;
                case "G2":
                    ColorText = "#dfa606";
                    break;
                case "SO":
                    ColorText = "#fead0a";
                    break;
                default:
                    ColorText = "#aad4e7";
                    break;
            }
        }
    }
}