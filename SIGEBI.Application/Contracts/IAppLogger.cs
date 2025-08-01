﻿
namespace SIGEBI.Application.Contracts
{
    public interface IAppLogger<T>
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(Exception ex, string message, params object[] args);
        void LogError(string message, params object[] args);

    }
}
