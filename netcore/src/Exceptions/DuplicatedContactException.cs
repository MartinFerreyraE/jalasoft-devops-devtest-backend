
using System;

namespace Contacts.Exceptions 
{
    public class DuplicatedContactException : Exception 
    {
        public DuplicatedContactException(string message) : base (message) 
        {            
        }
    }
}