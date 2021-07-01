// Copyright (c) Microsoft Corporation and contributors.  All Rights Reserved.  See License.txt in the project root for license information.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using TorchSharp.Tensor;

#nullable enable
namespace TorchSharp
{
    public class Identity : nn.Module
    {
        internal Identity (IntPtr handle, IntPtr boxedHandle) : base (handle, boxedHandle) { }

        [DllImport ("LibTorchSharp")]
        extern static IntPtr THSNN_Identity_forward (nn.Module.HType module, IntPtr tensor);

        public override TorchTensor forward (TorchTensor tensor)
        {
            var res = THSNN_Identity_forward (handle, tensor.Handle);
            if (res == IntPtr.Zero) { torch.CheckForErrors(); }
            return new TorchTensor (res);
        }
    }
    public static partial class nn
    {
        [DllImport ("LibTorchSharp")]
        private static extern IntPtr THSNN_Identity_ctor (out IntPtr pBoxedModule);

        /// <summary>
        /// A placeholder identity operator.
        /// </summary>
        /// <returns>The same tensor as is input.</returns>
        static public Identity Identity ()
        {
            var res = THSNN_Identity_ctor (out var boxedHandle);
            if (res == IntPtr.Zero) { torch.CheckForErrors(); }
            return new Identity (res, boxedHandle);
        }
    }
    public static partial class functional
    {
        /// <summary>
        /// A placeholder identity operator.
        /// </summary>
        /// <param name="x">The input tensor.</param>
        /// <returns>The same tensor as is input.</returns>
        static public TorchTensor Identity (TorchTensor x)
        {
            using (var d =nn.Identity()) {
                return d.forward (x);
            }
        }
    }

}
