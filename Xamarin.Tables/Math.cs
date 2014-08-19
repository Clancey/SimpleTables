//
// System.Math.cs
//
// Authors:
//   Bob Smith (bob@thestuff.net)
//   Dan Lewis (dihlewis@yahoo.co.uk)
//   Pedro Martínez Juliá (yoros@wanadoo.es)
//   Andreas Nahr (ClassDevelopment@A-SoftTech.com)
//
// (C) 2001 Bob Smith.  http://www.thestuff.net
// Copyright (C) 2003 Pedro Martínez Juliá <yoros@wanadoo.es>
// Copyright (C) 2004 Novell (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System
{
	static class Math
	{
		public const double E = 2.7182818284590452354;
		public const double PI = 3.14159265358979323846;

		public static nfloat Abs (nfloat value)
		{
			return (value < 0)? -value: value;
		}

		public static nint Abs (nint value)
		{
			if (value == Int32.MinValue)
				throw new OverflowException ("Value is too small.");
			return (value < 0)? -value: value;
		}



		// The following methods are defined in ECMA specs but they are
		// not implemented in MS.NET. However, they are in MS.NET 1.1

		public static long BigMul (nint a, nint b)
		{
			return ((long)a * (long)b);
		}

		public static nint DivRem (nint a, nint b, out nint result)
		{
			result = (a % b);
			return (nint)(a / b);
		}



		[ReliabilityContractAttribute (Consistency.WillNotCorruptState, Cer.Success)]
		public static nfloat Max (nfloat val1, nfloat val2)
		{
			if (nfloat.IsNaN (val1) || nfloat.IsNaN (val2)) {
				return nfloat.NaN;
			}
			return (val1 > val2)? val1: val2;
		}

		[ReliabilityContractAttribute (Consistency.WillNotCorruptState, Cer.Success)]
		public static nint Max (nint val1, nint val2)
		{
			return (val1 > val2)? val1: val2;
		}

		[ReliabilityContractAttribute (Consistency.WillNotCorruptState, Cer.Success)]
		public static nfloat Min (nfloat val1, nfloat val2)
		{
			if (nfloat.IsNaN (val1) || nfloat.IsNaN (val2)) {
				return Single.NaN;
			}
			return (val1 < val2)? val1: val2;
		}

		[ReliabilityContractAttribute (Consistency.WillNotCorruptState, Cer.Success)]
		public static nint Min (nint val1, nint val2)
		{
			return (val1 < val2)? val1: val2;
		}

	



		public static nint Sign (nfloat value)
		{
			if (nfloat.IsNaN (value))
				throw new ArithmeticException ("NAN");
			if (value > 0) return 1;
			return (value == 0)? 0: -1;
		}

		public static nint Sign (nint value)
		{
			if (value > 0) return 1;
			return (value == 0)? 0: -1;
		}

		public static nint Sign (long value)
		{
			if (value > 0) return 1;
			return (value == 0)? 0: -1;
		}

		[CLSCompliant (false)]
		public static nint Sign (sbyte value)
		{
			if (value > 0) return 1;
			return (value == 0)? 0: -1;
		}

		public static nint Sign (short value)
		{
			if (value > 0) return 1;
			return (value == 0)? 0: -1;
		}

	}
}