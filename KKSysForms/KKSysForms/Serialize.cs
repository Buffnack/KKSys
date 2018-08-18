using System;

using KKSysForms_CardModel;
using KKSysForms_Event;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KKSysForms_SerializeBoundModul
{
    static class Serialize
    {
        static MemoryStream ms;

        static BinaryFormatter bf = new BinaryFormatter();

        private static void KillMemoryStream ()
        {
            ms.Close();
            ms.Dispose();
        }

        public static Object GetDeserializeObject(byte[] data) 
        {
            KillMemoryStream();

            ms = new MemoryStream(data);
            return bf.Deserialize(ms);
        }




        public static byte[] GetSerializeByte(ContentCard card)
        {
            KillMemoryStream();
            ms = new MemoryStream();
            bf.Serialize(ms, card);
            return ms.ToArray();
        }

        public static byte[] GetSerializeByte(QACard card)
        {
            KillMemoryStream();
            ms = new MemoryStream();
            bf.Serialize(ms, card);
            return ms.ToArray();
        }

        public static byte[] GetSerializeByte(RepeatEvent @event)
        {
            KillMemoryStream();
            ms = new MemoryStream();
            bf.Serialize(ms, @event);
            return ms.ToArray();
        }

        public static byte[] GetSerializeByte(NonRepeatingEvents @event)
        {
            KillMemoryStream();
            ms = new MemoryStream();
            bf.Serialize(ms, @event);
            return ms.ToArray();
        }

        public static byte[] GetSerializeByte(ReferencedOneTimeEvent @event)
        {
            KillMemoryStream();
            ms = new MemoryStream();
            bf.Serialize(ms, @event);
            return ms.ToArray();
        }

        public static byte[] GetSerializeByte(NonReferencedOneTimeEvent @event)
        {
            KillMemoryStream();
            ms = new MemoryStream();
            bf.Serialize(ms, @event);
            return ms.ToArray();
        }


    }
}
