using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000948 RID: 2376
	[RequireComponent(typeof(Text))]
	public class NKCUIComSpecialStringChanger : MonoBehaviour
	{
		// Token: 0x06005ED9 RID: 24281 RVA: 0x001D72A4 File Offset: 0x001D54A4
		public void Translate()
		{
			Text component = base.GetComponent<Text>();
			if (component == null)
			{
				return;
			}
			NKCUIComSpecialStringChanger.eStringType eType = this.m_eType;
			string msg;
			if (eType != NKCUIComSpecialStringChanger.eStringType.OperatorSkill)
			{
				if (eType != NKCUIComSpecialStringChanger.eStringType.Buff)
				{
					msg = "";
				}
				else
				{
					NKMBuffTemplet buffTempletByStrID = NKMBuffManager.GetBuffTempletByStrID(this.m_targetStrID);
					msg = NKCUtilString.ApplyBuffValueToString(NKCStringTable.GetString(this.m_Key, false), buffTempletByStrID, this.m_targetLevel, this.m_targetTimeLevel);
				}
			}
			else
			{
				msg = NKCOperatorUtil.MakeOperatorSkillDesc(NKCOperatorUtil.GetSkillTemplet(this.m_targetStrID), this.m_targetLevel);
			}
			NKCUtil.SetLabelText(component, msg);
		}

		// Token: 0x06005EDA RID: 24282 RVA: 0x001D7325 File Offset: 0x001D5525
		private void Awake()
		{
			if (!this.m_bTranslateAtStart)
			{
				this.Translate();
			}
		}

		// Token: 0x06005EDB RID: 24283 RVA: 0x001D7335 File Offset: 0x001D5535
		private void Start()
		{
			if (this.m_bTranslateAtStart)
			{
				this.Translate();
			}
		}

		// Token: 0x04004AF9 RID: 19193
		public NKCUIComSpecialStringChanger.eStringType m_eType;

		// Token: 0x04004AFA RID: 19194
		public string m_targetStrID;

		// Token: 0x04004AFB RID: 19195
		public int m_targetLevel = 1;

		// Token: 0x04004AFC RID: 19196
		public int m_targetTimeLevel = 1;

		// Token: 0x04004AFD RID: 19197
		public bool m_bTranslateAtStart;

		// Token: 0x04004AFE RID: 19198
		public string m_Key;

		// Token: 0x020015D1 RID: 5585
		public enum eStringType
		{
			// Token: 0x0400A29A RID: 41626
			OperatorSkill,
			// Token: 0x0400A29B RID: 41627
			Buff
		}
	}
}
