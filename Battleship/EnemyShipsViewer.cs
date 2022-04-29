using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public partial class EnemyShipsViewer : Form
    {
        public EnemyShipsViewer()
        {
            InitializeComponent();
        }

        private void EnemyShipsViewer_Load(object sender, EventArgs e)
        {
            MakeGrid();
            LoadEnemyShip();
        }

        // Funzione che crea la griglia
        private void MakeGrid()
        {
            for (int y = 0; y < Helper.NUMBER_OF_CELLS; y++)
            {
                for (int x = 0; x < Helper.NUMBER_OF_CELLS; x++)
                {
                    PictureBox cell = new PictureBox();
                    cell.Name = $"cell_{x}{y}";
                    cell.Location = new Point(x * Helper.CELL_SIZE, y * Helper.CELL_SIZE);
                    cell.Size = new Size(Helper.CELL_SIZE, Helper.CELL_SIZE);
                    cell.BorderStyle = BorderStyle.FixedSingle;
                    cell.BackColor = Helper.DEFAULT_CELL_COLOR;

                    shipGrid.Controls.Add(cell);
                }
            }
        }

        // Funzione che carica le navi del nemico sulla griglia
        private void LoadEnemyShip()
        {
            foreach (var cell in Form1.enemy.Cells)
            {
                if (cell.ShipID != -1)
                {
                    // Contiene la cella (PictureBox) della griglia di visualizzazione
                    var shipCell = Helper.GetCellByLocalPos(shipGrid, cell.X, cell.Y);
                    Color shipColor = Color.Red;

                    // Assegna ad ogni nave un colore
                    // differente (utilizza l'ID di
                    // ogni nave)
                    switch (cell.ShipID)
                    {
                        case 0:
                            shipColor = Color.OrangeRed;
                            break;
                        case 1:
                            shipColor = Color.Orange;
                            break;
                        case 2:
                            shipColor = Color.Yellow;
                            break;
                        case 3:
                            shipColor = Color.Green;
                            break;
                        case 4:
                            shipColor = Color.Aqua;
                            break;
                        case 5:
                            shipColor = Color.Violet;
                            break;
                    }

                    // Assegna il colore scelto alla cella
                    shipCell.BackColor = shipColor;
                }
            }
        }
    }
}
