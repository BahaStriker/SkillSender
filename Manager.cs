// Decompiled with JetBrains decompiler
// Type: Enhance.Manager
// Assembly: Enhance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 19C01E0B-DD59-4615-AF59-E27D0274D556
// Assembly location: C:\Users\Maroon\Desktop\VOID SKILL SENDER.exe

using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Enhance
{
  internal class Manager
  {
    private string[] namehash = new string[5]
    {
      "9ff43b4f252f487b432cdda868c2043b",
      "aa8600b1e76f9c1a70e573192fe1f98b",
      "a479bb1575322328389df05785c61047",
      "871e003da1a46785b7edb88e281eabeb",
      "602fe114b1aa17adcf753155421daf10"
    };
    private const string Match = "1024";
    public bool Connected;
    public EventHandler<ClientEventArgs> OnConnect;
    private Client elementclient;
    private LoopMonitor Monitor;
    public EventHandler<TriggerEventArgs> OnSettingsChanged;

    public Manager(int index)
    {
      try
      {
        IntPtr pHandle;
        if ((pHandle = Manager.AttachProcess(index)) == IntPtr.Zero)
          return;
        this.elementclient = new Client(pHandle);
        this.Connected = true;
        this.Monitor = new LoopMonitor(this.elementclient);
        this.OnSettingsChanged += new EventHandler<TriggerEventArgs>(this.Monitor.OnSettingsChanged);
      }
      catch (Exception ex)
      {
      }
    }

    public void Connect()
    {
      try
      {
        if (this.OnConnect != null)
          this.OnConnect((object) this.elementclient, new ClientEventArgs(this.elementclient.Name, 0, 0));
        this.Monitor.Start();
      }
      catch (Exception ex)
      {
      }
    }

    private static IntPtr AttachProcess(int index)
    {
      try
      {
        IntPtr zero = IntPtr.Zero;
        return MemFunctions.OpenProcess(Process.GetProcessesByName("elementclient")[index].Id);
      }
      catch (Exception ex)
      {
        return IntPtr.Zero;
      }
    }

    private static bool CompareMatch(string match, uint UID)
    {
      return UID.ToString().Equals(match);
    }

    public void OnClose(object sender, EventArgs e)
    {
      this.Connected = false;
      this.Monitor.Abort();
    }

    public static string CreateMD5(string input)
    {
      using (MD5 md5 = MD5.Create())
      {
        byte[] bytes = Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(bytes);
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < hash.Length; ++index)
          stringBuilder.Append(hash[index].ToString("X2"));
        return stringBuilder.ToString().ToLower();
      }
    }
  }
}
