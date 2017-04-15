using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EasyBus.Shared.Helpers
{

    public static class SerializationHelper
    {
        public static byte[] SerializeObject(Type t, object target)
        {
            XmlSerializer formatter = new XmlSerializer(t);
            MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, target);
            ms.Position = 0;
            return ms.ToArray();
        }

        public static string SerializeObjectAsXml(Type t, object target)
        {
            byte[] value = SerializeObject(t, target);
            return Encoding.UTF8.GetString(value);
        }

        public static object DeSerializeObject(Type objectType, byte[] jsonString)
        {
            MemoryStream ms = new MemoryStream(jsonString);
            ms.Position = 0;
            XmlSerializer serializer = new XmlSerializer(objectType);
            return serializer.Deserialize(ms);
        }

        public static byte[] SerializeObjectAsZip(Type t, object target)
        {
            XmlSerializer formatter = new XmlSerializer(t);
            MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, target);
            ms.Position = 0;
            MemoryStream zipStream = new MemoryStream();
            GZipStream zip = new GZipStream(zipStream, CompressionMode.Compress);
            ms.CopyTo(zip);
            zip.Close();
            return zipStream.ToArray();
        }

        public static object DeSerializeObjectFromZip(Type t, byte[] buffer)
        {
            XmlSerializer formatter = new XmlSerializer(t);
            MemoryStream ms = new MemoryStream(buffer);
            ms.Position = 0;
            MemoryStream zipStream = new MemoryStream();
            GZipStream zip = new GZipStream(ms, CompressionMode.Decompress);
            zip.CopyTo(zipStream);
            zip.Close();
            zipStream.Position = 0;
            return formatter.Deserialize(zipStream);
        }

        public static object DeSerializeObjectAsJSON(Type objectType, byte[] jsonString)
        {
            throw new NotImplementedException();
        }

        public static byte[] SerializeObjectAsJSON(Type objectType, object target)
        {
            throw new NotImplementedException();
        }
    }


}

