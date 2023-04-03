using System;
using ClientPacket.Account;
using UnityEngine;

namespace NKC.Publisher
{
	// Token: 0x02000863 RID: 2147
	public class NKCPMNexonNGS
	{
		// Token: 0x06005539 RID: 21817 RVA: 0x0019E89E File Offset: 0x0019CA9E
		public static void SetNpaCode(string data)
		{
			NKCPMNexonNGS.s_NPA_code = data;
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x0019E8A6 File Offset: 0x0019CAA6
		public static string GetNpaCode()
		{
			return NKCPMNexonNGS.s_NPA_code;
		}

		// Token: 0x0600553B RID: 21819 RVA: 0x0019E8B0 File Offset: 0x0019CAB0
		public static void OnRecv(NKMPacket_NEXON_NGS_DATA_NOT cNKMPacket_NEXON_NGS_DATA_NOT)
		{
			Debug.Log("OnRecv NKMPacket_NEXON_NGS_DATA_NOT length : " + cNKMPacket_NEXON_NGS_DATA_NOT.buffer.Length.ToString());
		}

		// Token: 0x04004412 RID: 17426
		private static string s_NPA_code = "";
	}
}
