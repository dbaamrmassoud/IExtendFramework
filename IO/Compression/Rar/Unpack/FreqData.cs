using System.Text;
namespace IExtendFramework.IO.Compression.Rar.Unpack.PPM
{
    internal class FreqData : Pointer
    {
        internal const int Size = 6;

        //    struct FreqData
        //    {
        //        ushort SummFreq;
        //        STATE _PACK_ATTR * Stats;
        //    };

        internal FreqData(byte[] Memory)
            : base(Memory)
        {
        }

        internal int SummFreq
        {
            get
            {
                return Utilities.readShortLittleEndian(Memory, Address) & 0xffff;
            }

            set
            {
                Utilities.WriteLittleEndian(Memory, Address, (short)value);
            }

        }

        internal FreqData Initialize(byte[] mem)
        {
            return base.Initialize<FreqData>(mem);
        }

        internal void IncrementSummFreq(int dSummFreq)
        {
            Utilities.incShortLittleEndian(Memory, Address, (short)dSummFreq);
        }

        internal int GetStats()
        {
            return Utilities.readIntLittleEndian(Memory, Address + 2);
        }

        internal virtual void SetStats(State state)
        {
            SetStats(state.Address);
        }

        internal void SetStats(int state)
        {
            Utilities.WriteLittleEndian(Memory, Address + 2, state);
        }

        public override System.String ToString()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("FreqData[");
            buffer.Append("\n  Address=");
            buffer.Append(Address);
            buffer.Append("\n  size=");
            buffer.Append(Size);
            buffer.Append("\n  summFreq=");
            buffer.Append(SummFreq);
            buffer.Append("\n  stats=");
            buffer.Append(GetStats());
            buffer.Append("\n]");
            return buffer.ToString();
        }
    }
}