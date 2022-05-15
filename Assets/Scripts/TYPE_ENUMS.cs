public enum PanelType : int
{
    MainPanel,
    SingleplayerPanel,
    MultiplayerPanel,
    DashboardPanel,
    UpgradesPanel,
    SettingsPanel
}

public enum PREFS_NAMES : byte
{
    FirstLogin,
    MuteSoundName,
    MuteMusicName,
    MuteVibroName
}

public enum UpgradeType : byte
{
    HP,//
    Damage,//
    Reload,//
    MoveSpeed,//
    BulletSpeed,//
    ShieldHP
}

public enum CurrencyType : byte
{
    GC
}

public enum DialogWindowType : byte
{
    WarningWindow,
    OptionalWindow
}

public class PlayFabEnums
{
    public enum LeaderboardType : byte
    {
        MMR
    }
    
    public enum PlayerDataType : byte
    {
        Upgrades
    }
}