using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Script.UI.Dialogs.PopupDialogs.Components
{
public class ActionFieldsSetter: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _actionTitle;
    [SerializeField] private TextMeshProUGUI _actionDescription;

    public void Init(string actionTitle, List<ActionRewardConstructor> actionDescription)
    {
        _actionDescription.text = string.Empty;
        _actionTitle.text = actionTitle;
        
        foreach (var actionRewards in actionDescription)
        {
            _actionDescription.text += actionRewards.RewardValue + " " + actionRewards.RewardTitle + "\n";
        }
    }
}
}