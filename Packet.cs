// Decompiled with JetBrains decompiler
// Type: Enhance.Packet
// Assembly: Enhance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 19C01E0B-DD59-4615-AF59-E27D0274D556
// Assembly location: C:\Users\Maroon\Desktop\VOID SKILL SENDER.exe

using System;
using System.Collections.Generic;
using System.Linq;

namespace Enhance
{
  public class Packet
  {
    internal byte[] Pkt;
    internal int Address;

    internal byte Size
    {
      get
      {
        return (byte) this.Pkt.Length;
      }
    }

    public Packet(short header, int size)
    {
      this.Pkt = new byte[size];
      byte[] bytes = BitConverter.GetBytes(header);
      ((IEnumerable<byte>) bytes).Reverse<byte>();
      Buffer.BlockCopy((Array) bytes, 0, (Array) this.Pkt, 0, 2);
    }
  }
}
