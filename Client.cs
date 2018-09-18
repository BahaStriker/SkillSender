// Decompiled with JetBrains decompiler
// Type: Enhance.Client
// Assembly: vSync, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C8FEFBD4-1937-4DCC-887D-26988F45686D
// Assembly location: C:\Users\israe\Desktop\matka\vSync.exe

using System;
using System.Windows.Forms;
namespace Enhance
{
    internal class Client
    {
        private const int RBA = 14959780;
        private IntPtr pHandle;
        private Memory Mem;
        private PacketSender pSender;
        private Packet skillPkt;
        private Packet cskillPkt;
        private Packet finishPkt;

        private int GamePtr
        {
            get
            {
                return this.Mem.ReadInt32(this.Mem.ReadInt32(14959780) + 28);
            }
        }

        private int CharPtr
        {
            get
            {
                return this.Mem.ReadInt32(this.GamePtr + 52);
            }
        }

        private int ActionPtr
        {
            get
            {
                return this.Mem.ReadInt32(this.CharPtr + 5376);
            }
        }

        public uint UID
        {
            get
            {
                return this.Mem.ReadUInt32(this.CharPtr + 1208);
            }
        }

        public string Name
        {
            get
            {
                return this.Mem.ReadUnicode(this.Mem.ReadInt32(this.CharPtr + 1788));
            }
        }

        public int QueuePtr
        {
            get
            {
                return this.Mem.ReadInt32(this.ActionPtr + 64);
            }
        }

        public int QueueType
        {
            get
            {
                return this.Mem.ReadInt32(this.QueuePtr + 4);
            }
        }

        public uint QueueTarget
        {
            get
            {
                return this.Mem.ReadUInt32(this.Mem.ReadInt32(this.QueuePtr + 64) + 8);
            }
        }

        public int QueueSkillPtr
        {
            get
            {
                return this.Mem.ReadInt32(this.QueuePtr + 52);
            }
        }

        public int QueueSkillID
        {
            get
            {
                return this.Mem.ReadInt32(this.QueueSkillPtr + 8);
            }
        }

        public byte QueueSkillType
        {
            get
            {
                return (byte)this.Mem.ReadInt32(this.Mem.ReadInt32(this.Mem.ReadInt32(this.QueueSkillPtr + 4) + 4) + 28);
            }
        }

        public byte QueueSkillCType
        {
            get
            {
                return (byte)this.Mem.ReadInt32(this.Mem.ReadInt32(this.Mem.ReadInt32(this.QueueSkillPtr + 4) + 4) + 87);
            }
        }

        public int SkillPtr
        {
            get
            {
                return this.Mem.ReadInt32(this.CharPtr + 2016);
            }
        }

        public bool Charging
        {
            get
            {
                return this.Mem.ReadByte(this.SkillPtr + 36) == (byte)1;
            }
        }

        public bool CurrentSkillCooldown
        {
            get
            {
                return this.Mem.ReadInt32(this.SkillPtr + 16) > 0;
            }
        }

        public int CurrentSkillCastTime
        {
            get
            {
                TextBox t = Application.OpenForms["Window"].Controls["textBox1"] as TextBox;
                return this.Mem.ReadInt32(this.Mem.ReadInt32(this.Mem.ReadInt32(this.Mem.ReadInt32(this.Mem.ReadInt32(this.SkillPtr + 4) + 4)) + 36 + Convert.ToInt32(t.Text)) + 1);
            }
        }

        public int LatestPing
        {
            get
            {
                return this.Mem.ReadInt32(this.GamePtr + 376);
            }
        }

        private byte ProtectionSettings
        {
            get
            {
                int num1 = this.Mem.ReadInt32(this.Mem.ReadInt32(14959780) + 24);
                if ((int)(byte)this.Mem.ReadInt32(num1 + 642) == 0)
                    return 0;
                byte num2 = 1;
                if ((uint)(byte)this.Mem.ReadInt32(num1 + 643) > 0U)
                    num2 = (byte)3;
                if ((uint)(byte)this.Mem.ReadInt32(num1 + 644) > 0U)
                    num2 |= (byte)4;
                if ((uint)(byte)this.Mem.ReadInt32(num1 + 651) > 0U)
                    num2 |= (byte)8;
                if ((uint)(byte)this.Mem.ReadInt32(num1 + 654) > 0U)
                    num2 |= (byte)16;
                return num2;
            }
        }

        public Client(IntPtr pHandle)
        {
            this.pHandle = pHandle;
            this.Mem = new Memory(pHandle);
            this.pSender = new PacketSender(this.Mem);
        }

        public void FinishTakeAim()
        {
            TextBox t = Application.OpenForms["Window"].Controls["textBox1"] as TextBox;
            this.Mem.Write(this.SkillPtr + Convert.ToInt32(t.Text), (object)(byte)1);
            this.SendPacket(this.FinishPkt());
        }

        public Packet SkillPkt(int ID, uint target)
        {
            try
            {
                if (this.skillPkt == null)
                    this.skillPkt = new Packet((short)41, 12);
                if (this.skillPkt.Address == 0)
                    this.skillPkt.Address = this.Mem.Allocate(4);
                if (this.Mem.ReadInt32(this.skillPkt.Address) == 0)
                    this.Mem.Write(this.skillPkt.Address, (object)this.skillPkt.Pkt);
                this.Mem.Write(this.skillPkt.Address + 2, (object)ID, true);
                this.Mem.Write(this.skillPkt.Address + 6, (object)this.ProtectionSettings);
                this.Mem.Write(this.skillPkt.Address + 7, (object)(byte)1);
                this.Mem.Write(this.skillPkt.Address + 8, (object)target, true);
                return this.skillPkt;
            }
            catch (Exception ex)
            {
                return (Packet)null;
            }
        }

        public Packet cSkillPkt(int ID, uint target)
        {
            try
            {
                if (this.cskillPkt == null)
                    this.cskillPkt = new Packet((short)80, 12);
                if (this.cskillPkt.Address == 0)
                    this.cskillPkt.Address = this.Mem.Allocate(4);
                if (this.Mem.ReadInt32(this.cskillPkt.Address) == 0)
                    this.Mem.Write(this.cskillPkt.Address, (object)this.cskillPkt.Pkt);
                this.Mem.Write(this.cskillPkt.Address + 2, (object)ID, true);
                this.Mem.Write(this.cskillPkt.Address + 6, (object)this.ProtectionSettings);
                this.Mem.Write(this.cskillPkt.Address + 7, (object)(byte)1);
                this.Mem.Write(this.cskillPkt.Address + 8, (object)target, true);
                return this.cskillPkt;
            }
            catch (Exception ex)
            {
                return (Packet)null;
            }
        }

        public Packet FinishPkt()
        {
            try
            {
                if (this.finishPkt == null)
                    this.finishPkt = new Packet((short)51, 2);
                if (this.finishPkt.Address == 0)
                    this.finishPkt.Address = this.Mem.Allocate((int)this.finishPkt.Size);
                if (this.Mem.ReadInt32(this.finishPkt.Address) == 0)
                    this.Mem.Write(this.finishPkt.Address, (object)this.finishPkt.Pkt);
                return this.finishPkt;
            }
            catch (Exception ex)
            {
                return (Packet)null;
            }
        }

        public void SendPacket(Packet packet)
        {
            try
            {
                this.pSender.SendPacket(packet);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
