using System;

namespace WMTool.Reprocessing.Exceptions
{
    public abstract class ReprocessingException : Exception
    {
        protected ReprocessingException(string message) : base(message)
        {
        }
    }
}
