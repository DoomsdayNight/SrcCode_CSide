using System;
using ClientPacket.WorldMap;

namespace NKM
{
	// Token: 0x0200050B RID: 1291
	public static class NKMWorldMapEventGroupEx
	{
		// Token: 0x06002513 RID: 9491 RVA: 0x000BF7E4 File Offset: 0x000BD9E4
		public static void Clear(this NKMWorldMapEventGroup data)
		{
			data.worldmapEventID = 0;
			data.eventUid = 0L;
		}
	}
}
