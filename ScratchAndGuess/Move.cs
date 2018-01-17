using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchAndGuess
{
    class Move
    {
        private int move = 1;
        public int Moves
        {
            get => move;
        }

        private int count;
        public int CountPict
        {
            get => count;
            set => count = value;
        }

    }
}
