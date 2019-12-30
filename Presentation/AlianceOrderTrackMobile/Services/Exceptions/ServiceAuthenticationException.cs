﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AlianceOrderTrackMobile.Services.Exceptions
{
    public class ServiceAuthenticationException : Exception
    {
        public string Content { get; }

        public ServiceAuthenticationException()
        {
        }

        public ServiceAuthenticationException(string content)
        {
            Content = content;
        }
    }
}
