// Decompiled with JetBrains decompiler
// Type: Enhance.ClientEventArgs
// Assembly: Enhance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 19C01E0B-DD59-4615-AF59-E27D0274D556
// Assembly location: C:\Users\Maroon\Desktop\VOID SKILL SENDER.exe

using System;

namespace Enhance
{
  public class ClientEventArgs : EventArgs
  {
    public string Name;
    public string Class;

    public ClientEventArgs(string name, int _class, int level)
    {
      this.Name = name;
      string str = "";
      switch (_class)
      {
        case 0:
          str = "Blademaster";
          break;
        case 1:
          str = "Wizard";
          break;
        case 2:
          str = "Psychic";
          break;
        case 3:
          str = "Venomancer";
          break;
        case 4:
          str = "Barbarian";
          break;
        case 5:
          str = "Assassin";
          break;
        case 6:
          str = "Archer";
          break;
        case 7:
          str = "Cleric";
          break;
        case 8:
          str = "Seeker";
          break;
        case 9:
          str = "Mystic";
          break;
        case 10:
          str = "Stormbringer";
          break;
        case 11:
          str = "Duskblade";
          break;
      }
      this.Class = str + " Lv. " + (object) level;
    }
  }
}
