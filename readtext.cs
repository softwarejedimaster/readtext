/*
readtext.cs

Written Feb 11, 2021 by Tony Scarcia

C# program to be compiled with Visual Studio 2019. 
this program manuipulates array string data from an input text file and
writes the data to output text files.  The created .reg files are then
used, after performing a merge, to auto-login a user on a Windows PC.
The created files are written in the current direcctory where thg .dat file
is located. 
*/

using System;
using System.IO;

namespace readtext
{
    class Readtext
    {
        private static void Main()
        {
            int i;
            string[] username;
            string[] password;
            username = new string[1024];
            password = new string[1024];
            if (File.Exists("test_pcs.dat"))
            {
                string[] data = File.ReadAllText("test_pcs.dat").Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                for (i = 0; i < data.Length / 2; i++)
                {
                    username[i] = data[i * 2];
                    password[i] = data[i * 2 + 1];
                }
                for (i = 0; i < data.Length / 2; i++)
                {
                    using StreamWriter writetext = new StreamWriter(username[i] + ".reg");
                    writetext.WriteLine("Windows Registry Editor Version 5.00");
                    writetext.WriteLine();
                    writetext.WriteLine("[HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon]");
                    writetext.WriteLine("\"AutoAdminLogon\"=\"1\"");
                    writetext.WriteLine("\"DefaultUserName\"=\"{0}\"", username[i]);
                    writetext.WriteLine("\"DefaultDomainName\"=\"Carel.locale\"");
                    writetext.WriteLine("\"DefaultPassword\"=\"{0}\"", password[i]);
                }
            }
            else
            {
                Console.WriteLine("'test_pcs.dat' not found in current directory...\n");
            }
        }
    }
}