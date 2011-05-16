<Query Kind="Statements" />

var guid = Guid.NewGuid();
Console.WriteLine(guid.ToString());
Console.WriteLine(guid.ToString("N"));
Console.WriteLine(guid.ToString("D"));
Console.WriteLine(guid.ToString("B"));
Console.WriteLine(guid.ToString("P"));
Console.WriteLine(guid.ToString("X"));

var uri = new Uri("http://no.real.address/parent/child/content.html#search");
Console.WriteLine("uri.AbsolutePath: " + uri.AbsolutePath);
Console.WriteLine("uri.AbsoluteUri: " + uri.AbsoluteUri);
Console.WriteLine("uri.Fragment: " + uri.Fragment);
Console.WriteLine("uri.Host: " + uri.Host);