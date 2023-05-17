// Copyright (c) .NET Foundation and Contributors.  All Rights Reserved.  See LICENSE in the project root for license information.
using System;
using static TorchSharp.torch;
using static TorchSharp.PInvoke.LibTorchSharp;

namespace TorchSharp
{
    using Modules;

    namespace Modules
    {
        /// <summary>
        /// This class is used to represent a ReplicationPad3d module.
        /// </summary>
        public sealed class ReplicationPad3d : PadBase
        {
            internal ReplicationPad3d(params long[] padding) : base(nameof(ReplicationPad1d), PaddingModes.Replicate, 0, padding) { }
        }
    }

    public static partial class torch
    {
        public static partial class nn
        {
            /// <summary>
            /// Pads the input tensor using replication of the input boundary.
            /// </summary>
            /// <param name="padding">The size of the padding.</param>
            /// <returns></returns>
            public static ReplicationPad3d ReplicationPad3d(long padding)
            {
                return new ReplicationPad3d(padding, padding, padding, padding, padding, padding);
            }

            /// <summary>
            /// Pads the input tensor using replication of the input boundary.
            /// </summary>
            /// <param name="padding">The size of the padding: (padding_left, padding_right, padding_top, padding_bottom, padding_front, padding_back).</param>
            /// <returns></returns>
            public static ReplicationPad3d ReplicationPad3d((long, long, long, long, long, long) padding)
            {
                return new ReplicationPad3d(padding.Item1, padding.Item2, padding.Item3, padding.Item4, padding.Item5, padding.Item6);
            }
        }
    }
}
