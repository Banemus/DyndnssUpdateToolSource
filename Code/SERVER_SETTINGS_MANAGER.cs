using System.IO;
using System.Net;
using System.Collections.Generic;
using System;

public class SERVER_SETTING_MANAGER
{
    // -- PUBLIC

    // .. TYPES

    public enum EVENT
    {
        Error = 0,
        Fetch = 1
    } 

    // .. CONSTRUCTORS

    public SERVER_SETTING_MANAGER()  
    {
        using ( MemoryStream file_stream = new MemoryStream( File.ReadAllBytes( "Settings/ServerSettings.xml" ) ) )
        {
            EXTENSION_METHODS_XML_SERIALIZER.Deserialize( file_stream, SettingsMap );
        }
    }

    // .. OPERATIONS

    // .. INQUIRIES

    public bool TryGetSetting(
        string key,
        out object value
        )
    {
        bool
            result;

        result = SettingsMap.TryGetValue( key, out value );

        if ( !result )
        {
            Console.WriteLine( key + " this key is not found in settings" );
        }

        return result;
    }

    // ~~

    public bool TryGetSetting(
        string key,
        out float value
        )
    {
        object
            value_object;
        bool
            result;

        result = TryGetSetting( key, out value_object );

        value = 0.0f;

        if ( result )
        {
            value = ( float ) value_object;
        }

        return result;
    }

    // ~~

    public bool TryGetSetting(
        string key,
        out int value
        )
    {
        object
            value_object;
        bool
            result;

        result = TryGetSetting( key, out value_object );

        value = 0;

        if ( result )
        {
            value = ( int ) value_object;
        }

        return result;
    }

    // ~~

    public bool TryGetSetting(
        string key,
        out string value
        )
    {
        object
            value_object;
        bool
            result;

        result = TryGetSetting( key, out value_object );

        value = string.Empty;

        if ( result )
        {
            value = ( string ) value_object;
        }

        return result;
    }

    // .. ATTRIBUTES

    Dictionary<string, object>
        SettingsMap = new Dictionary<string, object>();
}
