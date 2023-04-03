using System;
using ClientPacket.Office;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Component.Office
{
	// Token: 0x02000C66 RID: 3174
	public class NKCUIComOfficeEnvScore : MonoBehaviour
	{
		// Token: 0x060093B8 RID: 37816 RVA: 0x0032717C File Offset: 0x0032537C
		public void UpdateEnvScore(NKMOfficeRoom room)
		{
			if (room == null)
			{
				NKCUtil.SetLabelText(this.m_lbEnvScore, "-");
				NKCUtil.SetLabelText(this.m_lbEnvInformation, "");
				return;
			}
			NKCUtil.SetLabelText(this.m_lbEnvScore, room.interiorScore.ToString());
			NKMOfficeGradeTemplet nkmofficeGradeTemplet = NKMOfficeGradeTemplet.Find(room.grade);
			if (nkmofficeGradeTemplet != null)
			{
				string @string = NKCStringTable.GetString("SI_DP_OFFICE_LOYALTY_SPEED", new object[]
				{
					nkmofficeGradeTemplet.ChargingTimeHour
				});
				NKCUtil.SetLabelText(this.m_lbEnvInformation, @string);
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbEnvInformation, "");
			}
			if (this.m_GradeStep != null)
			{
				int grade = (int)room.grade;
				int num = this.m_GradeStep.Length;
				for (int i = 0; i < num; i++)
				{
					Sprite orLoadAssetResource;
					if (i <= grade)
					{
						orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_office_sprite", this.m_onIconName, false);
					}
					else
					{
						orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_office_sprite", this.m_offIconName, false);
					}
					NKCUtil.SetImageSprite(this.m_GradeStep[i], orLoadAssetResource, true);
				}
			}
		}

		// Token: 0x040080AE RID: 32942
		public Text m_lbEnvScore;

		// Token: 0x040080AF RID: 32943
		public Text m_lbEnvInformation;

		// Token: 0x040080B0 RID: 32944
		public Image[] m_GradeStep;

		// Token: 0x040080B1 RID: 32945
		public string m_offIconName;

		// Token: 0x040080B2 RID: 32946
		public string m_onIconName;

		// Token: 0x040080B3 RID: 32947
		private const string m_strSpriteBundleName = "ab_ui_office_sprite";
	}
}
