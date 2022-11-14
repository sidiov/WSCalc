// <one line to give the program's name and a brief idea of what it does.>
// Copyright (C) 2022  Sidiov
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WSCalc
{
    public partial class wsd_form : Form
    {
        //TODO : recasting these datatables for filtering until i learn how to remote filter
        DataTable dt_p = new DataTable();
        DataTable dt_j = new DataTable();

        Random rnd1 = new Random();

        List<string> wstype = new List<string>();

        int _iterations = 100;

        public wsd_form()
        {
            InitializeComponent();
            MessageBox.Show("Warning!\n-Skill is currently estimated at 242+cap\n" +
                "-Some Job Traits may not be applied.\n" +
                "-WS 'Acc varies with TP' not implemented.\n" +
                "-Some combos not checked for validity (ie. Jobs use any WS)\n\t" +
                "\nMob stats may not be valid, many have not been recorded.");

            dt_p = MyTables._wsset();
            dt_j = MyTables._jobs();

            cb_Obi.SelectedItem = "0";
            cb_mob_SDT.SelectedItem = "100";
            populateMobBox();
            populateWpnTypeBox();
            populateJobsBox();
        }

        private void b_Calc_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {

                label_finaldamage.Text = Convert.ToString(wsdmgcalc(1000));
                label_finaldamage2k.Text = Convert.ToString(wsdmgcalc(2000));
                label_finaldamage3k.Text = Convert.ToString(wsdmgcalc(3000));

                label_avg_damage.Text = Convert.ToString(calcaveragedmg(1000));
                label_avg_damage2k.Text = Convert.ToString(calcaveragedmg(2000));
                label_avg_damage3k.Text = Convert.ToString(calcaveragedmg(3000));
            }
        }

        #region calcs
        private int calcaveragedmg(int tp)
        {
            int d = 0;
            for (int i = 1; i < _iterations; i++)
            {
                d = d + wsdmgcalc(tp);
            }
            return d / _iterations;
        }
        private int wsdmgcalc(int tp)
        {
            tp = tp + Int32.Parse(b_TP.Text);
            if (b_fencer.Checked)
            {
                if (cb_MJob.Text == "WAR")
                {
                    tp = tp + 500;
                }
                else if (cb_MJob.Text == "BST")
                {
                    tp = tp + 400;
                }
                else if (cb_MJob.Text == "BRD")
                {
                    tp = tp + 300;
                }
            }

            int _type = MyFunc.getrow_int(dt_p, combo_WS.Text, "Type");
            if (_type == 0)
            {
                return ws_p_dmgcalc(tp, _type);
            }
            else if (_type == 1)
            {
                return ws_m_dmgcalc("INT", tp, _type);
            }
            else if (_type == 2)
            {
                return ws_h_damagecalc(tp, _type);
            }
            else if (_type == 3)
            {
                return ws_r_damagecalc(tp, _type);
            }
            else if (_type == 4)
            {
                return ws_m_dmgcalc("AGI", tp, _type);
            }
            else if (_type == 5)
            {
                return ws_p_dmgcalc(tp, _type);
            }
            else if (_type == 6)
            {
                return ws_m_dmgcalc("MND", tp, _type);
            }
            else if (_type == 7)
            {
                return ws_m_dmgcalc("CHR", tp, _type);
            }
            else if (_type == 8)
            {
                return ws_p_dmgcalc(tp, _type);
            }
            else if (_type == 9)
            {
                return ws_h_damagecalc(tp, _type);
            }
            else if (_type == 10)
            {
                return ws_h_damagecalc(tp, _type);
            }
            else if (_type == 98)
            {
                return ws_p_dmgcalc(tp, _type);
            }

            return 0;

        }

        private int ws_m_dmgcalc(string stat, int tp, int htype)
        {
            int fint = 0;
            int pstat = 0;
            int mdm = Convert.ToInt32(b_mdmg.Text);

            if (combo_WS.Text == "Infernal Scythe" || combo_WS.Text == "Cloudsplitter" || combo_WS.Text == "Herculean Slash"
                || combo_WS.Text == "Seraph Strike" || combo_WS.Text == "Seraph Strike")
            {
                fint = 0;
            }
            else if (combo_WS.Text == "Sunburst")
            {
                fint = MyFunc.fint_calc(statval("MND"), (Int32.Parse(b_target_MND.Text)), combo_WS.Text);
                pstat = (Int32.Parse(b_MND.Text));
            }
            else if (stat == "INT")
            {
                fint = MyFunc.fint_calc(statval("INT"), (Int32.Parse(b_target_INT.Text)), combo_WS.Text);
                pstat = (Int32.Parse(b_INT.Text));
            }
            else if (stat == "AGI")
            {
                fint = MyFunc.fint_calc(statval("AGI"), (Int32.Parse(b_target_INT.Text)), combo_WS.Text);
                pstat = (Int32.Parse(b_AGI.Text));
            }
            else if (stat == "MND")
            {
                fint = MyFunc.fint_calc(statval("MND"), (Int32.Parse(b_target_MND.Text)), combo_WS.Text);
                pstat = (Int32.Parse(b_MND.Text));
            }
            else if (stat == "CHR")
            {
                fint = MyFunc.fint_calc(statval("CHR"), (Int32.Parse(b_target_INT.Text)), combo_WS.Text);
                pstat = (Int32.Parse(b_MND.Text));
            }

            double drgwsd = 0.0;
            if (cb_MJob.Text == "DRG")
            { drgwsd = 0.21; }
            else if (cb_SJob.Text == "DRG" && num_ML.Value >= 5)
            { drgwsd = 0.10; }
            else if (cb_SJob.Text == "DRG")
            { drgwsd = 0.07; }

            //TP Bonuses
            int fence = MyFunc.getrow_int(dt_j, cb_MJob.Text, "Fencer");
            if (fence > 0 && b_DMG2.Text == "0" && combo_wcats.Text != "Great Axe" && combo_wcats.Text != "Great Sword" && combo_wcats.Text != "Great Katana" && combo_wcats.Text != "Polearm" && combo_wcats.Text != "Scythe" && combo_wcats.Text != "Staff" && combo_wcats.Text != "Hand-to-Hand")
            {
                switch (fence)
                {
                    case 1:
                        tp += 200;
                        break;
                    case 2:
                        tp += 300;
                        break;
                    case 3:
                        tp += 400;
                        break;
                    case 4:
                        tp += 450;
                        break;
                    case 5:
                        tp += 500;
                        break;
                    case 6:
                        tp += 550;
                        break;
                    case 7:
                        tp += 600;
                        break;
                    case 8:
                        tp += 630;
                        break;
                }
                if (cb_MJob.Text == "WAR" || cb_MJob.Text == "BST")
                {
                    tp += 230;
                }
            }

            double tpsi = MyFunc.tpcalc(tp, combo_WS.Text, b_ftp.Value);

            int wsc = MyFunc.wsc_calc(this, combo_WS.Text);
            int lvlc = Convert.ToInt32(Math.Floor(2.45 * (119 - 99)));
            double mabr = (Double.Parse(b_MAB.Text) + 100) / (Double.Parse(b_target_MDB.Text) + 100);
            double wdb = Double.Parse(cb_Obi.Text) / 100;

            double wsd = Double.Parse(b_WSD.Text) / 100;
            if (MyFunc.getrow_int(dt_j, cb_MJob.Text, "WSD") > 0)
            {
                double wxx = MyFunc.getrow_int(dt_j, cb_MJob.Text, "WSD");
                wsd = wsd + (wxx / 100);
            }

            double sdt = Double.Parse(cb_mob_SDT.Text) / 100;

            double aff = Double.Parse(b_Affi.Text) / 100;
            if (cb_orph.Checked)
            { aff = aff + MyFunc.get_orph(track_orph.Value); }

            double rema = 0.0;
            if (cb_myth.Checked && MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") == 1)
            {
                rema = .3;
            }
            else if (cb_relic.Checked && MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") == 2)
            {
                rema = .4;
            }

            double ranks = Double.Parse(b_Ranks.Text) / 100;

            double macc = Double.Parse(b_macc.Text);
            if (htype == 4)
            {
                macc = macc + Double.Parse(tb_RAMaccSkill.Text);
            }
            else
            {
                macc = macc + Double.Parse(tb_maccSkill.Text);
            }

            double mresist = MyFunc.magicresist(macc, Double.Parse(b_target_meva.Text));

            double mdamage = Math.Floor((tpsi * (152 + lvlc + wsc) + fint) + mdm);

            mdamage = Math.Floor(mdamage * (1 + mabr));
            mdamage = Math.Floor(mdamage * sdt);
            mdamage = Math.Floor(mdamage * mresist);
            mdamage = Math.Floor(mdamage * (1 + wsd));
            mdamage = Math.Floor(mdamage * (1 + aff));
            mdamage = Math.Floor(mdamage * (1 + wdb));
            mdamage = Math.Floor(mdamage * (1 + rema));
            mdamage = Math.Floor(mdamage * (1 + ranks));
            mdamage = Math.Floor(mdamage * (1 + drgwsd));

            if (mdamage > 99999)
            {
                mdamage = 99999;
            }

            return Convert.ToInt32(mdamage);
        }

        private int ws_p_dmgcalc(int tp, int htype)
        {
            //Damage = WD * PDIF = ( D + fSTR + WSC) * fTP * PDIF
            int rank = 0;


            int fstr = MyFunc.fstr_calc(rank, statval("STR"), (Int32.Parse(b_target_VIT.Text)));

            /*if (combo_WS.Text == "Torcleaver" || combo_WS.Text == "Camlann's Torment")
            {
                fstr = MyFunc.fstr_calc(rank, statval("STR")*3, (Int32.Parse(b_target_VIT.Text)));
            }
            else if (combo_WS.Text == "Tachi: Fudo")
            {
                fstr = MyFunc.fstr_calc(rank, statval("STR")*2, (Int32.Parse(b_target_VIT.Text)));
            }
            else if (combo_WS.Text == "Blade: Hi")
            {
                fstr = MyFunc.fstr_calc(rank, statval("STR")*5, (Int32.Parse(b_target_VIT.Text)));
            }
            else
            {
                fstr = MyFunc.fstr_calc(rank, statval("STR"), (Int32.Parse(b_target_VIT.Text)));
            }*/

            int wd = MyFunc.wd_calc(this, combo_WS.Text, fstr);

            //work in weapon skill?
            //double acc = Double.Parse(b_acc.Text) + Math.Floor((Double.Parse(b_DEX.Text) * 0.75)) + 420;
            double acc = Double.Parse(b_acc.Text);
            double eva = Double.Parse(b_target_eva.Text);

            if (cb_building.Checked)
            {
                acc = Math.Floor(acc * 1.25);
            }

            double pdif = 0;

            int fence = MyFunc.getrow_int(dt_j, cb_MJob.Text, "Fencer");
            if (fence > 0 && b_DMG2.Text == "0" && combo_wcats.Text != "Great Axe" && combo_wcats.Text != "Great Sword" && combo_wcats.Text != "Great Katana" && combo_wcats.Text != "Polearm" && combo_wcats.Text != "Scythe" && combo_wcats.Text != "Staff" && combo_wcats.Text != "Hand-to-Hand")
            {
                switch (fence)
                {
                    case 1:
                        tp += 200;
                        break;
                    case 2:
                        tp += 300;
                        break;
                    case 3:
                        tp += 400;
                        break;
                    case 4:
                        tp += 450;
                        break;
                    case 5:
                        tp += 500;
                        break;
                    case 6:
                        tp += 550;
                        break;
                    case 7:
                        tp += 600;
                        break;
                    case 8:
                        tp += 630;
                        break;
                }
                if (cb_MJob.Text == "WAR" || cb_MJob.Text == "BST")
                {
                    tp += 230;
                }
            }

            double ftp = MyFunc.tpcalc(tp, combo_WS.Text, b_ftp.Value);
            double cdmg = Double.Parse(b_cdmg.Text); // divide by 100 later
            double ambu = Double.Parse(b_Ambu.Text) / 100;
            double ranks = Double.Parse(b_Ranks.Text) / 100;
            double wsd = Double.Parse(b_WSD.Text) / 100;

            if (cb_building.Checked)
            {
                wsd += 0.2;
            }


            double drgwsd = 0.0;
            if (cb_MJob.Text == "DRG")
            { drgwsd = 0.21; }
            else if (cb_SJob.Text == "DRG" && num_ML.Value >= 5)
            { drgwsd = 0.10; }
            else if (cb_SJob.Text == "DRG")
            { drgwsd = 0.07; }

            double rema = 0.0;
            if (cb_myth.Checked && MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") == 1)
            {
                rema = .3;
            }
            else if (cb_relic.Checked && MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") == 2)
            {
                rema = .4;
            }

            double damage = 0;

            int hits = MyFunc.getrow_int(dt_p, combo_WS.Text, "Hits");

            hits = hits + MyFunc.amImulti((Convert.ToDouble(b_da.Text)), (Convert.ToDouble(b_ta.Text)), (Convert.ToDouble(b_qa.Text)), cb_MJob.Text, cb_SJob.Text, Convert.ToInt32(num_ML.Value));
            if (hits > 8)
            {
                hits = 8;
            }
            else if (hits < 2 && num_striking.Value > 0)
            {
                hits = hits + 1;

            }
            else if (hits < 2 && cb_tern.Checked)
            {
                hits = hits + 2;
            }

            int mytham3 = 0;
            if (hits < 8 && cb_mytham3.Checked && MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") == 1)
            {
                hits = hits + MyFunc.amImulti(true);
                if (hits > 8)
                {
                    hits = 8;
                }
                mytham3 = 1;
            }

            if (htype == 98)
            {
                hits = 1;
            }

            //Crit Bonus
            double cbonus = MyFunc.getrow_double(dt_j, cb_MJob.Text, "CritBonus");
            if (cbonus > 0)
            {
                cdmg = cdmg + cbonus;
            }
            cdmg = cdmg / 100;


            //--Climactic Flourish
            int climax = 0;
            if (num_clim.Value > 0)
            {
                climax = Convert.ToInt32(num_clim.Value);
            }

            double climbonus = 0.0;

            switch (climax)
            {
                case 0:
                case 1:
                    break;
                case 2:
                    climbonus = 1.1;
                    break;
                case 3:
                    climbonus = 1.2;
                    break;
                case 4:
                    climbonus = 1.22;
                    break;
                case 5:
                    climbonus = 1.25;
                    break;
            }


            //Hit calculations
            for (int i = 1; i <= hits; i++)
            {
                bool _crit = false;
                double t_acc = acc;

                if (i == 1)
                {
                    t_acc = t_acc + 100;
                    if (b_ftp.Value == 1)
                    {
                        t_acc = t_acc + 100;
                    }
                    else if (b_ftp.Value == 2)
                    {
                        t_acc = t_acc + 100;
                    }
                }

                if (!MyFunc.amIhit(acc, eva, htype, cb_Mobs.Text) && !cb_SATA.Checked)
                {
                    continue;
                }

                if ((i == 1 || i == 2) && num_striking.Value > 0)
                {
                    _crit = MyFunc.amIcrit(this, true, tp);
                }
                else
                {
                    _crit = MyFunc.amIcrit(this, tp);
                }

                //Calculate Weapon Damage and Rank
                double tempwd = wd;

                if (combo_WS.Text == "Torcleaver" || combo_WS.Text == "Camlann's Torment")
                {
                    tempwd = tempwd + (3 * (Int32.Parse(b_DMG1.Text)));
                }
                else if (combo_WS.Text == "Tachi: Fudo")
                {
                    tempwd = tempwd + (2 * (Int32.Parse(b_DMG1.Text)));
                }
                else if (combo_WS.Text == "Blade: Hi")
                {
                    tempwd = tempwd + (5 * (Int32.Parse(b_DMG1.Text)));
                }
                else if ((combo_WS.Text == "Dragon Kick" || combo_WS.Text == "Tornado Kick") && cb_footw.Checked)
                {
                    tempwd = tempwd + 20 + Convert.ToInt32(tb_KAD.Text);
                }
                else
                {
                    tempwd = tempwd + Int32.Parse(b_DMG1.Text);
                }


                if (htype == 8)
                {
                    rank = ((Int32.Parse(b_DMG1.Text) + 3) / 9);
                }
                else
                {
                    rank = (Int32.Parse(b_DMG1.Text) / 9);
                }


                if (i == 1)
                {
                    if (cb_SATA.Checked)
                    {
                        _crit = true;
                    }
                    if (climax > 0)
                    {
                        _crit = true;
                        cdmg = cdmg * climbonus;
                        tempwd = tempwd + (Convert.ToInt32(b_CHR.Text) * .5);
                        climax = climax - 1;
                    }

                    pdif = pdif_calc(combo_WS.Text, _crit, tp, htype);

                    //final damage calcs
                    damage = damage + Math.Floor(tempwd * ftp);

                    if (cb_SATA.Checked && cb_MJob.Text == "THF")
                    {
                        damage = damage + Convert.ToInt32(b_DEX.Text);
                    }
                    if (num_striking.Value > 0)
                    {
                        damage = damage + Convert.ToInt32(b_CHR.Text);
                    }
                    else if (cb_tern.Checked)
                    {
                        damage = damage + Convert.ToInt32(b_CHR.Text);
                    }

                    damage = Math.Floor(damage * pdif);
                    damage = Math.Floor(damage * (1 + wsd));
                }
                else if (MyFunc.getrow_bool(dt_p, combo_WS.Text, "Replicate"))
                {
                    if (i == 2 && climax > 0)
                    {
                        _crit = true;
                        cdmg = cdmg * climbonus;
                        climax = 0;
                    }
                    pdif = pdif_calc(combo_WS.Text, _crit, tp, htype);
                    damage = damage + (tempwd * ftp * pdif);

                }
                else
                {
                    if (i == 2 && climax > 0)
                    {
                        _crit = true;
                        cdmg = cdmg * climbonus;
                        climax = 0;
                    }
                    pdif = pdif_calc(combo_WS.Text, _crit, tp, htype);
                    damage = damage + (tempwd * pdif);
                }

                //bonuses
                if (_crit)
                {
                    damage = Math.Floor(damage * (1 + cdmg));
                }
                damage = Math.Floor(damage * (1 + drgwsd));
            }
            if (htype == 5 && Convert.ToInt32(b_DMG2.Text) > 0 && hits < 8)
            {
                int offhits = 1;
                if (offhits + hits < 8)
                {
                    offhits = 1 + MyFunc.amImulti((Convert.ToDouble(b_da.Text)), (Convert.ToDouble(b_ta.Text)), (Convert.ToDouble(b_qa.Text)), cb_MJob.Text, cb_SJob.Text, Convert.ToInt32(num_ML.Value));
                    if (offhits + hits > 8)
                    {
                        offhits = 8 - hits;
                    }
                }
                else if (offhits + hits < 8 && cb_mytham3.Checked && mytham3 == 0 && MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") == 1)
                {
                    offhits = offhits + MyFunc.amImulti(true);
                    if (hits + offhits > 8)
                    {
                        offhits = 8 - hits;
                    }
                    mytham3 = 1;
                }

                int rank2 = (Int32.Parse(b_DMG2.Text) / 9);
                int fstroff = MyFunc.fstr_calc(rank, statval("STR"), (Int32.Parse(b_target_VIT.Text)));
                int wdoff = MyFunc.wd_calc(this, combo_WS.Text, fstroff);
                double tempwd = wdoff + Int32.Parse(b_DMG2.Text);

                for (int i = 1; i <= offhits; i++)
                {
                    bool _crit = false;

                    if (!MyFunc.amIhit(acc, eva, 0, cb_Mobs.Text))
                    {
                        continue;
                    }

                    if (cb_SATA.Checked && i == 1)
                    {
                        _crit = true;
                    }
                    else
                    {
                        _crit = MyFunc.amIcrit(this, tp);
                    }

                    if (i == 1 && climax > 0)
                    {
                        _crit = true;
                        cdmg = cdmg * climbonus;
                        climax = 0;
                    }

                    pdif = pdif_calc(combo_WS.Text, _crit, tp, htype);

                    if (MyFunc.getrow_bool(dt_p, combo_WS.Text, "Replicate"))
                    {
                        damage = damage + (tempwd * ftp * pdif);
                    }
                    else
                    {
                        damage = damage + (tempwd * pdif);
                    }

                    //bonuses
                    if (_crit)
                    {
                        damage = Math.Floor(damage * (1 + cdmg));
                    }

                    damage = Math.Floor(damage * (1 + drgwsd));
                }

            }
            if (MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") == 9)
            {
                damage = Math.Floor(damage * (1 + ambu));
            }

            damage = Math.Floor(damage * (1 + rema));

            if (MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") < 9 && MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") > 0)
            {
                damage = Math.Floor(damage * (1 + ranks));
            }

            if (damage > 99999)
            {
                damage = 99999;
            }

            return Convert.ToInt32(damage);

        }

        private int ws_r_damagecalc(int tp, int htype)
        {
            // floor(( Weapon Base Damage + (Ammo Damage) + fSTR(2) + WSC ) × fTP)
            int rank = (Int32.Parse(b_DMGr.Text) / 9);

            //fstr2 stuff
            int fstr2;
            if (combo_WS.Text == "Numbing Shot")
            {
                fstr2 = MyFunc.fstr2_calc(rank, statval("STR") * 3, (Int32.Parse(b_target_VIT.Text)));
            }
            else if (combo_WS.Text == "Slug Shot")
            {
                fstr2 = MyFunc.fstr2_calc(rank, statval("STR") * 4, (Int32.Parse(b_target_VIT.Text)));
            }
            else
            {
                fstr2 = MyFunc.fstr2_calc(rank, statval("STR"), (Int32.Parse(b_target_VIT.Text)));
            }

            int wd = MyFunc.wd_calc(this, combo_WS.Text, fstr2);
            double pdif = 0;

            //TP Stuff

            int fence = MyFunc.getrow_int(dt_j, cb_MJob.Text, "Fencer");
            if (fence > 0 && b_DMG2.Text == "0" && combo_wcats.Text != "Great Axe" && combo_wcats.Text != "Great Sword" && combo_wcats.Text != "Great Katana" && combo_wcats.Text != "Polearm" && combo_wcats.Text != "Scythe" && combo_wcats.Text != "Staff" && combo_wcats.Text != "Hand-to-Hand")
            {
                switch (fence)
                {
                    case 1:
                        tp += 200;
                        break;
                    case 2:
                        tp += 300;
                        break;
                    case 3:
                        tp += 400;
                        break;
                    case 4:
                        tp += 450;
                        break;
                    case 5:
                        tp += 500;
                        break;
                    case 6:
                        tp += 550;
                        break;
                    case 7:
                        tp += 600;
                        break;
                    case 8:
                        tp += 630;
                        break;
                }
                if (cb_MJob.Text == "WAR" || cb_MJob.Text == "BST")
                {
                    tp += 230;
                }
            }

            double ftp = MyFunc.tpcalc(tp, combo_WS.Text, b_ftp.Value);
            double wsd = Double.Parse(b_WSD.Text) / 100;
            double cdmg = Double.Parse(b_cdmg.Text) / 100;
            double ambu = Double.Parse(b_Ambu.Text) / 100;
            double ranks = Double.Parse(b_Ranks.Text) / 100;


            double drgwsd = 0.0;
            if (cb_MJob.Text == "DRG")
            { drgwsd = 0.21; }
            else if (cb_SJob.Text == "DRG" && num_ML.Value >= 5)
            { drgwsd = 0.10; }
            else if (cb_SJob.Text == "DRG")
            { drgwsd = 0.07; }

            double rema = 0.0;
            if (cb_myth.Checked && MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") == 1)
            {
                rema = .3;
            }
            else if (cb_relic.Checked && MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") == 2)
            {
                rema = .4;
            }

            //work in weapon skill?
            double acc = Double.Parse(b_racc.Text) + Math.Floor((Double.Parse(b_AGI.Text) * 0.75)) + 400;
            double eva = Double.Parse(b_target_eva.Text);


            double damage = 0;

            int hits = MyFunc.getrow_int(dt_p, combo_WS.Text, "Hits");
            hits = hits + MyFunc.amImulti((Convert.ToDouble(b_da.Text)), (Convert.ToDouble(b_ta.Text)), (Convert.ToDouble(b_qa.Text)), cb_MJob.Text, cb_SJob.Text, Convert.ToInt32(num_ML.Value));
            if (hits > 8)
            {
                hits = 8;
            }

            if (htype == 10)
            {
                hits = 1;
            }

            for (int i = 1; i <= hits; i++)
            {
                if (!MyFunc.amIhit(acc, eva, htype, cb_Mobs.Text))
                {
                    continue;
                }

                bool _crit = MyFunc.amIcrit(this, tp);

                double tempwd = wd;
                tempwd = tempwd + Int32.Parse(b_DMGr.Text) + Int32.Parse(b_DMGa.Text);

                if (i == 1)
                {
                    pdif = pdif_r(combo_WS.Text, _crit, tp);
                    damage = damage + Math.Floor(tempwd * ftp * pdif * (1 + wsd));
                }
                else if (MyFunc.getrow_bool(dt_p, combo_WS.Text, "Replicate"))
                {
                    pdif = pdif_r(combo_WS.Text, _crit, tp);
                    damage = damage + (tempwd * ftp * pdif);
                }
                else
                {
                    pdif = pdif_r(combo_WS.Text, _crit, tp);
                    damage = damage + (tempwd * pdif);
                }

                //bonuses
                if (_crit)
                {
                    damage = Math.Floor(damage * (1 + cdmg));
                }

                damage = Math.Floor(damage * (1 + drgwsd));
            }

            if (MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") == 9)
            {
                damage = Math.Floor(damage * (1 + ambu));
            }

            damage = Math.Floor(damage * (1 + rema));

            if (MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") < 9 && MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") > 0)
            {
                damage = Math.Floor(damage * (1 + ranks));
            }

            if (damage > 99999)
            {
                damage = 99999;
            }

            return Convert.ToInt32(damage);
        }

        private int ws_h_damagecalc(int tp, int htype)
        {
            //Damage = (Base Damage × pDIF) + (Base Damage × pDIF × modifiers of magic damage)

            double mabr = (Double.Parse(b_MAB.Text) + 100) / (Double.Parse(b_target_MDB.Text) + 100);
            double aff = Double.Parse(b_Affi.Text) / 100;
            double wdb = Double.Parse(cb_Obi.Text) / 100;
            double wsd = Double.Parse(b_WSD.Text) / 100;
            double ranks = Double.Parse(b_Ranks.Text) / 100;
            
            if (cb_orph.Checked)
            { aff = aff + MyFunc.get_orph(track_orph.Value); }

            double drgwsd = 0.0;
            if (cb_MJob.Text == "DRG")
            { drgwsd = 0.21; }
            else if (cb_SJob.Text == "DRG")
            { drgwsd = 0.07; }

            double rema = 0.0;
            if (cb_myth.Checked && MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") == 1)
            {
                rema = .3;
            }
            else if (cb_relic.Checked && MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") == 2)
            {
                rema = .4;
            }
            double damage = 0.0;
            if (htype == 10)
            {
                damage = ws_r_damagecalc(tp, htype);
            }
            else
            {
                damage = ws_p_dmgcalc(tp, 98);
            }

            if (damage > 99999)
            { damage = 99999; }

            double mdamage = Math.Floor(Math.Floor(Math.Floor(Math.Floor(Math.Floor(damage * (1 + mabr)) * (1 + wsd)) * (1 + aff)) * (1 + wdb)) * (1 + rema));

            if (MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") < 9 && MyFunc.getrow_int(dt_p, combo_WS.Text, "Mythic") > 0)
            {
                damage = Math.Floor(damage * (1 + ranks));
            }
            mdamage = Math.Floor(mdamage * (1 + drgwsd));

            if (mdamage > 99999)
            { mdamage = 99999; }

            double overall_damage = damage + mdamage;

            return Convert.ToInt32(overall_damage);
        }


        private double pdif_r(string wsname, bool _iscrit, int tp)
        {
            double tratt = Double.Parse(b_ratt.Text);
            double trdef = Double.Parse(b_target_def.Text);

            if (wsname == "Apex Arrow")
            {
                double ddown = 0.0;
                double a0 = 15.0;
                double a2 = 75.0;
                double m = (a0 - a2) / (1000 - 3000);

                double tps = tp - 1000;

                if (tps == 0)
                {
                    ddown = a0;
                }
                else //y = mx + b
                {
                    ddown = m * tps + a0;
                }

                double rem = Math.Floor(trdef * (ddown / 100));

                trdef = trdef - rem;
            }
            else if (wsname == "Empyreal Arrow")
            {
                tratt = tratt * 2.0;
            }

            double ratt_ratio = tratt / trdef;

            if (ratt_ratio > MyFunc.getrow_double(dt_p, wsname, "aRatio"))
            {
                ratt_ratio = MyFunc.getrow_double(dt_p, wsname, "aRatio");
            }

            //add level correction
            double c_ratio = ratt_ratio;
            double q_ratio = MyFunc.getrow_double(dt_p, wsname, "QRatio");

            double pdl = MyFunc.getrow_double(dt_j, cb_MJob.Text, "PDL");
            double pdls = MyFunc.getrow_double(dt_j, cb_SJob.Text, "PDL_s");

            if (cb_SJob.Text == "DRK" && num_ML.Value >= 5)
            {
                pdls = 0.3;
            }
            else if ((cb_SJob.Text == "THF" || cb_SJob.Text == "NIN") && num_ML.Value >= 5)
            {
                pdls = 0.1;
            }

            if (pdl != 0 || pdls != 0 || Convert.ToInt32(b_pdif.Text) != 0)
            {
                if (pdl != 0)
                {
                    q_ratio = q_ratio + pdl + ((Convert.ToDouble(b_pdif.Text) / 10));
                }
                else
                {
                    q_ratio = q_ratio + pdls + ((Convert.ToDouble(b_pdif.Text) / 10));
                }

            }

            double ul = 0.0;
            double ll = 0.0;

            //UL
            if (c_ratio >= 0 && c_ratio < 0.9)
            {
                ul = c_ratio * (10 / 9);
                if (ul > 1)
                { ul = 1; }
            }
            else if (c_ratio >= 0.9 && c_ratio < 1.1)
            {
                ul = 1;
            }
            else if (c_ratio >= 1.1)
            {
                ul = c_ratio;

                if (ul < q_ratio)
                { ul = q_ratio; }
            }


            //LL
            if (c_ratio >= 0 && c_ratio < 0.9)
            {
                ll = c_ratio;
                if (ll > 0.9)
                { ll = 0.9; }
            }
            else if (c_ratio >= 0.9 && c_ratio < 1.1)
            {
                ll = 1;
            }
            else if (c_ratio >= 1.1)
            {
                ll = c_ratio * (20 / 19) - (3 / 19);

                if (ll < q_ratio)
                { ll = q_ratio; }
            }

            double r_ratio = rnd1.NextDouble() * (ul - ll) + ll;


            if (r_ratio > q_ratio)
            {
                r_ratio = q_ratio;
            }
            else if (r_ratio < 0)
            {
                r_ratio = 0;
            }

            double rpdif = r_ratio * ((rnd1.NextDouble() * (1.05 - 1.0)) + 1.0);

            if (_iscrit)
            {
                rpdif = rpdif * 1.25;
            }

            //Add distance correction, True Shot...

            return rpdif;
        }

        private double pdif_calc(string wsname, bool _iscrit, int tp, int htype)
        {
            double tatk = Double.Parse(b_ATTK.Text);
            double atkpenalty = 0.0;
            //WS & Defense bonus/penalties
            double ttdef = Double.Parse(b_target_def.Text);
            //defdown vars
            double ddown, a0, a2, m, tps, rem = 0.0;


            //Smite
            if (htype == 0 || htype == 8 || htype == 9)
            {
                double smite = MyFunc.getrow_double(dt_j, cb_MJob.Text, "Smite");
                if (cb_SJob.Text == "DRK" && num_ML.Value >= 5 )
                {
                    smite = 19.9;
                }
                if (smite == 0)
                {
                    smite = MyFunc.getrow_double(dt_j, cb_SJob.Text, "Smite_s");
                }
                smite = smite / 100;
                smite++;

                tatk = Math.Floor(tatk * smite);
            }
            if (cb_building.Checked)
            {
                tatk = Math.Floor(tatk * 1.25);
            }
            

            switch (wsname)
            {
                case "Tachi: Shoha":
                case "Blade: Kamu":
                    tatk = tatk * 1.35;
                    break;
                case "Howling Fist":
                case "Retribution":
                    tatk = tatk * 1.5;
                    break;
                case "Dragon Kick":
                case "Tornado Kick":
                    tatk = tatk * 1.0976;
                    break;
                case "Shijin Spiral":
                    tatk = tatk * 1.05;
                    break;
                case "Ascetic's Fury":
                case "Viper Bite":
                    tatk = tatk * 2.0;
                    break;
                case "Mandalic Stab":
                    tatk = tatk * 1.75;
                    break;
                case "Drakesbane":
                    tatk = tatk * 0.8125;
                    break;
                case "Blade: Shun":
                    tatk = tatk * (0 + (tp / 1000));
                    break;
                case "Requiescat":
                    if (tp > 1000 && tp < 2999)
                    {
                        atkpenalty = tatk / (0.0 - ((tp - 1000) / 100));
                    }
                    else if (tp == 1000)
                    {
                        atkpenalty = tatk * (0 - 0.2);
                    }
                    break;
                case "Camlann's Torment":
                    ddown = 0.0;
                    a0 = 12.5;
                    a2 = 62.5;
                    m = (a0 - a2) / (1000 - 3000);

                    tps = tp - 1000;

                    if (tps == 0)
                    {
                        ddown = a0;
                    }
                    else //y = mx + b
                    {
                        ddown = m * tps + a0;
                    }

                    rem = Math.Floor(ttdef * (ddown / 100));

                    ttdef = ttdef - rem;

                    break;
                case "Quietus":
                    ddown = 0.0;
                    a0 = 10.0;
                    a2 = 50.0;
                    m = (a0 - a2) / (1000 - 3000);

                    tps = tp - 1000;

                    if (tps == 0)
                    {
                        ddown = a0;
                    }
                    else //y = mx + b
                    {
                        ddown = m * tps + a0;
                    }

                    rem = Math.Floor(ttdef * (ddown / 100));

                    ttdef = ttdef - rem;

                    break;
                default:
                    break;
            }
            

            //floor after bonuses
            tatk = Math.Floor(tatk);

            double att_ratio = (tatk - atkpenalty) / ttdef;

            if (att_ratio > MyFunc.getrow_double(dt_p, wsname, "aRatio"))
            {
                att_ratio = MyFunc.getrow_double(dt_p, wsname, "aRatio");
            }

            //add level correction
            double c_ratio = att_ratio;

            double w_ratio = c_ratio + 0.0;

            if (_iscrit)
            {
                w_ratio += 1.0;
            }

            double ul = 0.00;
            if (w_ratio < 0.5)
            {
                if (w_ratio < 0.00)
                { w_ratio = 0.00; }
                ul = w_ratio + 0.5;
                if (ul > 0.99)
                { ul = 0.99; }
            }
            else if (w_ratio >= 0.5 && w_ratio < 0.7)
            { ul = 1.00; }
            else if (w_ratio >= 0.7 && w_ratio < 1.2)
            {
                ul = w_ratio + 0.3;

                if (ul < 1.0)
                { ul = 1.0; }
                else if (ul > 1.499)
                { ul = 1.499; }
            }
            else if (w_ratio >= 1.2 && w_ratio < 1.5)
            {
                ul = (w_ratio * 0.25) + w_ratio;

                if (ul < 1.5)
                { ul = 1.5; }
                else if (ul > 1.874)
                { ul = 1.874; }
            }
            else if (w_ratio >= 1.5)
            {
                ul = w_ratio + 0.375;

                if (ul < 1.875)
                { ul = 1.875; }
                else if (ul > w_ratio)
                { ul = w_ratio; }
            }


            double ll = 0.00;
            if (w_ratio < 0.38)
            {
                if (w_ratio < 0.00)
                { w_ratio = 0.00; }
                ll = w_ratio + 0.5;
                if (ll > 0.99)
                { ll = 0.99; }
            }
            else if (w_ratio >= 0.38 && w_ratio < 1.25)
            {
                ll = (w_ratio) * (1176 / 1024) - (448 / 1024);

                if (ll < 0.0)
                { ll = 0.0; }
                else if (ll > 0.994)
                { ll = 0.994; }
            }
            else if (w_ratio >= 1.25 && w_ratio < 1.51)
            {
                ll = 1.0;
            }
            else if (w_ratio >= 1.51 && w_ratio < 2.44)
            {
                ll = (w_ratio) * (1176 / 1024) - (755 / 1024);

                if (ll < 0.9968359375)
                { ll = 0.9968359375; }
                else if (ll > 2.064882813)
                { ll = 2.064882813; }
            }
            else if (w_ratio >= 2.44)
            {
                ll = w_ratio - 0.375;

                if (ll < 2.065)
                { ll = 2.065; }
                else if (ll > w_ratio)
                { ll = w_ratio; }
            }

            double qratio = rnd1.NextDouble() * (ul - ll) + ll;

            double cap = MyFunc.getrow_double(dt_p, wsname, "QRatio");

            double pdl = MyFunc.getrow_double(dt_j, cb_MJob.Text, "PDL");
            double pdls = MyFunc.getrow_double(dt_j, cb_SJob.Text, "PDL_s");


            if (cb_SJob.Text == "DRK" && num_ML.Value >= 5)
            {
                pdls = 0.3;
            }
            else if ((cb_SJob.Text == "THF" || cb_SJob.Text == "NIN") && num_ML.Value >= 5)
            {
                pdls = 0.1;
            }

            if (pdl != 0 || pdls != 0 || Convert.ToInt32(b_pdif.Text) != 0)
            {
                if (pdl != 0)
                {
                    cap = cap + pdl + ((Convert.ToDouble(b_pdif.Text) / 10));
                }
                else
                {
                    cap = cap + pdls + ((Convert.ToDouble(b_pdif.Text) / 10));
                }

            }

            if (_iscrit && (qratio > cap + 1.0))
            {
                qratio = cap + 1.0;
            }
            else if (qratio > cap)
            {
                qratio = cap;
            }
            else if (qratio < 0)
            {
                qratio = 0;
            }

            double pdif = qratio * ((rnd1.NextDouble() * (1.05 - 1.0)) + 1.0);
            return pdif;
        }

       
        #endregion


        #region basic_funcs

        /// <summary>
        /// Get the value of a stat from form. 
        /// TODO: Defined locally and in MyFunc, find best place later
        /// </summary>
        /// <param name="stat"></param>
        /// <returns></returns>
        internal int statval(string stat)
        {
            switch (stat)
            {
                case "STR":
                    return Int32.Parse(b_STR.Text);
                case "DEX":
                    return Int32.Parse(b_DEX.Text);
                case "AGI":
                    return Int32.Parse(b_AGI.Text);
                case "INT":
                    return Int32.Parse(b_INT.Text);
                case "MND":
                    return Int32.Parse(b_MND.Text);
                case "VIT":
                    return Int32.Parse(b_VIT.Text);
                case "CHR":
                    return Int32.Parse(b_CHR.Text);
                default:
                    return 0;

            }
        }

        private void createLabelInfo()
        {
            string a = "Mods: " + MyFunc.getrow_str(dt_p, combo_WS.Text, "MOD1") + " " + MyFunc.getrow_int(dt_p, combo_WS.Text, "MOD1_n");
            if (MyFunc.getrow_str(dt_p, combo_WS.Text, "MOD2") != "")
            {
                a = a + " " + MyFunc.getrow_str(dt_p, combo_WS.Text, "MOD2") + " " + MyFunc.getrow_int(dt_p, combo_WS.Text, "MOD2_n");
            }
            if (MyFunc.getrow_str(dt_p, combo_WS.Text, "MOD3") != "")
            {
                a = a + " " + MyFunc.getrow_str(dt_p, combo_WS.Text, "MOD3") + " " + MyFunc.getrow_int(dt_p, combo_WS.Text, "MOD3_n");
            }

            string f1 = MyFunc.getrow_double(dt_p, combo_WS.Text, "FTP_1").ToString();
            string f2 = MyFunc.getrow_double(dt_p, combo_WS.Text, "FTP_2").ToString();
            string f3 = MyFunc.getrow_double(dt_p, combo_WS.Text, "FTP_3").ToString();
            string f = "fTP:  {" + f1 + "}  {" + f2 + "}   {" + f3 + "}";

            string h = "Hits: " + MyFunc.getrow_int(dt_p, combo_WS.Text, "Hits").ToString();

            label_mods.Text = a;
            label_ftp.Text = f;
            label_hits.Text = h;

            if (MyFunc.getrow_bool(dt_p, combo_WS.Text, "Replicate"))
            {
                label_carry.Text = "fTP Carry: YES";
            }
            else
            {
                label_carry.Text = "fTP Carry: NO";
            }

            if (MyFunc.getrow_bool(dt_p, combo_WS.Text, "Crits"))
            {
                label_Crits.Text = "Crits: YES";
            }
            else
            {
                label_Crits.Text = "Crits: NO";
            }
        }

        #endregion


        #region formcontrolevents
        public override bool ValidateChildren()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    if (c.Text == "")
                    {
                        c.Text = "0";
                    }
                }
            }

            return true;
        }

        private void tb_KAD_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_ATTK_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_STR_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_VIT_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_INT_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_DEX_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_MND_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_CHR_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_WSD_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_FTP_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_TP_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_DMG_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_target_VIT_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_target_def_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void b_MD1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void b_MAB_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_target_MDB_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void b_AGI_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void b_Myth_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void b_cdmg_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_crate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_target_MND_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void b_Affi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void cb_myth_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_myth.Checked)
            {
                cb_relic.Checked = false;
            }
        }
        private void cb_relic_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_relic.Checked)
            {
                cb_myth.Checked = false;
                cb_mytham3.Checked = false;
            }
        }

        private void b_fencer_CheckedChanged(object sender, EventArgs e)
        {
            if (b_fencer.Checked)
            {
                b_DMG2.Text = "0";
                b_DMG2.Enabled = false;
            }
            else
            {
                b_DMG2.Enabled = true;
            }
        }

        private void track_orph_ValueChanged(object sender, EventArgs e)
        {
            label_orph.Text = "Distance: " + (Convert.ToDouble(track_orph.Value) / 10.0).ToString() + "'  (" + MyFunc.get_orph(track_orph.Value) + ")";
        }
        private void b_Ambu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_mdmg_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_acc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_racc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void b_target_DEX_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tb_maccSkill_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void b_macc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void b_pdif_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void tb_RAMaccSkill_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void combo_WS_SelectedIndexChanged(object sender, EventArgs e)
        {
            createLabelInfo();
        }

        private void cb_Mobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            b_target_INT.Text = Convert.ToString(MyFunc.getrow_int(MyTables._mobset(), cb_Mobs.Text, "INT"));
            b_target_VIT.Text = Convert.ToString(MyFunc.getrow_int(MyTables._mobset(), cb_Mobs.Text, "VIT"));
            b_target_AGI.Text = Convert.ToString(MyFunc.getrow_int(MyTables._mobset(), cb_Mobs.Text, "AGI"));
            b_target_MND.Text = Convert.ToString(MyFunc.getrow_int(MyTables._mobset(), cb_Mobs.Text, "MND"));
            b_target_def.Text = Convert.ToString(MyFunc.getrow_int(MyTables._mobset(), cb_Mobs.Text, "Defense"));
            b_target_eva.Text = Convert.ToString(MyFunc.getrow_int(MyTables._mobset(), cb_Mobs.Text, "Evasion"));
            b_target_MDB.Text = Convert.ToString(MyFunc.getrow_int(MyTables._mobset(), cb_Mobs.Text, "MDB"));
            b_target_meva.Text = Convert.ToString(MyFunc.getrow_int(MyTables._mobset(), cb_Mobs.Text, "MEva"));

        }

        private void num_clim_ValueChanged(object sender, EventArgs e)
        {
            num_striking.Value = 0;
        }

        private void num_striking_ValueChanged(object sender, EventArgs e)
        {
            num_clim.Value = 0;
        }

        private void cb_tern_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_tern.Checked)
            {
                num_clim.Value = 0;
                num_striking.Value = 0;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.Show();
        }
        private void averageIterationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValueBox vb = new ValueBox("Iterations", _iterations.ToString());
            if (vb.ShowDialog(this) == DialogResult.OK)
            {
                _iterations = Convert.ToInt32(vb.BoxValue);
                if (_iterations > 10000)
                {
                    _iterations = 10000;
                    MessageBox.Show("Max iterations is 10,000.");
                }
                label_iterations.Text = "Iterations: " + _iterations.ToString();
            }
            else
            {
                MessageBox.Show("Invalid Input.");
            }
            vb.Dispose();
        }
        #endregion

        #region ComboBoxes
        //Mob box
        private void populateMobBox()
        {

            cb_Mobs.DataSource = MyTables._mobset();
            cb_Mobs.DisplayMember = "Name";
            cb_Mobs.SelectedItem = "Default";

            b_target_INT.Text = Convert.ToString(MyFunc.getrow_int(MyTables._mobset(), "Default", "INT"));
            b_target_VIT.Text = Convert.ToString(MyFunc.getrow_int(MyTables._mobset(), "Default", "VIT"));
            b_target_AGI.Text = Convert.ToString(MyFunc.getrow_int(MyTables._mobset(), "Default", "AGI"));
            b_target_MND.Text = Convert.ToString(MyFunc.getrow_int(MyTables._mobset(), "Default", "MND"));
            b_target_def.Text = Convert.ToString(MyFunc.getrow_int(MyTables._mobset(), "Default", "Defense"));
            b_target_eva.Text = Convert.ToString(MyFunc.getrow_int(MyTables._mobset(), "Default", "Evasion"));
            b_target_MDB.Text = Convert.ToString(MyFunc.getrow_int(MyTables._mobset(), "Default", "MDB"));
            b_target_meva.Text = Convert.ToString(MyFunc.getrow_int(MyTables._mobset(), "Default", "MEva"));
        }

        //Job Boxes
        private void populateJobsBox()
        {
            cb_MJob.DataSource = dt_j;
            cb_MJob.DisplayMember = "Name";
            cb_MJob.SelectedIndex = 1;

            cb_SJob.BindingContext = new BindingContext();
            cb_SJob.DataSource = dt_j;
            cb_SJob.DisplayMember = "Name";
            cb_SJob.SelectedIndex = 20;

            dt_j.DefaultView.Sort = "Name";
            dt_j = dt_j.DefaultView.ToTable();

        }
        private void cb_MJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_SJob.SelectedIndex == cb_MJob.SelectedIndex)
            {
                if (cb_SJob.SelectedIndex == 21)
                {
                    cb_SJob.SelectedIndex = 0;
                }
                else
                {
                    cb_SJob.SelectedIndex += 1;
                }
            }

            if (cb_MJob.Text == "DNC")
            {
                cb_building.Enabled = true;
                cb_tern.Enabled = true;
            }
            else
            {
                cb_building.Checked = false;
                cb_building.Enabled = false;
                cb_tern.Checked = false;
                cb_tern.Enabled = false;
            }

            if (cb_MJob.Text == "MNK")
            {
                cb_footw.Enabled = true;
            }
            else
            {
                cb_footw.Checked = false;
                cb_footw.Enabled = false;
            }

            if (cb_MJob.Text == "WAR")
            {
                cb_MS.Enabled = true;
            }
            else
            {
                cb_MS.Checked = false;
                cb_MS.Enabled = false;
            }

            if (cb_MJob.Text == "THF" || cb_SJob.Text == "THF")
            {
                cb_SATA.Enabled = true;
            }
            else
            {
                cb_SATA.Checked = false;
                cb_SATA.Enabled = false;
            }

            //Utu
            if (cb_MJob.Text == "WAR" || cb_MJob.Text == "DRK" || cb_MJob.Text == "SAM" || cb_MJob.Text == "DRG" || cb_MJob.Text == "RUN")
            {
                cb_Utu.Enabled = true;
            }
            else
            {
                cb_Utu.Enabled = false;
                cb_Utu.Checked = false;
            }
            //Crep knife
            if (cb_MJob.Text == "WAR" || cb_MJob.Text == "RDM" || cb_MJob.Text == "THF" || cb_MJob.Text == "BST" || cb_MJob.Text == "BRD" ||
                cb_MJob.Text == "RNG" || cb_MJob.Text == "NIN" || cb_MJob.Text == "COR" || cb_MJob.Text == "DNC")
            {
                cb_Crepes.Enabled = true;
            }
            else
            {
                cb_Crepes.Enabled = false;
                cb_Crepes.Checked = false;
            }
            //Shining
            if (cb_MJob.Text == "WAR" || cb_MJob.Text == "PLD" || cb_MJob.Text == "SAM" || cb_MJob.Text == "DRG")
            {
                cb_shining.Enabled = true;
            }
            else
            {
                cb_shining.Enabled = false;
                cb_shining.Checked = false;
            }
        }

        private void cb_SJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cb_SJob.SelectedIndex == cb_MJob.SelectedIndex)
            {
                if(cb_SJob.SelectedIndex == 21)
                {
                    cb_SJob.SelectedIndex = 0;
                }
                else
                {
                    cb_SJob.SelectedIndex += 1;
                }
            }
            //SUBJOB
        }

        //Weapon boxes
        private void populateWpnTypeBox()
        {
            wstype.Add("Archery");
            wstype.Add("Axe");
            wstype.Add("Club");
            wstype.Add("Dagger");
            wstype.Add("Hand-to-Hand");
            wstype.Add("Great Axe");
            wstype.Add("Great Katana");
            wstype.Add("Great Sword");
            wstype.Add("Katana");
            wstype.Add("Marksmanship");
            wstype.Add("Polearm");
            wstype.Add("Scythe");
            wstype.Add("Staff");
            wstype.Add("Sword");
            combo_wcats.DataSource = wstype;
        }
        private void combo_wcats_SelectedIndexChanged(object sender, EventArgs e)
        {

            populateWSBox(combo_wcats.Text);
        }

        private void populateWSBox(string filter)
        {
            combo_WS.DataSource = null;
            combo_WS.Items.Clear();

            string f = "catagory = '" + filter + "'";

            DataTable filter_rows = dt_p.Select(f).CopyToDataTable();

            combo_WS.DataSource = filter_rows;
            combo_WS.DisplayMember = "Name";
            combo_WS.SelectedIndex = 0;

            dt_p.DefaultView.Sort = "Name";
            dt_p = dt_p.DefaultView.ToTable();
        }
        #endregion

        #region Saving
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "WSCalc Save|*.wss";
            saveFileDialog1.Title = "Save WSCalc File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@saveFileDialog1.FileName))
                {
                    foreach (Control c in this.Controls)
                    {
                        if (c is TextBox)
                        {
                            file.WriteLine(EncodeTo64("TextBox," + ((TextBox)c).Name + "," + ((TextBox)c).Text));
                        }
                        else if(c is NumericUpDown)
                        {
                            file.WriteLine(EncodeTo64("NumericUpDown," + ((NumericUpDown)c).Name + "," + ((NumericUpDown)c).Value));
                        }
                        else if(c is CheckBox)
                        {
                            file.WriteLine(EncodeTo64("CheckBox," + ((CheckBox)c).Name + "," + ((CheckBox)c).Checked));
                        }
                        else if (c is ComboBox)
                        {
                            file.WriteLine(EncodeTo64("ComboBox," + ((ComboBox)c).Name + "," + ((ComboBox)c).Text));
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid File Name.");
            }
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "WSCalc Save|*.wss";
            openFileDialog1.Title = "Open WSCalc File";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName == "")
            {
                MessageBox.Show("Invalid File Name.");
                return;
            }

            string[] loadFile = File.ReadAllLines(openFileDialog1.FileName);

            foreach (string l in loadFile)
            {
                string d = DecodeFrom64(l);
                string[] obj = d.Split(',');

                if (obj[0].Equals("TextBox"))
                {
                    Control c = GetControlByName(obj[1]);
                    c.Text = obj[2];
                }
                else if(obj[0].Equals("NumericUpDown"))
                {
                    try
                    {
                        Control c = GetControlByName(obj[1]);
                        ((NumericUpDown)c).Value = Convert.ToDecimal(obj[2]);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Error loading. " + ex.ToString());
                    }
                }
                else if (obj[0].Equals("CheckBox"))
                {
                    try
                    {
                        Control c = GetControlByName(obj[1]);
                        ((CheckBox)c).Checked = Convert.ToBoolean(obj[2]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading. " + ex.ToString());
                    }
                }
                else if (obj[0].Equals("ComboBox"))
                {
                    try
                    {
                        Control c = GetControlByName(obj[1]);
                        ((ComboBox)c).Text = obj[2];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading. " + ex.ToString());
                    }
                }
            }
        }

        private Control GetControlByName(string Name)
        {
            foreach (Control c in this.Controls)
                if (c.Name == Name)
                    return c;

            return null;
        }

        static public string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes
                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue
                  = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        static public string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes
                = System.Convert.FromBase64String(encodedData);
            string returnValue =
               System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }
        #endregion

    }
}
