using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Logging;

namespace NKM
{
	// Token: 0x02000440 RID: 1088
	internal class MissionGroupIdValidator
	{
		// Token: 0x06001DAD RID: 7597 RVA: 0x0008D628 File Offset: 0x0008B828
		public void Add(NKMMissionTemplet templet)
		{
			if (!this.missionTempletsByGroupId.ContainsKey(templet.m_GroupId))
			{
				this.missionTempletsByGroupId[templet.m_GroupId] = new List<NKMMissionTemplet>();
			}
			this.missionTempletsByGroupId[templet.m_GroupId].Add(templet);
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x0008D678 File Offset: 0x0008B878
		public void Validate()
		{
			foreach (KeyValuePair<int, List<NKMMissionTemplet>> keyValuePair in this.missionTempletsByGroupId)
			{
				IEnumerable<IGrouping<int, NKMMissionTemplet>> source = from e in keyValuePair.Value
				group e by e.m_MissionTabId;
				if (source.Count<IGrouping<int, NKMMissionTemplet>>() > 1)
				{
					IEnumerable<int> values = from e in source
					select e.Key;
					Log.ErrorAndExit(string.Format("[MissionGroupIdValidator] ���� �ٸ� �ǿ� �ߺ��� �̼� �׷� ���̵� ������. TabIds : {0}, GroupId : {1}", string.Join<int>(",", values), keyValuePair.Key), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1495);
					break;
				}
				if (keyValuePair.Value.Count > 1)
				{
					if (keyValuePair.Value.Any((NKMMissionTemplet e) => e.m_TabTemplet.m_MissionType == NKM_MISSION_TYPE.MENTORING))
					{
						int groupId = keyValuePair.Value[0].m_GroupId;
						Log.ErrorAndExit(string.Format("[MissionGroupIdValidator] ���丵�� �ߺ� �׷��� ������ �Ұ����մϴ�. groupId: {0}", groupId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1504);
						break;
					}
					if ((from e in keyValuePair.Value
					group e by e.m_MissionCond.mission_cond).Count<IGrouping<NKM_MISSION_COND, NKMMissionTemplet>>() > 1)
					{
						int groupId2 = keyValuePair.Value[0].m_GroupId;
						Log.ErrorAndExit(string.Format("[MissionGroupIdValidator] �ش� �׷� ���̵� ���� �ٸ� �̼� ������� �����Ǿ����ϴ�. groupId: {0}", groupId2), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1512);
						break;
					}
				}
			}
		}

		// Token: 0x04001DF3 RID: 7667
		private Dictionary<int, List<NKMMissionTemplet>> missionTempletsByGroupId = new Dictionary<int, List<NKMMissionTemplet>>();
	}
}
