using MAFIA.Infrastructure.Commands;
using MAFIA.Models;
using MAFIA.ViewModels.Base;

using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAFIA.ViewModels
{
    class RandomizeViewModel : ViewModel
    {
        private string _RandomString;
        private readonly Action _action;

        public string RandomString
        {
            get => _RandomString;
            set => Set(ref _RandomString, value);
        }

        public ICommand NextWindowTemplateCommand { get; }
        private bool CanNextWindowTemplateCommandExecuted(object p)
        {
            return true;
        }
        private void OnNextWindowTemplateCommandExecuted(object p)
        {
            _action.Invoke();
        }

        public RandomizeViewModel(int PlayersNumber, Action action)
        {
            _action = action;

            Random random = new Random();

            int temp = random.Next(0, PlayersNumber);

            int[] RandomSet = new int[PlayersNumber];

            for (int i = 0; i < RandomSet.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (RandomSet[j] == temp)
                    { temp = random.Next(0, PlayersNumber); j = -1; }
                }
                RandomSet[i] = temp;

                random.Next(0, PlayersNumber);
            }

            RandomString = (RandomSet[0] + 1).ToString();

            for (int i = 1; i < PlayersNumber; i++)
            {
                RandomString += ", " + (RandomSet[i] + 1).ToString();
            }

            NextWindowTemplateCommand = new LambdaCommand(OnNextWindowTemplateCommandExecuted, CanNextWindowTemplateCommandExecuted);
        }
    }
}
