using System;
using System.Collections.Generic;

namespace Exercise.Games.DomainEntities.GameOfLife
{
    public class LifeGrid : InputResources
    {
        public LifeGrid()
        {
            GridRows = new List<Row>();
            Grid = new int[,] { { } };
            MaxIterations = 5;
            ChangeLog = new List<String>();
            ShowChangeLog = true;
        }
        public int Iteration { get; set; }
        public int MaxIterations { get; set; }
        public int[,] Grid { get; set; }
        public List<Row> GridRows { get; set; }
        public bool ShowChangeLog { get; set; }
        public List<String> ChangeLog { get; set; }


    }
}
