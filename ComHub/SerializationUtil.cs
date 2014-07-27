using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace ComHub
{
    public static class SerializationUtil
    {
        public static T Deserialize<T>(XDocument doc)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (var reader = doc.Root.CreateReader())
            {
                return (T)xmlSerializer.Deserialize(reader);
            }
        }

        public static XDocument Serialize<T>(Type t, T value)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(t);

            XDocument doc = new XDocument();
            using (var writer = doc.CreateWriter())
            {
                xmlSerializer.Serialize(writer, value);
            }

            return doc;
        }

        public static void ValidateXml(XDocument doc, string schemaFileName)
        {
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(null, schemaFileName);

            string msg = "";
            doc.Validate(schemas, (o, e) =>
            {
                msg += e.Message + Environment.NewLine;
            });

            if (msg != "")
            {
                throw new Exception("Document is invalid: " + msg);
            }
            //Console.WriteLine(msg == "" ? "Document is valid" : "Document invalid: " + msg);
        }

    }
}
