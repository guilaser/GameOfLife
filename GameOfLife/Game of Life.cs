using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class GameOfLifeForm : Form
    {
        bool running = false;
        public int cellSize = 20;
        public int rows = 50;
        public int cols = 40;
        public int[,] cells = new int[50, 40]; //An array containing all the cells
        public int[,] nextCells = new int[50, 40]; //A second array made for calculating the next set of cells
        public Random random = new Random();

        public GameOfLifeForm()
        {
            InitializeComponent();
            gameTimer.Interval = 200;
            GenerateDefaultMode(cells);
            Invalidate();
        }
        private void SpawnRandomCells(int[,] cells)
        //For every cell in the array, there is a 1/10 chance it will be "on", i.e its value will be 1
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int check = random.Next(1, 11);
                    if (check == 4)
                    {
                        cells[r, c] = 1;
                    }
                    else
                    {
                        cells[r, c] = 0;
                    }
                }
            }
        }

        private void CalcNextCells(int[,] cells)
        //Calculate what the next set of cells will look like based on the current one
        {
            int sum;
            int xDiffL; //There need to be restrictions on the check on surrounding cells. If the left, right, top or bottom-most cells are being checked, there are limited "surrounding" cells. These values make up for that.
            int xDiffR;
            int yDiffU;
            int yDiffD;
            for (int r = 0; r < rows; r++)
            {
                xDiffL = (r == 0) ? 0 : 1;
                xDiffR = (r == 49) ? 0 : 1;
                for (int c = 0; c < cols; c++)
                {
                    yDiffU = (c == 0) ? 0 : 1;
                    yDiffD = (c == 39) ? 0 : 1;
                    //Now we have made sure all of the outmost values are being checked accordingly, meaning no value will be out of bounds.
                    sum = 0;
                    for (int x = r - xDiffL; x <= r + xDiffR; x++) //Loop over every surrounding "cell" or array index, top to bottom then left to right, and add its value to "sum"
                    {
                        for (int y = c - yDiffU; y <= c + yDiffD; y++) //For example, say the cell (10,10) is being checked. The loop will add the value of (9,9), then (9,10), then (9,11), then (10,9) and so on
                        {
                            sum += cells[x, y];
                        }
                    }
                    sum -= cells[r, c];
                    nextCells[r, c] = (sum, cells[r, c]) switch //The calculation for what the next cellstate will be requires two inputs. What the sum of the surrounding cells are, aswell as if the cell is "on" or not
                    {
                        (3, _) => 1, //if any cell has three neighbors, it either survives or spawns no matter what
                        (2, 1) => 1, //if a cell has two neighbors, it survives, but doesnt spawn if the cell is empty
                        _ => 0 //in any case other than these two, a cell dies or doesn't spawn
                    };
                }
            }
        }
        private void ReplaceCells(int[,] replacee, int[,] replacer)
        //Simple code for replacing the current two-dimensional array with another
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    replacee[r, c] = replacer[r, c];
                }
            }
        }

        private void LeftClick(object sender, MouseEventArgs e)
        //Function that enables creating cells by clicking on the grid
        {
            if (!running) //function only works when game is paused
            {
                int xVal = (int) e.Location.X/cellSize; //e.Location was used in order to get the mouse position in relation to the console window
                int yVal = (int) e.Location.Y/cellSize; //Use the "int" cast in order to always round down the value, and divide by the cellSize to get the corresponding array index that you clicked
                cells[xVal, yVal] = cells[xVal, yVal] == 0 ? 1 : 0;
                Invalidate();
            }
        }

        private void UpdateButtonClick(object sender, EventArgs e)
        //Update the game whenever the button is clicked
        {
            CalcNextCells(cells);
            ReplaceCells(cells, nextCells);
            Invalidate();
        }

        private void PlayClick(object sender, EventArgs e)
        //Enable or disable the timer by clicking the play/pause button
        {
            running = !running;
            if (running)
            {
                gameTimer.Start();
            }
            else
            {
                gameTimer.Stop();
            }

        }
        private void gameTimer_Tick(object sender, EventArgs e)
        //Every x seconds, in this case 200 milliseconds, the game updates. This means that the next cells are calculated, replace the current cells, and are then drawn out in that specific order
        {
            CalcNextCells(cells);
            ReplaceCells(cells, nextCells);
            Invalidate();
        }
        private void GenerateCellsClick(object sender, EventArgs e)
        //Uses the spawn random cells method to generate random cells upon button click
        {
            SpawnRandomCells(cells);
            Invalidate();
        }

        private void ClearCellsClick(object sender, EventArgs e)
        //Clears the current grid by making all array values 0
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    cells[r, c] = 0;
                }
            }
            Invalidate();
        }
        private void GameOfLifeForm_Paint(object sender, PaintEventArgs e)
        //Groundwork for the graphics and visuals, simple code to draw colored squares at array indexes with value 1
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.DarkGray);
            SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
            SolidBrush grayBrush = new SolidBrush(Color.Gray);
            for (int r = 0; r < rows; r++) //This loop makes sure that all cells get drawn out in gold color, and the dead ones are painted in gray
            {
                for (int c = 0; c < cols; c++)
                {
                    if (cells[r, c] == 1)
                    {
                        g.FillRectangle(yellowBrush, cellSize * r, cellSize * c, cellSize, cellSize);
                    }
                    else
                    {
                        g.FillRectangle(grayBrush, cellSize * r, cellSize * c, cellSize, cellSize);
                    }
                }
            }
            for (int i = 0; i < rows; i++) //Code to draw a grid in order to make viewing the cells and distinguishing them from one another easier
            {
                g.DrawLine(p, i * cellSize, 0, i * cellSize, cols * cellSize);
            }
            for (int i = 0; i < cols; i++) //The grid is not a square since the dimensions of a standard computer monitor isn't, therefore these two cannot be drawn in the same loop
            {
                g.DrawLine(p, 0, i * cellSize, rows * cellSize, i * cellSize);
            }
        }
        private void GenerateDefaultMode(int[,] cells)
        // :D
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    cells[r, c] = 0;
                }
            }

            cells[0, 4] = 1;
            cells[0, 5] = 1;
            cells[1, 4] = 1;
            cells[1, 5] = 1;

            cells[10, 4] = 1;
            cells[10, 5] = 1;
            cells[10, 6] = 1;
            cells[11, 3] = 1;
            cells[11, 7] = 1;
            cells[12, 2] = 1;
            cells[12, 8] = 1;
            cells[13, 2] = 1;
            cells[13, 8] = 1;
            cells[14, 5] = 1;
            cells[15, 3] = 1;
            cells[15, 7] = 1;
            cells[16, 4] = 1;
            cells[16, 5] = 1;
            cells[16, 6] = 1;
            cells[17, 5] = 1;

            cells[20, 2] = 1;
            cells[20, 3] = 1;
            cells[20, 4] = 1;
            cells[21, 2] = 1;
            cells[21, 3] = 1;
            cells[21, 4] = 1;
            cells[22, 1] = 1;
            cells[22, 5] = 1;
            cells[24, 0] = 1;
            cells[24, 1] = 1;
            cells[24, 5] = 1;
            cells[24, 6] = 1;

            cells[34, 2] = 1;
            cells[34, 3] = 1;
            cells[35, 2] = 1;
            cells[35, 3] = 1;      
        }

        private void DefaultModeButtonClick(object sender, EventArgs e)
        // :D
        {
            GenerateDefaultMode(cells);
            Invalidate();
        }
    }
}