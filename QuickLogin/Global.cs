using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLogin
{
    internal class Global
    {
        internal const string App_Name = "QuickLogin";
        internal const string App_Author = "Catrol";
        internal const string App_Copyright = "Copyright © 2021-2099 Catrol";
        internal const int App_Build_Year = 2022;

        internal string WorkBase = Environment.CurrentDirectory;

        internal struct Pair
        {
            string keys;
            string pwd;
        }

        internal static Dictionary<string, Pair> pwds = new();
    }
}
