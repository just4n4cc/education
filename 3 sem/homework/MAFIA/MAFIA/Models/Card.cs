using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MAFIA.Models
{
    class Card
    {
        private string _name;
        private string _path;
        private string _color;

        public string Name { get => _name; }
        public string Path { get => _path; }
        public string Color { get => _color; }

        public Card()
        {
            _name = "Citizen";
            _path = "/../../Images/card_citizen.jpg";
            _color = "Red";
        }

        public Card(string Name)
        {
            _name = Name;
            switch (Name)
            {
                case "Mafia":
                    _color = "Black";
                    _path = "/../../Images/card_mafia.jpg";
                    break;
                case "Don":
                    _color = "Black";
                    _path = "/../../Images/card_don.jpg";
                    break;
                case "Sheriff":
                    _color = "Red";
                    _path = "/../../Images/card_comissar.jpg";
                    break;
                case "Doctor":
                    _color = "Red";
                    _path = "/../../Images/card_doctor.jpg";
                    break;
            }
        }
    }
}
