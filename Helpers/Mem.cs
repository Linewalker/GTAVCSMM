using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace GTAVCSMM.Helpers
{
    public class Mem
    {
        [DllImport("ntdll.dll")]
        public static extern int NtWriteVirtualMemory(IntPtr ProcessHandle, long BaseAddress, byte[] Buffer, int Size, int BytesWritten = 0);

        [DllImport("ntdll.dll")]
        public static extern int NtReadVirtualMemory(IntPtr ProcessHandle, long Address, byte[] buffer, int Size, int BytesRead = 0);

        public Process Proc;
        public long BaseAddress;

        public Mem(string process)
        {
            try
            {
                Proc = Process.GetProcessesByName(process)[0];
                BaseAddress = Proc.MainModule.BaseAddress.ToInt64();
            }
            catch { throw new Exception(); }
        }

        public IntPtr GetProcHandle()
        {
            try { return Proc.Handle; } catch { return IntPtr.Zero; }
        }

        public long GetPtrAddr(long Pointer, int[] Offset = null)
        {
            byte[] Buffer = new byte[8];

            NtReadVirtualMemory(GetProcHandle(), Pointer, Buffer, Buffer.Length);

            if (Offset != null)
            {
                for (int x = 0; x < (Offset.Length - 1); x++)
                {
                    Pointer = BitConverter.ToInt64(Buffer, 0) + Offset[x];
                    NtReadVirtualMemory(GetProcHandle(), Pointer, Buffer, Buffer.Length);
                }

                Pointer = BitConverter.ToInt64(Buffer, 0) + Offset[Offset.Length - 1];
            }

            return Pointer;
        }

        public long FindPattern(byte[] pattern, string mask)
        {
            int moduleSize = Proc.MainModule.ModuleMemorySize;

            if (moduleSize == 0) throw new Exception($"Size of module {Proc.MainModule.ModuleName} is INVALID.");

            byte[] moduleBytes = new byte[moduleSize];
            NtReadVirtualMemory(GetProcHandle(), BaseAddress, moduleBytes, moduleSize);

            for (long i = 0; i < moduleSize; i++)
            {
                for (int l = 0; l < mask.Length; l++)
                    //dirty hack heh
                    if (!(mask[l] == '?' || moduleBytes[l + i] == pattern[l])) goto SKIP;

                return i;
            SKIP:;
            }
            return 0;
        }

        public byte[] ReadBytes(long BasePTR, int[] offset, int Length)
        {
            byte[] Buffer = new byte[Length];
            NtReadVirtualMemory(GetProcHandle(), GetPtrAddr(BaseAddress + BasePTR, offset), Buffer, Length);
            return Buffer;
        }

        public byte[] ReadBytes_new(long BasePTR, int[] offset, int Length)
        {
            byte[] Buffer = new byte[Length];
            NtReadVirtualMemory(GetProcHandle(), BasePTR, Buffer, Length);
            return Buffer;
        }

        public void Write(long BasePTR, int[] offset, byte[] Bytes) => NtWriteVirtualMemory(GetProcHandle(), GetPtrAddr(BaseAddress + BasePTR, offset), Bytes, Bytes.Length);

        public void Write(long BasePTR, int[] offset, bool b) => Write(BasePTR, offset, b ? new byte[] { 0x01 } : new byte[] { 0x00 });
        public void Write(long BasePTR, int[] offset, float Value) => Write(BasePTR, offset, BitConverter.GetBytes(Value));
        public void Write(long BasePTR, int[] offset, double Value) => Write(BasePTR, offset, BitConverter.GetBytes(Value));
        public void Write(long BasePTR, int[] offset, int Value) => Write(BasePTR, offset, BitConverter.GetBytes(Value));
        public void Write(long BasePTR, int[] offset, string String) => Write(BasePTR, offset, new ASCIIEncoding().GetBytes(String));
        public void Write(long BasePTR, int[] offset, long Value) => Write(BasePTR, offset, BitConverter.GetBytes(Value));
        public void Write(long BasePTR, int[] offset, uint Value) => Write(BasePTR, offset, BitConverter.GetBytes(Value));
        public void Write(long BasePTR, int[] offset, byte Value) => Write(BasePTR, offset, new byte[] { Value });

        public void writeInt(long BasePTR, int[] offset, int Value)
        {
            NtWriteVirtualMemory(Proc.Handle, BasePTR, BitConverter.GetBytes(Value), 4);
        }
        public void writeUInt(long BasePTR, int[] offset, uint Value)
        {
            NtWriteVirtualMemory(Proc.Handle, BasePTR, BitConverter.GetBytes(Value), 4);
        }
        public void writePointer(long BasePTR, int[] offset, long Value)
        {
            NtWriteVirtualMemory(Proc.Handle, BasePTR, BitConverter.GetBytes(Value), 8);
        }
        public void writeFloat(long BasePTR, int[] offset, float Value)
        {
            NtWriteVirtualMemory(Proc.Handle, BasePTR, BitConverter.GetBytes(Value), 8);
        }
        public bool ReadBool(long BasePTR, int[] offset) => ReadByte(BasePTR, offset) != 0x00;
        public float ReadFloat(long BasePTR, int[] offset) => BitConverter.ToSingle(ReadBytes(BasePTR, offset, 4), 0);
        public double ReadDouble(long BasePTR, int[] offset) => BitConverter.ToDouble(ReadBytes(BasePTR, offset, 8), 0);
        public int ReadInt(long BasePTR, int[] offset) => BitConverter.ToInt32(ReadBytes(BasePTR, offset, 4), 0);
        public uint ReadUInt(long BasePTR, int[] offset) => BitConverter.ToUInt32(ReadBytes(BasePTR, offset, 4), 0);
        public string ReadString(long BasePTR, int[] offset, int size) => new ASCIIEncoding().GetString(ReadBytes(BasePTR, offset, size));
        public long ReadPointer(long BasePTR, int[] offset) => BitConverter.ToInt64(ReadBytes(BasePTR, offset, 8), 0);
        public byte ReadByte(long BasePTR, int[] offset) => ReadBytes(BasePTR, offset, 1)[0];
        public string ReadStr(long BasePTR, int[] offset, int size) => new ASCIIEncoding().GetString(ReadBytes_new(BasePTR, offset, size));

        public T Read<T>(long basePtr, int[] offsets) where T : struct
        {
            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
            NtReadVirtualMemory(GetProcHandle(), GetPtrAddr(basePtr, offsets), buffer, buffer.Length);
            return ByteArrayToStructure<T>(buffer);
        }

        public T Read<T>(long address) where T : struct
        {
            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
            NtReadVirtualMemory(GetProcHandle(), address, buffer, buffer.Length);
            return ByteArrayToStructure<T>(buffer);
        }

        public void Write<T>(long basePtr, int[] offsets, T value) where T : struct
        {
            byte[] buffer = StructureToByteArray(value);
            NtWriteVirtualMemory(GetProcHandle(), GetPtrAddr(basePtr, offsets), buffer, buffer.Length);
        }

        public void Write<T>(long address, T value) where T : struct
        {
            byte[] buffer = StructureToByteArray(value);
            NtWriteVirtualMemory(GetProcHandle(), address, buffer, buffer.Length);
        }

        public bool IsValid(long Address)
        {
            return Address >= 0x10000 && Address < 0x000F000000000000;
        }

        #region Conversion
        private static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }

        private static byte[] StructureToByteArray(object obj)
        {
            int length = Marshal.SizeOf(obj);
            byte[] array = new byte[length];
            IntPtr pointer = Marshal.AllocHGlobal(length);
            Marshal.StructureToPtr(obj, pointer, true);
            Marshal.Copy(pointer, array, 0, length);
            Marshal.FreeHGlobal(pointer);
            return array;
        }

        private static float[] ConvertToFloatArray(byte[] bytes)
        {
            if (bytes.Length % 4 != 0)
            {
                throw new ArgumentException();
            }

            float[] floats = new float[bytes.Length / 4];
            for (int i = 0; i < floats.Length; i++)
            {
                floats[i] = BitConverter.ToSingle(bytes, i * 4);
            }
            return floats;
        }
        #endregion
    }
}