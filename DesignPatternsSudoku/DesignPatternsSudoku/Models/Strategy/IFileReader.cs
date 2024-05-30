namespace DesignPatternsSudoku.Models.Strategy
{
    public interface IFileReader
    {
        SudokuFileInfo ReadFile(string path);
    }
}
