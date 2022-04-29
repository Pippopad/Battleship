using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    // Tipi di nave
    public enum ShipType
    {
        Ship1x1,
        Ship2x1,
        Ship3x1,
        Ship4x1
    }

    // Classe che definisce una nave
    class ShipInfo
    {
        // Forma della nave
        public int[,] Shape { get; set; }
        // Indice X da cui deve iniziare a renderizzare la nave
        public int StartIndexX { get; set; }
        // Indice Y da cui deve iniziare a renderizzare la nave
        public int StartIndexY { get; set; }
    }

    class Ship1x1Info : ShipInfo
    {
        public Ship1x1Info()
        {
            Shape = new int[,]
            {
                { 1 }
            };

            StartIndexX = 0;
            StartIndexY = 0;
        }
    }

    class Ship2x1Info : ShipInfo
    {
        public Ship2x1Info()
        {
            Shape = new int[,]
            {
                { 1, 1 }
            };

            StartIndexX = 0;
            StartIndexY = 0;
        }
    }

    class Ship3x1Info : ShipInfo
    {
        public Ship3x1Info()
        {
            Shape = new int[,]
            {
                { 1, 1, 1 }
            };

            StartIndexX = 1;
            StartIndexY = 0;
        }
    }

    class Ship4x1Info : ShipInfo
    {

        public Ship4x1Info()
        {
            Shape = new int[,]
            {
                { 1, 1, 1, 1 }
            };

            StartIndexX = 1;
            StartIndexY = 0;
        }
    }
}
