using System;

namespace Script.InputChecker.Base
{
public interface IObjectClickChecker
{
    event Action ObjectClicked;
    void OnObjectClicked();
    void Activate();
    void Deactivate();
}
}