using System;
using System.Collections.Generic;
using Exercise.Games.DomainEntities.GameOfLife;

namespace Exercise.Games.BusinessLayer
{
    public class Helper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcGrid"></param>
        public static ICollection<Row> ArrayToRows(int[,] srcGrid)
        {
            LifeGrid _life = new LifeGrid();
            _life.GridRows = new List<Row>();

            var row = new Row();
            var rows = new List<Row>();
            var cell = new Cell();
            row.Cells = new List<Cell>();

            for (int y = 0; y < srcGrid.GetLength(1); y++)
            {
                row = new Row();
                row.Cells = new List<Cell>();
                for (int x = 0; x < srcGrid.GetLength(0); x++)
                {
                    cell = new Cell();
                    cell.CoordinateY = y;
                    cell.CoordinateX = x;
                    cell.IsAlive = (srcGrid[y, x] == 1);
                    row.Cells.Add(cell);
                    cell.Value = srcGrid[y, x];
                }
                rows.Add(row);
            }

            return rows;
        }

        /// <summary>
        /// Method for Converting LifeGrid Rows to 2D Array
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static int[,] RowsToArray(List<Row> rows)
        {
            int[,] srcArray = new int[,] { 
            { 0,0,0,0,0,0,0,0} ,
            { 0,0,0,0,0,0,0,0} , 
            { 0,0,0,0,0,0,0,0} , 
            { 0,0,0,0,0,0,0,0} , 
            { 0,0,0,0,0,0,0,0} , 
            { 0,0,0,0,0,0,0,0} , 
            { 0,0,0,0,0,0,0,0} , 
            { 0,0,0,0,0,0,0,0} ,
            };

            foreach (var row in rows)
            {
                foreach (var cell in row.Cells)
                {
                    srcArray.Initialize();
                    srcArray[cell.CoordinateY, cell.CoordinateX] = cell.Value;
                }
            }
            return srcArray;
        }

        /// <summary>
        /// Method for Printing LifeGrid Rows
        /// </summary>
        /// <param name="rows"></param>
        public static void PrintRows(List<Row> rows)
        {

            foreach (var row in rows)
            {
                foreach (var cell in row.Cells)
                {
                    Console.Write(cell.Value);
                }
                Console.WriteLine();
            }
        }
    }
}
