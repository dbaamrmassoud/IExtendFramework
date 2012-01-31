namespace IExtendFramework.Text.RTF
{
    using System;

    /// <summary>
    /// Exposes an underlying RTFBuilderBase
    /// </summary>
    public interface IBuilderContent : IDisposable
    {
        RTFBuilderBase Content { get; }
    }
}

