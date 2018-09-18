// Decompiled with JetBrains decompiler
// Type: Enhance.Program
// Assembly: Enhance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 19C01E0B-DD59-4615-AF59-E27D0274D556
// Assembly location: C:\Users\Maroon\Desktop\VOID SKILL SENDER.exe

using System;
using System.Windows.Forms;

namespace Enhance
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      using (Window window = new Window())
        Application.Run((Form) window);
    }
  }
}
