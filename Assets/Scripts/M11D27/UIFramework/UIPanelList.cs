using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIPanelList.asset", menuName = "UIPanel")]
public class UIPanelList : ScriptableObject
{
    public List<PanelInfo> panelList;
}
