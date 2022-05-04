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

        internal static Random rnd = new Random();

        #region GetData
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
        #endregion

        /// <summary>
        /// Return magic resist and level of resist
        /// </summary>
        /// <param name="_macc"></param>
        /// <param name="_meva"></param>
        /// <returns></returns>
        public static double magicresist(double _macc, double _meva)
        {
            double dmacc = _macc - _meva;
            double mhr = 0.0;

            if (dmacc < 0)
            {
                mhr = 50 - (0.5 * (dmacc));
            }
            else
            {
                mhr = 50 + dmacc;
            }

            if (mhr < 5)
            {
                mhr = 5;
            }
            else if (mhr > 99)
            {
                mhr = 99;
            }
            mhr = mhr / 100;

            if (rnd.NextDouble() < mhr)
            {
                return 1.0;
            }

            if (rnd.NextDouble() < mhr)
            {
                return 0.5;
            }

            if (rnd.NextDouble() < mhr)
            {
                return 0.25;
            }

            if (rnd.NextDouble() < mhr)
            {
                return 0.125;
            }

            return 1.0;
        }

        #region amISomething
        /// <summary>
        /// Calculating if a hit landed
        /// </summary>
        /// <param name="_acc"></param>
        /// <param name="_eva"></param>
        /// <param name="htype"></param>
        /// <param name="_mob"></param>
        /// <returns></returns>
        public static bool amIhit(double _acc, double _eva, int htype, string _mob)
        {
            double hitrate = 0.0;
            //_acc = _acc + Convert.ToDouble(b_ftp.Value * 100);

            //Hit Rate (%) = 75 + floor( (Accuracy - Evasion)÷2 ) - 2×(dLVL)
            int dlvl = MyFunc.getrow_int(MyTables._mobset(), _mob, "Level") - 119;
            if (_acc - _eva == 0)
            {
                hitrate = 75.0;
            }
            else
            {
                hitrate = (75.0 + Math.Floor((_acc - _eva) / 2.0)) - (2.0 * dlvl);
            }


            if (htype == 0 && hitrate > 95.0)
            {
                hitrate = 95.0;
            }
            else if (hitrate > 99.0)
            {
                hitrate = 99.0;
            }
            else if (hitrate < 0.0 && (htype == 10 || htype == 3))
            {
                hitrate = 0.0;
            }
            else if (hitrate < 20.0)
            {
                hitrate = 20.0;
            }

            hitrate = Math.Floor(hitrate);
            
            if (rnd.NextDouble() < hitrate / 100)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check how many hits
        /// </summary>
        /// <returns></returns>
        public static int amImulti(double da, double ta, double qa, string mjob, string sjob)
        {
            if (mjob == "WAR")
            {
                da = da + 28;
            }
            else if (sjob == "WAR")
            {
                da = da + 10;
            }

            if (mjob == "THF")
            {
                ta = ta + 14;
            }

            if (rnd.NextDouble() < (qa / 100))
            {
                return 3;
            }
            else if (rnd.NextDouble() < (ta / 100))
            {
                return 2;
            }
            else if (rnd.NextDouble() < (da / 100))
            {
                return 1;
            }
            return 0;
        }

        public static int amImulti(bool _mytham3)
        {
            if (_mytham3 && rnd.NextDouble() < .2)
            {
                return 2;
            }
            else if (_mytham3 && rnd.NextDouble() < .4)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// Determine if critical
        /// TODO: collapse to 1 func
        /// </summary>
        /// <param name="form"></param>
        /// <param name="ja"></param>
        /// <param name="tp"></param>
        /// <returns></returns>
        public static bool amIcrit(wsd_form form, bool ja, int tp)
        {
            if (form.cb_MS.Checked)
            {
                return true;
            }

            double rate = Double.Parse(form.b_crate.Text);

            if (form.b_fencer.Checked)
            {
                if (form.cb_MJob.Text == "WAR")
                {
                    rate += 10;
                }
                else if (form.cb_MJob.Text == "BST")
                {
                    rate += 7;
                }
                else if (form.cb_MJob.Text == "BRD")
                {
                    rate += 5;
                }
            }

            if (form.cb_MJob.Text == "WAR")
            {
                rate += 10.0; //10 from 1200JP gift
            }
            else if (form.cb_building.Checked)
            {
                rate += 5.0;
            }

            if (!MyFunc.getrow_bool(MyTables._wsset(), form.combo_WS.Text, "Crits") && !form.cb_shining.Checked)
            {
                return false;
            }
            else if (MyFunc.getrow_bool(MyTables._wsset(), form.combo_WS.Text, "Crits"))
            {
                rate = rate + MyFunc.crate_calc(form.combo_WS.Text, tp);
            }

            switch (form.num_striking.Value)
            {
                case 2:
                    rate = rate + 25;
                    break;
                case 3:
                    rate = rate + 50;
                    break;
                case 4:
                    rate = rate + 55;
                    break;
                case 5:
                    rate = rate + 60;
                    break;
            }

            if (form.cb_shining.Checked && form.combo_wcats.SelectedText == "Polearm")
            {
                rate = rate + MyFunc.crate_calc(tp, 5, 5, 5);
                if (rnd.NextDouble() < (rate / 100))
                {
                    return true;
                }
            }

            if (rnd.NextDouble() < (rate / 100))
            {
                return true;

            }
            return false;
        }
        #endregion

        public static bool amIcrit(wsd_form form, int tp)
        {
            return amIcrit(form, false, tp);
        }

        /// <summary>
        /// Get the FTP value
        /// </summary>
        /// <param name="tp"></param>
        /// <param name="ws"></param>
        /// <param name="b_ftp"></param>
        /// <returns></returns>
        public static double tpcalc(int tp, string ws, decimal b_ftp)
        {
            if (tp > 3000)
            {
                tp = 3000;
            }
            double tps = 0;

            double anch_1 = MyFunc.getrow_double(MyTables._wsset(), ws, "FTP_2") - MyFunc.getrow_double(MyTables._wsset(), ws, "FTP_1");
            double anch_2 = MyFunc.getrow_double(MyTables._wsset(), ws, "FTP_3") - MyFunc.getrow_double(MyTables._wsset(), ws, "FTP_2");
            double fotia = 0.0;
            if (b_ftp == 1)
            {
                fotia = 0.09765625;
            }
            else if (b_ftp == 2)
            {
                fotia = 0.09765625 * 2;
            }

            if (tp == 3000)
            {
                tps = MyFunc.getrow_double(MyTables._wsset(), ws, "FTP_3");
            }
            else if (tp >= 2000)
            {
                if (tp == 2000)
                { tps = 1; }
                else
                { tps = tp - 2000; }

                tps = tps / 1000 * anch_2 + MyFunc.getrow_double(MyTables._wsset(), ws, "FTP_2");
            }
            else
            {
                if (tp == 1000)
                { tps = 1; }
                else
                { tps = tp - 1000; }

                tps = tps / 1000 * anch_1 + MyFunc.getrow_double(MyTables._wsset(), ws, "FTP_1");
            }

            tps = tps + fotia;
            return tps;
        }

        /// <summary>
        /// Calculate the crit rate
        /// </summary>
        /// <param name="wsname"></param>
        /// <param name="tp"></param>
        /// <returns></returns>
        public static double crate_calc(string wsname, int tp)
        {
            double anch_0 = MyFunc.getrow_double(MyTables._critset(), wsname, "C_1");
            double anch_1 = MyFunc.getrow_double(MyTables._critset(), wsname, "C_2") - MyFunc.getrow_double(MyTables._critset(), wsname, "C_1");
            double anch_2 = MyFunc.getrow_double(MyTables._critset(), wsname, "C_3") - MyFunc.getrow_double(MyTables._critset(), wsname, "C_2");

            return crate_calc(tp, anch_0, anch_1, anch_2);
        }

        public static double crate_calc(int tp, double anch_0, double anch_1, double anch_2)
        {
            if (tp > 3000)
            {
                tp = 3000;
            }

            double tcr = 0.0;

            if (tp == 3000)
            {
                tcr = anch_0 + anch_1 + anch_2;
            }
            else if (tp >= 2000)
            {
                if (tp == 2000)
                { tcr = 1; }
                else
                { tcr = tp - 2000; }

                tcr = tcr / 1000 * anch_2 + (anch_0 + anch_1);
            }
            else
            {
                if (tp == 1000)
                { tcr = 1; }
                else
                { tcr = tp - 1000; }

                tcr = tcr / 1000 * anch_1 + anch_0;
            }

            return Math.Floor(tcr);

        }



        /// <summary>
        /// Damage from Orpheus Belt
        /// </summary>
        /// <param name="_distance"></param>
        /// <returns></returns>
        public static double get_orph(double _distance)
        {
            double a0 = 15;
            double a2 = 1;

            double b0 = 19;

            double rate = a0 - ((_distance - b0) * Math.Abs(a2 - a0) / 100);
            if (rate < a2)
            { rate = a2; }
            else if (rate > a0)
            { rate = a0; }

            rate = Math.Floor(rate * 10) / 1000;

            return rate;
        }


        //ftp functions

        /// <summary>
        /// fint
        /// </summary>
        /// <param name="p"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int fint_calc(int p, int t, string wsname)
        {
            double f = 0.0;

            f = f + MyFunc.getrow_int(MyTables._wsset(), wsname, "dstatmod");
            if (wsname == "Primal Rend")
            {
                f = (p - t) * 1.5;
            }
            else
            {
                f = (p - t) * 2.0;
            }

            if (f > MyFunc.getrow_int(MyTables._wsset(), wsname, "dstatcap"))
            {
                f = MyFunc.getrow_int(MyTables._wsset(), wsname, "dstatcap");
            }
            else
            {
                return Convert.ToInt32(f);
            }

            return Convert.ToInt32(f);
        }


        /// <summary>
        /// fstr
        /// </summary>
        /// <param name="r"></param>
        /// <param name="s"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static int fstr_calc(int r, int s, int v)
        {
            int fstr = (s - v + 4) / 4;

            if (fstr > (r + 8))
            {
                fstr = (r + 8);
            }
            else if (r == 0 && fstr <= 0)
            {
                fstr = -1;
            }
            else if (fstr < (0 - r))
            {
                fstr = (0 - r);
            }

            return fstr;
        }

        /// <summary>
        /// fstr2
        /// </summary>
        /// <param name="r"></param>
        /// <param name="s"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static int fstr2_calc(int r, int s, int v)
        {
            int fstr2 = (s - v + 4) / 2;

            if (fstr2 > ((r * 2) + 14) * 2)
            {
                fstr2 = ((r * 2) + 14) * 2;
            }
            else if (r == 0 && fstr2 <= 0)
            {
                fstr2 = -1;
            }
            else if (fstr2 < (0 - (7 + (r * 2)) * 2))
            {
                fstr2 = (0 - (7 + (r * 2)) * 2);
            }

            return fstr2;
        }

        /// <summary>
        /// Calculate WSC
        /// </summary>
        /// <param name="form"></param>
        /// <param name="wsname"></param>
        /// <returns></returns>
        public static int wsc_calc(wsd_form form, string wsname)
        {
            string m1 = MyFunc.getrow_str(MyTables._wsset(), wsname, "MOD1");
            int m1n = MyFunc.getrow_int(MyTables._wsset(), wsname, "MOD1_n");
            string m2 = MyFunc.getrow_str(MyTables._wsset(), wsname, "MOD2");
            int m2n = MyFunc.getrow_int(MyTables._wsset(), wsname, "MOD2_n");
            string m3 = MyFunc.getrow_str(MyTables._wsset(), wsname, "MOD3");
            int m3n = MyFunc.getrow_int(MyTables._wsset(), wsname, "MOD3_n");
            string wtl = MyFunc.getrow_str(MyTables._wsset(), wsname, "catagory");

            //Utu grip stuff
            if (form.cb_Utu.Checked && (wtl == "Great Sword" || wtl == "Great Katana" || wtl == "Staff" || wtl == "Great Axe" || wtl == "Scythe" || wtl == "Polearm"))
            {
                if (m1 == "DEX")
                {
                    m1n = m1n + 10;
                }
                else if (m2 == "DEX")
                {
                    m2n = m2n + 10;
                }
                else if (m3 == "DEX")
                {
                    m3n = m3n + 10;
                }
                else if (m2 == "")
                {
                    m2 = "DEX";
                    m2n = 10;
                }
                else if (m3 == "")
                {
                    m3 = "DEX";
                    m3n = 10;
                }
            }

            //Crepescular Knife
            if (form.cb_Crepes.Checked)
            {
                if (m1 == "CHR")
                {
                    m1n = m1n + 3;
                }
                else if (m2 == "CHR")
                {
                    m2n = m2n + 3;
                }
                else if (m3 == "CHR")
                {
                    m3n = m3n + 3;
                }
                else if (m2 == "")
                {
                    m2 = "CHR";
                    m2n = 3;
                }
                else if (m3 == "")
                {
                    m3 = "CHR";
                    m3n = 3;
                }
            }

            double s1n = statval(form, m1);
            double z1 = (s1n * (m1n / 100.0));

            double wsc = z1;

            if (m2 != "")
            {
                double s2n = statval(form, m2);

                double z2 = (s2n * (m2n / 100.0));

                wsc = (wsc + z2);
            }

            if (m3 != "")
            {
                double s3n = statval(form, m3);

                double z3 = (s3n * (m3n / 100.0));

                wsc = (wsc + z3);
            }

            return Convert.ToInt32((wsc) / 0.85);
        }

        public static int wd_calc(wsd_form form, string wsname, int fstr)
        {
            int wsc = wsc_calc(form, wsname);
            return (fstr + wsc);
        }

        /// <summary>
        /// Get the value of a stat from the form
        /// </summary>
        /// <param name="form"></param>
        /// <param name="stat"></param>
        /// <returns></returns>
        public static int statval(wsd_form form, string stat)
        {
            switch (stat)
            {
                case "STR":
                    return Int32.Parse(form.b_STR.Text);
                case "DEX":
                    return Int32.Parse(form.b_DEX.Text);
                case "AGI":
                    return Int32.Parse(form.b_AGI.Text);
                case "INT":
                    return Int32.Parse(form.b_INT.Text);
                case "MND":
                    return Int32.Parse(form.b_MND.Text);
                case "VIT":
                    return Int32.Parse(form.b_VIT.Text);
                case "CHR":
                    return Int32.Parse(form.b_CHR.Text);
                default:
                    return 0;

            }
        }
    }
}
