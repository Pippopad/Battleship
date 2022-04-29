using LanguageSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Battleship.Helper;

namespace Battleship
{
    public partial class Form1 : Form
    {
        // Variabile che contiene lo stato corrente
        State currentState = State.NORMAL;

        // Dichiarazione del giocatore e del nemico
        Player player;
        public static Player enemy;

        // Gestisce le celle selezionate (nella modalità SHIP_PLACING)
        List<CellInfo> selectedCells = new List<CellInfo>();
        // Informazioni sulla nave seleziona dall'utente
        ShipInfo playerShipInfo;

        // Variabili che permetto l'accesso alla
        // griglia del giocatore e a quella di
        // attaco in modo statico
        public static Panel s_ShipGrid;
        public static Panel s_AtkGrid;

        // Gestore lingua
        Language en;
        Language it;
        LanguageManager languageManager;
        ////////////////

        public Form1()
        {
            InitializeComponent();

            // Inizializzazione della libreria che gestisce la lingua
            en = new Language("English", Path.GetFullPath("lang/en.lang"));
            it = new Language("Italian", Path.GetFullPath("lang/it.lang"));
            languageManager = new LanguageManager(en);
            /////////////////////////////////////////////////////////

            // Imposta le operazioni da eseguire quando la lingua viene cambiata
            languageManager.OnLanguageChanged += () =>
            {
                // Form1 UI
                lblShipGrid.Text = languageManager.GetTranslation("ship_grid");
                lblAtkGrid.Text = languageManager.GetTranslation("atk_grid");
                btnPlaceShips.Text = languageManager.GetTranslation("place_ships");
                btnTurn.Text = languageManager.GetTranslation("turn_ship");
                btnStart.Text = languageManager.GetTranslation("start");
                ///////////

                // Menu
                languageToolStripMenuItem.Text = languageManager.GetTranslation("language");
                english.Text = languageManager.GetTranslation("english");
                italian.Text = languageManager.GetTranslation("italian");

                quitToolStripMenuItem.Text = languageManager.GetTranslation("quit");
                ///////
            };

            // Chiama una prima volta la funzione che aggiorna la lingua
            languageManager.OnLanguageChanged();
            /////////////////////////////////////////////////////////

            // Inizializzazione del player e dell'enemy
            player = new Player();
            enemy = new Player();

            // Inizializzazione della lista che mostra i tipi di nave
            SetupShipList();

            // Generazione della griglia del giocatore e della griglia di attacco
            MakeShipGrid();
            MakeAtkGrid();

            // Inizializzazione delle variabili statiche
            s_ShipGrid = shipGrid;
            s_AtkGrid = atkGrid;

            // Inizializzazione della variabile che
            // contiene le informazioni della nave
            // selezionata
            playerShipInfo = GetShipData(GetShipType(lstShips.SelectedItem.ToString()));
        }

        // Genera gli elementi della lista delle navi
        public void SetupShipList()
        {
            // Aggiunge i nomi di tutti i tipi di nave
            lstShips.Items.Add("1x1");
            lstShips.Items.Add("2x1");
            lstShips.Items.Add("3x1");
            lstShips.Items.Add("4x1");

            // Imposta il primo elemento da visualizzare nella lista
            lstShips.SelectedIndex = 0;
        }

        // Genera la griglia del player
        public void MakeShipGrid()
        {
            for (int y = 0; y < NUMBER_OF_CELLS; y++)
            {
                for (int x = 0; x < NUMBER_OF_CELLS; x++)
                {
                    // Genera la cella (PictureBox)
                    PictureBox cell = new PictureBox();
                    // Imposta alcune proprietà (es: nome, colore sfondo, posizione, eventi, ...)
                    cell.Name = $"cell_{x}{y}";
                    cell.Location = new Point(x * CELL_SIZE, y * CELL_SIZE);
                    cell.Size = new Size(CELL_SIZE, CELL_SIZE);
                    cell.BorderStyle = BorderStyle.FixedSingle;
                    cell.BackColor = DEFAULT_CELL_COLOR;
                    cell.Click += ShipGrid_Click;
                    cell.MouseEnter += ShipGrid_MouseEnter;
                    cell.MouseLeave += ShipGrid_MouseLeave;

                    // Aggiunge la cella alla griglia
                    shipGrid.Controls.Add(cell);

                    // Variabile che contiene le informazioni di una determinata cella
                    CellInfo cellInfo = new CellInfo(x, y);
                    // Collega la cella (PictureBox) a cellInfo
                    cellInfo.Cell = cell;

                    // Aggiunge cellInfo alla griglia del player
                    player.Cells.Add(cellInfo);
                }
            }
        }

        // Genera la griglia per l'attacco
        public void MakeAtkGrid()
        {
            for (int y = 0; y < NUMBER_OF_CELLS; y++)
            {
                for (int x = 0; x < NUMBER_OF_CELLS; x++)
                {
                    PictureBox cell = new PictureBox();
                    cell.Name = $"cell_{x}{y}";
                    cell.Location = new Point(x * CELL_SIZE, y * CELL_SIZE);
                    cell.Size = new Size(CELL_SIZE, CELL_SIZE);
                    cell.BorderStyle = BorderStyle.FixedSingle;
                    cell.BackColor = DEFAULT_CELL_COLOR;
                    cell.Click += AtkGrid_Click;
                    cell.MouseEnter += AtkGrid_MouseEnter;
                    cell.MouseLeave += AtkGrid_MouseLeave;

                    atkGrid.Controls.Add(cell);
                }
            }
        }

        // Generazione della griglia del nemico
        public void GenerateEnemy()
        {
            // Genera la griglia contenente le informazioni di ogni cella del nemico
            for (int y = 0; y < NUMBER_OF_CELLS; y++)
            {
                for (int x = 0; x < NUMBER_OF_CELLS; x++)
                {
                    CellInfo info = new CellInfo(x, y);
                    enemy.Cells.Add(info);
                }
            }

            Random r = new Random();

            // Ottiene un tipo di nave in modo random
            Array val = Enum.GetValues(typeof(ShipType));
            int idx = r.Next(val.Length);
            ShipType type = (ShipType)val.GetValue(idx);

            // Ciclo che si ripete fino a che
            // tutte le navi da piazzare non
            // sono piazzate
            do
            {
                int x;
                int y;

                // Ottiene delle coordinate random e verifica
                // che non ci sia già una nave
                //
                // (Se enemy.Cells[x + y * NUMBER_OF_CELLS].ShipID è -1
                // significa che la cella è vuota
                do
                {
                    x = (int)r.Next(NUMBER_OF_CELLS);
                    y = (int)r.Next(NUMBER_OF_CELLS);
                } while (enemy.Cells[x + y * NUMBER_OF_CELLS].ShipID != -1);

                // Seleziona un nuovo tipo di nave se
                // il limite di navi piazzabili per quel
                // tipo è stato raggiunto
                while (enemy.ShipsCount[idx] == Player.ShipsLimit[idx])
                {
                    idx = r.Next(val.Length);
                    type = (ShipType)val.GetValue(idx);
                }

                // Lista che contiene tutte le celle selezionate
                List<CellInfo> sc = new List<CellInfo>();

                // Contiene la cella con coordinate x e y (appartenente alla griglia del nemico)
                CellInfo cellInfo = GetCellByLocalPos(enemy, x, y);

                // Informazioni sul tipo di nave selezionata
                ShipInfo info = GetShipData(type);

                // Decide se ruotare la nave (50% di possibilità)
                if (r.Next(2) == 1) info = Helper.TurnShip(info);

                // Offset:
                // A seconda degli StartIndex della nave (contenuti in info),
                // crea degli offset per ogni punto della nave,
                // Esempio:
                //  Shape:
                //      [
                //          [ 1, 1, 1 ],
                //          [ 1, 1, 0 ]
                //      [
                //  StartIndexX = 1
                //  StartIndexY = 1
                //
                //  Offset:
                //      [
                //          [ [-1, -1], [0, -1], [1, -1] ], 
                //          [ [-1,  0], [0,  0]          ], 
                //      ]
                List<int[]> offset = new List<int[]>();

                // Generazione offset
                for (int i = 0; i < info.Shape.GetLength(0); i++)
                {
                    for (int j = 0; j < info.Shape.GetLength(1); j++)
                    {
                        // Se il valore info.Shape[i, j] è 0
                        // significa che non c'è niente e di
                        // conseguenza continua il ciclo (salta
                        // questo turno)
                        if (info.Shape[i, j] == 0) continue;

                        // Calcola l'offset e lo aggiunge alla lista
                        offset.Add(new int[] { j - info.StartIndexX, i - info.StartIndexY });
                    }
                }

                // Controlla la presenza di errori (viene aggiornata in caso di errore)
                bool error = false;

                // Seleziona le celle che il tipo di nave richiede e
                // se le celle da selezionare sono fuori dai bordi,
                // la flag error viene impostata a true
                for (int i = 0; i < info.Shape.GetLength(0); i++)
                {
                    for (int j = 0; j < info.Shape.GetLength(1); j++)
                    {
                        if (info.Shape[i, j] == 0) continue;
                        int xx = cellInfo.X + offset[j + i * info.Shape.GetLength(1)][0];
                        int yy = cellInfo.Y + offset[j + i * info.Shape.GetLength(1)][1];

                        if (xx < 0 ||
                            xx >= NUMBER_OF_CELLS ||
                            yy < 0 ||
                            yy >= NUMBER_OF_CELLS) error = true;

                        if (error) break;

                        sc.Add(enemy.Cells[xx + yy * NUMBER_OF_CELLS]);
                    }

                    if (error) break;
                }
                //////////////////////////////////////////////

                // Se non è stato possibile selezionare le celle,
                // rinizia il ciclo
                if (error) continue;

                // Se nelle celle selezionate è presente un'altra nave,
                // imposta la flag error a true
                foreach (var ci in sc)
                {
                    if (ci.ShipID != -1)
                    {
                        error = true;
                        break;
                    }
                }

                if (error) continue;

                // Cambia alcune configurazioni delle celle selezionate
                foreach (var ci in sc)
                {
                    ci.Player = enemy;
                    ci.ShipID = enemy.ShipID;
                }

                // Agiorna la lista di navi piazzate (incrementa il tipo corrispondente ad idx)
                enemy.ShipsCount[idx]++;
                // Aggiorna la list di navi aggiungendo il numero di celle aggiunte
                enemy.Ships.Add(sc.Count);
                // Incrementa l'id della nave corrente
                enemy.ShipID++;
            } while (Sum(enemy.ShipsCount) != Sum(Player.ShipsLimit));
        }

        ///////////////////////////////////////////////////////////////////////////////////
        /// ShipGrid Cell Management
        ///////////////////////////////////////////////////////////////////////////////////
        public void ShipGrid_Click(object sender, EventArgs e)
        {
            // Se lo stato corrente è impostato su SHIP_PLACING
            if (currentState == State.SHIP_PLACING)
            {
                // Se il limite di navi piazzabili per il tipo di nave selzionato
                // è stato raggiunto, interrompe la funzione e comunica l'errore
                if (player.ShipsCount[lstShips.SelectedIndex] == Player.ShipsLimit[lstShips.SelectedIndex])
                {
                    MessageBox.Show(languageManager.GetTranslation("ship_type_limit"), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Se la nave da piazzare finisce fuori dai bordi,
                // interrompe la funzione e comunica l'errore
                if (selectedCells.Count != Sum(GetShipData(GetShipType(lstShips.SelectedItem.ToString())).Shape))
                {
                    MessageBox.Show(languageManager.GetTranslation("ship_out_of_bounds"), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Se la nave da piazzare si trova sopra un'altra nave,
                // interrompe la funzione e comunica l'errore
                foreach (var cellInfo in selectedCells)
                {
                    if (cellInfo.ShipID != -1)
                    {
                        MessageBox.Show(languageManager.GetTranslation("ship_conflict"), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Cambia alcune configurazioni delle celle selezionate
                foreach (var cellInfo in selectedCells)
                {
                    cellInfo.Player = player;
                    cellInfo.ShipID = player.ShipID;
                    var cell = cellInfo.Cell;
                    cell.BackColor = DEFAULT_SHIP_NORMAL_COLOR;
                }

                // Aggiorna il player
                player.ShipsCount[lstShips.SelectedIndex]++;
                player.Ships.Add(selectedCells.Count);
                player.ShipID++;

                // Se non sono state piazzate tutte le navi,
                // la funzione si interrompe
                for (int i = 0; i < player.ShipsCount.Length; i++)
                {
                    if (player.ShipsCount[i] != Player.ShipsLimit[i]) return;
                }

                // Se tutte le navi sono state piazzate ritorna
                // allo stato NORMAL, abilita il pulsante di
                // inizio gioco e disabilita il pulsante per
                // piazzare le navi
                ToggleShipPlace();
                btnStart.Enabled = true;
                btnPlaceShips.Enabled = false;
            }
        }

        public void ShipGrid_MouseEnter(object sender, EventArgs e)
        {
            if (currentState == State.SHIP_PLACING)
            {
                // Prende gli indici x ed y della cella
                // (PosizioneCella / CELL_SIZE)
                int x = (sender as PictureBox).Location.X / CELL_SIZE;
                int y = (sender as PictureBox).Location.Y / CELL_SIZE;

                // Contiene la cella con coordinate x e y (appartenente alla griglia del player)
                CellInfo cellInfo = GetCellByLocalPos(player, x, y);

                // Lista degli offset
                List<int[]> offset = new List<int[]>();

                // Calcolo degli offset
                for (int i = 0; i < playerShipInfo.Shape.GetLength(0); i++)
                {
                    for (int j = 0; j < playerShipInfo.Shape.GetLength(1); j++)
                    {
                        if (playerShipInfo.Shape[i, j] == 0) continue;
                        offset.Add(new int[] { j - playerShipInfo.StartIndexX, i - playerShipInfo.StartIndexY });
                    }
                }

                // Seleziona le celle che il tipo di nave richiede
                for (int i = 0; i < playerShipInfo.Shape.GetLength(0); i++)
                {
                    for (int j = 0; j < playerShipInfo.Shape.GetLength(1); j++)
                    {
                        if (playerShipInfo.Shape[i, j] == 0) continue;
                        int xx = cellInfo.X + offset[j + i * playerShipInfo.Shape.GetLength(1)][0];
                        int yy = cellInfo.Y + offset[j + i * playerShipInfo.Shape.GetLength(1)][1];
                        if (xx < 0 ||
                            xx >= NUMBER_OF_CELLS ||
                            yy < 0 ||
                            yy >= NUMBER_OF_CELLS) continue;
                        player.Cells[xx + yy * NUMBER_OF_CELLS].Cell.BackColor = DEFAULT_SHIP_PRE_PLACE_COLOR;
                        selectedCells.Add(player.Cells[xx + yy * NUMBER_OF_CELLS]);
                    }
                }
            }
        }

        public void ShipGrid_MouseLeave(object sender, EventArgs e)
        {
            if (currentState == State.SHIP_PLACING)
            {
                // Svuota la lista delle celle selezionate
                selectedCells.Clear();

                // Per ogni cella nella griglia del player
                foreach (var ci in player.Cells)
                {
                    var cell = ci.Cell;

                    // Se la cella non contiene una nave (ci.ShipID != -1)
                    // il colore è DEFAULT_CELL_COLOR, altrimenti il
                    // colore è DEFAULT_SHIP_NORMAL_COLOR
                    if (ci.ShipID != -1) cell.BackColor = DEFAULT_SHIP_NORMAL_COLOR;
                    else cell.BackColor = DEFAULT_CELL_COLOR;
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////
        /// ShipGrid Cell Management
        ///////////////////////////////////////////////////////////////////////////////////
        public void AtkGrid_Click(object sender, EventArgs e)
        {
            // Se lo stato corrente è impostato su SHIP_ATK
            if (currentState == State.SHIP_ATK)
            {
                // Prende gli indici x ed y della cella
                // (PosizioneCella / CELL_SIZE)
                int x = (sender as PictureBox).Location.X / CELL_SIZE;
                int y = (sender as PictureBox).Location.Y / CELL_SIZE;

                // Contiene la cella con coordinate x e y (appartenente alla griglia dell'enemy)
                CellInfo enemyCell = GetCellByLocalPos(enemy, x, y);

                // Se la cella è già stata colpita, interrompe
                // la funzione e comunica un errore
                if (enemyCell.Hit)
                {
                    MessageBox.Show(languageManager.GetTranslation("already_hit"), Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Colpisce la cella
                enemyCell.HitCell(out bool last);

                // Se la nave è stata colpita, è l'ultima
                // ed il numero di navi nemiche è 0, allora
                // termina la partita. Altrimenti si avvia
                // il turno del nemico
                if (last &&
                    Sum(enemy.Ships.ToArray()) == 0)
                {
                    End(true);
                }
                else
                {
                    EnemyTurn();
                    if (last) MessageBox.Show(languageManager.GetTranslation("ship_sunk"), Text);
                }
            }
        }

        public void AtkGrid_MouseEnter(object sender, EventArgs e)
        {
            // Cambia il colore della cella se il mouse è sopra
            // di essa e se la modalità di gioco è SHIP_ATK 
            if (currentState == State.SHIP_ATK)
            {
                (sender as PictureBox).BackColor = DEFAULT_CELL_HOVER_COLOR;
            }
        }

        public void AtkGrid_MouseLeave(object sender, EventArgs e)
        {
            // Ripristina il colore della cella
            if (currentState == State.SHIP_ATK)
            {
                int x = (sender as PictureBox).Location.X / CELL_SIZE;
                int y = (sender as PictureBox).Location.Y / CELL_SIZE;

                CellInfo enemyCell = GetCellByLocalPos(enemy, x, y);

                Color shipColor = DEFAULT_CELL_COLOR;

                if (enemyCell.Hit)
                {
                    if (enemyCell.ShipID == -1) shipColor = DEFAULT_SHIP_HIT_FAIL_COLOR;
                    else shipColor = DEFAULT_SHIP_HIT_COLOR;
                }

                (sender as PictureBox).BackColor = shipColor;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////

        // Se l'utente seleziona un'altro tipo di nave,
        // playerShipInfo ottiene le informazioni sul
        // nuovo tipo di nave
        private void lstShips_SelectionChangeCommitted(object sender, EventArgs e)
        {
            playerShipInfo = GetShipData(GetShipType(lstShips.SelectedItem.ToString()));
        }

        // Cambia lo stato di gioco
        // da NORMAL a SHIP_PLACING
        // e viceversa
        public void ToggleShipPlace()
        {
            if (currentState == State.NORMAL)
            {
                currentState = State.SHIP_PLACING;

                lstShips.Enabled = true;
                btnTurn.Enabled = true;
            }
            else
            {
                currentState = State.NORMAL;

                lstShips.Enabled = false;
                btnTurn.Enabled = false;
            }
        }

        // Cambia lo stato di gioco
        // da NORMAL a SHIP_ATK e
        // viceversa
        public void ToggleAtk()
        {
            if (currentState == State.NORMAL)
            {
                currentState = State.SHIP_ATK;

                atkGrid.Enabled = true;
            }
            else
            {
                currentState = State.NORMAL;

                atkGrid.Enabled = false;
            }
        }

        private void btnPlaceShips_Click(object sender, EventArgs e)
        {
            ToggleShipPlace();
        }

        private void btnTurn_Click(object sender, EventArgs e)
        {
            // Gira la nave selezionata
            playerShipInfo = Helper.TurnShip(playerShipInfo);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Imposta lo stato a NORMAL, disabilita sia
            // il pulsante per iniziare la partita che
            // quello per piazzare le navi
            if (currentState == State.SHIP_PLACING) ToggleShipPlace();
            btnPlaceShips.Enabled = false;
            btnStart.Enabled = false;

            // Genera il nemico
            GenerateEnemy();

            // Se il programma si avvia con l'opzione "-debug",
            // quando la partita inizia si apre una schermata
            // che mostra le navi del nemico
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 2 &&
                args[1].ToLower() == "-debug") new EnemyShipsViewer().Show();

            // Inizia il gioco (inizia il giocatore)
            PlayerTurn();
        }

        // Menu
        // Funzione per il menù

        // Cambio della lingua
        private void changeLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch ((sender as ToolStripMenuItem).Name)
            {
                case "english":
                    languageManager.SetLanguage(en);
                    break;
                case "italian":
                    languageManager.SetLanguage(it);
                    break;
                default:
                    languageManager.SetLanguage(en);
                    break;
            }
        }

        // Fine programma
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        ////////////
        /// GAME ///
        ////////////

        // Turno del player
        public void PlayerTurn()
        {
            ToggleAtk();
        }

        // Turno del nemico
        public void EnemyTurn()
        {
            ToggleAtk();

            CellInfo playerCell;

            int x;
            int y;

            Random r = new Random();

            // Prende una posizione a caso e
            // se quella cella è già stata
            // colpita il ciclo si ripete
            do
            {
                x = r.Next(NUMBER_OF_CELLS);
                y = r.Next(NUMBER_OF_CELLS);

                playerCell = GetCellByLocalPos(player, x, y);
            } while (playerCell.Hit);

            // Colpisce la cella
            playerCell.HitCell(out bool last);

            // Se la cella conteneva una nave il colore viene impostano
            // è DEFAULT_SHIP_HIT_COLOR altrimenti DEFAULT_SHIP_HIT_FAIL_COLOR
            if (playerCell.ShipID != -1) playerCell.Cell.BackColor = DEFAULT_SHIP_HIT_COLOR;
            else playerCell.Cell.BackColor = DEFAULT_SHIP_HIT_FAIL_COLOR;

            // Se la nave è stata colpita, è l'ultima ed
            // il numero di navi del giocatore è 0, allora
            // termina la partita. Altrimenti si avvia
            // il turno del giocatore
            if (last &&
                Sum(player.Ships.ToArray()) == 0) End(false);
            else PlayerTurn();
        }

        public void End(bool playerWin)
        {
            // Mostra il messaggio di fine partita
            MessageBox.Show(languageManager.GetTranslation(playerWin ? "player_win" : "enemy_win"), Text);
            if (currentState == State.SHIP_ATK) ToggleAtk();
        }
    }
}
