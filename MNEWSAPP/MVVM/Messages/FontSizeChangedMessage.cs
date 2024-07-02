﻿using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNEWSAPP.MVVM.Messages
{
    public class FontSizeChangedMessage : ValueChangedMessage<int>
    {
        public FontSizeChangedMessage(int value) : base(value)
        {
        }
    }
}
