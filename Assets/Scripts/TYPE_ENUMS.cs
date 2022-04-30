public enum PanelType : int
{
    MainPanel,
    SingleplayerPanel,
    MultiplayerPanel,
    DashboardPanel,
    UpgradesPanel,
    SettingsPanel
}

public enum PrefsNames : byte
{
    firstLogin
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