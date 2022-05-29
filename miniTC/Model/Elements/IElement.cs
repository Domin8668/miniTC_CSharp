namespace miniTC.Model
{
    public interface IElement
    {
        string Name
        { get; set; }

        string Path
        { get; set; }

        string Type
        { get; set; }

        string ToString();
    }
}

