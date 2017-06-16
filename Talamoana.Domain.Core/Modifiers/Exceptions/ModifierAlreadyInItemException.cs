using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talamoana.Domain.Core.Modifiers.Exceptions
{
    [Serializable]
    public class ModifierAlreadyInItemException : Exception
    {
        public ModifierAlreadyInItemException() : base("This modifier is already in the modifier list")
        {
        }

        public ModifierAlreadyInItemException(string message) : base(message)
        {
        }

        public ModifierAlreadyInItemException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
