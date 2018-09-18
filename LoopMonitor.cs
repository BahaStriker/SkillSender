// Decompiled with JetBrains decompiler
// Type: Enhance.LoopMonitor
// Assembly: Enhance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 19C01E0B-DD59-4615-AF59-E27D0274D556
// Assembly location: C:\Users\Maroon\Desktop\VOID SKILL SENDER.exe

using System;
using System.Threading;

namespace Enhance
{
  internal class LoopMonitor
  {
    private int Delay = 0;
    private Client elementclient;
    private bool Attack;
    private bool Self;
    private bool Buff;
    private bool Charge;
    private bool Running;
    private Thread TMonitor;
    private Thread TAutoComplete;

    public LoopMonitor(Client elementclient)
    {
      this.elementclient = elementclient;
    }

    public void OnSettingsChanged(object sender, TriggerEventArgs e)
    {
      int msg = e.Msg;
      switch (msg >> 4)
      {
        case 1:
          this.Attack = (msg & 15) > 0;
          break;
        case 2:
          this.Self = (msg & 15) > 0;
          break;
        case 3:
          this.Buff = (msg & 15) > 0;
          break;
        case 4:
          this.Charge = (msg & 15) > 0;
          break;
      }
    }

    public void Start()
    {
      this.Running = true;
      this.Attack = true;
      this.Self = true;
      this.Buff = true;
      this.Charge = true;
      this.TMonitor = new Thread((ThreadStart) (() => this.InterceptAdv()));
      this.TAutoComplete = new Thread((ThreadStart) (() => this.AutoComplete()));
      this.TMonitor.Start();
      this.TAutoComplete.Start();
    }

    private void Intercept()
    {
      try
      {
        while (this.Running)
        {
          Thread.Sleep(1);
          int queuePtr = this.elementclient.QueuePtr;
          if (queuePtr != 0 && this.elementclient.QueueType == 2 && (this.elementclient.QueueSkillType != (byte) 8 && this.elementclient.QueueTarget != 0U) && ((int) this.elementclient.QueueTarget != (int) this.elementclient.UID || this.Self) && ((this.elementclient.QueueSkillType != (byte) 2 || (int) this.elementclient.QueueTarget == (int) this.elementclient.UID || this.Buff) && (this.elementclient.QueueSkillType != (byte) 1 || this.Attack)))
          {
            if (this.elementclient.QueueSkillCType == (byte) 0)
              this.elementclient.SendPacket(this.elementclient.SkillPkt(this.elementclient.QueueSkillID, this.elementclient.QueueTarget));
            else
              this.elementclient.SendPacket(this.elementclient.cSkillPkt(this.elementclient.QueueSkillID, this.elementclient.QueueTarget));
            while (this.elementclient.QueuePtr == queuePtr && this.Running)
              Thread.Sleep(1);
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void InterceptAdv()
    {
      try
      {
        while (this.Running)
        {
          Thread.Sleep(1);
          int queuePtr = this.elementclient.QueuePtr;
          int queueSkillId = this.elementclient.QueueSkillID;
          if (queuePtr != 0 && this.elementclient.QueueType == 2 && (this.elementclient.QueueSkillType != (byte) 8 && this.elementclient.QueueTarget != 0U) && ((int) this.elementclient.QueueTarget != (int) this.elementclient.UID || this.Self) && ((this.elementclient.QueueSkillType != (byte) 2 || (int) this.elementclient.QueueTarget == (int) this.elementclient.UID || this.Buff) && (this.elementclient.QueueSkillType != (byte) 1 || this.Attack)))
          {
            if (this.elementclient.QueueSkillCType == (byte) 0)
              this.elementclient.SendPacket(this.elementclient.SkillPkt(queueSkillId, this.elementclient.QueueTarget));
            else
              this.elementclient.SendPacket(this.elementclient.cSkillPkt(queueSkillId, this.elementclient.QueueTarget));
            while (this.elementclient.QueueSkillID == queueSkillId && this.Running)
              Thread.Sleep(60);
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void AutoComplete()
    {
      try
      {
        while (this.Running)
        {
          Thread.Sleep(1);
          Console.WriteLine(this.elementclient.SkillPtr);
          if (this.Charge && this.elementclient.Charging)
          {
            this.elementclient.FinishTakeAim();
            while (this.elementclient.Charging && this.Running)
              Thread.Sleep(1);
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    public void Abort()
    {
      this.Running = false;
    }
  }
}
