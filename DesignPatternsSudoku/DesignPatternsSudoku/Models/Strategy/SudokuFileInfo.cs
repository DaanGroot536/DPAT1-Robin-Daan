namespace DesignPatternsSudoku.Models.Strategy
{
    public class SudokuFileInfo
    {
        public string Content { get; set; }
        public int ClusterHeight { get; set; }
        public int ClusterWidth { get; set; }
        public int Size { get; set; }
        public string FileExtension { get; set; }

        public SudokuFileInfo(string content)
        {
            Content = content;
        }
    }
}
