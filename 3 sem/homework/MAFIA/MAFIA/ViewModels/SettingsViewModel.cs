using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MAFIA.Infrastructure.Commands;
using MAFIA.Models;
using MAFIA.ViewModels.Base;

namespace MAFIA.ViewModels
{
    class SettingsViewModel : ViewModel
    {
        private int _PlayersNumber = 10;
        private int _MafiasNumber = 2;
        private int _DonNumber = 1;
        private int _DoctorNumber = 0;
        private int _SheriffNumber = 1;
        private int _CitizensNumber = 6;

        public int PlayersNumber
        {
            get => _PlayersNumber;
            set
            {
                if (value > 0)
                {
                    Set(ref _PlayersNumber, value);
                }
                else
                    Set(ref _PlayersNumber, 4);
                CitizensNumber = PlayersNumber - SheriffNumber - DoctorNumber - MafiasNumber - DonNumber;
            }
        }

        public int MafiasNumber
        {
            get => _MafiasNumber;
            set
            {
                if (value > 0)
                {
                    Set(ref _MafiasNumber, value);
                }
                else
                    Set(ref _MafiasNumber, 1);
                CitizensNumber = PlayersNumber - SheriffNumber - DoctorNumber - MafiasNumber - DonNumber;
            }
        }

        public int DonNumber
        {
            get => _DonNumber;
            set
            {
                Set(ref _DonNumber, value);
                CitizensNumber = PlayersNumber - SheriffNumber - DoctorNumber - MafiasNumber - DonNumber;
            }
        }

        public int DoctorNumber
        {
            get => _DoctorNumber;
            set
            {
                Set(ref _DoctorNumber, value);
                CitizensNumber = PlayersNumber - SheriffNumber - DoctorNumber - MafiasNumber - DonNumber;
            }
        }

        public int SheriffNumber
        {
            get => _SheriffNumber;
            set
            {
                Set(ref _SheriffNumber, value);
                CitizensNumber = PlayersNumber - SheriffNumber - DoctorNumber - MafiasNumber - DonNumber;
            }
        }

        public int CitizensNumber
        {
            get => _CitizensNumber;

            set
            {
                if (value >= 0)
                    Set(ref _CitizensNumber, value);
            }
        }
        

        public ICommand NextWindowTemplateCommand { get; }
        private bool CanNextWindowTemplateCommandExecuted(object p)
        {
            return true;
        }
        private void OnNextWindowTemplateCommandExecuted(object p)
        {
            if (PlayersNumber != SheriffNumber + MafiasNumber + CitizensNumber + DonNumber + DoctorNumber)
                PlayersNumber = SheriffNumber + MafiasNumber + CitizensNumber + DonNumber + DoctorNumber;
            else
            if ((MafiasNumber + DonNumber + 1 <= CitizensNumber + DoctorNumber + SheriffNumber))
            {
                Deck GameDeck = new Deck(PlayersNumber);
                int i = 0;
                for (; i < MafiasNumber; i++)
                {
                    GameDeck.deck[i] = new Card("Mafia");
                }
                if (DoctorNumber == 1)
                    GameDeck.deck[i++] = new Card("Doctor");
                if (SheriffNumber == 1)
                    GameDeck.deck[i++] = new Card("Sheriff");
                if (DonNumber == 1)
                    GameDeck.deck[i++] = new Card("Don");
                for (; i < PlayersNumber; i++)
                {
                    GameDeck.deck[i] = new Card();
                }
                    
                GameDeck.ShuffleDeck();
                action.Invoke(GameDeck); 
            }
                else
                    MessageBox.Show("Мирных ролей должно быть больше!");
        }

        private readonly Action<Deck> action;        

        public SettingsViewModel(Action<Deck> ParentAction)
        {
            action = ParentAction;
            NextWindowTemplateCommand = new LambdaCommand(OnNextWindowTemplateCommandExecuted, CanNextWindowTemplateCommandExecuted);
            
        }
    }
}
