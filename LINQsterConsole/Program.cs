using System; 
using System.Linq;  // "this DOES need a namespace, so... (...so we'll put in this library stmt) -seems like an odd use of the word "namespace""
using CSharp2SqlLibrary;
using System.Collections.Generic;

namespace LINQsterConsole {
  
    class State {
        public string Code { get; set; }
        public string Name { get; set; }
    }


    class Program {

        void Run() {
            // need a connection:
            var Connection = new Connection("localhost\\sqlexpress", "PrsDb");  //  Instead of @, using double backslash "\\" in this project, just for fun       // "conn" in other project?
            Connection.Open();
            Users.Connection = Connection;
            Vendors.Connection = Connection;


            Products.Connection = Connection;

            Console.WriteLine("THIS IS A BIG DEAL:  EXTENSION METHODS.  THE PRINT STATEMENT IS IN THE **OTHER CLASS**  " +
                "  \nNOW DON'T HAVE TO DO THE WORDY  *FOREACH*  BLOCK EVERY SINGLE TIME \n");
            var products = Products.GetAll();
            products.Print();  // generic List class DOES NOT have a print!  The print we're calling is the static extension method
            Console.WriteLine("\n THAT WAS A PRINT METHOD CALLED FROM ANOTHER CLASS. IMPRESSIVE, NO?? \n\n");

            Users myUser = Users.GetByPk(4);
            myUser.PrintFullName();
            Console.WriteLine("-and THAT is an extension method I created myself.  It prints one user's full name. \n\n");



            //JOIN STMT in LINQ.  
            //Note word "equal" spelled out, not = sign
            var prods = from p in Products.GetAll()
                           join v in Vendors.GetAll()
                           on p.VendorId equals v.Id
                           select new {     // this part is diff from sql
                               Product = p.Name, 
                               Price = p.Price,
                               Vendor = v.Name
                           };
            foreach (var p in prods) {
                Console.WriteLine($"{p.Product} priced at {p.Price} is from {p.Vendor}");
            }




            // can link sql arrays to arrays already in my program , with join views
            // joining a sql Db with in-memory collection (in this case a short list of (2) states:  Washington state, and outer space
            var states = new State[] {
                new State() { Code = "WA", Name = "Washington" },
                new State() { Code = "XX", Name = "Outer Space" }
            };
            var vendorsWithState = from v in Vendors.GetAll()
                                   join s in states
                                   on v.State equals s.Code
                                   select new {
                                       Vendor = v.Name,
                                       State = s.Name
                                   };   //.ToList();  --can Tack this on, bring something back to a regular list.  Very useful.
            foreach (var v in vendorsWithState) {
                Console.WriteLine($"Vendor {v.Vendor} is in state {v.State}");
            }





            // can chain  
            // Eexist tons of string methods.  Look at all the available methods in intellisense
            //some string methods, that we can apply to our query:
            // str.Contains("2")    --any user or vendor whose (name) contains the character "2"; 
            // str.StartsWith("ABC")



            //  *******  HERE ARE A BUNCH OF SUCCESSFUL EXAMPLES OF QUERY SYNTAX    *******  

            //method syntax/ query syntax hybrid            //get total of prices of all products
            var totalAllProducts = (from p in Products.GetAll()
                                    select p).Sum(p => p.Price);
            Console.WriteLine($"Total all prices is {totalAllProducts}");


            //100% method syntax:            //get total of prices of all products
            var priceTotal = Products.GetAll().Sum(p => p.Price);
            Console.WriteLine($"Total: {priceTotal}");


            // Remember:  This syntax is not just for sql.  It is for Collections and fixed arrays, too 
            List<string> threeWords = new List<string> { "bar", "Near", "cannibal" };
            var words = from wrd in threeWords
                        where wrd.Contains("ar")
                        orderby wrd descending
                        select wrd[0];
            foreach(var word in words) {
                Console.WriteLine(word);  //output  N  b
            }


            double[] myDoubles = new double[] { 7.7, 9, 100, -8.975 };
            var someDoubles = from dub in myDoubles
                              select dub;
            foreach(var doubl in someDoubles) {
                Console.WriteLine(doubl);
            }


            var vndors = from v in Vendors.GetAll()
                                where v.Code == "MSMS"
                                select v.Name;
            foreach(var ladder in vndors) {
                Console.WriteLine(ladder);
            }


            var vendors = from v in Vendors.GetAll()
                          orderby v.Name
                          select v.Name;
            foreach(var ven in vendors) {
                Console.WriteLine(ven);
            }
            Console.WriteLine();


            var reviewers = from u in Users.GetAll()
                            where u.IsReviewer
                            select u.Lastname;
            foreach(var reviewer in reviewers) {
                Console.WriteLine(reviewer);
            }


            var twoletterses = from u in Users.GetAll()
                               where u.Phone.Length == 12
                               select u.Firstname;
            foreach(var two in twoletterses) {
                Console.WriteLine(two);
            }
            Console.WriteLine();


            var aaas = from u in Users.GetAll()
                         where u.Firstname.Contains("a")
                         select u.Firstname;
            foreach (var aaa in aaas) {
                Console.WriteLine(aaa);
            }

            
            // Types out Margaret's info, because she is my only admininstrator
            var admins = from u in Users.GetAll()
                         where u.IsAdmin
                         select u;
            foreach (var admin in admins) {
                Console.WriteLine(admin);
            }
            
            // Types out "Geoff Hoyle is a nice guy"
            var users = from u in Users.GetAll()
                        where u.Username.Equals("threelegs")        // .Equals(), C# in general, recommended way to compare two strings
                        select u;
            foreach (var usr in users) {
                Console.WriteLine($"{usr.Firstname} {usr.Lastname} is a nice guy");
            }


            var vendPersons = from v in Vendors.GetAll()
                          where v.Email.StartsWith("w")         // .Equals(), C# in general, recommended way to compare two strings
                          select v;
            foreach (var usr in users) {
                Console.WriteLine($"{usr.Firstname} {usr.Lastname} is a nice guy");
            }


            // linq query syntax
            admins = from u in Users.GetAll()
                         where u.Username.Equals("threelegs")
                         select u;
            foreach (var user in admins) {
                Console.WriteLine($"{user.Firstname} {user.Lastname} is an admin");
            }



            Connection.Close();
        }


        static void Main(string[] args) {
            var pgm = new Program();
            pgm.Run();
        }
    }
}
