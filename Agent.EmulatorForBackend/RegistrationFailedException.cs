using System.Runtime.Serialization;

namespace Agent.EmulatorForBackend.Exceptions;

#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
[Serializable]
public class RegistrationFailedException : Exception
{
    const string DefaultMessage = "Registration failed.";

    public RegistrationFailedException()
        : base(DefaultMessage)
    {
    }

    public RegistrationFailedException(string? details)
        : base($"{DefaultMessage}{(string.IsNullOrEmpty(details) ? string.Empty : $" Details: {details}")}")
    {
    }

    protected RegistrationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
