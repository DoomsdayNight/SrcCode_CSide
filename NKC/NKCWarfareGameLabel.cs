using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007E1 RID: 2017
	public class NKCWarfareGameLabel : MonoBehaviour
	{
		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06004FC6 RID: 20422 RVA: 0x00181E4B File Offset: 0x0018004B
		// (set) Token: 0x06004FC7 RID: 20423 RVA: 0x00181E53 File Offset: 0x00180053
		public int Index { get; private set; }

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06004FC8 RID: 20424 RVA: 0x00181E5C File Offset: 0x0018005C
		// (set) Token: 0x06004FC9 RID: 20425 RVA: 0x00181E64 File Offset: 0x00180064
		public WARFARE_LABEL_TYPE LabelType { get; private set; }

		// Token: 0x06004FCA RID: 20426 RVA: 0x00181E70 File Offset: 0x00180070
		public static NKCWarfareGameLabel GetNewInstance(Transform parent)
		{
			NKCWarfareGameLabel component = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WARFARE", "NUM_WARFARE_LABEL", false, null).m_Instant.GetComponent<NKCWarfareGameLabel>();
			if (component == null)
			{
				Debug.LogError("NKCWarfareGameLabel Prefab null!");
				return null;
			}
			if (parent != null)
			{
				component.transform.SetParent(parent);
				component.transform.localScale = Vector3.one;
			}
			component.Hide();
			return component;
		}

		// Token: 0x06004FCB RID: 20427 RVA: 0x00181EDA File Offset: 0x001800DA
		public void Hide()
		{
			this.SetWFLabelType(-1, WARFARE_LABEL_TYPE.NONE);
		}

		// Token: 0x06004FCC RID: 20428 RVA: 0x00181EE4 File Offset: 0x001800E4
		public void SetWFLabelType(int index, WARFARE_LABEL_TYPE labelType)
		{
			this.Index = index;
			this.LabelType = labelType;
			base.gameObject.SetActive(labelType > WARFARE_LABEL_TYPE.NONE);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_ENTER._obj, false);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_HOLD._obj, labelType == WARFARE_LABEL_TYPE.HOLD);
			NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_SUMMON._obj, labelType == WARFARE_LABEL_TYPE.SUMMON);
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x00181F48 File Offset: 0x00180148
		public void SetWFLabelCount(int count)
		{
			NKCWarfareGameLabel.WFLabel currentWFLabel = this.GetCurrentWFLabel();
			if (currentWFLabel == null)
			{
				return;
			}
			NKCUtil.SetLabelText(currentWFLabel._text, count.ToString());
			NKCUtil.SetGameobjectActive(currentWFLabel._fx, count == 0);
		}

		// Token: 0x06004FCE RID: 20430 RVA: 0x00181F84 File Offset: 0x00180184
		private NKCWarfareGameLabel.WFLabel GetCurrentWFLabel()
		{
			switch (this.LabelType)
			{
			case WARFARE_LABEL_TYPE.ENTER:
				return this.m_NUM_WARFARE_ENTER;
			case WARFARE_LABEL_TYPE.HOLD:
				return this.m_NUM_WARFARE_HOLD;
			case WARFARE_LABEL_TYPE.SUMMON:
				return this.m_NUM_WARFARE_SUMMON;
			default:
				return null;
			}
		}

		// Token: 0x04003FD1 RID: 16337
		public NKCWarfareGameLabel.WFLabel m_NUM_WARFARE_ENTER;

		// Token: 0x04003FD2 RID: 16338
		public NKCWarfareGameLabel.WFLabel m_NUM_WARFARE_HOLD;

		// Token: 0x04003FD3 RID: 16339
		public NKCWarfareGameLabel.WFLabel m_NUM_WARFARE_SUMMON;

		// Token: 0x02001498 RID: 5272
		[Serializable]
		public class WFLabel
		{
			// Token: 0x04009E7B RID: 40571
			public GameObject _obj;

			// Token: 0x04009E7C RID: 40572
			public Text _text;

			// Token: 0x04009E7D RID: 40573
			public GameObject _fx;
		}
	}
}
