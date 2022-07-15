
using System;

namespace Contacts.Exceptions 
{
    public class RequiredFieldException : Exception 
    {
        public RequiredFieldException(string message) : base(message) 
        {            
        }
    } 
}