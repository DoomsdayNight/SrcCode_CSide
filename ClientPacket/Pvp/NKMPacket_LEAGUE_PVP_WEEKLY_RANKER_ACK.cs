using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DBB RID: 3515
	[PacketId(ClientPacketId.kNKMPacket_LEAGUE_PVP_WEEKLY_RANKER_ACK)]
	public sealed class NKMPacket_LEAGUE_PVP_WEEKLY_RANKER_ACK : ISerializable
	{
		// Token: 0x0600966F RID: 38511 RVA: 0x0032B8B9 File Offset: 0x00329AB9
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUserProfileData>(ref this.userProfileData);
		}

		// Token: 0x04008865 RID: 34917
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008866 RID: 34918
		public List<NKMUserProfileData> userProfileData = new List<NKMUserProfileData>();
	}
}
