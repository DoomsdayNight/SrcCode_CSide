using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F4C RID: 3916
	[PacketId(ClientPacketId.kNKMPacket_GAME_DEV_COOL_TIME_RESET_REQ)]
	public sealed class NKMPacket_GAME_DEV_COOL_TIME_RESET_REQ : ISerializable
	{
		// Token: 0x06009978 RID: 39288 RVA: 0x003300F2 File Offset: 0x0032E2F2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_TEAM_TYPE>(ref this.teamType);
			stream.PutOrGet(ref this.isSkill);
		}

		// Token: 0x04008C7A RID: 35962
		public NKM_TEAM_TYPE teamType;

		// Token: 0x04008C7B RID: 35963
		public bool isSkill;
	}
}
