﻿using System;
using System.Runtime.Serialization;

namespace NhaHang.Module.ElFinder.Exceptions
{
    public class FileTypeNotAllowedException : ApplicationException
    {
        public FileTypeNotAllowedException()
        {
        }

        public FileTypeNotAllowedException(string message)
            : base(message)
        {
        }

        public FileTypeNotAllowedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected FileTypeNotAllowedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}