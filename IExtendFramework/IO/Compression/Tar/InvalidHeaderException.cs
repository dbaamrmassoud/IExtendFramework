namespace IExtendFramework.IO.Compression.Tar {
	
	/// <summary>
	/// This exception is used to indicate that there is a problem
	/// with a TAR archive header.
	/// </summary>
	public class InvalidHeaderException : TarException
	{
		/// <summary>
		/// Initialise a new instance of the InvalidHeaderException class.
		/// </summary>
		public InvalidHeaderException()
		{
		}

		/// <summary>
		/// Initialises a new instance of the InvalidHeaderException class with a specified message.
		/// </summary>
		public InvalidHeaderException(string msg) : base(msg)
		{
		}
	}
}

/* The original Java file had this header:
** Authored by Timothy Gerard Endres
** <mailto:time@gjt.org>  <http://www.trustice.com>
** 
** This work has been placed into the public domain.
** You may use this work in any way and for any purpose you wish.
**
** THIS SOFTWARE IS PROVIDED AS-IS WITHOUT WARRANTY OF ANY KIND,
** NOT EVEN THE IMPLIED WARRANTY OF MERCHANTABILITY. THE AUTHOR
** OF THIS SOFTWARE, ASSUMES _NO_ RESPONSIBILITY FOR ANY
** CONSEQUENCE RESULTING FROM THE USE, MODIFICATION, OR
** REDISTRIBUTION OF THIS SOFTWARE. 
** 
*/
