/**
 * Why would I make my own logger? Well because I just spent 10 minutes trying to create a
 * new instance of a logger and it requires some sort of weird Microsoft witchcraft instead 
 * of just new Logger(), these people are on drugs, I'm only using this language because I need
 * it for the library, I suggest using anything else more sane, never use this language/ecosystem
 * unless you absolutely have to.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nvidia_settings_cli
{
    class Logger 
    {
        public static bool DEBUG;

        public static void Debug(string message) {
            if (DEBUG) {
                Console.WriteLine(message);
            }
        }
    }
}
