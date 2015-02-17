using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace Finance_Management_System
{
    public static class ClsExtension
    {
        public static IEnumerable<int> FindMissing(this List<int> list)
        {
            list.Sort();
            var firstNumber = list.First();
            var lastNumber = list.Last();
            var range = Enumerable.Range(firstNumber, lastNumber - firstNumber);
            var missingNumbers = range.Except(list);
            return missingNumbers;
        }

        public static string CurrFormat(this System.Windows.Forms.TextBox Txt)
        {
            if (Txt.Text.Trim().Length == 0)
            {
                Txt.Text = "0";
            }
            return string.Format("{0:0.00}", double.Parse(Txt.Text));
        }

        public static DataTable Delete(this DataTable table, string filter)
        {
            table.Select(filter).Delete();
            return table;
        }
        public static void Delete(this IEnumerable<DataRow> rows)
        {
            foreach (var row in rows)
                row.Delete();
        }

        public static string FormatOrdinalNumber(this int number)
        {
            if (number == 0) return "0";
            switch (number % 100)
            {
                case 11:
                case 12:
                case 13:
                    return number + "th";
            }
            switch (number % 10)
            {
                case 1: return number + "st";
                case 2: return number + "nd";
                case 3: return number + "rd";
            }
            return number + "th";
        }

        public static string DateFormat(this System.Windows.Forms.DateTimePicker Txt)
        {
            return Txt.Value.ToString("yyyyMMdd");
        }

        public static string DateFormat(this System.DateTime Txt)
        {
            return Txt.ToString("yyyyMMdd");
        }

        public static string CurrFormat(this string Str)
        {
            if (Str.Trim().Length == 0)
            {
                Str = "0";
            }
            return string.Format("{0:0.00}", double.Parse(Str));
        }

        public static string StringEmpty(this string Str)
        {
            if (Str.Trim().Length == 0)
            {
                Str = "0";
            }
            return Str;
        }

        public static string StringEmpty(this System.Windows.Forms.TextBox Str)
        {
            if (Str.Text.Trim().Length == 0)
            {
                Str.Text = "0";
            }
            return Str.Text;
        }

        public static double DoubleParse(this System.Windows.Forms.TextBox Str)
        {
            if (Str.Text.Trim().Length == 0)
            {
                Str.Text = "0";
            }
            return double.Parse(Str.Text);
        }

        public static int IntParse(this string Str)
        {
            if (Str.Trim().Length == 0)
            {
                Str = "0";
            }
            return int.Parse(Str);
        }

        public static double DoubleParse(this string Str)
        {
            if (Str.Trim().Length == 0)
            {
                Str = "0";
            }
            return double.Parse(Str);
        }

        public static string SqlEncode(this string Str)
        {
            return Str.Replace("'", "''").Trim();
        }

        public static string SqlEncode(this System.Windows.Forms.TextBox Txt)
        {
            return Txt.Text.Replace("'", "''").Trim();
        }

        public static int ValueGet(this vControls.vDropDown Txt, string Message)
        {
            int Value = 0;
            if (Txt.SelectedValue == null)
            {
                if (Message.Trim().Length > 0)
                {
                    clsGeneral.ShowMessage(Message);
                } return Value = 0;
            }
            int.TryParse(Txt.SelectedValue.ToString(), out Value);
            if (Value == 0)
            {
                if (Message.Trim().Length > 0)
                {
                    clsGeneral.ShowMessage(Message);
                } return Value = 0;
            }
            return Value;

        }

        public static int ValueGet(this System.Windows.Forms.ComboBox Txt, string Message)
        {
            int Value = 0;
            if (Txt.SelectedValue == null)
            {
                if (Message.Trim().Length > 0)
                {
                    clsGeneral.ShowMessage(Message);
                } return Value = 0;
            }
            int.TryParse(Txt.SelectedValue.ToString(), out Value);
            if (Value == 0)
            {
                if (Message.Trim().Length > 0)
                {
                    clsGeneral.ShowMessage(Message);
                } return Value = 0;
            }
            return Value;

        }
    }
}
