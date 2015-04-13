/* Copyright (C) 2014 Tal Aloni <tal.aloni.il@gmail.com>. All rights reserved.
 * 
 * You can redistribute this program and/or modify it under the terms of
 * the GNU Lesser Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 */
using System;
using System.Collections.Generic;
using System.Text;
using Utilities;

namespace SMBLibrary.Authentication
{
    public class AuthenticationMessageUtils
    {
        public const string ValidSignature = "NTLMSSP\0";

        public static string ReadAnsiStringBufferPointer(byte[] buffer, int offset)
        {
            byte[] bytes = ReadBufferPointer(buffer, offset);
            return ASCIIEncoding.Default.GetString(bytes);
        }

        public static string ReadUnicodeStringBufferPointer(byte[] buffer, int offset)
        {
            byte[] bytes = ReadBufferPointer(buffer, offset);
            return UnicodeEncoding.Unicode.GetString(bytes);
        }

        public static byte[] ReadBufferPointer(byte[] buffer, int offset)
        {
            ushort length = LittleEndianConverter.ToUInt16(buffer, offset);
            ushort maxLength = LittleEndianConverter.ToUInt16(buffer, offset + 2);
            uint bufferOffset = LittleEndianConverter.ToUInt32(buffer, offset + 4);

            if (length == 0)
            {
                return new byte[0];
            }
            else
            {
                return ByteReader.ReadBytes(buffer, (int)bufferOffset, length);
            }
        }

        public static void WriteBufferPointer(byte[] buffer, int offset, ushort bufferLength, uint bufferOffset)
        {
            LittleEndianWriter.WriteUInt16(buffer, offset, bufferLength);
            LittleEndianWriter.WriteUInt16(buffer, offset + 2, bufferLength);
            LittleEndianWriter.WriteUInt32(buffer, offset + 4, bufferOffset);
        }

        public static bool IsSignatureValid(byte[] messageBytes)
        {
            string signature = ByteReader.ReadAnsiString(messageBytes, 0, 8);
            return (signature == ValidSignature);
        }

        public static MessageTypeName GetMessageType(byte[] messageBytes)
        {
            return (MessageTypeName)LittleEndianConverter.ToUInt32(messageBytes, 8);
        }
    }
}
