using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Puzzles;
using DesignPatternsSudoku.Models.Strategy;

namespace DesignPatternsSudoku.Models.Builder
{
    public class JigsawBuilder : Builder
    {
        public JigsawBuilder(SudokuFileInfo fileInfo) : base(fileInfo)
        {
            CreateGrid(fileInfo.Content);
            CreateClusters();
            SetPossibleNumbers(_puzzle);
        }

        override
        public void CreateGrid(string input)
        {
            string filteredInput = input.Replace("SumoCueV1=","");
            string[] stringArray = filteredInput.Split("=");
            Cluster[] clusters = new Cluster[9] { new Cluster(), new Cluster(), new Cluster(), new Cluster(), new Cluster(), new Cluster(), new Cluster(), new Cluster(), new Cluster() };

            for (int i = 0; i < stringArray.Length; i++)
            {
                int row = i / _fileInfo.Size;
                int col = i % _fileInfo.Size;
                Cell cell = new Cell(
                    new Coord(row, col),
                    int.Parse(stringArray[i].Split("J")[0].ToString()),
                    Enumerable.Range(1, _fileInfo.Size).ToList()
                );
                clusters[int.Parse(stringArray[i].Split("J")[1].ToString())].Add(cell);
                cell.AddCluster(clusters[int.Parse(stringArray[i].Split("J")[1].ToString())]);
                _grid[row, col] = cell;
            }
            foreach (var cluster in clusters)
            {
                _puzzle.Add(cluster);
            }
        }
        override
        public void CreateClusters()
        {
            for (int x = 0; x < _fileInfo.Size; x++)
            {
                Cluster row = new Cluster();
                Cluster col = new Cluster();
                for (int y = 0; y < _fileInfo.Size; y++)
                {
                    Cell rowCell = _grid[x, y];
                    Cell colCell = _grid[y, x];

                    rowCell.AddCluster(row);
                    colCell.AddCluster(col);

                    row.Add(rowCell);
                    col.Add(colCell);
                }
                _puzzle.Add(row);
                _puzzle.Add(col);
            }
        }
    }
}
