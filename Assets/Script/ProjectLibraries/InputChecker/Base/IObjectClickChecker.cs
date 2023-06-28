using System;

namespace Script.ProjectLibraries.InputChecker.Base
{
public interface IObjectClickChecker
{
    bool IsBlockedByUI { get; }
    event Action ObjectClicked;
    void OnObjectClicked();
    void Activate();
    void Deactivate();
}
}