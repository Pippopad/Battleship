using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public class CellInfo
    {
        // Contine il player a cui fa riferimento questa cella
        public Player Player;
        // Indica se la cella è già stata colpita
        public bool Hit = false;
        // Indica l'id della nave (-1 = nessuna nave)
        public int ShipID = -1;
        // Fa riferimento alla cella (PictureBox)
        public PictureBox Cell = null;

        // Cordinate della cella (indici)
        public int X = -1;
        public int Y = -1;

        public CellInfo(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Funzione che colpisce la cella
        public bool HitCell(out bool last)
        {
            // Questa funzione ritorna true se una nave è stata colpita,
            // altrimenti ritorna false. In più ritorna la variabile
            // last che sarà true se la cella contiene una nave ed è
            // l'ultima cella di quella nave

            last = false;
            if (Hit) return false;

            Hit = true;

            if (ShipID != -1)
            {
                Player.Ships[ShipID]--;
                if (Player.Ships[ShipID] == 0) last = true;
                return true;
            }

            return false;
        }
    }
}
