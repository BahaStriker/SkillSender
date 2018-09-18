// Decompiled with JetBrains decompiler
// Type: Enhance.Memory
// Assembly: Enhance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 19C01E0B-DD59-4615-AF59-E27D0274D556
// Assembly location: C:\Users\Maroon\Desktop\VOID SKILL SENDER.exe

using System;
using System.Collections.Generic;
using System.Linq;

namespace Enhance
{
  internal class Memory
  {
    private IntPtr pHandle;

    public Memory(IntPtr pHandle)
    {
      this.pHandle = pHandle;
    }

    public int ReadInt32(int address)
    {
      return MemFunctions.MemReadInt(this.pHandle, address);
    }

    public uint ReadUInt32(int address)
    {
      return MemFunctions.MemReadUInt(this.pHandle, address);
    }

    public float ReadFloat(int address)
    {
      return MemFunctions.MemReadFloat(this.pHandle, address);
    }

    public byte[] ReadBytes(int address, int amount)
    {
      return MemFunctions.MemReadBytes(this.pHandle, address, amount);
    }

    public string ReadUnicode(int address)
    {
      return MemFunctions.MemReadUnicode(this.pHandle, address);
    }

    public byte ReadByte(int address)
    {
      return (byte) MemFunctions.MemReadInt(this.pHandle, address);
    }

    public void Write(int address, object data, bool reverse)
    {
      if (data is byte)
      {
        MemFunctions.MemWriteByte(this.pHandle, address, (byte) data);
      }
      else
      {
        byte[] numArray = (byte[]) null;
        if (data is int)
          numArray = BitConverter.GetBytes((int) data);
        if (data is uint)
          numArray = BitConverter.GetBytes((uint) data);
        if (data is short)
          numArray = BitConverter.GetBytes((short) data);
        if (data is float)
          numArray = BitConverter.GetBytes((float) data);
        if (data is bool)
          numArray = BitConverter.GetBytes((bool) data);
        if (data is byte[])
          numArray = (byte[]) data;
        if (reverse && numArray != null)
          ((IEnumerable<byte>) numArray).Reverse<byte>();
        MemFunctions.MemWriteBytes(this.pHandle, address, numArray);
      }
    }

    public void Write(int address, object data)
    {
      this.Write(address, data, false);
    }

    public int Allocate(int amount)
    {
      return MemFunctions.AllocateMemory(this.pHandle, amount);
    }

    public IntPtr CreateRemoteThread(int address)
    {
      return MemFunctions.CreateRemoteThread(this.pHandle, address);
    }

    public void WaitThread(IntPtr thread)
    {
      MemFunctions.WaitForSingleObject(thread);
    }

    public void CloseThread(IntPtr thread)
    {
      MemFunctions.CloseProcess(thread);
    }
  }
}
