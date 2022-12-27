using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogs.PopupDialogs.Components
{
public class ActionFieldsSetter: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _actionTitle;
    [SerializeField] private TextMeshProUGUI _actionDescription;
    [SerializeField] private Image _actionIcon;

    public void Init(Sprite actionIcon, string actionTitle, List<ActionRewardConstructor> actionDescription)
    {
        _actionDescription.text = string.Empty;
        _actionTitle.text = actionTitle;
        _actionIcon.sprite = actionIcon;
        
        foreach (var actionRewards in actionDescription)
        {
            _actionDescription.text += actionRewards.RewardValue + " " + actionRewards.RewardTitle + "\n";
        }
    }
}
}