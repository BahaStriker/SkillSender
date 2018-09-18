// Decompiled with JetBrains decompiler
// Type: Enhance.Properties.Settings
// Assembly: Enhance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 19C01E0B-DD59-4615-AF59-E27D0274D556
// Assembly location: C:\Users\Maroon\Desktop\VOID SKILL SENDER.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Enhance.Properties
{
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
  [CompilerGenerated]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings defaultInstance = Settings.defaultInstance;
        Settings settings = defaultInstance;
        return settings;
      }
    }
  }
}
