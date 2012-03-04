using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace IExtendFramework
{
    /// <summary>
    /// Serializes an object. The object must have the Serializable attribute.
    /// </summary>
    public class Serializer
    {
        public static void Serialize(object obj, string filename)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, obj);
            stream.Close();
        }
        
        public static object Deserialize(string filename)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filename, FileMode.Open);
            object o = formatter.Deserialize(stream);
            stream.Close();
            return o;
        }
        
        public static T Deserialize<T>(string filename)
        {
            return (T)Serializer.Deserialize(filename);
        }
        
        public static T DeserializeObject<T>(byte[] data)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream(data);
            object o = formatter.Deserialize(stream);
            stream.Close();
            return (T)o;
        }
        
        public static byte[] SerializeObject(object o)
        {
            IFormatter f = new BinaryFormatter();
            Stream s = new MemoryStream();
            f.Serialize(s, o);
            s.Seek(0, SeekOrigin.Begin);
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            return b;
        }
    }
}
