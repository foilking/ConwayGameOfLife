public class Grid
{
    public Cell[,] CellGrid { get; set; }

    public int Rows { get; set; }
    public int Columns { get; set; }

    public Grid(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        CellGrid = new Cell[Rows, Columns];
    }
}

public static class GridExtensions
{
    public static Cell UpdateCellStatus(this Grid grid, Cell cell)
    {
        var newCell = new Cell
        {
            Row = cell.Row,
            Column = cell.Column,
            Status = Status.Dead
        };

        // check alive neighbors
        var aliveNeighbors = 0;
        for (var row = -1; row <= 1; row++)
        {
            for (var column = -1; column <= 1; column++)
            {
                if (row == 0 && column == 0) continue;

                if (cell.Row + row < 0
                    || cell.Row + row >= grid.Rows
                    || cell.Column + column < 0
                    || cell.Column + column >= grid.Columns) continue;

                aliveNeighbors += grid.CellGrid[cell.Row + row, cell.Column + column].Status == Status.Alive ? 1 : 0;
            }
        }
        
        if (aliveNeighbors == 3)
        {
            newCell.Status = Status.Alive;
        }
        else if (aliveNeighbors == 2 && cell.Status == Status.Alive)
        {
            newCell.Status = Status.Alive;
        }

        return newCell;
    }
    
    public static Grid TickGrid(this Grid currentGrid)
    {
        var nextGrid = new Grid(currentGrid.Rows, currentGrid.Columns);

        for (int rows = 0; rows < currentGrid.Rows; rows++)
        {
            for (int columns = 0; columns < currentGrid.Columns; columns++)
            {
                var currentCell = currentGrid.CellGrid[rows, columns];
                nextGrid.CellGrid[rows, columns] = currentGrid.UpdateCellStatus(currentCell);
            }
        }

        return nextGrid;
    }
}