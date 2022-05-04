using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSCalc
{
    internal static class MyFunc
    {
        public static int getrow_int(DataTable dt, string WSName, string Val)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["Name"].ToString() == WSName)
                {
                    string tmpstr = row[Val].ToString();
                    return Int32.Parse(tmpstr);
                }
            }
            return -999;
        }

        public static double getrow_double(DataTable dt, string WSName, string Val)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["Name"].ToString() == WSName)
                {
                    string tmpstr = row[Val].ToString();
                    return Double.Parse(tmpstr);
                }
            }
            return -999;
        }
        public static string getrow_str(DataTable dt, string WSName, string Val)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["Name"].ToString() == WSName)
                {
                    return row[Val].ToString();
                }
            }
            return null;
        }
        public static bool getrow_bool(DataTable dt, string WSName, string Val)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["Name"].ToString() == WSName)
                {
                    return Convert.ToBoolean(row[Val]);
                }
            }
            return false;
        }
    }
}
