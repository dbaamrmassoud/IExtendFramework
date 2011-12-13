/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 11/15/2011
 * Time: 12:25 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.Serialization;

namespace IExtendFramework
{
    /// <summary>
    /// Exceptions in IExtendFramework
    /// </summary>
    public class IExtendFrameworkException : Exception, ISerializable
    {
        public IExtendFrameworkException()
        {
        }

         public IExtendFrameworkException(string message) : base(message)
        {
        }

        public IExtendFrameworkException(string message, Exception innerException) : base(message, innerException)
        {
        }

        // This constructor is needed for serialization.
        protected IExtendFrameworkException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}