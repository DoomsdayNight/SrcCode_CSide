using System;
using System.Text;
using NKC;
using UnityEngine;
using UnityEngine.UI;

namespace NKM
{
	// Token: 0x0200036B RID: 875
	public class NKCUnitBuffIcon
	{
		// Token: 0x06001528 RID: 5416 RVA: 0x0004F988 File Offset: 0x0004DB88
		public void InitObject(GameObject cUNIT_GAGE, int index)
		{
			StringBuilder builder = NKMString.GetBuilder();
			builder.AppendFormat(string.Format("UNIT_BUFF/UNIT_BUFF{0}", index), Array.Empty<object>());
			this.m_UNIT_BUFF = cUNIT_GAGE.transform.Find(builder.ToString()).gameObject;
			this.m_UNIT_BUFF_Image = this.m_UNIT_BUFF.GetComponent<Image>();
			builder.Clear();
			builder.AppendFormat(string.Format("UNIT_BUFF/UNIT_OUTLINE{0}", index), Array.Empty<object>());
			this.m_UNIT_BUFF_OUTLINE = cUNIT_GAGE.transform.Find(builder.ToString()).gameObject;
			builder.Clear();
			builder.AppendFormat(string.Format("UNIT_BUFF/UNIT_BUFF{0}/UNIT_BUFF_COOL", index), Array.Empty<object>());
			this.m_UNIT_BUFF_COOL_Image = cUNIT_GAGE.transform.Find(builder.ToString()).gameObject.GetComponent<Image>();
			builder.Clear();
			builder.AppendFormat(string.Format("UNIT_BUFF/UNIT_BUFF{0}_TEXT", index), Array.Empty<object>());
			this.m_UNIT_BUFF_TEXT = cUNIT_GAGE.transform.Find(builder.ToString()).gameObject;
			this.m_UNIT_BUFF_TEXT_OVERLAP_Text = this.m_UNIT_BUFF_TEXT.transform.Find("UNIT_BUFF_TEXT_OVERLAP").gameObject.GetComponent<Text>();
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0004FACB File Offset: 0x0004DCCB
		public void Init()
		{
			this.m_GageBuffID = 0;
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0004FAD4 File Offset: 0x0004DCD4
		public void Unload()
		{
			this.m_UNIT_BUFF = null;
			this.m_UNIT_BUFF_OUTLINE = null;
			this.m_UNIT_BUFF_Image = null;
			this.m_UNIT_BUFF_COOL_Image = null;
			this.m_UNIT_BUFF_TEXT = null;
			this.m_UNIT_BUFF_TEXT_OVERLAP_Text = null;
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0004FB00 File Offset: 0x0004DD00
		public void GageSetBuffIconActive(bool bActive, NKMBuffData cNKMBuffData = null, float fLifeTimeRate = 1f)
		{
			if (cNKMBuffData != null && cNKMBuffData.m_NKMBuffTemplet != null && !cNKMBuffData.m_NKMBuffTemplet.m_bShowBuffIcon)
			{
				bActive = false;
			}
			bool bValue = false;
			if (cNKMBuffData != null && cNKMBuffData.m_NKMBuffTemplet != null && cNKMBuffData.m_NKMBuffTemplet.m_bInfinity && bActive)
			{
				bValue = true;
			}
			if (cNKMBuffData != null && cNKMBuffData.m_NKMBuffTemplet != null && cNKMBuffData.m_NKMBuffTemplet.m_bNotDispel && bActive)
			{
				bValue = true;
			}
			NKCUtil.SetGameobjectActive(this.m_UNIT_BUFF, bActive);
			NKCUtil.SetGameobjectActive(this.m_UNIT_BUFF_OUTLINE, bValue);
			if (!bActive)
			{
				if (this.m_UNIT_BUFF_TEXT.activeSelf)
				{
					this.m_UNIT_BUFF_TEXT.SetActive(false);
					return;
				}
			}
			else
			{
				this.m_UNIT_BUFF_COOL_Image.fillAmount = 1f - fLifeTimeRate;
				if (cNKMBuffData.m_NKMBuffTemplet != null && this.m_GageBuffID != (int)cNKMBuffData.m_NKMBuffTemplet.m_BuffID && cNKMBuffData.m_NKMBuffTemplet.m_IconName.Length > 1)
				{
					this.m_UNIT_BUFF_Image.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UNIT_GAME_NKM_UNIT_SPRITE", cNKMBuffData.m_NKMBuffTemplet.m_IconName, false);
				}
				this.m_GageBuffID = (int)cNKMBuffData.m_NKMBuffTemplet.m_BuffID;
				if (cNKMBuffData.m_NKMBuffTemplet.m_RangeOverlap)
				{
					if (cNKMBuffData.m_BuffSyncData.m_BuffStatLevel >= 1)
					{
						if (!this.m_UNIT_BUFF_TEXT.activeSelf)
						{
							this.m_UNIT_BUFF_TEXT.SetActive(true);
						}
						if (this.m_OverlapCount != cNKMBuffData.m_BuffSyncData.m_BuffStatLevel)
						{
							this.m_OverlapCount = cNKMBuffData.m_BuffSyncData.m_BuffStatLevel;
							this.m_UNIT_BUFF_TEXT_OVERLAP_Text.text = string.Format("{0}", (int)(this.m_OverlapCount - 1));
							return;
						}
					}
					else if (this.m_UNIT_BUFF_TEXT.activeSelf)
					{
						this.m_UNIT_BUFF_TEXT.SetActive(false);
						return;
					}
				}
				else if (cNKMBuffData.m_BuffSyncData.m_OverlapCount > 1)
				{
					if (!this.m_UNIT_BUFF_TEXT.activeSelf)
					{
						this.m_UNIT_BUFF_TEXT.SetActive(true);
					}
					if (this.m_OverlapCount != cNKMBuffData.m_BuffSyncData.m_OverlapCount)
					{
						this.m_OverlapCount = cNKMBuffData.m_BuffSyncData.m_OverlapCount;
						this.m_UNIT_BUFF_TEXT_OVERLAP_Text.text = string.Format("{0}", this.m_OverlapCount);
						return;
					}
				}
				else if (this.m_UNIT_BUFF_TEXT.activeSelf)
				{
					this.m_UNIT_BUFF_TEXT.SetActive(false);
				}
			}
		}

		// Token: 0x04000E7D RID: 3709
		private GameObject m_UNIT_BUFF;

		// Token: 0x04000E7E RID: 3710
		private Image m_UNIT_BUFF_Image;

		// Token: 0x04000E7F RID: 3711
		private Image m_UNIT_BUFF_COOL_Image;

		// Token: 0x04000E80 RID: 3712
		private GameObject m_UNIT_BUFF_OUTLINE;

		// Token: 0x04000E81 RID: 3713
		private int m_GageBuffID;

		// Token: 0x04000E82 RID: 3714
		private GameObject m_UNIT_BUFF_TEXT;

		// Token: 0x04000E83 RID: 3715
		private byte m_OverlapCount;

		// Token: 0x04000E84 RID: 3716
		private Text m_UNIT_BUFF_TEXT_OVERLAP_Text;
	}
}
