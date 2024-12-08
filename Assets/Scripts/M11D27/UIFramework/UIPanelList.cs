using System.Collections.Generic;
using UnityEngine;

namespace M11D27.UIFramework
{
    [CreateAssetMenu(fileName = "UIPanelList.asset", menuName = "UIPanel")]
    public class UIPanelList : ScriptableObject
    {
        public List<PanelInfo> panelList;
    }
}
