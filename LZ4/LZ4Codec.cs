using System;
using System.Runtime.CompilerServices;
using System.Text;
using LZ4.Services;

namespace LZ4
{
	// Token: 0x02000610 RID: 1552
	public static class LZ4Codec
	{
		// Token: 0x06002FD9 RID: 12249 RVA: 0x000EA254 File Offset: 0x000E8454
		static LZ4Codec()
		{
			if (LZ4Codec.Try<bool>(new LZ4Codec.Func<bool>(LZ4Codec.Has2015Runtime), false))
			{
				LZ4Codec.Try(new LZ4Codec.Action(LZ4Codec.InitializeLZ4mm));
				LZ4Codec.Try(new LZ4Codec.Action(LZ4Codec.InitializeLZ4cc));
			}
			LZ4Codec.Try(new LZ4Codec.Action(LZ4Codec.InitializeLZ4n));
			LZ4Codec.Try(new LZ4Codec.Action(LZ4Codec.InitializeLZ4s));
			ILZ4Service encoder;
			ILZ4Service decoder;
			ILZ4Service encoderHC;
			LZ4Codec.SelectCodec(out encoder, out decoder, out encoderHC);
			LZ4Codec.Encoder = encoder;
			LZ4Codec.Decoder = decoder;
			LZ4Codec.EncoderHC = encoderHC;
			if (LZ4Codec.Encoder == null || LZ4Codec.Decoder == null)
			{
				throw new NotSupportedException("No LZ4 compression service found");
			}
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x000EA2F0 File Offset: 0x000E84F0
		private static void SelectCodec(out ILZ4Service encoder, out ILZ4Service decoder, out ILZ4Service encoderHC)
		{
			if (IntPtr.Size == 4)
			{
				ILZ4Service ilz4Service;
				if ((ilz4Service = LZ4Codec._service_MM32) == null && (ilz4Service = LZ4Codec._service_MM64) == null && (ilz4Service = LZ4Codec._service_N32) == null && (ilz4Service = LZ4Codec._service_CC32) == null && (ilz4Service = LZ4Codec._service_N64) == null && (ilz4Service = LZ4Codec._service_CC64) == null)
				{
					ilz4Service = (LZ4Codec._service_S32 ?? LZ4Codec._service_S64);
				}
				encoder = ilz4Service;
				ILZ4Service ilz4Service2;
				if ((ilz4Service2 = LZ4Codec._service_MM32) == null && (ilz4Service2 = LZ4Codec._service_MM64) == null && (ilz4Service2 = LZ4Codec._service_CC64) == null && (ilz4Service2 = LZ4Codec._service_CC32) == null && (ilz4Service2 = LZ4Codec._service_N64) == null && (ilz4Service2 = LZ4Codec._service_N32) == null)
				{
					ilz4Service2 = (LZ4Codec._service_S64 ?? LZ4Codec._service_S32);
				}
				decoder = ilz4Service2;
				ILZ4Service ilz4Service3;
				if ((ilz4Service3 = LZ4Codec._service_MM32) == null && (ilz4Service3 = LZ4Codec._service_MM64) == null && (ilz4Service3 = LZ4Codec._service_N32) == null && (ilz4Service3 = LZ4Codec._service_CC32) == null && (ilz4Service3 = LZ4Codec._service_N64) == null && (ilz4Service3 = LZ4Codec._service_CC64) == null)
				{
					ilz4Service3 = (LZ4Codec._service_S32 ?? LZ4Codec._service_S64);
				}
				encoderHC = ilz4Service3;
				return;
			}
			ILZ4Service ilz4Service4;
			if ((ilz4Service4 = LZ4Codec._service_MM64) == null && (ilz4Service4 = LZ4Codec._service_MM32) == null && (ilz4Service4 = LZ4Codec._service_N64) == null && (ilz4Service4 = LZ4Codec._service_N32) == null && (ilz4Service4 = LZ4Codec._service_CC64) == null && (ilz4Service4 = LZ4Codec._service_CC32) == null)
			{
				ilz4Service4 = (LZ4Codec._service_S32 ?? LZ4Codec._service_S64);
			}
			encoder = ilz4Service4;
			ILZ4Service ilz4Service5;
			if ((ilz4Service5 = LZ4Codec._service_MM64) == null && (ilz4Service5 = LZ4Codec._service_N64) == null && (ilz4Service5 = LZ4Codec._service_N32) == null && (ilz4Service5 = LZ4Codec._service_CC64) == null && (ilz4Service5 = LZ4Codec._service_MM32) == null && (ilz4Service5 = LZ4Codec._service_CC32) == null)
			{
				ilz4Service5 = (LZ4Codec._service_S64 ?? LZ4Codec._service_S32);
			}
			decoder = ilz4Service5;
			ILZ4Service ilz4Service6;
			if ((ilz4Service6 = LZ4Codec._service_MM64) == null && (ilz4Service6 = LZ4Codec._service_MM32) == null && (ilz4Service6 = LZ4Codec._service_CC32) == null && (ilz4Service6 = LZ4Codec._service_CC64) == null && (ilz4Service6 = LZ4Codec._service_N32) == null && (ilz4Service6 = LZ4Codec._service_N64) == null)
			{
				ilz4Service6 = (LZ4Codec._service_S32 ?? LZ4Codec._service_S64);
			}
			encoderHC = ilz4Service6;
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x000EA4B0 File Offset: 0x000E86B0
		private static ILZ4Service AutoTest(ILZ4Service service)
		{
			byte[] bytes = Encoding.UTF8.GetBytes("Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
			byte[] array = new byte[LZ4Codec.MaximumOutputLength(bytes.Length)];
			int num = service.Encode(bytes, 0, bytes.Length, array, 0, array.Length);
			if (num < 0)
			{
				return null;
			}
			byte[] array2 = new byte[bytes.Length];
			if (service.Decode(array, 0, num, array2, 0, array2.Length, true) != bytes.Length)
			{
				return null;
			}
			if (Encoding.UTF8.GetString(array2, 0, array2.Length) != "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.")
			{
				return null;
			}
			if (service.Decode(array, 0, num, array2, 0, array2.Length, false) != bytes.Length)
			{
				return null;
			}
			if (Encoding.UTF8.GetString(array2, 0, array2.Length) != "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.")
			{
				return null;
			}
			byte[] array3 = new byte[LZ4Codec.MaximumOutputLength(bytes.Length)];
			int num2 = service.EncodeHC(bytes, 0, bytes.Length, array3, 0, array3.Length);
			if (num2 < 0)
			{
				return null;
			}
			byte[] array4 = new byte[bytes.Length];
			if (service.Decode(array3, 0, num2, array4, 0, array4.Length, true) != bytes.Length)
			{
				return null;
			}
			if (Encoding.UTF8.GetString(array4, 0, array4.Length) != "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.")
			{
				return null;
			}
			if (service.Decode(array3, 0, num2, array4, 0, array4.Length, false) != bytes.Length)
			{
				return null;
			}
			if (Encoding.UTF8.GetString(array4, 0, array4.Length) != "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.")
			{
				return null;
			}
			return service;
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x000EA608 File Offset: 0x000E8808
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void Try(LZ4Codec.Action method)
		{
			try
			{
				method();
			}
			catch
			{
			}
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x000EA630 File Offset: 0x000E8830
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static T Try<T>(LZ4Codec.Func<T> method, T defaultValue)
		{
			T result;
			try
			{
				result = method();
			}
			catch
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x000EA65C File Offset: 0x000E885C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static ILZ4Service TryService<T>() where T : ILZ4Service, new()
		{
			ILZ4Service result;
			try
			{
				result = LZ4Codec.AutoTest(Activator.CreateInstance<T>());
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06002FDF RID: 12255 RVA: 0x000EA694 File Offset: 0x000E8894
		public static string CodecName
		{
			get
			{
				return string.Format("{0}/{1}/{2}HC", (LZ4Codec.Encoder == null) ? "<none>" : LZ4Codec.Encoder.CodecName, (LZ4Codec.Decoder == null) ? "<none>" : LZ4Codec.Decoder.CodecName, (LZ4Codec.EncoderHC == null) ? "<none>" : LZ4Codec.EncoderHC.CodecName);
			}
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x000EA6F3 File Offset: 0x000E88F3
		public static int MaximumOutputLength(int inputLength)
		{
			return inputLength + inputLength / 255 + 16;
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000EA701 File Offset: 0x000E8901
		public static int Encode(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			return LZ4Codec.Encoder.Encode(input, inputOffset, inputLength, output, outputOffset, outputLength);
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000EA718 File Offset: 0x000E8918
		public static byte[] Encode(byte[] input, int inputOffset, int inputLength)
		{
			if (inputLength < 0)
			{
				inputLength = input.Length - inputOffset;
			}
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (inputOffset < 0 || inputOffset + inputLength > input.Length)
			{
				throw new ArgumentException("inputOffset and inputLength are invalid for given input");
			}
			byte[] array = new byte[LZ4Codec.MaximumOutputLength(inputLength)];
			int num = LZ4Codec.Encode(input, inputOffset, inputLength, array, 0, array.Length);
			if (num == array.Length)
			{
				return array;
			}
			if (num < 0)
			{
				throw new InvalidOperationException("Compression has been corrupted");
			}
			byte[] array2 = new byte[num];
			Buffer.BlockCopy(array, 0, array2, 0, num);
			return array2;
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x000EA798 File Offset: 0x000E8998
		public static int EncodeHC(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			return (LZ4Codec.EncoderHC ?? LZ4Codec.Encoder).EncodeHC(input, inputOffset, inputLength, output, outputOffset, outputLength);
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000EA7B8 File Offset: 0x000E89B8
		public static byte[] EncodeHC(byte[] input, int inputOffset, int inputLength)
		{
			if (inputLength < 0)
			{
				inputLength = input.Length - inputOffset;
			}
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (inputOffset < 0 || inputOffset + inputLength > input.Length)
			{
				throw new ArgumentException("inputOffset and inputLength are invalid for given input");
			}
			byte[] array = new byte[LZ4Codec.MaximumOutputLength(inputLength)];
			int num = LZ4Codec.EncodeHC(input, inputOffset, inputLength, array, 0, array.Length);
			if (num == array.Length)
			{
				return array;
			}
			if (num < 0)
			{
				throw new InvalidOperationException("Compression has been corrupted");
			}
			byte[] array2 = new byte[num];
			Buffer.BlockCopy(array, 0, array2, 0, num);
			return array2;
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x000EA838 File Offset: 0x000E8A38
		public static int Decode(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength = 0, bool knownOutputLength = false)
		{
			return LZ4Codec.Decoder.Decode(input, inputOffset, inputLength, output, outputOffset, outputLength, knownOutputLength);
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x000EA850 File Offset: 0x000E8A50
		public static byte[] Decode(byte[] input, int inputOffset, int inputLength, int outputLength)
		{
			if (inputLength < 0)
			{
				inputLength = input.Length - inputOffset;
			}
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (inputOffset < 0 || inputOffset + inputLength > input.Length)
			{
				throw new ArgumentException("inputOffset and inputLength are invalid for given input");
			}
			byte[] array = new byte[outputLength];
			if (LZ4Codec.Decode(input, inputOffset, inputLength, array, 0, outputLength, true) != outputLength)
			{
				throw new ArgumentException("outputLength is not valid");
			}
			return array;
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x000EA8AF File Offset: 0x000E8AAF
		private static void Poke4(byte[] buffer, int offset, uint value)
		{
			buffer[offset] = (byte)value;
			buffer[offset + 1] = (byte)(value >> 8);
			buffer[offset + 2] = (byte)(value >> 16);
			buffer[offset + 3] = (byte)(value >> 24);
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000EA8D3 File Offset: 0x000E8AD3
		private static uint Peek4(byte[] buffer, int offset)
		{
			return (uint)((int)buffer[offset] | (int)buffer[offset + 1] << 8 | (int)buffer[offset + 2] << 16 | (int)buffer[offset + 3] << 24);
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000EA8F4 File Offset: 0x000E8AF4
		private static byte[] Wrap(byte[] inputBuffer, int inputOffset, int inputLength, bool highCompression)
		{
			inputLength = Math.Min(inputBuffer.Length - inputOffset, inputLength);
			if (inputLength < 0)
			{
				throw new ArgumentException("inputBuffer size of inputLength is invalid");
			}
			if (inputLength == 0)
			{
				return new byte[8];
			}
			int num = inputLength;
			byte[] array = new byte[num];
			num = (highCompression ? LZ4Codec.EncodeHC(inputBuffer, inputOffset, inputLength, array, 0, num) : LZ4Codec.Encode(inputBuffer, inputOffset, inputLength, array, 0, num));
			byte[] array2;
			if (num >= inputLength || num <= 0)
			{
				array2 = new byte[inputLength + 8];
				LZ4Codec.Poke4(array2, 0, (uint)inputLength);
				LZ4Codec.Poke4(array2, 4, (uint)inputLength);
				Buffer.BlockCopy(inputBuffer, inputOffset, array2, 8, inputLength);
			}
			else
			{
				array2 = new byte[num + 8];
				LZ4Codec.Poke4(array2, 0, (uint)inputLength);
				LZ4Codec.Poke4(array2, 4, (uint)num);
				Buffer.BlockCopy(array, 0, array2, 8, num);
			}
			return array2;
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000EA99D File Offset: 0x000E8B9D
		public static byte[] Wrap(byte[] inputBuffer, int inputOffset = 0, int inputLength = 2147483647)
		{
			return LZ4Codec.Wrap(inputBuffer, inputOffset, inputLength, false);
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000EA9A8 File Offset: 0x000E8BA8
		public static byte[] WrapHC(byte[] inputBuffer, int inputOffset = 0, int inputLength = 2147483647)
		{
			return LZ4Codec.Wrap(inputBuffer, inputOffset, inputLength, true);
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000EA9B4 File Offset: 0x000E8BB4
		public static byte[] Unwrap(byte[] inputBuffer, int inputOffset = 0)
		{
			int num = inputBuffer.Length - inputOffset;
			if (num < 8)
			{
				throw new ArgumentException("inputBuffer size is invalid");
			}
			int num2 = (int)LZ4Codec.Peek4(inputBuffer, inputOffset);
			num = (int)LZ4Codec.Peek4(inputBuffer, inputOffset + 4);
			if (num > inputBuffer.Length - inputOffset - 8)
			{
				throw new ArgumentException("inputBuffer size is invalid or has been corrupted");
			}
			byte[] array;
			if (num >= num2)
			{
				array = new byte[num];
				Buffer.BlockCopy(inputBuffer, inputOffset + 8, array, 0, num);
			}
			else
			{
				array = new byte[num2];
				LZ4Codec.Decode(inputBuffer, inputOffset + 8, num, array, 0, num2, true);
			}
			return array;
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000EAA2D File Offset: 0x000E8C2D
		private static bool Has2015Runtime()
		{
			return false;
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000EAA30 File Offset: 0x000E8C30
		private static void InitializeLZ4mm()
		{
			LZ4Codec._service_MM32 = (LZ4Codec._service_MM64 = null);
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000EAA3E File Offset: 0x000E8C3E
		private static void InitializeLZ4cc()
		{
			LZ4Codec._service_CC32 = (LZ4Codec._service_CC64 = null);
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000EAA4C File Offset: 0x000E8C4C
		private static void InitializeLZ4n()
		{
			LZ4Codec._service_N32 = (LZ4Codec._service_N64 = null);
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000EAA5A File Offset: 0x000E8C5A
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void InitializeLZ4s()
		{
			LZ4Codec._service_S32 = LZ4Codec.TryService<Safe32LZ4Service>();
			LZ4Codec._service_S64 = LZ4Codec.TryService<Safe64LZ4Service>();
		}

		// Token: 0x04002F74 RID: 12148
		private static readonly ILZ4Service Encoder;

		// Token: 0x04002F75 RID: 12149
		private static readonly ILZ4Service EncoderHC;

		// Token: 0x04002F76 RID: 12150
		private static readonly ILZ4Service Decoder;

		// Token: 0x04002F77 RID: 12151
		private static ILZ4Service _service_MM32;

		// Token: 0x04002F78 RID: 12152
		private static ILZ4Service _service_MM64;

		// Token: 0x04002F79 RID: 12153
		private static ILZ4Service _service_CC32;

		// Token: 0x04002F7A RID: 12154
		private static ILZ4Service _service_CC64;

		// Token: 0x04002F7B RID: 12155
		private static ILZ4Service _service_N32;

		// Token: 0x04002F7C RID: 12156
		private static ILZ4Service _service_N64;

		// Token: 0x04002F7D RID: 12157
		private static ILZ4Service _service_S32;

		// Token: 0x04002F7E RID: 12158
		private static ILZ4Service _service_S64;

		// Token: 0x04002F7F RID: 12159
		private const int WRAP_OFFSET_0 = 0;

		// Token: 0x04002F80 RID: 12160
		private const int WRAP_OFFSET_4 = 4;

		// Token: 0x04002F81 RID: 12161
		private const int WRAP_OFFSET_8 = 8;

		// Token: 0x04002F82 RID: 12162
		private const int WRAP_LENGTH = 8;

		// Token: 0x020012D8 RID: 4824
		// (Invoke) Token: 0x0600A489 RID: 42121
		public delegate void Action();

		// Token: 0x020012D9 RID: 4825
		// (Invoke) Token: 0x0600A48D RID: 42125
		public delegate T Func<T>();
	}
}
