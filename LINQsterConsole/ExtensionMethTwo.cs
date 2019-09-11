using System;
using System.Collections.Generic;
using System.Text;
using CSharp2SqlLibrary;


namespace LINQsterConsole {
    static class ExtensionMethTwo {

        public static void PrintFullName(this Users user) {
            Console.WriteLine($"{user.Firstname} {user.Lastname}");
        }

    }
}
