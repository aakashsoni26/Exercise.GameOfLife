using System;
using System.Collections.Generic;
using Exercise.GameOfLife.DomainEntities.Enums;
using Exercise.Games.BusinessLayer;

using Exercise.Games.DomainEntities;
using Exercise.Games.DomainEntities.GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercise.Games.Tests
{
    [TestClass]
    public class GameOfLifeTest
    {
        #region Positive Tests
        [TestMethod]
        public void Play()
        {
            int[,] srcGrid = new int[,]
                {
                    { 0, 1, 0 },
                    { 1, 0, 1 },
                    { 0, 1, 0},
                };
            var result = GetGameResult(srcGrid);
            Assert.AreEqual(result.LifeGrid.Grid.Length > 0, true);
        }

        [TestMethod]
        public void PlayWithBlankInput()
        {
            int[,] srcGrid = new int[,] { };

            var result = GetGameResult(srcGrid);
        }

        [TestMethod]
        public void CheckDefaultSettings()
        {
            var resource = new Resources();
            var lifeGrid = new LifeGrid();

            Assert.AreEqual(lifeGrid.MaxIterations, 5);
            Assert.AreEqual(lifeGrid.ShowChangeLog, true);
        }

        /// <summary>
        /// Any live cell with fewer than two live neighbours dies, as if by loneliness.
        /// </summary>
        [TestMethod]
        public void RuleForLoneliness()
        {
            int[,] srcGrid = new int[,]
                {
                    { 1, 0, 0 },
                    { 0, 0, 1 },
                    { 0, 0, 0},
                };

            var result = GetGameResult(srcGrid);

            Assert.AreEqual(result.LifeGrid.Grid[0, 0], 0);
            Assert.AreEqual(result.LifeGrid.Grid[1, 2], 0);

        }

        /// <summary>
        ///  //Any live cell with more than three live neighbours dies, as if by overcrowding.
        /// </summary>
        [TestMethod]
        public void RuleForOverCrowding()
        {
            int[,] srcGrid = new int[,]
                {
                    { 1, 1, 0 },
                    { 1, 1, 1 },
                    { 0, 0, 0},
                };

            var result = GetGameResult(srcGrid);

            Assert.AreEqual(result.LifeGrid.Grid[0, 1], 0);
            Assert.AreEqual(result.LifeGrid.Grid[0, 0], 1);
        }

        /// <summary>
        /// //Any dead cell with exactly three live neighbours comes to life.
        /// </summary>
        [TestMethod]
        public void RuleForAliveWith3Neighbours()
        {
            int[,] srcGrid = new int[,]
                {
                    { 0, 1, 0 },
                    { 1, 1, 1 },
                    { 0, 0, 0},
                };

            var result = GetGameResult(srcGrid);
            Assert.AreEqual(result.LifeGrid.Grid[0, 0], 1);
        }

        #endregion

        #region Negative Tests
        [TestMethod]
        public void NegativeCaseRuleForDeadCellAliveWith5Neighbours()
        {
            int[,] srcGrid = new int[,]
                {
                    { 1, 0, 1 },
                    { 1, 1, 1 },
                    { 0, 0, 0},
                };

            var result = GetGameResult(srcGrid);

            Assert.AreEqual(result.LifeGrid.Grid[0, 1], 0);
        }

        [TestMethod]
        public void NegativeCaseForHandlingNullArray()
        {
            int[,] srcGrid = null;
            var result = GetGameResult(srcGrid);
        }

        [TestMethod]
        [ExpectedException(typeof(System.IndexOutOfRangeException))]
        public void NegativeCaseForArrayOutOfSize()
        {
            int[,] srcGrid = new int[,]
                {
                    { 1, 0, 1 , 1, 0, 1,  1, 0, 1,  1, 0, 1},
                    { 1, 0, 1 , 1, 0, 1,  1, 0, 1,  1, 0, 1},
                    { 1, 0, 1 , 1, 0, 1,  1, 0, 1,  1, 0, 1},
                    { 1, 0, 1 , 1, 0, 1,  1, 0, 1,  1, 0, 1},
                    { 1, 0, 1 , 1, 0, 1,  1, 0, 1,  1, 0, 1},
                    { 1, 0, 1 , 1, 0, 1,  1, 0, 1,  1, 0, 1},
                    { 1, 0, 1 , 1, 0, 1,  1, 0, 1,  1, 0, 1},
                    { 1, 0, 1 , 1, 0, 1,  1, 0, 1,  1, 0, 1},
                    { 1, 0, 1 , 1, 0, 1,  1, 0, 1,  1, 0, 1},
                };

            GetGameResult(srcGrid);

        }
        #endregion

        #region Helper Methods
        private static Resources GetGameResult(int[,] srcGrid)
        {
            var resource = new Resources();
            var lifeGrid = new LifeGrid();
            lifeGrid.MaxIterations = 1;

            lifeGrid.Grid = srcGrid;
            resource.LifeGrid = lifeGrid;

            IGame _iGame = new MyGameOfLife();
            var resourceForGame = _iGame.Prepare(InputTypeEnum.UserDefined, resource);
            var result = _iGame.Play(resourceForGame);

            return result;
        }
        #endregion

        #region Miscellaneous
        [TestMethod]
        public void TestHelpers()
        {
            //var rows = new List<Row>();

            var rows = GameRepository.GetSampleLifeGrid();
            int[,] srcGrid = new int[,] { { 1, 1 }, { 0, 0 } };
            Helper.ArrayToRows(srcGrid);
            Helper.RowsToArray(rows);
            Helper.PrintRows(rows);
        }

        [TestMethod]
        public void GetGameResultWithRandomType()
        {
            IGame _iGame = new MyGameOfLife();
            var resourceForGame = _iGame.Prepare(InputTypeEnum.Random, null);
            var result = _iGame.Play(resourceForGame);
        }

        [TestMethod]
        public void TestDisplayResult()
        {
            int[,] srcGrid = new int[,] { { 1, 1 }, { 0, 0 } };
            IGame _iGame = new MyGameOfLife();
            var result = GetGameResult(srcGrid);
            _iGame.DisplayResult(result);

        }

        [TestMethod]
        public void CheckOperationsIsInternal()
        {
            //var a = new GameOfLifeOperations();
        } 
        #endregion
    }
}
