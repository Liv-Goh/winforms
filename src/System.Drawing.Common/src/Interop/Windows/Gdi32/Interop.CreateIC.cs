// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Gdi32
    {
#if NET7_0_OR_GREATER
        [LibraryImport(Libraries.Gdi32, StringMarshalling = StringMarshalling.Utf16)]
        public static partial IntPtr CreateICW(
#else
        [DllImport(Libraries.Gdi32, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateICW(
#endif
            string pszDriver,
            string pszDevice,
            string? pszPort,
            IntPtr pdm);
    }
}
