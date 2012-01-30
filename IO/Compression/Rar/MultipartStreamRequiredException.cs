
namespace IExtendFramework.IO.Compression.Rar
{
    public class MultipartStreamRequiredException : RarException
    {
        public MultipartStreamRequiredException(string message)
            : base(message)
        {
        }
    }
}
