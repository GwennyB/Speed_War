using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWar.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool PlayerTurn { get; set; }
        public int FirstCard { get; set; }
        public int SecondCard { get; set; }
        public bool EmptyDecks { get; set; }
    }
}
