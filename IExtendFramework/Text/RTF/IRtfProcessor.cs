namespace IExtendFramework.Text.RTF
{
    /// <summary>
    /// Processor of RTF
    /// </summary>
    public interface IRtfProcessor
    {
        #region Public Properties

        RTFBuilderBase Builder { get; }

        #endregion

        #region Abstract Methods

        string Process(string rtf);

        #endregion
    }
}