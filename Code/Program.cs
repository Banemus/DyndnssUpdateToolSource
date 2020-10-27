using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;

class UPDATE_DATA
{
    // -- PUBLIC

    // .. ATTRIBUTES

    public string
        UserName,
        Password,
        Domain;
}

class Program
{
    static void Main(
        string[] args
        )
    {
        SERVER_SETTING_MANAGER
            server_setting_manager;
        List<UPDATE_DATA>
            update_data_table;

        update_data_table = new List<UPDATE_DATA>();
        server_setting_manager = new SERVER_SETTING_MANAGER();

        if ( !server_setting_manager.TryGetSetting( "Delay", out int delay_minutes ) )
        {
            Console.WriteLine( "Missing \"Delay\" setting in the server_setting file" );
            Console.ReadKey();
            return;
        }

        while( server_setting_manager.TryGetSetting( "Domain_"+ update_data_table.Count, out string domain ) &&
            server_setting_manager.TryGetSetting( "Username_" + update_data_table.Count, out string user_name ) &&
            server_setting_manager.TryGetSetting( "Password_" + update_data_table.Count, out string password )
            )
        {
            update_data_table.Add( 
                new UPDATE_DATA 
                {
                    Domain = domain,
                    UserName = user_name,
                    Password = password
                }
            );
        }

        if ( update_data_table.Count > 0 )
        {
            bool
                run;

            run = true;

            new Timer(
                ( state ) =>
                {
                    using ( WebClient client = new WebClient() )
                    {
                        foreach ( UPDATE_DATA data in update_data_table )
                        {
                            client.DownloadString( "https://dyndnss.net/?user=" + data.UserName + "&pass="+ data.Password+"&domain="+data.Domain+"&updater=other" );
                            Console.WriteLine( "Updated: with user_name " + data.UserName + " domain: " + data.Domain );
                        }
                    }
                }, null, new TimeSpan( 0, 0, 1 ), new TimeSpan( 0, 0, delay_minutes, 0 )
            );

            while ( run )
            {
                switch ( Console.ReadLine() )
                {
                    case "quit":
                    {
                        run = false;
                    }
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine(
                "No domain data found in server_settings file" 
                );
            Console.ReadKey();
        }


    }
}
