using System;
using System.Runtime.Serialization;
using System.Windows;

namespace StratumUi.Wpf.Core;

[Serializable]
public class StratumException : Exception
{
    public StratumException()
    {
    }

    public StratumException(string message)
        : base(message)
    {
    }

    public StratumException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }

    protected StratumException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}

[Serializable]
public class MissingRequiredTemplatePartException : StratumException
{
    public MissingRequiredTemplatePartException(FrameworkElement target, string templatePart)
        : base($"Template part \"{templatePart}\" in template for \"{target.GetType().FullName}\" is missing.")
    {
    }
        
    protected MissingRequiredTemplatePartException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}