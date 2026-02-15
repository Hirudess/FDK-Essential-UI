using FDK.UI.Base.Interface;
using System.Collections.Generic;

namespace FDK.UI
{
    [System.Serializable]
    public struct GenericSelectableListData
    {
        public List<string> GenericSelectableData;
    }

    public class GenericSelectableListController : BaseSelectableController
    {
        public GenericSelectableListController(GenericSelectableListData uiData, SelectableUIRef refs, ISelectionModel selectableModel) : base(refs, selectableModel)
        {
            var data = uiData.GenericSelectableData;
            var list = new List<IGameUIData>();
            foreach (var id in data)
            {
                var genericUI = new GenericUIData(id);
                list.Add(genericUI);
            }

            _selectableModel.Initialize(list);
        }
    }
}
