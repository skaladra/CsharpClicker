namespace CSharpClicker.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHealthChecks();
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");
            app.MapHealthChecks("health-check");

            app.Run();
        }
    }
}
