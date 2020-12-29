using MAFIA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAFIA.Models
{
    class Player
    {
        #region Свойства

        private Card _card;

        public Card card
        {
            get => _card;
            set
            {
                _card = value;
            }
        }

        private bool _IsAlive;
        public bool IsAlive
        {
            get => _IsAlive;
            set
            {
                _IsAlive = value;
            }
        }

        private bool _IsElected;
        public bool IsElected
        {
            get => _IsElected;
            set
            {
                _IsElected = value;
            }
        }

        #endregion

        public Player()
        {
            IsAlive = true;
            IsElected = false;
            card = new Card();
        }
            


    }
}
