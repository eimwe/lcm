using System.Numerics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var route = Environment.GetEnvironmentVariable("ROUTE") ?? "/";

app.MapGet(route, (HttpContext context) =>
{
    var query = context.Request.Query;

    if (!query.ContainsKey("x") || !query.ContainsKey("y"))
        return Results.Ok("NaN");

    string xStr = query["x"].ToString();
    string yStr = query["y"].ToString();

    if (!BigInteger.TryParse(xStr, out BigInteger x) ||
        !BigInteger.TryParse(yStr, out BigInteger y) ||
        x <= 0 || y <= 0)
    {
        return Results.Ok("NaN");
    }

    BigInteger gcd = BigInteger.GreatestCommonDivisor(x, y);
    BigInteger lcm = x / gcd * y;

    return Results.Text(lcm.ToString());
});

app.Run();