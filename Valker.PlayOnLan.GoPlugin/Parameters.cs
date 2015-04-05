using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Valker.PlayOnLan.GoPlugin
{
    public class Parameters
    {
        public override string ToString()
        {
            var sb = new StringBuilder();
            using(var sw = new StringWriter(sb, CultureInfo.InvariantCulture))
            {
                Serializer.Serialize(sw, this);
            }

            return sb.ToString();
        }

        public static Parameters Parse(string value)
        {
            using (var sr = new StringReader(value))
            {
                return (Parameters) Serializer.Deserialize(sr);
            }
        }

        static readonly XmlSerializer Serializer = new XmlSerializer(typeof(Parameters));
        public int Width { get; set; }
    }
}
