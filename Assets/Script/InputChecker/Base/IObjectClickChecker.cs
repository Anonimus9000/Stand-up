using System;

namespace Script.InputChecker.Base
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