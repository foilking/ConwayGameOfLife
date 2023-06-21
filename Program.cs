using System;
using System.Linq;
using System.Threading;

var runContinuously = false;
if (args.Any())
{
    Boolean.TryParse(args?[0] ?? "false", out runContinuously);
}

Grid CreateRandomGrid(int rows, int columns)
{
    var grid = new Grid(rows, columns);
    var random = new Random();
    for (int row = 0; row < rows; row++)
    {
        for (int column = 0; column < columns; column++)
        {
            grid.CellGrid[row, column] = new Cell
            {
                Row = row,
                Column = column,
                Status = random.Next(0, 2) == 1 ? Status.Alive : Status.Dead
            };
        }
    }
    return grid;
}

void PrintGrid(Grid grid, int timeout = 0)
{
    for (int rows = 0; rows < grid.Rows; rows++)
    {
        for (int columns = 0; columns < grid.Columns; columns++)
        {
            var currentCell = grid.CellGrid[rows, columns];
            var output = " ";
            switch (currentCell.Status) {
                case Status.Alive:
                    output = "O";
                    Console.ForegroundColor = ConsoleColor.Green;
                break;
                case Status.Dead:
                    output = "X";
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    break;
            }
            Console.Write(output);
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("\n");
    }
    Thread.Sleep(timeout);
}
var grid = CreateRandomGrid(10, 10);

var continueLoop = true;

Console.CancelKeyPress += (sender, args) =>
{
    continueLoop = false;
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("\nEnding simulation.");
};

while (continueLoop)
{
    PrintGrid(grid, runContinuously ? 500 : default);
    grid = grid.TickGrid();
    if (!runContinuously)
    {
        Console.WriteLine("Run again? Y/N");
        var end = Console.ReadLine();
        continueLoop = end?.Equals("Y", StringComparison.InvariantCultureIgnoreCase) ?? false;
    }
    else
    {
        Console.Clear();
    }
}