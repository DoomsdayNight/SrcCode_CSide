using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F4D RID: 3917
	[PacketId(ClientPacketId.kNKMPacket_GAME_DEV_COOL_TIME_RESET_ACK)]
	public sealed class NKMPacket_GAME_DEV_COOL_TIME_RESET_ACK : ISerializable
	{
		// Token: 0x0600997A RID: 39290 RVA: 0x00330114 File Offset: 0x0032E314
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<NKM_TEAM_TYPE>(ref this.teamType);
			stream.PutOrGet(ref this.isSkill);
		}

		// Token: 0x04008C7C RID: 35964
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C7D RID: 35965
		public NKM_TEAM_TYPE teamType;

		// Token: 0x04008C7E RID: 35966
		public bool isSkill;
	}
}
