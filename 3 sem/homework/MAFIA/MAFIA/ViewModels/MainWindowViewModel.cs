using MAFIA.Infrastructure.Commands;
using MAFIA.Models;
using MAFIA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MAFIA.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Заголовок окна
        private string _Title = "MAFIA";

        /// <summary>
        /// Заголовок окна 
        /// </summary>

        public string Title
        {
            get { return _Title; }
            set
            {
                //if (Equals(_Title, value)) return;
                //_Title = value;
                //OnPropertyChanged();

                Set(ref _Title, value);
            }
        }
        #endregion

        #region Фристайл

        private Deck _gameDeck = new Deck(0);

        public Deck GameDeck
        {
            get => _gameDeck;
            set => Set(ref _gameDeck, value);
        }

        private void NextWindowTemplateAction(Deck _GameDeck)
        {
            GameDeck = _GameDeck;
            WindowTemplates[1] = new RandomizeViewModel(GameDeck.deck.Length, NextWindowTemplateAction);
            CurrentViewModel = WindowTemplates[++WindowTemplateNumber] ;
        }

        private void NextWindowTemplateAction()
        {
            CurrentViewModel = new PickingViewModel(GameDeck, GameWindowTemplateAction);
        }

        private void GameWindowTemplateAction(Deck gameDeck)
        {
            CurrentViewModel = new RoundViewModel(gameDeck);
        }

        private int _WindowTemplateNumber = 0;

        public int WindowTemplateNumber
        {
            get => _WindowTemplateNumber;
            set => Set(ref _WindowTemplateNumber, value);
        }

        

        #endregion

        #region ViewModelsы

        private ViewModel _CurrentViewModel;
        private ViewModel[] _WindowTemplates = new ViewModel[4];
        

        public ViewModel[] WindowTemplates
        {
            get => _WindowTemplates;
            set => Set(ref _WindowTemplates, value);
        }

        public ViewModel CurrentViewModel
        {
            get => _CurrentViewModel;
            set
            {
                Set(ref _CurrentViewModel, value);
            }
        }

        //private SettingsViewModel _settingsViewModel = new SettingsViewModel(NextTemplateAction);
        //private PickingViewModel _pickingViewModel = new PickingViewModel();

        //public ViewModel settingsViewModel
        //{
        //    get => _settingsViewModel;
        //    set => _settingsViewModel = new SettingsViewModel(NextTemplateAction);
        //}

        //public ViewModel pickingViewModel
        //{
        //    get => _pickingViewModel;
        //    set => _pickingViewModel = new PickingViewModel();
        //}
        #endregion

        #region Команды
        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecuted(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #region NextWindowTemplateCommand

        

        #endregion

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);


            #endregion

            #region Свойства

            WindowTemplates[0] = new SettingsViewModel(NextWindowTemplateAction);
            CurrentViewModel = WindowTemplates[0];

            #endregion
        }
    }
}
