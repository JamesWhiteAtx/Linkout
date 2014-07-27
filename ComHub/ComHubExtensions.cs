using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace ComHub
{
    /// partner = Roadwire
    /// vendor = Roadwire
    /// merchant = costco

    public class MessageBatch
    {
        protected string xsdFile;
        public const string Costco = "Costco";

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public string XsdFile { get { return xsdFile; } }

        public static T Deserialize<T>(string path)
        {
            XDocument doc = XDocument.Load(path);
            return SerializationUtil.Deserialize<T>(doc);
        }

        public XDocument Serialize()
        {
            Type t = this.GetType();
            return SerializationUtil.Serialize(t, this);
        }

        public void ValidateXml()
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string xsdFilePath = Path.Combine(path, xsdFile);

            SerializationUtil.ValidateXml(Serialize(), xsdFilePath);
        }

        public FileInfo SaveFile(string path)
        {
            XDocument doc = Serialize();
            doc.Save(path);
            return new FileInfo(path);
        }
    }

}
