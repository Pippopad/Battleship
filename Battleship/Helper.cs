using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    static class Helper
    {
        // Dimensioni
        public const int GRID_SIZE = 320;
        public const int NUMBER_OF_CELLS = 10;
        public const int CELL_SIZE = GRID_SIZE / NUMBER_OF_CELLS;

        // Varibili contenenti i colori delle celle
        public static Color DEFAULT_CELL_COLOR = Color.FromArgb(0, 97, 255);
        public static Color DEFAULT_CELL_HOVER_COLOR = Color.FromArgb(0, 81, 211);
        public static Color DEFAULT_SHIP_PRE_PLACE_COLOR = Color.FromArgb(220, 220, 220);
        public static Color DEFAULT_SHIP_NORMAL_COLOR = Color.FromArgb(90, 90, 90);
        public static Color DEFAULT_SHIP_HIT_COLOR = Color.FromArgb(219, 0, 21);
        public static Color DEFAULT_SHIP_HIT_FAIL_COLOR = Color.FromArgb(0, 67, 175);

        // Stati di gioco
        public enum State
        {
            NORMAL,             // Normale
            SHIP_PLACING,       // Modalità piazzamento navi
            SHIP_ATK            // Modalità attacco
        }

        // Funzione che dati un giocatore, una x ed una y,
        // ritorna un'oggetto contenente informazioni sulla
        // cella selezionata
        //
        // N.B.: x ed y sono coordinate relative (l'origine è l'inizio della griglia)
        public static CellInfo GetCellByWorldPos(Player p, int x, int y)
        {
            // Toglie l'offset della griglia
            x -= Form1.s_ShipGrid.Location.X;
            y -= Form1.s_AtkGrid.Location.Y;

            foreach (var cellInfo in p.Cells)
            {
                var cell = cellInfo.Cell;
                var loc = cell.Location;
                var size = cell.Size;
                if (x >= loc.X && x <= loc.X + size.Width &&
                    y >= loc.Y && y <= loc.Y + size.Height)
                {
                    return cellInfo;
                }
            }

            return null;
        }

        // Funzione che dati un giocatore, una x ed una y,
        // ritorna un'oggetto contenente informazioni sulla
        // cella selezionata
        //
        // N.B.: x ed y sono gli indici della griglia
        public static CellInfo GetCellByLocalPos(Player p, int x, int y)
        {
            foreach (var cellInfo in p.Cells)
            {
                if (x == cellInfo.X &&
                    y == cellInfo.Y) return cellInfo;
            }

            return null;
        }

        // Funzione che dati un griglia(Panel), una x ed una y,
        // ritorna una cella (PictureBox)
        //
        // N.B.: x ed y sono gli indici della griglia
        public static PictureBox GetCellByLocalPos(Panel p, int x, int y)
        {
            foreach (PictureBox cell in p.Controls)
            {
                if (x == cell.Location.X / Helper.CELL_SIZE &&
                    y == cell.Location.Y / Helper.CELL_SIZE) return cell;
            }

            return null;
        }


        // Funzione che dato un tipo di nave,
        // ritorna un oggetto contenente le
        // informazioni su di essa
        public static ShipInfo GetShipData(ShipType? type)
        {
            ShipInfo info;

            switch (type)
            {
                case ShipType.Ship1x1:
                    info = new Ship1x1Info();
                    break;
                case ShipType.Ship2x1:
                    info = new Ship2x1Info();
                    break;
                case ShipType.Ship3x1:
                    info = new Ship3x1Info();
                    break;
                case ShipType.Ship4x1:
                    info = new Ship4x1Info();
                    break;
                default:
                    info = null;
                    break;
            }

            return info;
        }


        // Funzione che dato il nome di una nave,
        // ne ritorna il tipo
        public static ShipType? GetShipType(string type)
        {
            switch (type.ToLower())
            {
                case "1x1":
                    return ShipType.Ship1x1;
                case "2x1":
                    return ShipType.Ship2x1;
                case "3x1":
                    return ShipType.Ship3x1;
                case "4x1":
                    return ShipType.Ship4x1;
                default:
                    return null;
            }
        }

        // Verifica che il valore dato sia all'interno
        // di certi limiti (min, max) e se necessario lo
        // riporta al valore limite più vicino
        public static int Clamp(int value, int min, int max)
        {
            // Se il valore massimo è più piccolo
            // di quello minimo, inverte min e max
            if (max < min)
            {
                int tmp = min;
                min = max;
                max = tmp;
            }

            return Math.Max(Math.Min(value, max), min);
        }

        // Dato un vettore, ritorna la somma di tutti i suoi valori
        public static int Sum(int[] arr)
        {
            int sum = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
            }

            return sum;
        }

        // Data una matrice, ritorna la somma di tutti i suoi valori
        public static int Sum(int[,] mat)
        {
            int sum = 0;

            for (int y = 0; y < mat.GetLength(0); y++)
            {
                for (int x = 0; x < mat.GetLength(1); x++)
                {
                    sum += mat[y, x];
                }
            }

            return sum;
        }

        // Data una matrice, ritorna una nuova matrice ruotata (90° clockwise)
        public static int[,] Rotate(int[,] mat)
        {
            int[,] rotatedMat = new int[mat.GetLength(1), mat.GetLength(0)];

            for (int y = 0; y < rotatedMat.GetLength(0); y++)
            {
                for (int x = 0; x < rotatedMat.GetLength(1); x++)
                {
                    rotatedMat[y, x] = mat[mat.GetLength(0) - 1 - x, y];
                }
            }

            return rotatedMat;
        }

        // Dato un'oggeto contenente informazioni sul tipo
        // di una nave, ritorna un'altro oggetto (stesso tipo)
        // contenente le stesse informazioni ma ruotate (90° clockwise)
        public static ShipInfo TurnShip(ShipInfo info)
        {
            ShipInfo rotatedShip = new ShipInfo();
            rotatedShip.Shape = Helper.Rotate(info.Shape);
            rotatedShip.StartIndexX = rotatedShip.Shape.GetLength(1) - 1 - info.StartIndexY;
            rotatedShip.StartIndexY = info.StartIndexX;
            return rotatedShip;
        }
    }
}
