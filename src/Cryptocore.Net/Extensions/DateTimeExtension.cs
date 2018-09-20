﻿using System;

namespace Cryptocore.Net
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Convert <see cref="DateTime"/> to Unix time milliseconds.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
        }
    }
}