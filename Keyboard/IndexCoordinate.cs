namespace Keyboard
{
    public class IndexCoordinate
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public IndexCoordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}