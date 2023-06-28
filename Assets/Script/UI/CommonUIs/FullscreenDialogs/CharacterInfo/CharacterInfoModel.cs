using System;
using Script.DataServices.Services.PlayerDataService;
using Script.ProjectLibraries.MVVM;
using UnityEngine;

namespace Script.UI.CommonUIs.FullscreenDialogs.CharacterInfo
{
public class CharacterInfoModel : IModel
{
    private readonly PlayerDataService _playerData;
    public Texture PlayerAvatar { get; private set; }

    public string PlayerName { get; private set; }

    public event Action<Texture> AvatarChanged; 
    public event Action<string> PlayerNameChanged; 


    public CharacterInfoModel(PlayerDataService data)
    {
        var playerProfileContainer = data.DataContainer as PlayerProfileContainer;
        
        _playerData = data;
        PlayerName = playerProfileContainer!.PlayerName;
        PlayerAvatar = playerProfileContainer!.Avatar;
    }

    public void ChangeAvatar(Texture avatar)
    {
        PlayerAvatar = avatar;
        
        AvatarChanged?.Invoke(PlayerAvatar);
    }

    public void ChangeName(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            PlayerName = name;
        }

        PlayerNameChanged?.Invoke(PlayerName);
    }
}
}