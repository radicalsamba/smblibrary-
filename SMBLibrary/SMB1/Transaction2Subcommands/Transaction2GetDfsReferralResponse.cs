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

namespace SMBLibrary.SMB1
{
    /// <summary>
    /// TRANS2_GET_DFS_REFERRAL Response
    /// We will always return STATUS_NO_SUCH_DEVICE
    /// </summary>
    public class Transaction2GetDfsReferralResponse : Transaction2Subcommand
    {
        // Data:
        ResponseGetDfsReferral ReferralResponse; 

        public Transaction2GetDfsReferralResponse() : base()
        {
        }

        public Transaction2GetDfsReferralResponse(byte[] parameters, byte[] data) : base()
        {
            ReferralResponse = new ResponseGetDfsReferral(data);
        }

        public override byte[] GetData(bool isUnicode)
        {
            return ReferralResponse.GetBytes();
        }

        public override Transaction2SubcommandName SubcommandName
        {
            get
            {
                return Transaction2SubcommandName.TRANS2_GET_DFS_REFERRAL;
            }
        }
    }
}
