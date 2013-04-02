using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercise.Games.DomainEntities.GameOfLife;

namespace Exercise.Games.BusinessLayer
{

    public class GameRepository
    {

        /// <summary>
        /// Method to Generate Random Rows
        /// </summary>
        public static List<Row> GetSampleLifeGrid()
        {
            LifeGrid _life = new LifeGrid();
            _life.GridRows = new List<Row>();

            Random rVal = new Random();

            var height = rVal.Next(5, 8);
            var width = rVal.Next(5, 5);

            var row = new Row();
            var rows = new List<Row>();
            var cell = new Cell();
            row.Cells = new List<Cell>();

            for (int y = 0; y < height; y++)
            {
                row = new Row();
                row.Cells = new List<Cell>();
                for (int x = 0; x < width; x++)
                {
                    cell = new Cell();
                    cell.CoordinateY = y;
                    cell.CoordinateX = x;
                    cell.Value = rVal.Next(100) % 2;
                    cell.IsAlive = (cell.Value == 1);
                    row.Cells.Add(cell);
                }
                rows.Add(row);
            }
            return rows;
        }
    }
}
