using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAFIA.ViewModels.Base;
using MAFIA.Infrastructure.Commands;
using System.Reflection.Emit;
using System.Windows.Input;
using System.Windows;
using System.Windows.Data;
using MAFIA.Models;

namespace MAFIA.ViewModels
{
    class PickingViewModel : ViewModel 
    {
        private int _PlayerNumber;
        public int PlayerNumber
        {
            get => _PlayerNumber;
            set => Set(ref _PlayerNumber, value);
        }

        private string _LabelContext;
        public string LabelContext
        {
            get => _LabelContext;
              
            set => Set(ref _LabelContext, value);
            
        }

        private string _CardPath;

        public string CardPath
        {
            get => _CardPath;
            set => Set(ref _CardPath, value);
        }

        private string _FontColor;

        public string FontColor
        {
            get => _FontColor;
            set => Set(ref _FontColor, value);
        }

        #region Команды

        public ICommand NextPlayerCardCommand { get; }
        private bool CanNextPlayerCardCommandExecuted(object p) => true;
        private void OnNextPlayerCardCommandExecuted(object p)
        {
            if (++PlayerNumber == GameDeck.deck.Length)
                action.Invoke(GameDeck);
            else
            {
                LabelContext = "#" + (PlayerNumber + 1);
                CardPath = GameDeck.deck[PlayerNumber].Path;
                FontColor = GameDeck.deck[PlayerNumber].Color;
            }
        }


        #endregion

        private readonly Action<Deck> action;

        private Deck _GameDeck;

        public Deck GameDeck
        {
            get => _GameDeck;
            set => Set(ref _GameDeck, value);
        }

        public PickingViewModel(Deck gameDeck, Action<Deck> ParentAction)
        {
            action = ParentAction;

            GameDeck = gameDeck;

            NextPlayerCardCommand = new LambdaCommand(OnNextPlayerCardCommandExecuted, CanNextPlayerCardCommandExecuted);

            PlayerNumber = 0;

            LabelContext = "#" + (PlayerNumber + 1);

            CardPath = GameDeck.deck[PlayerNumber].Path;

            FontColor = GameDeck.deck[PlayerNumber].Color;
        }
    }
}
