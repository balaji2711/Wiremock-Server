using Newtonsoft.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

class WiremockSample
{
    static void Main(string[] args)
    {
        Console.WriteLine("Started");
        int port = 8082;

        var server = WireMockServer.Start(port);
        Console.WriteLine("WireMockServer running at {0}", string.Join(",", server.Ports));
        server
            .Given(Request.Create().WithPath("/*").UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(@"{ ""msg"": ""Hello Balaji!""}")
            );

        server
            .Given(Request.Create().WithPath("/data").UsingPost())
            .RespondWith(Response.Create()
                .WithStatusCode(201)
                .WithHeader("Content-Type", "application/json")
                .WithBody(@"{ ""result"": ""Post Request""}"));

        server
            .Given(Request.Create().WithPath("/data").UsingPut())
            .RespondWith(Response.Create()
                .WithStatusCode(204)
                .WithHeader("Content-Type", "application/json")
                .WithBody(@"{ ""result"": ""Put Request""}"));

        server
            .Given(Request.Create().WithPath("/data").UsingDelete())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(@"{ ""result"": ""Delete Request""}"));

        Console.WriteLine("Press any key to stop the server");
        Console.ReadKey();

        Console.WriteLine("Displaying all requests");
        var allRequests = server.LogEntries;
        Console.WriteLine(JsonConvert.SerializeObject(allRequests, Formatting.Indented));

        Console.WriteLine("Press any key to quit");
        Console.ReadKey();
    }
}