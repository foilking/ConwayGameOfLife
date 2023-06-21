public class Cell
{
    public Status Status { get; set; }
    public int Row { get; init; }
    public int Column { get; init; }
}

public enum Status
{
    Alive,
    Dead
}