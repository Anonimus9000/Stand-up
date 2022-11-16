using System.Threading;
using UnityEditor;
using UnityEngine;

namespace Script.Utils.ThreadUtils
{
public class ApplicationQuitTokenSource
{
    public CancellationToken Token { get; }

    public SynchronizationContext UnityContext { get; private set; }

    private readonly CancellationTokenSource _quitSource;

    public ApplicationQuitTokenSource()
    {
        _quitSource = new CancellationTokenSource();
        Token = _quitSource.Token;

        MainThreadInitialize();
    }

    private void MainThreadInitialize()
    {
        UnityContext = SynchronizationContext.Current;
        Application.quitting += _quitSource.Cancel;
#if UNITY_EDITOR
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
    }

#if UNITY_EDITOR
    private void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            _quitSource.Cancel();
        }
    }
#endif
}
}