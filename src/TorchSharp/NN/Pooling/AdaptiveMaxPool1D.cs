// Copyright (c) Microsoft Corporation and contributors.  All Rights Reserved.  See License.txt in the project root for license information.
using System;
using System.Runtime.InteropServices;
using TorchSharp.Tensor;

namespace TorchSharp
{
    /// <summary>
    /// This class is used to represent a AdaptiveMaxPool1D module.
    /// </summary>
    public class AdaptiveMaxPool1d : nn.Module
    {
        internal AdaptiveMaxPool1d (IntPtr handle, IntPtr boxedHandle) : base (handle, boxedHandle)
        {
        }

        [DllImport ("LibTorchSharp")]
        private static extern IntPtr THSNN_AdaptiveMaxPool1d_forward (IntPtr module, IntPtr tensor);

        public override TorchTensor forward (TorchTensor tensor)
        {
            var res = THSNN_AdaptiveMaxPool1d_forward (handle.DangerousGetHandle (), tensor.Handle);
            if (res == IntPtr.Zero) { torch.CheckForErrors(); }
            return new TorchTensor (res);
        }
    }
    public static partial class nn
    {
        [DllImport ("LibTorchSharp")]
        extern static IntPtr THSNN_AdaptiveMaxPool1d_ctor (IntPtr psizes, int length, out IntPtr pBoxedModule);

        /// <summary>
        /// Applies a 1D adaptive max pooling over an input signal composed of several input planes.
        /// The output size is H, for any input size.The number of output features is equal to the number of input planes.
        /// </summary>
        /// <param name="outputSize">The target output size H.</param>
        /// <returns></returns>
        static public AdaptiveMaxPool1d AdaptiveMaxPool1d (long outputSize)
        {
            unsafe {
                fixed (long* pkernelSize = new long[] { outputSize }) {
                    var handle = THSNN_AdaptiveMaxPool1d_ctor ((IntPtr)pkernelSize, 1, out var boxedHandle);
                    if (handle == IntPtr.Zero) { torch.CheckForErrors(); }
                    return new AdaptiveMaxPool1d (handle, boxedHandle);
                }
            }
        }
    }

    public static partial class functional
    {
        /// <summary>
        /// Applies a 1D adaptive max pooling over an input signal composed of several input planes.
        /// The output size is H, for any input size.The number of output features is equal to the number of input planes.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="outputSize">The target output size H.</param>
        /// <returns></returns>
        static public TorchTensor AdaptiveMaxPool1d (TorchTensor x, long outputSize)
        {
            using (var d =nn.AdaptiveMaxPool1d (outputSize)) {
                return d.forward (x);
            }
        }
    }
}
