using System;
using NKC.UI.NPC;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000A15 RID: 2581
	public class NKCUIStageInfoSubStory : NKCUIStageInfoSubBase
	{
		// Token: 0x060070BD RID: 28861 RVA: 0x0025625E File Offset: 0x0025445E
		public override void InitUI(NKCUIStageInfoSubBase.OnButton onButton)
		{
			base.InitUI(onButton);
			this.m_btnReady.PointerClick.RemoveAllListeners();
			this.m_btnReady.PointerClick.AddListener(new UnityAction(base.OnOK));
		}

		// Token: 0x060070BE RID: 28862 RVA: 0x00256293 File Offset: 0x00254493
		public void Close()
		{
			if (this.m_unitIllust != null)
			{
				this.m_unitIllust.Unload();
				this.m_unitIllust = null;
			}
		}

		// Token: 0x060070BF RID: 28863 RVA: 0x002562B0 File Offset: 0x002544B0
		public override void SetData(NKMStageTempletV2 stageTemplet, bool bFirstOpen = true)
		{
			base.SetData(stageTemplet, bFirstOpen);
			if (this.m_unitIllust != null)
			{
				this.m_unitIllust.Unload();
				this.m_unitIllust = null;
			}
			if (string.IsNullOrEmpty(stageTemplet.m_StageCharStr))
			{
				this.m_unitIllust = this.AddSpineIllustration("NKM_NPC_OPERATOR_LENA");
			}
			else
			{
				this.m_unitIllust = this.AddSpineIllustration(stageTemplet.m_StageCharStr);
			}
			if (this.m_unitIllust != null && this.m_NKCUINPCSpineIllust != null)
			{
				this.m_NKCUINPCSpineIllust.m_spUnitIllust = this.m_unitIllust.m_SpineIllustInstant_SkeletonGraphic;
				this.m_unitIllust.SetParent(this.m_NKCUINPCSpineIllust.transform, false);
				this.m_unitIllust.SetAnchoredPosition(this.DEFAULT_CHAR_POS);
				NKCASUIUnitIllust.eAnimation eAnimation;
				if (string.IsNullOrEmpty(stageTemplet.m_StageCharStrFace))
				{
					if (this.m_unitIllust.HasAnimation(NKCASUIUnitIllust.eAnimation.UNIT_IDLE))
					{
						this.m_unitIllust.SetAnimation(NKCASUIUnitIllust.eAnimation.UNIT_IDLE, true, 0, true, 0f, true);
					}
				}
				else if (Enum.TryParse<NKCASUIUnitIllust.eAnimation>(stageTemplet.m_StageCharStrFace, out eAnimation) && this.m_unitIllust.HasAnimation(eAnimation))
				{
					this.m_unitIllust.SetAnimation(eAnimation, true, 0, true, 0f, true);
				}
				this.m_UpdateTimer = 0.1f;
			}
		}

		// Token: 0x060070C0 RID: 28864 RVA: 0x002563DC File Offset: 0x002545DC
		public override void Update()
		{
			base.Update();
			this.m_UpdateTimer -= Time.deltaTime;
			if (this.m_UpdateTimer < 0f)
			{
				this.m_UpdateTimer = 5f;
				this.m_unitIllust.InvalidateWorldRect();
				this.m_unitIllust.GetWorldRect(false);
			}
		}

		// Token: 0x060070C1 RID: 28865 RVA: 0x00256431 File Offset: 0x00254631
		private NKCASUISpineIllust AddSpineIllustration(string prefabStrID)
		{
			return (NKCASUISpineIllust)NKCResourceUtility.OpenSpineIllustWithManualNaming(prefabStrID, false);
		}

		// Token: 0x04005C80 RID: 23680
		[Header("스토리")]
		[Header("상단")]
		public NKCUINPCSpineIllust m_NKCUINPCSpineIllust;

		// Token: 0x04005C81 RID: 23681
		public Vector2 DEFAULT_CHAR_POS = new Vector2(-9.97f, -190f);

		// Token: 0x04005C82 RID: 23682
		[Space]
		[Header("하단")]
		public NKCUIComStateButton m_btnReady;

		// Token: 0x04005C83 RID: 23683
		private const string DEFAULT_CHAR_STR = "NKM_NPC_OPERATOR_LENA";

		// Token: 0x04005C84 RID: 23684
		private NKCASUISpineIllust m_unitIllust;

		// Token: 0x04005C85 RID: 23685
		private float m_UpdateTimer;
	}
}
