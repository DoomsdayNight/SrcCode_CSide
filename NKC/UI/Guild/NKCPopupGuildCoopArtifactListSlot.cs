using System;
using NKM.Guild;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B29 RID: 2857
	public class NKCPopupGuildCoopArtifactListSlot : MonoBehaviour
	{
		// Token: 0x06008228 RID: 33320 RVA: 0x002BEA31 File Offset: 0x002BCC31
		public void Init()
		{
			if (this.m_slot != null)
			{
				this.m_slot.Init();
				this.m_slot.SetUseBigImg(true);
			}
		}

		// Token: 0x06008229 RID: 33321 RVA: 0x002BEA58 File Offset: 0x002BCC58
		public void SetData(GuildDungeonArtifactTemplet templet)
		{
			NKCUtil.SetLabelText(this.m_lbOrderNum, templet.GetOrder().ToString());
			NKCUtil.SetImageSprite(this.m_imgGradeBG, NKCUtil.GetGuildArtifactBgProbImage(templet.GetBgProbImage()), false);
			NKCUtil.SetGameobjectActive(this.m_objGradeGlow, templet.GetBgProbImage() == GuildDungeonArtifactTemplet.ArtifactProbType.HIGH);
			this.m_slot.SetData(NKCUISlot.SlotData.MakeGuildArtifactData(templet.GetArtifactId(), 1), true, null);
			NKCUtil.SetLabelText(this.m_lbName, templet.GetName());
			NKCUtil.SetLabelText(this.m_lbDesc, templet.GetDescFull());
			NKCUtil.SetGameobjectActive(this.m_objClear, false);
			NKCUtil.SetGameobjectActive(this.m_objCurrent, false);
		}

		// Token: 0x0600822A RID: 33322 RVA: 0x002BEAFC File Offset: 0x002BCCFC
		public void SetClear(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objClear, bValue);
		}

		// Token: 0x0600822B RID: 33323 RVA: 0x002BEB0A File Offset: 0x002BCD0A
		public void SetCurrent(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objCurrent, bValue);
		}

		// Token: 0x04006E51 RID: 28241
		public Text m_lbOrderNum;

		// Token: 0x04006E52 RID: 28242
		public Image m_imgGradeBG;

		// Token: 0x04006E53 RID: 28243
		public GameObject m_objGradeGlow;

		// Token: 0x04006E54 RID: 28244
		public NKCUISlot m_slot;

		// Token: 0x04006E55 RID: 28245
		public Text m_lbName;

		// Token: 0x04006E56 RID: 28246
		public Text m_lbDesc;

		// Token: 0x04006E57 RID: 28247
		public GameObject m_objClear;

		// Token: 0x04006E58 RID: 28248
		public GameObject m_objCurrent;
	}
}
