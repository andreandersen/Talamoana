using System;

namespace Talamoana.Domain.Core.Modifiers.Exceptions
{
    /// <summary>
    /// Exception thrown when values does not match modifiers
    /// when materializing a modifier.
    /// </summary>
    [Serializable]
    public class ValuesDoNotMatchModifierException : Exception
    {
        public ValuesDoNotMatchModifierException() : base("The values do not match the required by the modifier")
        {
        }

        public ValuesDoNotMatchModifierException(string message) : base(message)
        {
        }

        public ValuesDoNotMatchModifierException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}