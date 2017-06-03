using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    /// <summary>
    /// This class represents actual user. This is different from
    /// the Player class which only exists in the context of the game. Outside
    /// of the game it does not exist.
    /// </summary>
    public class User
    {
        private string name = "None";

        public User(string name)
        {
            this.name = name;
        }

    }
}
