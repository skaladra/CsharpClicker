namespace CSharpClicker.Web.Domain;

public class Boost
{
    public int Id { get; set; }

    public string Title { get; set; }

    public long Price { get; set; }

    public long Profit { get; set; }

    public byte[] Image { get; set; } 

    public bool IsAuto { get; set; }
}
