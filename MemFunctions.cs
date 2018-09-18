// Decompiled with JetBrains decompiler
// Type: Enhance.MemFunctions
// Assembly: Enhance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 19C01E0B-DD59-4615-AF59-E27D0274D556
// Assembly location: C:\Users\Maroon\Desktop\VOID SKILL SENDER.exe

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Enhance
{
  public class MemFunctions
  {
    private const uint INFINITE = 4294967295;
    private const uint WAIT_ABANDONED = 256;
    private const uint WAIT_OBJECT_0 = 0;
    private const uint WAIT_TIMEOUT = 258;

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

    [DllImport("Kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, ref uint lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    private static extern int CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, ref uint lpNumberOfBytesWritten);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, MemFunctions.FreeType dwFreeType);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

    [DllImport("kernel32.dll")]
    private static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

    public static int AllocateMemory(IntPtr processHandle, int memorySize)
    {
      return (int) MemFunctions.VirtualAllocEx(processHandle, (IntPtr) 0, (uint) memorySize, 4096U, 64U);
    }

    public static IntPtr CreateRemoteThread(IntPtr processHandle, int address)
    {
      return MemFunctions.CreateRemoteThread(processHandle, (IntPtr) 0, 0U, (IntPtr) address, (IntPtr) 0, 0U, (IntPtr) 0);
    }

    public static void WaitForSingleObject(IntPtr threadHandle)
    {
      if (MemFunctions.WaitForSingleObject(threadHandle, uint.MaxValue) == 0U)
        return;
      Debug.WriteLine("Failed waiting for single object");
    }

    public static void FreeMemory(IntPtr processHandle, int address)
    {
      MemFunctions.VirtualFreeEx(processHandle, (IntPtr) address, 0, MemFunctions.FreeType.Release);
    }

    public static IntPtr OpenProcess(int pId)
    {
      return MemFunctions.OpenProcess(2035711U, 0, (uint) pId);
    }

    public static void CloseProcess(IntPtr handle)
    {
      MemFunctions.CloseHandle(handle);
    }

    public static void MemWriteBytes(IntPtr processHandle, int address, byte[] value)
    {
      uint lpNumberOfBytesWritten = 0;
      MemFunctions.WriteProcessMemory(processHandle, (IntPtr) address, value, (uint) value.Length, ref lpNumberOfBytesWritten);
    }

    public static void MemWriteStruct(IntPtr processHandle, int address, object value)
    {
      byte[] lpBuffer = MemFunctions.RawSerialize(value);
      uint lpNumberOfBytesWritten = 0;
      MemFunctions.WriteProcessMemory(processHandle, (IntPtr) address, lpBuffer, (uint) lpBuffer.Length, ref lpNumberOfBytesWritten);
    }

    public static void MemWriteInt(IntPtr processHandle, int address, int value)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      uint lpNumberOfBytesWritten = 0;
      MemFunctions.WriteProcessMemory(processHandle, (IntPtr) address, bytes, 4U, ref lpNumberOfBytesWritten);
    }

    public static void MemWriteFloat(IntPtr processHandle, int address, float value)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      uint lpNumberOfBytesWritten = 0;
      MemFunctions.WriteProcessMemory(processHandle, (IntPtr) address, bytes, 4U, ref lpNumberOfBytesWritten);
    }

    public static void MemWriteShort(IntPtr processHandle, int address, short value)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      uint lpNumberOfBytesWritten = 0;
      MemFunctions.WriteProcessMemory(processHandle, (IntPtr) address, bytes, 2U, ref lpNumberOfBytesWritten);
    }

    public static void MemWriteByte(IntPtr processHandle, int address, byte value)
    {
      byte[] bytes = BitConverter.GetBytes((short) value);
      uint lpNumberOfBytesWritten = 0;
      MemFunctions.WriteProcessMemory(processHandle, (IntPtr) address, bytes, 1U, ref lpNumberOfBytesWritten);
    }

    public static byte[] MemReadBytes(IntPtr processHandle, int address, int size)
    {
      byte[] lpBuffer = new byte[size];
      uint lpNumberOfBytesRead = 0;
      MemFunctions.ReadProcessMemory(processHandle, (IntPtr) address, lpBuffer, (uint) size, ref lpNumberOfBytesRead);
      return lpBuffer;
    }

    public static int MemReadInt(IntPtr processHandle, int address)
    {
      byte[] lpBuffer = new byte[4];
      uint lpNumberOfBytesRead = 0;
      MemFunctions.ReadProcessMemory(processHandle, (IntPtr) address, lpBuffer, 4U, ref lpNumberOfBytesRead);
      return BitConverter.ToInt32(lpBuffer, 0);
    }

    public static uint MemReadUInt(IntPtr processHandle, int address)
    {
      byte[] lpBuffer = new byte[4];
      uint lpNumberOfBytesRead = 0;
      MemFunctions.ReadProcessMemory(processHandle, (IntPtr) address, lpBuffer, 4U, ref lpNumberOfBytesRead);
      return BitConverter.ToUInt32(lpBuffer, 0);
    }

    public static float MemReadFloat(IntPtr processHandle, int address)
    {
      byte[] lpBuffer = new byte[4];
      uint lpNumberOfBytesRead = 0;
      MemFunctions.ReadProcessMemory(processHandle, (IntPtr) address, lpBuffer, 4U, ref lpNumberOfBytesRead);
      return BitConverter.ToSingle(lpBuffer, 0);
    }

    public static string MemReadUnicode(IntPtr processHandle, int address)
    {
      byte[] numArray = new byte[400];
      uint lpNumberOfBytesRead = 0;
      MemFunctions.ReadProcessMemory(processHandle, (IntPtr) address, numArray, 400U, ref lpNumberOfBytesRead);
      return MemFunctions.ByteArrayToString(numArray, MemFunctions.EncodingType.Unicode);
    }

    public static object MemReadStruct(IntPtr processHandle, int address, Type anyType)
    {
      int length = Marshal.SizeOf(anyType);
      byte[] numArray = new byte[length];
      uint lpNumberOfBytesRead = 0;
      MemFunctions.ReadProcessMemory(processHandle, (IntPtr) address, numArray, (uint) length, ref lpNumberOfBytesRead);
      return MemFunctions.RawDeserialize(numArray, 0, anyType);
    }

    private static object RawDeserialize(byte[] rawData, int position, Type anyType)
    {
      int num1 = Marshal.SizeOf(anyType);
      if (num1 > rawData.Length)
        return (object) null;
      IntPtr num2 = Marshal.AllocHGlobal(num1);
      Marshal.Copy(rawData, position, num2, num1);
      object structure = Marshal.PtrToStructure(num2, anyType);
      Marshal.FreeHGlobal(num2);
      return structure;
    }

    private static byte[] RawSerialize(object anything)
    {
      int length = Marshal.SizeOf(anything);
      IntPtr num = Marshal.AllocHGlobal(length);
      Marshal.StructureToPtr(anything, num, false);
      byte[] destination = new byte[length];
      Marshal.Copy(num, destination, 0, length);
      Marshal.FreeHGlobal(num);
      return destination;
    }

    private static string ByteArrayToString(byte[] bytes)
    {
      return MemFunctions.ByteArrayToString(bytes, MemFunctions.EncodingType.Unicode);
    }

    private static string ByteArrayToString(byte[] bytes, MemFunctions.EncodingType encodingType)
    {
      Encoding encoding = (Encoding) null;
      string str = "";
      switch (encodingType)
      {
        case MemFunctions.EncodingType.ASCII:
          encoding = (Encoding) new ASCIIEncoding();
          break;
        case MemFunctions.EncodingType.Unicode:
          encoding = (Encoding) new UnicodeEncoding();
          break;
        case MemFunctions.EncodingType.UTF7:
          encoding = (Encoding) new UTF7Encoding();
          break;
        case MemFunctions.EncodingType.UTF8:
          encoding = (Encoding) new UTF8Encoding();
          break;
      }
      int count = 0;
      while (count < bytes.Length)
      {
        if (bytes[count] == (byte) 0 && bytes[count + 1] == (byte) 0)
        {
          str = encoding.GetString(bytes, 0, count);
          break;
        }
        count += 2;
      }
      return str;
    }

    private static byte[] StringToByteArray(string str, MemFunctions.EncodingType encodingType)
    {
      Encoding encoding = (Encoding) null;
      switch (encodingType)
      {
        case MemFunctions.EncodingType.ASCII:
          encoding = (Encoding) new ASCIIEncoding();
          break;
        case MemFunctions.EncodingType.Unicode:
          encoding = (Encoding) new UnicodeEncoding();
          break;
        case MemFunctions.EncodingType.UTF7:
          encoding = (Encoding) new UTF7Encoding();
          break;
        case MemFunctions.EncodingType.UTF8:
          encoding = (Encoding) new UTF8Encoding();
          break;
      }
      return encoding.GetBytes(str);
    }

    public delegate int ThreadProc(IntPtr param);

    public enum EncodingType
    {
      ASCII,
      Unicode,
      UTF7,
      UTF8,
    }

    [Flags]
    public enum FreeType
    {
      Decommit = 16384, // 0x00004000
      Release = 32768, // 0x00008000
    }
  }
}
