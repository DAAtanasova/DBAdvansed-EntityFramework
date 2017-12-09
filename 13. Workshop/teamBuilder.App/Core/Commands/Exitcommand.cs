using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Utilities;

namespace TeamBuilder.App.Core.Commands
{
    public class Exitcommand
    {
        public static string Execute(string[] data)
        {
            Check.CheckLenght(0, data);
            Environment.Exit(0);
            return "Bye!";
        }
    }
}
