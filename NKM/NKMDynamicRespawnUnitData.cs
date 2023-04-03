using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000411 RID: 1041
	public class NKMDynamicRespawnUnitData : ISerializable
	{
		// Token: 0x06001B43 RID: 6979 RVA: 0x00077729 File Offset: 0x00075929
		public virtual void Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMUnitData>(ref this.m_NKMUnitData);
			stream.PutOrGet(ref this.m_MasterGameUnitUID);
			stream.PutOrGet(ref this.m_bLoadedServer);
			stream.PutOrGet(ref this.m_bLoadedClient);
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0007775B File Offset: 0x0007595B
		public void DeepCopyFromSource(NKMDynamicRespawnUnitData source)
		{
			this.m_NKMUnitData.DeepCopyFrom(source.m_NKMUnitData);
			this.m_MasterGameUnitUID = source.m_MasterGameUnitUID;
			this.m_bLoadedServer = source.m_bLoadedServer;
			this.m_bLoadedClient = source.m_bLoadedClient;
		}

		// Token: 0x04001B01 RID: 6913
		public NKMUnitData m_NKMUnitData = new NKMUnitData();

		// Token: 0x04001B02 RID: 6914
		public short m_MasterGameUnitUID;

		// Token: 0x04001B03 RID: 6915
		public bool m_bLoadedServer;

		// Token: 0x04001B04 RID: 6916
		public bool m_bLoadedClient;
	}
}
