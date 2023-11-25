namespace Itmo.ObjectOrientedProgramming.Lab3.Loggers;

public interface ILogger
{
    public void LogOut(string logOut);
    public void WarningOut(string warningOut);
    public void ErrorOut(string errorOut);
}