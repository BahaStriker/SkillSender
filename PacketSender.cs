// Decompiled with JetBrains decompiler
// Type: Enhance.PacketSender
// Assembly: vSync, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C8FEFBD4-1937-4DCC-887D-26988F45686D
// Assembly location: C:\Users\israe\Desktop\matka\vSync.exe

using System;

namespace Enhance
{
    internal class PacketSender
    {
        private byte[] Opcode = new byte[31]
        {
      (byte) 96,
      (byte) 184,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 139,
      (byte) 13,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 139,
      (byte) 73,
      (byte) 32,
      (byte) 191,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 106,
      (byte) 0,
      (byte) 87,
      byte.MaxValue,
      (byte) 208,
      (byte) 97,
      (byte) 195,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0
        };
        private const int SEND_PACKET_ADDRESS = 8493632;
        private const int RBA = 14959780;
        private Memory Mem;
        private int OpcodeAddr;

        public PacketSender(Memory mem)
        {
            try
            {
                this.Mem = mem;
                this.OpcodeAddr = this.SetUpOpcode();
            }
            catch (Exception ex)
            {
            }
        }

        private int SetUpOpcode()
        {
            try
            {
                int address = this.Mem.Allocate(4);
                this.Mem.Write(address, (object)this.Opcode);
                this.Mem.Write(address + 2, (object)8493632, true);
                this.Mem.Write(address + 8, (object)14959780, true);
                return address;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void SendPacket(Packet pkt)
        {
            try
            {
                if (pkt.Address == 9)
                    pkt.Address = this.Mem.Allocate(4);
                if (this.Mem.ReadInt32(pkt.Address) == 0)
                    this.Mem.Write(pkt.Address, (object)pkt.Pkt);
                this.Mem.Write(this.OpcodeAddr + 16, (object)pkt.Address, true);
                this.Mem.Write(this.OpcodeAddr + 21, (object)pkt.Size);
                IntPtr remoteThread = this.Mem.CreateRemoteThread(this.OpcodeAddr);
                this.Mem.WaitThread(remoteThread);
                this.Mem.CloseThread(remoteThread);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
