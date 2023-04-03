using System;
using System.Runtime.InteropServices;
using System.Text;

namespace KeraLua
{
	// Token: 0x02000089 RID: 137
	public struct LuaDebug
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x000160BA File Offset: 0x000142BA
		public static LuaDebug FromIntPtr(IntPtr ar)
		{
			return Marshal.PtrToStructure<LuaDebug>(ar);
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x000160C2 File Offset: 0x000142C2
		public string Name
		{
			get
			{
				return Marshal.PtrToStringAnsi(this.name);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x000160CF File Offset: 0x000142CF
		public string NameWhat
		{
			get
			{
				return Marshal.PtrToStringAnsi(this.what);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x000160DC File Offset: 0x000142DC
		public string What
		{
			get
			{
				return Marshal.PtrToStringAnsi(this.what);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x000160E9 File Offset: 0x000142E9
		public string Source
		{
			get
			{
				return Marshal.PtrToStringAnsi(this.source, this.SourceLength);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x000160FC File Offset: 0x000142FC
		public int SourceLength
		{
			get
			{
				return this.sourceLen.ToInt32();
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0001610C File Offset: 0x0001430C
		public string ShortSource
		{
			get
			{
				if (this.shortSource[0] == 0)
				{
					return string.Empty;
				}
				int num = 0;
				while (num < this.shortSource.Length && this.shortSource[num] != 0)
				{
					num++;
				}
				return Encoding.ASCII.GetString(this.shortSource, 0, num);
			}
		}

		// Token: 0x04000268 RID: 616
		[MarshalAs(UnmanagedType.I4)]
		public LuaHookEvent Event;

		// Token: 0x04000269 RID: 617
		private IntPtr name;

		// Token: 0x0400026A RID: 618
		private IntPtr nameWhat;

		// Token: 0x0400026B RID: 619
		private IntPtr what;

		// Token: 0x0400026C RID: 620
		private IntPtr source;

		// Token: 0x0400026D RID: 621
		private IntPtr sourceLen;

		// Token: 0x0400026E RID: 622
		public int CurrentLine;

		// Token: 0x0400026F RID: 623
		public int LineDefined;

		// Token: 0x04000270 RID: 624
		public int LastLineDefined;

		// Token: 0x04000271 RID: 625
		public byte NumberUpValues;

		// Token: 0x04000272 RID: 626
		public byte NumberParameters;

		// Token: 0x04000273 RID: 627
		[MarshalAs(UnmanagedType.I1)]
		public bool IsVarArg;

		// Token: 0x04000274 RID: 628
		[MarshalAs(UnmanagedType.I1)]
		public bool IsTailCall;

		// Token: 0x04000275 RID: 629
		public ushort IndexFirstValue;

		// Token: 0x04000276 RID: 630
		public ushort NumberTransferredValues;

		// Token: 0x04000277 RID: 631
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
		private byte[] shortSource;

		// Token: 0x04000278 RID: 632
		private IntPtr i_ci;
	}
}
