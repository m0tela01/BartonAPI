using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Barton1792DB.DBO
{
    [TypeConverter(typeof(TemplateConverter))]
    public class Template
    {
        public string JobName { get; set; }
        public string DepartmentName { get; set; }
        public int Shift1 { get; set; }
        public int Shift2 { get; set; }
        public int Shift3 { get; set; }

        public static bool TryParse(string s, out Template result)
        {
            result = null;
            var parts = s.Split(',');
            if (parts.Length != 5)
            {
                return false;
            }
            string jobName = "";
            string departmentName = "";
            int shift1 = 0;
            int shift2 = 0;
            int shift3 = 0;
            if (!string.IsNullOrWhiteSpace(parts[0].ToString()) && !string.IsNullOrWhiteSpace(parts[1].ToString())
                && int.TryParse(parts[2], out shift1) && int.TryParse(parts[3], out shift2) && int.TryParse(parts[4], out shift3))
            {
                result = new Template()
                {
                    JobName = parts[0].ToString(),
                    DepartmentName = parts[1].ToString(),
                    Shift1 = shift1,
                    Shift2 = shift2,
                    Shift3 = shift3
                };
                return true;
            }
            return false;
        }
    }

    class TemplateConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                Template temp = new Template();
                if (Template.TryParse((string)value, out temp))
                {
                    return temp;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
