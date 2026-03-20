using MongoDB.Bson;
using MongoDB.Driver;


namespace ListTodos
{

    interface IClient
    {
        private readonly static int id;
        private readonly static string? URI;

        public static string? GetClientData(string URI) => URI;
    }

   
    static class GetStuff
    {
        extension(string s)
        {
            public static bool IsRunningWorker() => true;
        }
    }

    class ClientDB : IClient 
    {
        private readonly static int id;
        private readonly static string? URI;

        public static string? GetClientData(string URI)
        {
            return URI;
        }
    }

    static class Program
    {
        static string GetURI()
        {
            return "mongodb://localhost:27017";
        }

        extension(string s)
        {
            public static string IsTodo(string msg) => msg.ToUpper();
            public static bool IsPlaced() => true; 
        };

        public static string UpperTextString(string s)
        {
            return s.ToUpper();
        }

        public static void MessagesPrinted(string s)
        {
            Console.WriteLine(UpperTextString(s));
        }

        public static async Task<Task> Main(string[] _)
        {

          
            while (GetStuff.IsRunningWorker())
            {
                if (IsPlaced())
                {
                    var client = new MongoClient(GetURI());

                    string s = IsTodo("RJ");

                    MessagesPrinted("Enter First Todo: ");
                    string todo1 = Console.ReadLine() ?? "";

                    MessagesPrinted("Enter Second Todo: ");
                    string todo2 = Console.ReadLine() ?? "";

                    List<string> todosListed = new List<string>();
                    todosListed.Add(todo1);
                    todosListed.Add(todo2);

                    foreach (var todo in todosListed)
                    {


                        var db = client.GetDatabase("todos");
                        var coll = db.GetCollection<BsonDocument>("todo");

                        var data = new BsonDocument
                        {
                            { "data", $"{todo}" }
                        };

                        var options = new InsertOneOptions() { BypassDocumentValidation = true };

                        coll.InsertOne(data, options);


                    }

                    ValidPrint(s);

                    Console.ReadLine();

                }
            }


            return Task.Delay(3);
        }

        public static void ValidPrint(string s)
        {
            Console.WriteLine(s);
        }
    }
}