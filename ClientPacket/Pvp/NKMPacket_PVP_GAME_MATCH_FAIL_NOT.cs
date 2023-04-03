using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D76 RID: 3446
	[PacketId(ClientPacketId.kNKMPacket_PVP_GAME_MATCH_FAIL_NOT)]
	public sealed class NKMPacket_PVP_GAME_MATCH_FAIL_NOT : ISerializable
	{
		// Token: 0x060095E7 RID: 38375 RVA: 0x0032AF66 File Offset: 0x00329166
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x040087E1 RID: 34785
		public NKM_ERROR_CODE errorCode;
	}
}
