﻿using ClientCore.Settings;
using Rampastring.Tools;
using System;
using System.Windows.Forms;

namespace ClientCore
{
    public class UserINISettings
    {
        private static UserINISettings _instance;

        public static UserINISettings Instance
        {
            get
            {
                if (_instance == null)
                    throw new InvalidOperationException("UserINISettings not initialized!");

                return _instance;
            }
        }

        public static void Initialize(string iniFileName)
        {
            if (_instance != null)
                throw new InvalidOperationException("UserINISettings has already been initialized!");

            var iniFile = new IniFile(ProgramConstants.GamePath + iniFileName);

            _instance = new UserINISettings(iniFile);
        }

        protected UserINISettings(IniFile iniFile)
        {
            SettingsIni = iniFile;

            const string VIDEO = "Video";
            const string MULTIPLAYER = "MultiPlayer";
            const string OPTIONS = "Options";
            const string AUDIO = "Audio";
#if YR
            const string WINDOWED_MODE_KEY = "Windowed";
            BackBufferInVRAM = new BoolSetting(iniFile, VIDEO, "VideoBackBuffer", false);
#else
            const string WINDOWED_MODE_KEY = "Video.Windowed";
            BackBufferInVRAM = new BoolSetting(iniFile, VIDEO, "UseGraphicsPatch", true);
#endif

            IngameScreenWidth = new IntSetting(iniFile, VIDEO, "ScreenWidth", 1024);
            IngameScreenHeight = new IntSetting(iniFile, VIDEO, "ScreenHeight", 768);
            ClientTheme = new IntSetting(iniFile, MULTIPLAYER, "Theme", 0);
            DetailLevel = new IntSetting(iniFile, OPTIONS, "DetailLevel", 2);
            Renderer = new StringSetting(iniFile, "Compatibility", "Renderer", string.Empty);
            WindowedMode = new BoolSetting(iniFile, VIDEO, WINDOWED_MODE_KEY, false);
            BorderlessWindowedMode = new BoolSetting(iniFile, VIDEO, "NoWindowFrame", false);

            ClientResolutionX = new IntSetting(iniFile, VIDEO, "ClientResolutionX", Screen.PrimaryScreen.Bounds.Width);
            ClientResolutionY = new IntSetting(iniFile, VIDEO, "ClientResolutionY", Screen.PrimaryScreen.Bounds.Height);
            BorderlessWindowedClient = new BoolSetting(iniFile, VIDEO, "BorderlessWindowedClient", true);

            ScoreVolume = new DoubleSetting(iniFile, AUDIO, "ScoreVolume", 0.7);
            SoundVolume = new DoubleSetting(iniFile, AUDIO, "SoundVolume", 0.7);
            VoiceVolume = new DoubleSetting(iniFile, AUDIO, "VoiceVolume", 0.7);
            IsScoreShuffle = new BoolSetting(iniFile, AUDIO, "IsScoreShuffle", true);
            ClientVolume = new DoubleSetting(iniFile, AUDIO, "ClientVolume", 1.0);
            PlayMainMenuMusic = new BoolSetting(iniFile, AUDIO, "PlayMainMenuMusic", true);
            StopMusicOnMenu = new BoolSetting(iniFile, AUDIO, "StopMusicOnMenu", true);
            MessageSound = new BoolSetting(iniFile, AUDIO, "ChatMessageSound", true);

            ScrollRate = new IntSetting(iniFile, OPTIONS, "ScrollRate", 3);
            TargetLines = new BoolSetting(iniFile, OPTIONS, "UnitActionLines", true);
            ScrollCoasting = new IntSetting(iniFile, OPTIONS, "ScrollMethod", 0);
            Tooltips = new BoolSetting(iniFile, OPTIONS, "ToolTips", true);
            ShowHiddenObjects = new BoolSetting(iniFile, OPTIONS, "ShowHidden", true);
            MoveToUndeploy = new BoolSetting(iniFile, OPTIONS, "MoveToUndeploy", true);
            TextBackgroundColor = new IntSetting(iniFile, OPTIONS, "TextBackgroundColor", 0);
            DragDistance = new IntSetting(iniFile, OPTIONS, "DragDistance", 4);

            PlayerName = new StringSetting(iniFile, MULTIPLAYER, "Handle", string.Empty);

            ChatColor = new IntSetting(iniFile, MULTIPLAYER, "ChatColor", -1);
            LANChatColor = new IntSetting(iniFile, MULTIPLAYER, "LANChatColor", -1);
            PingUnofficialCnCNetTunnels = new BoolSetting(iniFile, MULTIPLAYER, "PingCustomTunnels", true);
            WritePathToRegistry = new BoolSetting(iniFile, OPTIONS, "WriteInstallationPathToRegistry", true);
            PlaySoundOnGameHosted = new BoolSetting(iniFile, MULTIPLAYER, "PlaySoundOnGameHosted", true);
            SkipConnectDialog = new BoolSetting(iniFile, MULTIPLAYER, "SkipConnectDialog", false);
            PersistentMode = new BoolSetting(iniFile, MULTIPLAYER, "PersistentMode", false);
            AutomaticCnCNetLogin = new BoolSetting(iniFile, MULTIPLAYER, "AutomaticCnCNetLogin", false);
            NotifyOnUserListChange = new BoolSetting(iniFile, MULTIPLAYER, "NotifyOnUserListChange", true);
            EnableMapSharing = new BoolSetting(iniFile, MULTIPLAYER, "EnableMapSharing", true);
            AlwaysDisplayTunnelList = new BoolSetting(iniFile, MULTIPLAYER, "AlwaysDisplayTunnelList", false);

            CheckForUpdates = new BoolSetting(iniFile, OPTIONS, "CheckforUpdates", true);

            IsFirstRun = new BoolSetting(iniFile, OPTIONS, "IsFirstRun", true);
            Difficulty = new IntSetting(iniFile, OPTIONS, "Difficulty", 1);
            GameSpeed = new IntSetting(iniFile, OPTIONS, "GameSpeed", 1);
            PreloadMapPreviews = new BoolSetting(iniFile, VIDEO, "PreloadMapPreviews", false);
            ForceLowestDetailLevel = new BoolSetting(iniFile, VIDEO, "ForceLowestDetailLevel", false);
            MinimizeWindowsOnGameStart = new BoolSetting(iniFile, OPTIONS, "MinimizeWindowsOnGameStart", true);
            AutoRemoveUnderscoresFromName = new BoolSetting(iniFile, OPTIONS, "AutoRemoveUnderscoresFromName", true);
        }

        public IniFile SettingsIni { get; private set; }

        public event EventHandler SettingsSaved;

        /*********/
        /* VIDEO */
        /*********/

        public IntSetting IngameScreenWidth { get; private set; }
        public IntSetting IngameScreenHeight { get; private set; }
        public IntSetting ClientTheme { get; private set; }
        public IntSetting DetailLevel { get; private set; }
        public StringSetting Renderer { get; private set; }
        public BoolSetting WindowedMode { get; private set; }
        public BoolSetting BorderlessWindowedMode { get; private set; }
        public BoolSetting BackBufferInVRAM { get; private set; }
        public IntSetting ClientResolutionX { get; private set; }
        public IntSetting ClientResolutionY { get; private set; }
        public BoolSetting BorderlessWindowedClient { get; private set; }

        /*********/
        /* AUDIO */
        /*********/

        public DoubleSetting ScoreVolume { get; private set; }
        public DoubleSetting SoundVolume { get; private set; }
        public DoubleSetting VoiceVolume { get; private set; }
        public BoolSetting IsScoreShuffle { get; private set; }
        public DoubleSetting ClientVolume { get; private set; }
        public BoolSetting PlayMainMenuMusic { get; private set; }
        public BoolSetting StopMusicOnMenu { get; private set; }
        public BoolSetting MessageSound { get; private set; }

        /********/
        /* GAME */
        /********/

        public IntSetting ScrollRate { get; private set; }
        public BoolSetting TargetLines { get; private set; }
        public IntSetting ScrollCoasting { get; private set; }
        public BoolSetting Tooltips { get; private set; }
        public BoolSetting ShowHiddenObjects { get; private set; }
        public BoolSetting MoveToUndeploy { get; private set; }
        public IntSetting TextBackgroundColor { get; private set; }
        public IntSetting DragDistance { get; private set; }

        /************************/
        /* MULTIPLAYER (CnCNet) */
        /************************/

        public StringSetting PlayerName { get; private set; }

        public IntSetting ChatColor { get; private set; }
        public IntSetting LANChatColor { get; private set; }
        public BoolSetting PingUnofficialCnCNetTunnels { get; private set; }
        public BoolSetting WritePathToRegistry { get; private set; }
        public BoolSetting PlaySoundOnGameHosted { get; private set; }

        public BoolSetting SkipConnectDialog { get; private set; }
        public BoolSetting PersistentMode { get; private set; }
        public BoolSetting AutomaticCnCNetLogin { get; private set; }

        public BoolSetting NotifyOnUserListChange { get; private set; }

        public BoolSetting EnableMapSharing { get; private set; }

        public BoolSetting AlwaysDisplayTunnelList { get; private set; }

        /********/
        /* MISC */
        /********/

        public BoolSetting CheckForUpdates { get; private set; }

        public BoolSetting IsFirstRun { get; private set; }

        public IntSetting Difficulty { get; private set; }

        public IntSetting GameSpeed { get; private set; }

        public BoolSetting PreloadMapPreviews { get; private set; }

        public BoolSetting ForceLowestDetailLevel { get; private set; }

        public BoolSetting MinimizeWindowsOnGameStart { get; private set; }

        public BoolSetting AutoRemoveUnderscoresFromName { get; private set; }

        public bool IsGameFollowed(string gameName)
        {
            return SettingsIni.GetBooleanValue("Channels", gameName, false);
        }

        public void ReloadSettings()
        {
            SettingsIni.Reload();
        }

        public void SaveSettings()
        {
            Logger.Log("Writing settings INI.");

            ForceLowestDetailLevel.Value = false;

            SettingsIni.WriteIniFile();

            SettingsSaved?.Invoke(this, EventArgs.Empty);
        }
    }
}
