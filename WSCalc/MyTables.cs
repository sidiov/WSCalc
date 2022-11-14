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
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSCalc
{
    internal static class MyTables
    {
        public static DataTable _jobs()
        {
            DataTable table1 = new DataTable("job_info");
            table1.Columns.Add("Name", typeof(string));
            table1.Columns.Add("Smite", typeof(double));
            table1.Columns.Add("PDL", typeof(double));
            table1.Columns.Add("CritBonus", typeof(int));
            table1.Columns.Add("True", typeof(int));
            table1.Columns.Add("Smite_s", typeof(double));
            table1.Columns.Add("PDL_s", typeof(double));
            table1.Columns.Add("Fencer", typeof(int));
            table1.Columns.Add("WSD", typeof(int));

            table1.Rows.Add(new object[] { "WAR", 19.9, 0.2, 18, 0, 9.7, 0.1, 5, 3 });
            table1.Rows.Add(new object[] { "MNK", 15, 0.3, 0, 0, 9.7, 0.1, 0, 0 });
            table1.Rows.Add(new object[] { "WHM", 0, 0, 0, 0, 0, 0, 0, 0 });
            table1.Rows.Add(new object[] { "BLM", 0, 0, 0, 0, 0, 0, 0, 0 });
            table1.Rows.Add(new object[] { "RDM", 0, 0.1, 0, 0, 0, 0, 0, 0 });
            table1.Rows.Add(new object[] { "THF", 0, 0.1, 22, 0, 0, 0, 0, 0 });
            table1.Rows.Add(new object[] { "PLD", 0, 0, 0, 0, 0, 0, 0, 0 });
            table1.Rows.Add(new object[] { "DRK", 29.6, 0.5, 16, 0, 15, 0.2, 0, 8 });
            table1.Rows.Add(new object[] { "BST", 0, 0.2, 0, 0, 0, 0.1, 3, 0 });
            table1.Rows.Add(new object[] { "BRD", 0, 0, 0, 0, 0, 0, 2, 0 });
            table1.Rows.Add(new object[] { "RNG", 0, 0.3, 8, 7, 0, 0.1, 0, 0 });
            table1.Rows.Add(new object[] { "SMN", 0, 0, 0, 0, 0, 0, 0, 0 });
            table1.Rows.Add(new object[] { "SAM", 0, 0.2, 0, 0, 0, 0.1, 0, 0 });
            table1.Rows.Add(new object[] { "NIN", 0, 0.1, 0, 0, 0, 0, 0, 5 });
            table1.Rows.Add(new object[] { "DRG", 15, 0.3, 8, 0, 9.7, 0.1, 0, 0 });
            table1.Rows.Add(new object[] { "BLU", 0, 0, 0, 0, 0, 0, 0, 0 });
            table1.Rows.Add(new object[] { "COR", 0, 0, 0, 5, 0, 0, 0, 0 });
            table1.Rows.Add(new object[] { "PUP", 9.7, 0.2, 0, 0, 0, 0.1, 0, 0 });
            table1.Rows.Add(new object[] { "DNC", 0, 0.2, 19, 0, 0, 0.1, 0, 0 });
            table1.Rows.Add(new object[] { "SCH", 0, 0, 0, 0, 0, 0, 0, 0 });
            table1.Rows.Add(new object[] { "GEO", 0, 0, 0, 0, 0, 0, 0, 0 });
            table1.Rows.Add(new object[] { "RUN", 0, 0, 0, 0, 0, 0, 0, 0 });

            return table1;
        }


        public static DataTable _critset()
        {
            DataTable table1 = new DataTable("crit_info");
            table1.Columns.Add("Name", typeof(string));
            table1.Columns.Add("C_1", typeof(double));
            table1.Columns.Add("C_2", typeof(double));
            table1.Columns.Add("C_3", typeof(double));

            table1.Rows.Add(new object[] { "Chant du Cygne", 15, 25, 40 });
            table1.Rows.Add(new object[] { "Evisceration", 10, 25, 50 });
            table1.Rows.Add(new object[] { "Rampage", 10, 20, 40 });
            table1.Rows.Add(new object[] { "Vorpal Blade", 10, 20, 40 });
            table1.Rows.Add(new object[] { "Ukko's Fury", 20, 35, 55 });
            table1.Rows.Add(new object[] { "Blade: Hi", 15, 20, 25 });
            table1.Rows.Add(new object[] { "Blade: Jin", 10, 20, 40 });
            table1.Rows.Add(new object[] { "Jishnu's Radiance", 10, 25, 45 });
            table1.Rows.Add(new object[] { "Victory Smite", 10, 25, 45 });
            table1.Rows.Add(new object[] { "Ascetic's Fury", 20, 30, 50 });
            table1.Rows.Add(new object[] { "Stringing Pummel", 15, 25, 45 });
            table1.Rows.Add(new object[] { "Hexa Strike", 10, 15, 25 });
            table1.Rows.Add(new object[] { "Drakesbane", 10, 25, 40 });

            return table1;
        }
        public static DataTable _wsset()
        {
            DataTable table1 = new DataTable("ws_info");
            table1.Columns.Add("Name", typeof(string));
            table1.Columns.Add("Type", typeof(int)); //0 - phys 2h , 1 - mgc , 2- hyb p1h, 3 - rng , 4 -magic agi, 5 -phys 1h, 6 -magic MND, 7 -magic chr, 8 -h2h, 9-hyb p 2h, 10 hyb r
            table1.Columns.Add("Mythic", typeof(int)); //0 -none, 1- mythic, 2-relic, 3 - empy, 4 - merit/aeonic, 9-ambu
            table1.Columns.Add("MOD1", typeof(string));
            table1.Columns.Add("MOD1_n", typeof(int));
            table1.Columns.Add("MOD2", typeof(string));
            table1.Columns.Add("MOD2_n", typeof(int));
            table1.Columns.Add("MOD3", typeof(string));
            table1.Columns.Add("MOD3_n", typeof(int));
            table1.Columns.Add("FTP_1", typeof(double));
            table1.Columns.Add("FTP_2", typeof(double));
            table1.Columns.Add("FTP_3", typeof(double));
            table1.Columns.Add("Hits", typeof(int));
            table1.Columns.Add("Replicate", typeof(bool));
            table1.Columns.Add("Crits", typeof(bool));
            table1.Columns.Add("QRatio", typeof(double));
            table1.Columns.Add("dstatmod", typeof(int));
            table1.Columns.Add("dstatcap", typeof(int));
            table1.Columns.Add("aRatio", typeof(double));
            table1.Columns.Add("catagory", typeof(string));

            //2hand
            table1.Rows.Add(new object[] { "Resolution", 0, 4, "STR", 100, null, 0, null, 0, 0.71875, 1.5, 2.25, 5, true, false, 3.75, 0, 0, 4.125, "Great Sword" });
            table1.Rows.Add(new object[] { "Dimidiation", 0, 1, "DEX", 80, null, 0, null, 0, 2.25, 4.5, 6.75, 2, false, false, 3.75, 0, 0, 4.125, "Great Sword" });
            table1.Rows.Add(new object[] { "Ground Strike", 0, 0, "STR", 50, "INT", 50, null, 0, 1.5, 1.75, 3.0, 1, false, false, 3.75, 0, 0, 4.125, "Great Sword" });
            table1.Rows.Add(new object[] { "Torcleaver", 0, 3, "VIT", 80, null, 0, null, 0, 4.75, 7.5, 9.765625, 1, false, false, 3.75, 0, 0, 4.125, "Great Sword" });
            table1.Rows.Add(new object[] { "Scourge", 0, 2, "VIT", 40, "STR", 40, null, 0, 3.0, 3.0, 3.0, 1, false, false, 3.75, 0, 0, 4.125, "Great Sword" });
            table1.Rows.Add(new object[] { "Shockwave", 0, 0, "STR", 30, "MND", 30, null, 0, 1.0, 1.0, 1.0, 2, false, false, 3.75, 0, 0, 4.125, "Great Sword" });

            table1.Rows.Add(new object[] { "Upheaval", 0, 4, "VIT", 100, null, 0, null, 0, 1.0, 3.5, 6.5, 4, false, false, 3.75, 0, 0, 4.125, "Great Axe" });
            table1.Rows.Add(new object[] { "King's Justice", 0, 1, "STR", 50, null, 0, null, 0, 1.0, 3.0, 5.0, 4, false, false, 3.75, 0, 0, 4.125, "Great Axe" });
            table1.Rows.Add(new object[] { "Ukko's Fury", 0, 3, "STR", 80, null, 0, null, 0, 2.0, 2.0, 2.0, 2, false, true, 3.75, 0, 0, 4.125, "Great Axe" });
            table1.Rows.Add(new object[] { "Fell Cleave", 0, 0, "STR", 60, null, 0, null, 0, 2.75, 2.75, 2.75, 1, false, false, 3.75, 0, 0, 4.125, "Great Axe" });

            table1.Rows.Add(new object[] { "Shattersoul", 0, 4, "INT", 100, null, 0, null, 0, 1.375, 1.375, 1.375, 3, true, false, 3.75, 0, 0, 4.125, "Staff" });
            table1.Rows.Add(new object[] { "Gate of Tartarus", 0, 2, "INT", 80, null, 0, null, 0, 3.0, 3.0, 3.0, 1, false, false, 3.75, 0, 0, 4.125, "Staff" });
            table1.Rows.Add(new object[] { "Retribution", 0, 2, "MND", 50, "STR", 30, null, 0, 2.0, 2.5, 3.0, 1, false, false, 3.75, 0, 0, 4.125, "Staff" });

            table1.Rows.Add(new object[] { "Drakesbane", 0, 1, "STR", 50, null, 0, null, 0, 1.0, 1.0, 1.0, 4, false, true, 3.75, 0, 0, 4.125, "Polearm" });
            table1.Rows.Add(new object[] { "Camlann's Torment", 0, 3, "STR", 60, "VIT", 60, null, 0, 3.0, 3.0, 3.0, 1, false, false, 3.75, 0, 0, 4.125, "Polearm" });
            table1.Rows.Add(new object[] { "Impulse Drive", 0, 9, "STR", 100, null, 0, null, 0, 1.0, 3.0, 5.5, 2, false, false, 3.75, 0, 0, 4.125, "Polearm" });
            table1.Rows.Add(new object[] { "Stardiver", 0, 4, "STR", 100, null, 0, null, 0, 0.75, 1.25, 1.75, 4, true, false, 3.75, 0, 0, 4.125, "Polearm" });

            //Scythe
            table1.Rows.Add(new object[] { "Catastrophe", 0, 2, "STR", 40, "INT", 40, null, 0, 2.75, 2.75, 2.75, 1, false, false, 4.0, 0, 0, 4.125, "Scythe" });
            table1.Rows.Add(new object[] { "Insurgency", 0, 1, "STR", 20, "INT", 20, null, 0, 0.5, 3.25, 6.0, 4, false, false, 4.0, 0, 0, 4.125, "Scythe" });
            table1.Rows.Add(new object[] { "Cross Reaper", 0, 0, "STR", 60, "MND", 60, null, 0, 2.0, 4.0, 7.0, 2, false, false, 4.0, 0, 0, 4.125, "Scythe" });
            table1.Rows.Add(new object[] { "Entropy", 0, 4, "INT", 100, null, 0, null, 0, 0.75, 1.25, 2.0, 4, true, false, 4.0, 0, 0, 4.125, "Scythe" });
            table1.Rows.Add(new object[] { "Spiral Hell", 0, 9, "STR", 50, "INT", 50, null, 0, 1.375, 2.75, 4.75, 1, false, false, 4.0, 0, 0, 4.125, "Scythe" });
            table1.Rows.Add(new object[] { "Quietus", 0, 3, "STR", 60, "MND", 60, null, 0, 3.0, 3.0, 3.0, 1, false, false, 4.0, 0, 0, 4.125, "Scythe" });

            //GK
            table1.Rows.Add(new object[] { "Tachi: Kasha", 0, 9, "STR", 75, null, 0, null, 0, 1.5625, 2.6875, 4.125, 1, false, false, 3.5, 0, 0, 3.875, "Great Katana" });
            table1.Rows.Add(new object[] { "Tachi: Ageha", 0, 0, "CHR", 60, "STR", 40, null, 0, 2.625, 2.625, 2.625, 1, false, false, 3.5, 0, 0, 3.875, "Great Katana" });
            table1.Rows.Add(new object[] { "Tachi: Rana", 0, 1, "STR", 50, null, 0, null, 0, 1.0, 1.0, 1.0, 3, false, false, 3.5, 0, 0, 3.875, "Great Katana" });
            table1.Rows.Add(new object[] { "Tachi: Shoha", 0, 4, "STR", 100, null, 0, null, 0, 1.375, 2.1875, 2.6875, 2, false, false, 3.5, 0, 0, 3.875, "Great Katana" });
            table1.Rows.Add(new object[] { "Tachi: Fudo", 0, 3, "STR", 80, null, 0, null, 0, 3.75, 5.75, 8.0, 1, false, false, 3.5, 0, 0, 3.875, "Great Katana" });
            table1.Rows.Add(new object[] { "Tachi: Kaiten", 0, 2, "STR", 80, null, 0, null, 0, 3.0, 3.0, 3.0, 1, false, false, 3.5, 0, 0, 3.875, "Great Katana" });

            //1hand
            //table1.Rows.Add(new object[] { "Atonement", 5, 1, "DEX", 80, null, 0, null, 0, 1.0, 1.5, 2.0, 1, false, false, 3.25, 0, 0, 3.625, "Sword" });
            table1.Rows.Add(new object[] { "Savage Blade", 5, 9, "STR", 50, "MND", 50, null, 0, 4.0, 10.25, 13.75, 2, false, false, 3.25, 0, 0, 3.625, "Sword" });
            table1.Rows.Add(new object[] { "Death Blossom", 5, 1, "STR", 30, "MND", 50, null, 0, 4.0, 4.0, 4.0, 3, false, false, 3.25, 0, 0, 3.625, "Sword" });
            table1.Rows.Add(new object[] { "Requiescat", 5, 4, "MND", 100, null, 0, null, 0, 1.0, 1.0, 1.0, 5, false, false, 3.25, 0, 0, 3.625, "Sword" });
            table1.Rows.Add(new object[] { "Expiacion", 5, 1, "STR", 30, "INT", 30, "DEX", 20, 3.796875, 9.390625, 12.1875, 2, false, false, 3.25, 0, 0, 3.625, "Sword" });
            table1.Rows.Add(new object[] { "Chant du Cygne", 5, 3, "DEX", 80, null, 0, null, 0, 1.6328125, 1.6328125, 1.6328125, 3, true, true, 3.25, 0, 0, 3.625, "Sword" });
            table1.Rows.Add(new object[] { "Vorpal Blade", 5, 0, "STR", 60, null, 0, null, 0, 1.375, 1.375, 1.375, 4, true, true, 3.25, 0, 0, 3.625, "Sword" });

            table1.Rows.Add(new object[] { "Rudra's Storm", 5, 3, "DEX", 80, null, 0, null, 0, 5.0, 10.19, 13, 1, false, false, 3.25, 0, 0, 3.625, "Dagger" });
            table1.Rows.Add(new object[] { "Mordant Rime", 5, 1, "CHR", 70, "DEX", 30, null, 0, 5.0, 5.0, 5.0, 2, false, false, 3.25, 0, 0, 3.625, "Dagger" });
            table1.Rows.Add(new object[] { "Evisceration", 5, 9, "DEX", 50, null, 0, null, 0, 1.25, 1.25, 1.25, 5, true, true, 3.25, 0, 0, 3.625, "Dagger" });
            table1.Rows.Add(new object[] { "Mandalic Stab", 5, 1, "DEX", 60, null, 0, null, 0, 4.0, 6.09, 8.5, 1, false, false, 3.25, 0, 0, 3.625, "Dagger" });
            table1.Rows.Add(new object[] { "Exenterator", 5, 4, "AGI", 100, null, 0, null, 0, 1.0, 1.0, 1.0, 4, true, false, 3.25, 0, 0, 3.625, "Dagger" });
            table1.Rows.Add(new object[] { "Pyrrhic Kleos", 5, 1, "STR", 40, "DEX", 40, null, 0, 1.75, 1.75, 1.75, 4, true, false, 3.25, 0, 0, 3.625, "Dagger" });
            table1.Rows.Add(new object[] { "Shark Bite", 5, 0, "DEX", 40, "AGI", 40, null, 0, 4.5, 6.8, 8.5, 2, false, false, 3.25, 0, 0, 3.625, "Dagger" });
            table1.Rows.Add(new object[] { "Dancing Edge", 5, 0, "DEX", 40, "CHR", 10, null, 0, 1.1875, 1.1875, 1.1875, 5, true, false, 3.25, 0, 0, 3.625, "Dagger" });
            table1.Rows.Add(new object[] { "Viper Bite", 5, 0, "DEX", 100, null, 0, null, 0, 1.0, 1.0, 1.0, 1, false, false, 3.25, 0, 0, 3.625, "Dagger" });

            table1.Rows.Add(new object[] { "Realmrazer", 5, 4, "MND", 100, null, 0, null, 0, 0.9, 0.9, 0.9, 7, true, false, 3.25, 0, 0, 3.625, "Club" });
            table1.Rows.Add(new object[] { "Black Halo", 5, 9, "MND", 70, "STR", 30, null, 0, 3.0, 7.25, 9.75, 2, false, false, 3.25, 0, 0, 3.625, "Club" });
            table1.Rows.Add(new object[] { "Hexa Strike", 5, 0, "MND", 30, "STR", 30, null, 0, 1.125, 1.125, 1.125, 2, true, true, 3.25, 0, 0, 3.625, "Club" });

            table1.Rows.Add(new object[] { "Ruinator", 5, 4, "STR", 100, null, 0, null, 0, 1.08, 1.08, 1.08, 4, true, false, 3.25, 0, 0, 3.625, "Axe" });
            table1.Rows.Add(new object[] { "Decimation", 5, 9, "STR", 50, null, 0, null, 0, 1.75, 1.75, 1.75, 3, true, false, 3.25, 0, 0, 3.625, "Axe" });
            table1.Rows.Add(new object[] { "Mistral Axe", 5, 0, "STR", 50, null, 0, null, 0, 4, 10.5, 13.625, 1, false, false, 3.25, 0, 0, 3.625, "Axe" });
            table1.Rows.Add(new object[] { "Rampage", 5, 0, "STR", 50, null, 0, null, 0, 1.0, 1.0, 1.0, 5, true, true, 3.25, 0, 0, 3.625, "Axe" });

            table1.Rows.Add(new object[] { "Blade: Ten", 5, 0, "DEX", 30, "STR", 30, null, 0, 4.5, 11.5, 15.5, 1, false, false, 3.25, 0, 0, 3.625, "Katana" });
            table1.Rows.Add(new object[] { "Blade: Hi", 5, 3, "AGI", 80, null, 0, null, 0, 5.0, 5.0, 5.0, 1, false, true, 3.25, 0, 0, 3.625, "Katana" });
            table1.Rows.Add(new object[] { "Blade: Shun", 5, 4, "DEX", 100, null, 0, null, 0, 1.0, 1.0, 1.0, 5, true, false, 3.25, 0, 0, 3.625, "Katana" });
            table1.Rows.Add(new object[] { "Blade: Jin", 5, 0, "STR", 30, "DEX", 30, null, 0, 1.375, 1.375, 1.375, 3, true, true, 3.25, 0, 0, 3.625, "Katana" });
            table1.Rows.Add(new object[] { "Blade: Metsu", 5, 2, "DEX", 80, null, 0, null, 0, 5.0, 5.0, 5.0, 1, false, false, 3.25, 0, 0, 3.625, "Katana" });
            table1.Rows.Add(new object[] { "Blade: Kamu", 5, 1, "STR", 60, "INT", 60, null, 0, 1.0, 1.0, 1.0, 1, false, false, 3.25, 0, 0, 3.625, "Katana" });

            //h2h
            table1.Rows.Add(new object[] { "Victory Smite", 8, 3, "STR", 80, null, 0, null, 0, 1.5375, 1.5375, 1.5375, 4, true, true, 3.5, 0, 0, 3.875, "Hand-to-Hand" });
            table1.Rows.Add(new object[] { "Howling Fist", 8, 0, "VIT", 50, "STR", 20, null, 0, 2.05, 3.58, 5.8, 2, true, false, 3.5, 0, 0, 3.875, "Hand-to-Hand" });
            table1.Rows.Add(new object[] { "Tornado Kick", 8, 0, "VIT", 40, "STR", 40, null, 0, 1.68, 2.8, 4.575, 3, true, false, 3.5, 0, 0, 3.875, "Hand-to-Hand" });
            table1.Rows.Add(new object[] { "Spinning Attack", 8, 0, "STR", 100, null, 0, null, 0, 1.0, 1.0, 1.0, 1, false, false, 3.5, 0, 0, 3.875, "Hand-to-Hand" });
            table1.Rows.Add(new object[] { "Shijin Spiral", 8, 4, "DEX", 100, null, 0, null, 0, 1.375, 1.375, 1.375, 5, true, false, 3.5, 0, 0, 3.875, "Hand-to-Hand" });
            table1.Rows.Add(new object[] { "Ascetic's Fury", 8, 1, "STR", 50, "VIT", 50, null, 0, 1.0, 1.0, 1.0, 1, true, true, 3.5, 0, 0, 3.875, "Hand-to-Hand" });
            table1.Rows.Add(new object[] { "Stringing Pummel", 8, 1, "STR", 32, "VIT", 32, null, 0, 1.0, 1.0, 1.0, 6, false, true, 3.5, 0, 0, 3.875, "Hand-to-Hand" });
            table1.Rows.Add(new object[] { "Final Heaven", 8, 2, "VIT", 80, null, 0, null, 0, 3.0, 3.0, 3.0, 1, false, false, 3.5, 0, 0, 3.875, "Hand-to-Hand" });
            table1.Rows.Add(new object[] { "Asuran Fists", 8, 9, "VIT", 15, "STR", 15, null, 0, 1.2375, 1.2375, 1.2375, 8, true, false, 3.5, 0, 0, 3.875, "Hand-to-Hand" });
            table1.Rows.Add(new object[] { "Dragon Kick", 8, 0, "VIT", 50, "STR", 50, null, 0, 1.68, 2.8, 4.575, 1, true, false, 3.5, 0, 0, 3.875, "Hand-to-Hand" });
            table1.Rows.Add(new object[] { "Raging Fists", 8, 0, "STR", 30, "DEX", 30, null, 0, 0.975, 2.25, 3.75, 5, true, false, 3.5, 0, 0, 3.875, "Hand-to-Hand" });

            //Magic
            table1.Rows.Add(new object[] { "Leaden Salute", 4, 1, "AGI", 100, null, 0, null, 0, 4.0, 6.7, 10.0, 1, false, false, 3.75, 0, 1500, 0, "Marksmanship" });
            table1.Rows.Add(new object[] { "Wildfire", 4, 3, "AGI", 60, null, 0, null, 0, 5.5, 5.5, 5.5, 1, false, false, 3.75, 0, 1276, 0, "Marksmanship" });
            table1.Rows.Add(new object[] { "Trueflight", 4, 1, "AGI", 100, null, 0, null, 0, 3.890625, 6.4921875, 9.671875, 1, false, false, 3.75, 0, 1500, 0, "Marksmanship" });
            table1.Rows.Add(new object[] { "Sanguine Blade", 1, 0, "MND", 50, "STR", 30, null, 0, 2.75, 2.75, 2.75, 1, false, false, 3.25, 0, 1276, 0, "Sword" });
            table1.Rows.Add(new object[] { "Red Lotus Blade", 1, 0, "INT", 40, "STR", 40, null, 0, 1.0, 2.3828125, 3.75, 1, false, false, 3.25, 8, 32, 0, "Sword" });
            table1.Rows.Add(new object[] { "Seraph Blade", 1, 0, "STR", 40, "MND", 40, null, 0, 1.125, 2.625, 4.125, 1, false, false, 3.25, 0, 1500, 0, "Sword" });
            table1.Rows.Add(new object[] { "Aeolian Edge", 1, 0, "DEX", 40, "INT", 40, null, 0, 2.0, 3.0, 4.5, 1, false, false, 3.25, 8, 32, 0, "Dagger" });
            table1.Rows.Add(new object[] { "Gust Slash", 1, 0, "DEX", 40, "INT", 40, null, 0, 1.0, 2.0, 2.5, 1, false, false, 3.25, 8, 32, 0, "Dagger" });
            table1.Rows.Add(new object[] { "Primal Rend", 7, 1, "CHR", 60, "DEX", 30, null, 0, 3.0625, 5.8359375, 7.5625, 1, false, false, 3.25, 0, 651, 0, "Axe" });
            table1.Rows.Add(new object[] { "Cloudsplitter", 1, 3, "STR", 40, "MND", 40, null, 0, 3.75, 6.69921875, 8.5, 1, false, false, 3.25, 0, 1500, 0, "Axe" });
            table1.Rows.Add(new object[] { "Vidohunir", 1, 1, "INT", 80, null, 0, null, 0, 1.75, 1.75, 1.75, 1, false, false, 3.75, 0, 1276, 0, "Staff" });
            table1.Rows.Add(new object[] { "Omniscience", 1, 1, "MND", 80, null, 0, null, 0, 2.0, 2.0, 2.0, 1, false, false, 3.75, 0, 1276, 0, "Staff" });
            table1.Rows.Add(new object[] { "Garland of Bliss", 6, 1, "MND", 70, "STR", 30, null, 0, 2.25, 2.25, 2.25, 1, false, false, 3.75, 0, 1500, 0, "Staff" });
            table1.Rows.Add(new object[] { "Earth Crusher", 1, 0, "INT", 40, "STR", 40, null, 0, 1.0, 2.3125, 3.625, 1, false, false, 3.75, 8, 32, 0, "Staff" });
            table1.Rows.Add(new object[] { "Sunburst", 1, 0, "STR", 40, "MND", 40, null, 0, 1.0, 2.5, 4.0, 1, false, false, 3.75, 8, 32, 0, "Staff" });
            table1.Rows.Add(new object[] { "Cataclysm", 1, 0, "INT", 30, "STR", 30, null, 0, 2.75, 4.0, 5.0, 1, false, false, 3.75, 8, 32, 0, "Staff" });
            table1.Rows.Add(new object[] { "Flash Nova", 1, 0, "MND", 50, "STR", 50, null, 0, 3.0, 3.0, 3.0, 1, false, false, 3.25, 8, 32, 0, "Club" });
            table1.Rows.Add(new object[] { "Seraph Strike", 1, 0, "STR", 40, "MND", 40, null, 0, 2.125, 3.675, 6.125, 1, false, false, 3.25, 0, 1500, 0, "Club" });
            table1.Rows.Add(new object[] { "Raiden Thrust", 1, 0, "INT", 40, "STR", 40, null, 0, 1.0, 2.0, 3.0, 1, false, false, 3.75, 8, 32, 0, "Polearm" });
            table1.Rows.Add(new object[] { "Herculean Slash", 1, 0, "VIT", 80, null, 0, null, 0, 3.5, 3.5, 3.5, 1, false, false, 3.75, 0, 1500, 0, "Great Sword" });
            table1.Rows.Add(new object[] { "Shadow of Death", 1, 0, "INT", 40, "STR", 40, null, 0, 1.0, 4.175, 8.6125, 1, false, false, 3.75, 0, 1500, 0, "Scythe" });
            table1.Rows.Add(new object[] { "Infernal Scythe", 1, 0, "INT", 70, "STR", 30, null, 0, 3.5, 3.5, 3.5, 1, false, false, 3.75, 0, 500, 0, "Scythe" });
            table1.Rows.Add(new object[] { "Blade: Ei", 1, 0, "INT", 40, "STR", 40, null, 0, 1.0, 3.0, 5.0, 1, false, false, 3.25, 8, 32, 0, "Katana" });

            //Hybrid
            table1.Rows.Add(new object[] { "Blade: Chi", 5, 0, "STR", 80, "INT", 30, null, 0, 0.5, 1.375, 2.25, 1, false, false, 3.25, 0, 1500, 3.625, "Katana" });
            table1.Rows.Add(new object[] { "Blade: To", 5, 0, "STR", 40, "INT", 40, null, 0, 0.5, 1.5, 2.5, 1, false, false, 3.25, 0, 1500, 3.625, "Katana" });
            table1.Rows.Add(new object[] { "Blade: Teki", 5, 0, "STR", 30, "INT", 30, null, 0, 0.5, 1.375, 2.25, 1, false, false, 3.25, 0, 1500, 3.625, "Katana" });
            table1.Rows.Add(new object[] { "Tachi: Goten", 2, 0, "STR", 60, null, 0, null, 0, 0.5, 1.5, 2.5, 1, false, false, 3.75, 0, 1500, 3.875, "Great Katana" });
            table1.Rows.Add(new object[] { "Tachi: Jinpu", 2, 0, "STR", 30, null, 0, null, 0, 0.5, 1.5, 2.5, 1, false, false, 3.75, 0, 1500, 3.875, "Great Katana" });
            table1.Rows.Add(new object[] { "Tachi: Kagero", 2, 0, "STR", 75, null, 0, null, 0, 0.5, 1.5, 2.5, 1, false, false, 3.75, 0, 1500, 3.875, "Great Katana" });
            table1.Rows.Add(new object[] { "Tachi: Koki", 2, 0, "STR", 50, "MND", 30, null, 0, 0.5, 1.5, 2.5, 1, false, false, 3.75, 0, 1500, 3.875, "Great Katana" });
            table1.Rows.Add(new object[] { "Hot Shot", 10, 0, "AGI", 70, null, 0, null, 0, 0.5, 1.55, 2.1, 1, false, false, 3.5, 0, 1500, 3.475, "Marksmanship" });
            table1.Rows.Add(new object[] { "Flaming Arrow", 10, 0, "AGI", 50, "STR", 20, null, 0, 0.5, 1.55, 2.1, 1, false, false, 3.25, 0, 1500, 3.475, "Archery" });

            //Ranged
            table1.Rows.Add(new object[] { "Last Stand", 3, 4, "AGI", 100, null, 0, null, 0, 2.0, 3.0, 4.0, 2, true, false, 3.5, 0, 0, 3.475, "Marksmanship" });
            table1.Rows.Add(new object[] { "Coronach", 3, 0, "DEX", 40, "AGI", 40, null, 0, 3.0, 3.0, 3.0, 1, false, false, 3.5, 0, 0, 3.475, "Marksmanship" });
            table1.Rows.Add(new object[] { "Detonator", 3, 9, "AGI", 70, null, 0, null, 0, 1.5, 2.5, 5.0, 1, false, false, 3.5, 0, 0, 3.475, "Marksmanship" });
            table1.Rows.Add(new object[] { "Numbing Shot", 3, 0, "AGI", 80, null, 0, null, 0, 3.0, 3.0, 3.0, 1, false, false, 3.5, 0, 0, 3.475, "Marksmanship" });
            table1.Rows.Add(new object[] { "Slug Shot", 3, 0, "AGI", 70, null, 0, null, 0, 5.0, 5.0, 5.0, 1, false, false, 3.5, 0, 0, 3.475, "Marksmanship" });

            table1.Rows.Add(new object[] { "Jishnu's Radiance", 3, 3, "DEX", 80, null, 0, null, 0, 1.75, 1.75, 1.75, 3, true, true, 3.25, 0, 0, 3.475, "Archery" });
            table1.Rows.Add(new object[] { "Apex Arrow", 3, 4, "AGI", 100, null, 0, null, 0, 3.0, 3.0, 3.0, 4, true, false, 3.25, 0, 0, 3.475, "Archery" });
            table1.Rows.Add(new object[] { "Namas Arrow", 3, 2, "STR", 40, "AGI", 40, null, 0, 2.75, 2.75, 2.75, 1, false, false, 3.25, 0, 0, 3.475, "Archery" });
            table1.Rows.Add(new object[] { "Refulgent Arrow", 3, 0, "STR", 60, null, 0, null, 0, 3.0, 4.25, 7.0, 2, false, false, 3.25, 0, 0, 3.475, "Archery" });
            table1.Rows.Add(new object[] { "Empyreal Arrow", 3, 0, "AGI", 50, "STR", 20, null, 0, 1.5, 2.5, 5.0, 1, false, false, 3.25, 0, 0, 3.475, "Archery" });

            return table1;
        }

        //Thanks to @shi-shin-juju for extensive testing for some macc estimates.
        public static DataTable _mobset()
        {
            DataTable table1 = new DataTable("mob_info");
            table1.Columns.Add("Name", typeof(string));
            table1.Columns.Add("Level", typeof(int));
            table1.Columns.Add("VIT", typeof(int));
            table1.Columns.Add("AGI", typeof(int));
            table1.Columns.Add("MND", typeof(int));
            table1.Columns.Add("INT", typeof(int));
            table1.Columns.Add("Defense", typeof(int));
            table1.Columns.Add("Evasion", typeof(int));
            table1.Columns.Add("MDB", typeof(int));
            table1.Columns.Add("MEva", typeof(int));

            table1.Rows.Add(new object[] { "Default", 130, 200, 200, 200, 200, 500, 500, 20, 100 });
            table1.Rows.Add(new object[] { "Rarab", 1, 4, 5, 1, 1, 5, 6, 0, 0 });
            table1.Rows.Add(new object[] { "Trivial", 84, 75, 75, 75, 75, 360, 350, 1, 10 });
            table1.Rows.Add(new object[] { "L100", 100, 110, 106, 110, 110, 498, 468, 3, 25 });
            table1.Rows.Add(new object[] { "Apex Bat 135", 135, 310, 275, 100, 248, 1436, 1224, 0, 170 });
            table1.Rows.Add(new object[] { "Apex Crab 130", 130, 350, 250, 100, 230, 1260, 1073, 0, 150 });
            table1.Rows.Add(new object[] { "Tojil", 130, 200, 200, 200, 200, 1900, 870, 10, 250 });
            table1.Rows.Add(new object[] { "Warder of Courage", 145, 390, 330, 330, 330, 2200, 1460, 200, 580 });
            table1.Rows.Add(new object[] { "Maju", 145, 410, 350, 350, 350, 2100, 1540, 150, 690 });
            table1.Rows.Add(new object[] { "Schah", 150, 450, 400, 400, 400, 2800, 1696, 220, 850 });

            return table1;
        }

    }
}
