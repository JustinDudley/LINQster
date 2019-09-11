using System;
using System.Collections.Generic;
using System.Text;
using CSharp2SqlLibrary;

namespace LINQsterConsole {
    static class ExtensionMethods {

        public static void Print(this List<Products> products) {

            foreach (var product in products) {
                Console.WriteLine(product);   // Note: Curly braces are optional IF the body of a foreach statement is only one line long. Could omit. 
            } 
        }


        // An alternate possibility, to just work on a single product:
        // public static void PrintBrief(this Products product) {
        //     Console.WriteLine($"Product name is {product.Name}");
        // }

    }
}
    //Print(Products)  We could do it that way, ????instead of using the this keyword????, but then we couldn't do the chaining we want to do.