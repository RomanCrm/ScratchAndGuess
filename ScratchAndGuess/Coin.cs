using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchAndGuess
{
    enum Coins
    {
        One = 1,
        Three = 3,
        Five = 5,
        Ten = 10 //lucky one =)
    }

    class Coin
    {
        private int coin;
        public int TotalCoins
        {
            get => coin;
            set => coin = value;
        }


        public string AddCoin(string labelLast)
        {
            TotalCoins = int.Parse(labelLast);
            TotalCoins += (int)Coins.One;
            return TotalCoins.ToString();
        }

        public string MinusCoin(string labelLast)
        {
            TotalCoins = int.Parse(labelLast) - 1;
            return TotalCoins.ToString();
        }
    }
}
