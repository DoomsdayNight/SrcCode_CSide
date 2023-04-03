using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000925 RID: 2341
	public class NKCComUnlockConfirm : MonoBehaviour
	{
		// Token: 0x06005DCB RID: 24011 RVA: 0x001CF388 File Offset: 0x001CD588
		private void OnEnable()
		{
			bool flag = this.IsActivated();
			if (this.m_setTargetObject)
			{
				if (this.m_objActiveWhenConfirmed != null)
				{
					int num = this.m_objActiveWhenConfirmed.Length;
					for (int i = 0; i < num; i++)
					{
						NKCUtil.SetGameobjectActive(this.m_objActiveWhenConfirmed[i], flag);
					}
				}
				if (this.m_objActiveWhenDenied != null)
				{
					int num2 = this.m_objActiveWhenDenied.Length;
					for (int j = 0; j < num2; j++)
					{
						NKCUtil.SetGameobjectActive(this.m_objActiveWhenDenied[j], !flag);
					}
					return;
				}
			}
			else
			{
				base.gameObject.SetActive(flag);
			}
		}

		// Token: 0x06005DCC RID: 24012 RVA: 0x001CF410 File Offset: 0x001CD610
		private bool IsActivated()
		{
			if (this.m_unlockCondition == null)
			{
				return base.gameObject.activeSelf;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return base.gameObject.activeSelf;
			}
			List<UnlockInfo> list = new List<UnlockInfo>();
			int num = this.m_unlockCondition.Length;
			for (int i = 0; i < num; i++)
			{
				if (UnlockInfo.IsDateTimeData(this.m_unlockCondition[i].reqType))
				{
					DateTime reqDateTime;
					DateTime.TryParse(this.m_unlockCondition[i].reqValueStr, out reqDateTime);
					UnlockInfo item = new UnlockInfo(this.m_unlockCondition[i].reqType, this.m_unlockCondition[i].reqValue, reqDateTime);
					list.Add(item);
				}
				else
				{
					UnlockInfo item2 = new UnlockInfo(this.m_unlockCondition[i].reqType, this.m_unlockCondition[i].reqValue, this.m_unlockCondition[i].reqValueStr);
					list.Add(item2);
				}
			}
			bool flag = NKMContentUnlockManager.IsContentUnlocked(nkmuserData, list, this.m_ignoreSuperUser);
			if (!this.m_setTargetObject && this.m_applyAsDeactivator)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x04004A05 RID: 18949
		public NKCComUnlockConfirm.UnlockCondition[] m_unlockCondition;

		// Token: 0x04004A06 RID: 18950
		public bool m_applyAsDeactivator;

		// Token: 0x04004A07 RID: 18951
		public bool m_setTargetObject;

		// Token: 0x04004A08 RID: 18952
		public GameObject[] m_objActiveWhenConfirmed;

		// Token: 0x04004A09 RID: 18953
		public GameObject[] m_objActiveWhenDenied;

		// Token: 0x04004A0A RID: 18954
		public bool m_ignoreSuperUser;

		// Token: 0x020015B6 RID: 5558
		[Serializable]
		public struct UnlockCondition
		{
			// Token: 0x0400A25B RID: 41563
			public STAGE_UNLOCK_REQ_TYPE reqType;

			// Token: 0x0400A25C RID: 41564
			public int reqValue;

			// Token: 0x0400A25D RID: 41565
			public string reqValueStr;
		}
	}
}
