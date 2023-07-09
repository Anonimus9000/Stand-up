using Script.DataServices.Base;
using Script.ProjectLibraries.MVVM;

namespace Script.UI.CommonUIs.FullscreenDialogs.CharacterInfo.Characteristics
{
public class CharacteristicsListViewModel : ViewModel
{
    private readonly CharacteristicsListModel _model;
    private readonly CharacteristicsListView _view;

    public CharacteristicsListViewModel(CharacteristicsListView view, IDataService dataService)
    {
        _view = AddDisposable(view);

        var prefab = view.CharacteristicElementPrefab;
        var parent = view.CharacteristicsParent;
        _model = AddDisposable(new CharacteristicsListModel(dataService, prefab, parent));
    }

    public void Deinit()
    {
        _model.DeinitElements();
    }
}
}