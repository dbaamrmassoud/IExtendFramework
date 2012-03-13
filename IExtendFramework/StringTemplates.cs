/*
 * User: elijah
 * Date: 1/30/2012
 * Time: 3:14 PM
 */
using System;

namespace IExtendFramework
{
    /// <summary>
    /// Some useful string templates
    /// </summary>
    public class StringTemplates
    {
        /// <summary>
        /// Format: {0}@{1}.{2}
        /// </summary>
        public const string EMAIL_TEMPLATE = "{0}@{1}.{2}";
        /// <summary>
        /// Format of http://{0}.{1} ({0} = url, {1} = ext, a.k.a. com, net
        /// </summary>
        public const string BASIC_HTTP_URL = "http://{0}.{1}";
        
        public const string BASIC_HTTPS_URL = "https://{0}.{1}";
        
        /// <summary>
        /// format: {0}://{1}.{2}
        /// e.g. URL.Format("http", "google", "com")
        /// </summary>
        public const string URL = "{0}://{1}.{2}";
        
        /// <summary>
        /// format with firstname, lastname
        /// </summary>
        public const string NAME = "{0} {1}";
        
        /// <summary>
        /// Contains first, middle, and last names
        /// </summary>
        public const string FULLNAME = "{0} {1} {2}";
        
    }
}
