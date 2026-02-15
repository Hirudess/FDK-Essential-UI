using FDK.UI.Base.Interface;
using VContainer.Unity;

namespace FDK.UI
{
    public interface ISelectableController
    {

    }
    [System.Serializable]
    public struct SelectableUIRef
    {
        public BaseSelectableGroupUI SelectableListUIPanel;
    }

    public abstract class BaseSelectableController : ISelectableController, IStartable
    {
        protected readonly ISelectableGroupUI _listUIPanel;
        protected readonly ISelectionModel<IGameUIData> _selectableModel;

        public BaseSelectableController(SelectableUIRef refs, ISelectionModel<IGameUIData> selectableModel)
        {
            _selectableModel = selectableModel;
            _listUIPanel = refs.SelectableListUIPanel;

            _listUIPanel.OnCurrentSelectedChangedEvt.AddListener(OnCurrentSelectedUpdated);
            _selectableModel.OnDataUpdatedEvt.AddListener(OnDataUpdated);
        }

        public void Start()
        {
        }

        protected virtual void OnCurrentSelectedUpdated(IGameUIData gameUIData)
        {
            _selectableModel.SetSelected(gameUIData);
        }

        protected virtual void OnDataUpdated(IGameUIData gameUIData)
        {
            _listUIPanel.UpdateUI();
        }
    }
}
