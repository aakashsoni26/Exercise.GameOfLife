using System;
using Exercise.Games.BusinessLayer.Contracts;
using Exercise.Games.DomainEntities;
using Exercise.Games.DomainEntities.GameOfLife;

namespace Exercise.Games.BusinessLayer
{
    internal class GameOfLifeOperations : ValidationBase, IGameRules
    {
        #region Declarations
        #endregion

        #region Constructors

        public GameOfLifeOperations()
        {
            var lifeGrid = new LifeGrid();
            new GameOfLifeOperations(lifeGrid);
        }
        public GameOfLifeOperations(LifeGrid lifeGrid)
        {
            Console.WriteLine(GameConstants.LBL_DEFAULT_SETTINGS);
            Console.WriteLine("Max Iteration: " + lifeGrid.MaxIterations);
            Console.WriteLine("Show Change Log: " + lifeGrid.ShowChangeLog);
            Console.WriteLine();

        }
        #endregion

        #region ValidationMethods

       
        public override bool IsValid(Resources resource)
        {
            if (resource.LifeGrid.Grid == null)
            {
                ErrorMessages.Add("Source Grid is not valid");
                return false;
            }
            if (resource.LifeGrid.Grid.GetLength(1) == 0)
            {
                ErrorMessages.Add("Source Grid is not valid");
                return false;
            }

            return true;
        }
        #endregion

        #region TraversalMethods
        /// <summary>
        /// Method to determine the count of active neighbours, this will lead to the change in cell state based on rules
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        public int GetActiveNeighboursCount(int x, int y, int[,] grid)
        {
            int count = 0;
            var maxLength = grid.GetLength(0) - 1;
            var maxHieght = grid.GetLength(1) - 1;

            var offSetX = maxLength - x;
            var offSetY = maxHieght - y;

            if (offSetX >= 0 && offSetY >= 0)
            {
                //Right
                if (y < maxHieght && grid[x, y + 1] == 1)
                    count++;

                //Right Bottom
                if (x < maxLength && y < maxHieght && grid[x + 1, y + 1] == 1)
                    count++;

                //Bottom
                if (x < maxLength && grid[x + 1, y] == 1)
                    count++;

                //Top
                if (x > 0 && grid[x - 1, y] == 1)
                    count++;

                //Top Right
                if (x > 0 && y < maxHieght && grid[x - 1, y + 1] == 1)
                    count++;

                //Left
                if (y > 0 && grid[x, y - 1] == 1)
                    count++;

                //Top Left
                if (x > 0 && y > 0 && grid[x - 1, y - 1] == 1)
                    count++;

                //Bottom Left
                if (x < maxLength && y > 0 && grid[x + 1, y - 1] == 1)
                    count++;
            }

            return count;
        }
        #endregion

        #region Rules
        /// <summary>
        /// Method to apply the rules of the Game
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public Resources ApplyGameRules(Resources resource)
        {
            var _lifeGrid = resource.LifeGrid;
            var srcGrid = resource.LifeGrid.Grid;

            var gameResult = new GameResult();
            Resources _resource = new InputResources();
            if (IsValid(resource))
            {
                _lifeGrid.Grid = srcGrid;
                _resource.LifeGrid = _lifeGrid;

                Console.WriteLine(GameConstants.LBL_INITIAL_MSG);
                Display(_resource);

                int[,] targetGrid = { };
                _lifeGrid.Iteration = 1;

                IterateGenerations(ref _resource, ref targetGrid, ref srcGrid);
            }
            else
                foreach (var msg in ErrorMessages)
                {
                    Console.WriteLine(GameConstants.VALIDATION_ERROR + msg);
                }
            //Display(_input);
            gameResult.LifeGrid = _lifeGrid;
            return gameResult;
        }

        /// <summary>
        /// Method to determine where we have Active Cells to consider next iteration/generation
        /// </summary>
        /// <param name="srcGrid"></param>
        /// <returns></returns>
        private bool ShouldMoveToNextIteration(int[,] srcGrid)
        {
            int liveCellCount = 0;
            for (int y = 0; y < srcGrid.GetLength(1); y++)
                for (int x = 0; x < srcGrid.GetLength(0); x++)
                    if (srcGrid[y, x] == 1)
                        liveCellCount++;

            return liveCellCount > 1;
        }

        /// <summary>
        /// Method to loop through the input source and displaying the final grid after applying the RULES..
        /// </summary>
        /// <param name="_input"></param>
        /// <param name="targetGrid"></param>
        private void IterateGenerations(ref Resources _input, ref int[,] targetGrid, ref int[,] srcGrid)
        {
            var _lifeGrid = _input.LifeGrid;
            do
            {
                if (ShouldMoveToNextIteration(srcGrid))
                {
                    targetGrid = srcGrid;
                    for (int y = 0; y < _lifeGrid.Grid.GetLength(1); y++)
                    {
                        for (int x = 0; x < _lifeGrid.Grid.GetLength(0); x++)
                        {
                            var activeNeighbours = GetActiveNeighboursCount(x, y, srcGrid);

                            // Any live cell with fewer than two live neighbours dies, as if by loneliness.
                            //Any live cell with more than three live neighbours dies, as if by overcrowding.
                            RuleForLiveCellOverCrowdingAndLoneliness(_lifeGrid, targetGrid, y, x, activeNeighbours);

                            //Any dead cell with exactly three live neighbours comes to life.
                            RuleForDeadCellToGetLife(_lifeGrid, targetGrid, y, x, activeNeighbours);
                        }
                    }


                    //_lifeGrid.LifeGrid = new LifeGrid();
                    _lifeGrid.Grid = targetGrid;

                    _input = new InputResources();
                    _input.LifeGrid = _lifeGrid;

                    Console.WriteLine(GameConstants.LBL_ITERATION + _lifeGrid.Iteration);
                    Display(_input);
                    _lifeGrid.Iteration++;
                    srcGrid = targetGrid;
                }
                else
                {
                    Console.WriteLine(GameConstants.LBL_NO_LIVE_CELLS_MSG);
                    break;
                }
            } while (_lifeGrid.Iteration < _lifeGrid.MaxIterations);
        }
        #endregion

        #region RuleHelperMethods
        /// <summary>
        /// method for dealing with rule of dead cells getting life on 3 neighbhours
        /// </summary>
        /// <param name="_lifeGrid"></param>
        /// <param name="targetGrid"></param>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <param name="activeNeighbours"></param>
        private void RuleForDeadCellToGetLife(LifeGrid _lifeGrid, int[,] targetGrid, int y, int x, int activeNeighbours)
        {
            if (_lifeGrid.Grid[x, y] == 0 && activeNeighbours == 3) // Rule 3
            {
                targetGrid[x, y] = 1;
                _lifeGrid.ChangeLog.Add("Element (" + x + "," + y + ") 0 to 1, active Neighbours:" + activeNeighbours);
            }
        }

        /// <summary>
        /// method for dealing with rule of live cells dieing our of loneliness or overcrowding
        /// </summary>
        /// <param name="_lifeGrid"></param>
        /// <param name="targetGrid"></param>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <param name="activeNeighbours"></param>
        private void RuleForLiveCellOverCrowdingAndLoneliness(LifeGrid _lifeGrid, int[,] targetGrid, int y, int x, int activeNeighbours)
        {
            if (_lifeGrid.Grid[x, y] == 1 && (activeNeighbours > 3 || activeNeighbours < 2)) // Rule 3
            {
                targetGrid[x, y] = 0;
                _lifeGrid.ChangeLog.Add("Element (" + x + "," + y + ") 1 to 0, active Neighbours:" + activeNeighbours);
            }
        }
        #endregion

        #region Operations
        /// <summary>
        /// Method to display the results
        /// </summary>
        /// <param name="resources"></param>
        public void Display(Resources resources)
        {
            for (int y = 0; y < resources.LifeGrid.Grid.GetLength(0); y++)
            {
                for (int x = 0; x < resources.LifeGrid.Grid.GetLength(1); x++)
                    Console.Write(" {0} ", resources.LifeGrid.Grid[y, x]);
                Console.WriteLine();
            }
            Console.WriteLine();

            if (resources.LifeGrid.ShowChangeLog && resources.LifeGrid.Iteration > 0)
            {
                Console.WriteLine("Change Log.................");

                foreach (var log in resources.LifeGrid.ChangeLog)
                {
                    Console.WriteLine(log);
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Method to Prepare the initial state of game
        /// </summary>
        /// <returns></returns>
        public Resources PrepareGameOfLife()
        {
            var inputResources = new InputResources();
            inputResources.LifeGrid = new LifeGrid();
            var lifeGrid = new LifeGrid();

            var rows = GameRepository.GetSampleLifeGrid();
            var srcGrid = Helper.RowsToArray(rows);

            lifeGrid.Grid = srcGrid;
            inputResources.LifeGrid = lifeGrid;
            return inputResources;
        } 
        #endregion

    }
}
