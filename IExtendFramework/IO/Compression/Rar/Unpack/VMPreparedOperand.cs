
namespace IExtendFramework.IO.Compression.Rar.Unpack.VM
{
    internal class VMPreparedOperand
    {
        internal VMOpType Type
        {
            get;
            set;
        }
        internal int Data
        {
            get;
            set;
        }
        internal int Base
        {
            get;
            set;
        }
        internal int Offset
        {
            get;
            set;
        }
    }
}