using MAFIA.Infrastructure.Commands;
using MAFIA.Models;
using MAFIA.ViewModels.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MAFIA.ViewModels
{
    class RoundViewModel : ViewModel
    {
        #region Свойства

        private Player[] _Board;

        public Player[] Board
        {
            get => _Board;
            set => _Board = value;
        }

        private int _RedNumber = 0;
        public int RedNumber
        {
            get => _RedNumber;
            set => Set(ref _RedNumber, value);
        }

        private int _BlackNumber = 0;
        public int BlackNumber
        {
            get => _BlackNumber;
            set => Set(ref _BlackNumber, value);
        }

        private int _RoundNumber = -1;
        public int RoundNumber
        {
            get => _RoundNumber;
            set => Set(ref _RoundNumber, value);
        }

        private string _Round;
        public string Round
        {
            get => _Round;
            set => Set(ref _Round, value);
        }

        private string _ElectedPlayers;
        public string ElectedPlayers
        {
            get => _ElectedPlayers;
            set => Set(ref _ElectedPlayers, value);
        }

        private int _SelectedPlayerNumber;
        public int SelectedPlayerNumber
        {
            get => _SelectedPlayerNumber;
            set => Set(ref _SelectedPlayerNumber, value);
        }

        private bool _CouldBeSelected;
        public bool CouldBeSelected
        {
            get => _CouldBeSelected;
            set => Set(ref _CouldBeSelected, value);
        }


        private string _Header;
        public string Header
        {
            get => _Header;
            set => Set(ref _Header, value);
        }

        private string _UnderHeader;
        public string UnderHeader
        {
            get => _UnderHeader;
            set => Set(ref _UnderHeader, value);
        }

        private int _HeaderSize;
        public int HeaderSize
        {
            get => _HeaderSize;
            set => Set(ref _HeaderSize, value);
        }

        private int _CurrentPlayerNumber;
        public int CurrentPlayerNumber
        {
            get => _CurrentPlayerNumber;
            set => Set(ref _CurrentPlayerNumber, value);
        }

        private int _FirstPlayer = -1;
        public int FirstPlayer
        {
            get => _FirstPlayer;
            set => Set(ref _FirstPlayer, value);
        }

        private int[] _Queue;
        public int[] Queue
        {
            get => _Queue;
            set => Set(ref _Queue, value);
        }

        private int _PlayersAliveNumber;
        public int PlayersAliveNumber
        {
            get => _PlayersAliveNumber;
            set => Set(ref _PlayersAliveNumber, value);
        }

        private int _ElectedPlayersNumber;
        public int ElectedPlayersNumber
        {
            get => _ElectedPlayersNumber;
            set => Set(ref _ElectedPlayersNumber, value);
        }

        private string _Mode;
        public string Mode
        {
            get => _Mode;
            set
            {
                Set(ref _Mode, value);
                switch (Mode)
                {
                    case "mafiaminute":
                        MafiaMinuteInitialize();
                        break;
                    case "day":
                        DayInitialize();
                        break;
                    case "election":
                        ElectionInitialize();
                        break;
                    case "night":
                        NightInitialize();
                        break;
                    default:
                        break;
                }
            }
        }

        private string _Statusbar;
        public string StatusBar
        {
            get => _Statusbar;
            set => Set(ref _Statusbar, value);
        }

        #region Кнопки

        private bool _PlayButtonIsEnabled;
        public bool PlayButtonIsEnabled
        {
            get => _PlayButtonIsEnabled;
            set => Set(ref _PlayButtonIsEnabled, value);
        }

        private bool _PauseButtonIsEnabled;
        public bool PauseButtonIsEnabled
        {
            get => _PauseButtonIsEnabled;
            set => Set(ref _PauseButtonIsEnabled, value);
        }

        #endregion

        #region Часы

        private string _StopWatch;

        public string StopWatch
        {
            get => _StopWatch;
            set => Set(ref _StopWatch, value);
        }

        private DispatcherTimer _Timer ;

        public DispatcherTimer Timer
        {
            get => _Timer;
            set => Set(ref _Timer, value);
        }

        private TimeSpan _Time;

        public TimeSpan Time
        {
            get => _Time;
            set => Set(ref _Time, value);
        }

        private int _Seconds = 58;

        public int Seconds
        {
            get => _Seconds;
            set => Set(ref _Seconds, value);
        }

        #endregion

        #region Роли

        private int _ActiveRolesNumber = 1;
        public int ActiveRolesNumber
        {
            get => _ActiveRolesNumber;
            set => Set(ref _ActiveRolesNumber, value);
        }

        private bool[] _RolesPriority = {true, false, false, false };
        public bool[] RolesPriority
        {
            get => _RolesPriority;
            set => Set(ref _RolesPriority, value);
        }

        private string[,] _RolesPhrases = new string[,] { { "Мафия выходит", "на охоту" },{ "Дон ищет", "Шерифа" }, { "Шериф ищет", "Мафию" }, { "Доктор выбирает,", "кого вылечить"} };
        public string[,] RolesPhrases
        {
            get => _RolesPhrases;
            set => Set(ref _RolesPhrases, value);
        }

        private int _CurrentActiveRole;
        public int CurrentActiveRole
        {
            get => _CurrentActiveRole;
            set => Set(ref _CurrentActiveRole, value);
        }

        private int _MafiaChoice;
        public int MafiaChoice
        {
            get => _MafiaChoice;
            set => Set(ref _MafiaChoice, value);
        }

        private int _DoctorChoice = 0;
        public int DoctorChoice
        {
            get => _DoctorChoice;
            set => Set(ref _DoctorChoice, value);
        }

        #endregion

        #endregion

        #region Команды

        public ICommand StopWatchStartCommand { get; }
        private bool CanStopWatchStartCommandExecuted(object p) => true;
        private void OnStopWatchStartCommandExecuted(object p)
        {
            StopWatch = "00:" + (Seconds + 1);
            Timer.Start();
        }

        public ICommand StopWatchPauseCommand { get; }
        private bool CanStopWatchPauseCommandExecuted(object p) => true;
        private void OnStopWatchPauseCommandExecuted(object p)
        {
            Timer.Stop();            
            Seconds = (StopWatch[3] - 48) * 10 + StopWatch[4] - 50;
            Time = TimeSpan.FromSeconds(Seconds);
        }

        public ICommand NextPlayerCommand { get; }
        private bool CanNextPlayerCommandExecuted(object p) => true;
        private void OnNextPlayerCommandExecuted(object p)
        {
            switch (Mode)
            {
                case "mafiaminute":
                    MafiaMinuteNextButton();
                    break;
                case "day":
                    DayNextButton();
                    break;
                case "election":
                    ElectionNextButton();
                    break;
                case "night":
                    NightNextButton();
                    break;
                default:
                    Application.Current.Shutdown();
                    break;
            }            
        }

        #endregion


        public void DayInitialize()
        {
            FirstPlayerInitialize();
            StopWatch = "01:00";
            CurrentPlayerNumber = FirstPlayer;
            HeaderSize = 100;
            Header = "#" + (CurrentPlayerNumber + 1);
            UnderHeader = null;
            StatusBar = "Выставить: ";
            Round = (++RoundNumber).ToString() + " Круг";
            PauseButtonIsEnabled = PlayButtonIsEnabled = CouldBeSelected = true;
        }
        public void ElectionInitialize()
        {
            StopWatch = "";
            PauseButtonIsEnabled = PlayButtonIsEnabled = false;
            if (ElectedPlayersNumber > 0)
            {
                Header = "#" + Queue[0];
                StatusBar = "Проголосовало: ";
            }
            else
            {
                Header = "Никто не был выставлен";
                UnderHeader = "на голосование";
                CouldBeSelected = false;
                HeaderSize = 50;
                StatusBar = "";
            }
        }
        public void NightInitialize()
        {
            HeaderSize = 50;
            StatusBar = "";
            Header = RolesPhrases[0, 0];
            UnderHeader = RolesPhrases[0, 1];
            CurrentActiveRole = 0;
            if (CouldBeSelected == false) CouldBeSelected = true;
        }
        public void MafiaMinuteInitialize()
        {
            StopWatch = "01:00";
            Seconds = 58;
            Header = "Мафии предоставлена";
            UnderHeader = "минута на договорку";
            HeaderSize = 50;
            PlayButtonIsEnabled = PauseButtonIsEnabled = true;
            CouldBeSelected = false;
        }

        public void DayNextButton()
        {
            if (Timer.IsEnabled)
            {
                Timer.Stop();
            }
            Seconds = 58;
            Time = TimeSpan.FromSeconds(Seconds);
            StopWatch = "01:00";

            if ((SelectedPlayerNumber > 0) && (SelectedPlayerNumber <= Board.Length))
            {
                if (Board[SelectedPlayerNumber - 1].IsAlive == true)
                {
                    if (Board[SelectedPlayerNumber - 1].IsElected == false)
                    {
                        if (ElectedPlayersNumber == 0)
                            ElectedPlayers += " " + SelectedPlayerNumber.ToString();
                        else
                            ElectedPlayers += ", " + SelectedPlayerNumber.ToString();

                        Board[SelectedPlayerNumber - 1].IsElected = true;
                        Queue[ElectedPlayersNumber++] = SelectedPlayerNumber;
                    }
                }
            }

            for (; ++CurrentPlayerNumber <= Board.Length;)
            {
                if (CurrentPlayerNumber == Board.Length)
                {
                    CurrentPlayerNumber = -1;
                    continue;
                }

                if (CurrentPlayerNumber == FirstPlayer)
                {
                    Mode = "election";
                    return;
                }
                if (Board[CurrentPlayerNumber].IsAlive == true)
                {
                    break;
                }
            }

            Header = "#" + (CurrentPlayerNumber + 1);

            SelectedPlayerNumber = 0;
        }
        public void ElectionNextButton()
        {
            if (UnderHeader != null)
            {
                Mode = "night";
                return;
            }
        }
        public void NightNextButton()
        {
            switch (CurrentActiveRole)
            {
                case 0:
                    MafiaChoice = SelectedPlayerNumber;
                    break;
                case 3:
                    DoctorChoice = SelectedPlayerNumber;
                    break;
                default:
                    break;
            }
            CurrentActiveRole++;
            for (; CurrentActiveRole < 4; CurrentActiveRole++)
            {
                if (RolesPriority[CurrentActiveRole] == true)
                {
                    Header = RolesPhrases[CurrentActiveRole, 0];
                    UnderHeader = RolesPhrases[CurrentActiveRole, 1];
                    break;
                }
            }
            switch (CurrentActiveRole)
            {
                case 0:
                    MafiaChoice = SelectedPlayerNumber;
                    break;
                case 1:
                    CouldBeSelected = false;
                    SelectedPlayerNumber = 0;
                    break;
                case 2:
                    if (CouldBeSelected == true) CouldBeSelected = false;
                    SelectedPlayerNumber = 0;
                    break;
                case 3:
                    if (CouldBeSelected == false) CouldBeSelected = true;
                    SelectedPlayerNumber = 0;
                    break;
                default:
                    if (DoctorChoice != MafiaChoice)
                        PlayerDies(MafiaChoice);
                    if (CheckEnd())
                    {
                        Mode = "end";
                        return;
                    }                    
                    Mode = "day";
                    return;
            }

        }
        public void MafiaMinuteNextButton()
        {
            if (Timer.IsEnabled)
            {
                Timer.Stop();
            }
            Mode = "day";
        }

        public void FirstPlayerInitialize()
        {            
            ++FirstPlayer;
            for (; ; FirstPlayer++)
            {
                if (FirstPlayer == Board.Length)
                {
                    FirstPlayer = -1;
                }
                else
                    if (Board[FirstPlayer].IsAlive == true)
                    {
                        break;
                    }
            }
        }
        public void BoardInitialize(Deck GameDeck)
        {
            Board = new Player[GameDeck.deck.Length];

            for (int i = 0; i < Board.Length; i++)
            {
                Board[i] = new Player();
                Board[i].card = GameDeck.deck[i];
                if (GameDeck.deck[i].Color == "Red")
                    RedNumber++;
                else
                    BlackNumber++;
                switch (GameDeck.deck[i].Name)
                {
                    case "Citizen":
                        break;
                    case "Don":
                        ActiveRolesNumber++;
                        RolesPriority[1] = true;
                        break;
                    case "Sheriff":
                        ActiveRolesNumber++;
                        RolesPriority[2] = true;
                        break;
                    case "Doctor":
                        ActiveRolesNumber++;
                        RolesPriority[3] = true;
                        break;
                    default:
                        break;
                }
            }
        }

        public void PlayerDies(int Number)
        {
            Board[--Number].IsAlive = false;
            if (Board[Number].card.Color == "Red")
            {
                RedNumber--;
            }
            else
                BlackNumber--;
        }

        public bool CheckEnd()
        {
            if (RedNumber <= BlackNumber)
            {
                HeaderSize = 50;
                Header = "Победа";
                UnderHeader = "Мафии";
                CouldBeSelected = PauseButtonIsEnabled = PlayButtonIsEnabled = false;
                return true;
            }
            if (BlackNumber == 0)
            {
                HeaderSize = 50;
                Header = "Победа";
                UnderHeader = " Мирного города";
                CouldBeSelected = PauseButtonIsEnabled = PlayButtonIsEnabled = false;
                return true;
            }
            return false;
        }

        public RoundViewModel(Deck GameDeck)
        {
            BoardInitialize(GameDeck);

            PlayersAliveNumber = GameDeck.deck.Length;

            Queue = new int[PlayersAliveNumber];

            Time = TimeSpan.FromSeconds(Seconds);

            Timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, delegate
            {
                StopWatch = Time.ToString("c");
                StopWatch = StopWatch.Substring(3);
                if (Time == TimeSpan.Zero) Timer.Stop();
                Time = Time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
            Timer.Stop();

            StopWatchStartCommand = new LambdaCommand(OnStopWatchStartCommandExecuted, CanStopWatchStartCommandExecuted);
            StopWatchPauseCommand = new LambdaCommand(OnStopWatchPauseCommandExecuted, CanStopWatchPauseCommandExecuted);
            NextPlayerCommand = new LambdaCommand(OnNextPlayerCommandExecuted, CanNextPlayerCommandExecuted);

            Mode = "mafiaminute";
        }

    }
}
