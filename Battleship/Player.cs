using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Player
    {
        // Lista contenente le informazioni di
        // tutte le celle della griglia
        public List<CellInfo> Cells = new List<CellInfo>();
        // ID della nave corrente
        public int ShipID = 0;

        // Contiene il numero di celle per ogni nave
        public List<int> Ships = new List<int>();

        // Contiene il numero di navi piazzate
        public int[] ShipsCount = new int[]
        {
            0,      // 1x1
            0,      // 2x1
            0,      // 3x1
            0       // 4x1
        };

        // Contiene il limite di navi piazzabili
        public static int[] ShipsLimit = new int[]
        {
            1,      // 1x1
            2,      // 2x1
            2,      // 3x1
            1       // 4x1
        };
    }
}
