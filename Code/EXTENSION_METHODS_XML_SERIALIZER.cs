using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public static class EXTENSION_METHODS_XML_SERIALIZER
{
    // -- PUBLIC

    // .. TYPES

    public class Entry
    {
        // -- PUBLIC

        // .. CONSTRUCTORS

        public Entry()
        {
        }

        // ~~

        public Entry(
            object key,
            object value
            )
        {
            Key = key;
            Value = value;
        }

        // .. ATTRIBUTES

        public object
            Key,
            Value;
    }

    // .. EXTENSION_METHODS

    public static void Serialize(
        TextWriter writer,
        IDictionary dictionary
        )
    {
        List<Entry>
            entries = new List<Entry>( dictionary.Count );

        foreach ( object key in dictionary.Keys )
        {
            entries.Add( new Entry( key, dictionary[ key ] ) );
        }

        XmlSerializer
            serializer = new XmlSerializer( typeof( List<Entry> ) );

        serializer.Serialize( writer, entries );
    }

    // ~~

    public static void Deserialize(
        TextReader reader,
        IDictionary dictionary
        )
    {
        XmlSerializer
            serializer;
        List<Entry>
            list;

        dictionary.Clear();

        serializer = new XmlSerializer( typeof( List<Entry> ) );

        list = ( List<Entry> ) serializer.Deserialize( reader );

        foreach ( Entry entry in list )
        {
            dictionary[ entry.Key ] = entry.Value;
        }
    }

    // ~~

    public static void Deserialize(
        Stream stream,
        IDictionary dictionary
        )
    {
        XmlSerializer
            serializer;
        List<Entry>
            list;
          
        dictionary.Clear(); 

        serializer = new XmlSerializer( typeof( List<Entry> ) );

        list = ( List<Entry> ) serializer.Deserialize( stream );

        foreach ( Entry entry in list )
        {
            dictionary[ entry.Key ] = entry.Value;
        }
    }
}
