using System.IO;
using Factories;
using GameStates;
using Settings;
using Utils;
using VContainer.Unity;

public class GameController : IStartable
{
    private readonly LevelSettingsParser _levelSettingsParser;
    private readonly GameSettings _gameSettings;
    private readonly FinishScreenPresenterFactory _finishScreenPresenterFactory;
    private readonly GameStateMachine _stateMachine;

    public GameController(
        LevelSettingsParser levelSettingsParser,
        GameSettings gameSettings,
        FinishScreenPresenterFactory finishScreenPresenterFactory,
        GameStateMachine stateMachine)
    {
        _levelSettingsParser = levelSettingsParser;
        _gameSettings = gameSettings;
        _finishScreenPresenterFactory = finishScreenPresenterFactory;
        _stateMachine = stateMachine;
    }

    public async void Start()
    {
        var files = Directory.GetFiles(GlobalConstants.LEVELS_CONFIGS_PATH);
        var levelsSettings = _levelSettingsParser.ParseLevelsFromTextFiles(files);
        _gameSettings.SaveLevelsSettings(levelsSettings);

        _finishScreenPresenterFactory.CreateFinishScreenPresenter();

        await _stateMachine.Enter<LevelLoaderState, LevelLoaderStateContext>(
            new LevelLoaderStateContext(false));
    }
}