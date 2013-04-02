using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Games.DomainEntities.GameOfLife
{
    public class Cell
    {
        public bool IsAlive { get; set; }
        public int Value { get; set; }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
    }
}
