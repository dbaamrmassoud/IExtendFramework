/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/5/2011
 * Time: 1:51 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;

namespace IExtendFramework
{
    /// <summary>
    /// Conversion facility
    /// </summary>
    public class Converter
    {
        private Converter()
        {
        }
        
        /// <summary>
        /// Converts any object to Boolean true or false
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static bool ObjectToBoolean(object arg)
        {
            // Null counts as false
            if (arg == null)
                return false;
            
            // The boolean false is clearly false
            if ((arg is bool) && (!(bool)arg))
                return false;
            
            // Empty strings are false
            if ((arg is string) && (arg.ToString() == ""))
                return false;
            
            // string "false" is false
            if ((arg is string) && (arg.ToString().ToLower() == "false"))
                return false;
            
            // Zero is false
            if ((arg.GetType() == typeof(System.Double)) && ((Double)arg == 0))
                return false;
            if ((arg.GetType() == typeof(System.Int32)) && ((int)arg == 0))
                return false;
            
            return true;
        }
        
        public static uint IpToUint(IPAddress ip)
        {
            uint uip = (uint)ip.Address; //TODO try use ip.GetAddressBytes instead
            //fix endianess from network
            if (BitConverter.IsLittleEndian)
            {
                byte[] bytes = BitConverter.GetBytes(uip);
                uip = (uint)((bytes[0] << 24) | (bytes[1] << 16) | (bytes[2] << 8) | bytes[3]);
            }
            return uip;
        }

        public static IPAddress UintToIP(uint ip)
        {
            //fix Endianess from network

            if (BitConverter.IsLittleEndian)
            {
                ip = ((ip << 24) & 0xFF000000) + ((ip << 8) & 0x00FF0000) + ((ip >> 8) & 0x0000FF00) + ((ip >> 24) & 0x000000FF);
            }

            return new System.Net.IPAddress(ip);
        }
    }
}