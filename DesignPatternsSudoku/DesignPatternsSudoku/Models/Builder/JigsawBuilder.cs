using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Puzzles;
using DesignPatternsSudoku.Models.Strategy;

namespace DesignPatternsSudoku.Models.Builder
{
    public class JigsawBuilder : NormalBuilder
    {
        public JigsawBuilder(SudokuFileInfo fileInfo) : base(fileInfo)
        {
        }

        public override void InitializeGrid(string input)
        {
            string filteredInput = input.Replace("SumoCueV1=", "");
            string[] stringArray = filteredInput.Split("=");
            Cluster[] clusters = new Cluster[9]
            {
                new Cluster(), new Cluster(), new Cluster(), new Cluster(),
                new Cluster(), new Cluster(), new Cluster(), new Cluster(), new Cluster()
            };

            for (int i = 0; i < stringArray.Length; i++)
            {
                int row = i / FileInfo.Size;
                int col = i % FileInfo.Size;
                Cell cell = new Cell(
                    new Coord(row, col),
                    int.Parse(stringArray[i].Split("J")[0]),
                    Enumerable.Range(1, FileInfo.Size).ToList()
                );
                int clusterIndex = int.Parse(stringArray[i].Split("J")[1]);
                clusters[clusterIndex].Add(cell);
                cell.AddCluster(clusters[clusterIndex]);
                Grid[row, col] = cell;
            }
            foreach (var cluster in clusters)
            {
                PuzzleInstance.Add(cluster);
            }
        }

        public override void InitializeClusters()
        {
            for (int x = 0; x < FileInfo.Size; x++)
            {
                Cluster rowCluster = new Cluster();
                Cluster colCluster = new Cluster();
                for (int y = 0; y < FileInfo.Size; y++)
                {
                    Cell rowCell = Grid[x, y];
                    Cell colCell = Grid[y, x];

                    rowCell.AddCluster(rowCluster);
                    colCell.AddCluster(colCluster);

                    rowCluster.Add(rowCell);
                    colCluster.Add(colCell);
                }
                PuzzleInstance.Add(rowCluster);
                PuzzleInstance.Add(colCluster);
            }
        }
    }
}
