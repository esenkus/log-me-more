using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace log_me_more.Services;

public class Scanner : StringReader {
    public Scanner(string s) : base(s) { }

    public T readNext<T>() where T : IConvertible {
        var sb = new StringBuilder();

        do {
            var current = Read();
            if (current < 0) {
                break;
            }

            sb.Append((char)current);

            var next = (char)Peek();
            if (char.IsWhiteSpace(next)) {
                break;
            }
        } while (true);

        var value = sb.ToString().Trim();
        if (value.Length == 0) {
            return readNext<T>();
        }

        var type = typeof(T);
        if (type.IsEnum) {
            return (T)Enum.Parse(type, value);
        }

        return (T)((IConvertible)value).ToType(typeof(T), CultureInfo.CurrentCulture);
    }
}