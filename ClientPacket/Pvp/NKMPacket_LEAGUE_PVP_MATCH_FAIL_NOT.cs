using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DA3 RID: 3491
	[PacketId(ClientPacketId.kNKMPacket_LEAGUE_PVP_MATCH_FAIL_NOT)]
	public sealed class NKMPacket_LEAGUE_PVP_MATCH_FAIL_NOT : ISerializable
	{
		// Token: 0x0600963F RID: 38463 RVA: 0x0032B658 File Offset: 0x00329858
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008849 RID: 34889
		public NKM_ERROR_CODE errorCode;
	}
}
