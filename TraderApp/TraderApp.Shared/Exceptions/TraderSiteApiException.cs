using System.Runtime.Serialization;

namespace TraderApp.Shared.Exceptions;

[Serializable]
public class TraderSiteApiException : Exception
{
    protected TraderSiteApiException(
        SerializationInfo serializationInfo, 
        StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
    
    public TraderSiteApiException(string message) : base(message)
    {
    }
}
