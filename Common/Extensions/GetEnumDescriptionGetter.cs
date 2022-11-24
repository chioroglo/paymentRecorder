using System.ComponentModel;
using System.Reflection;

namespace Common.Extensions;

public static class GetEnumDescriptionGetter
{
    public static string GetEnumDescription(this Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
}