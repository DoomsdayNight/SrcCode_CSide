using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DE1 RID: 3553
	[PacketId(ClientPacketId.kNKMPacket_STRATEGY_PVP_REFRESH_NOT)]
	public sealed class NKMPacket_STRATEGY_PVP_REFRESH_NOT : ISerializable
	{
		// Token: 0x060096B9 RID: 38585 RVA: 0x0032BD90 File Offset: 0x00329F90
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<PvpState>(ref this.data);
		}

		// Token: 0x0400889F RID: 34975
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040088A0 RID: 34976
		public PvpState data;
	}
}
