using System;
using System.Collections;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Result
{
	// Token: 0x02000BAA RID: 2986
	public class NKCUIResultSubUIWorldmap : NKCUIResultSubUIBase
	{
		// Token: 0x060089F0 RID: 35312 RVA: 0x002EC134 File Offset: 0x002EA334
		public override void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060089F1 RID: 35313 RVA: 0x002EC142 File Offset: 0x002EA342
		private void Init()
		{
			if (this.m_bInit)
			{
				return;
			}
			this.m_trSD = base.transform.Find("SD_CHAR_AREA");
			this.m_bInit = true;
		}

		// Token: 0x060089F2 RID: 35314 RVA: 0x002EC16C File Offset: 0x002EA36C
		public void SetData(bool bBigSuccess, NKMUnitData leaderUnit)
		{
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_spineSD);
			NKCUtil.SetGameobjectActive(this.m_lbText, true);
			if (!bBigSuccess || leaderUnit == null)
			{
				base.ProcessRequired = false;
				this.m_spineSD = null;
				return;
			}
			this.Init();
			this.m_spineSD = NKCResourceUtility.OpenSpineSD(leaderUnit, false);
			if (this.m_spineSD == null)
			{
				base.ProcessRequired = false;
				return;
			}
			this.m_spineSD.SetParent(this.m_trSD, false);
			RectTransform rectTransform = this.m_spineSD.GetRectTransform();
			if (rectTransform != null)
			{
				rectTransform.localPosition = Vector2.zero;
				rectTransform.localScale = Vector3.one;
				rectTransform.localRotation = Quaternion.identity;
			}
			base.ProcessRequired = true;
		}

		// Token: 0x060089F3 RID: 35315 RVA: 0x002EC228 File Offset: 0x002EA428
		public void SetData(bool bBigSuccess, int unitID)
		{
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_spineSD);
			NKCUtil.SetGameobjectActive(this.m_lbText, false);
			if (!bBigSuccess || unitID <= 0)
			{
				base.ProcessRequired = false;
				this.m_spineSD = null;
				return;
			}
			this.Init();
			this.m_spineSD = NKCResourceUtility.OpenSpineSD(unitID, 0, false);
			if (this.m_spineSD == null)
			{
				base.ProcessRequired = false;
				return;
			}
			this.m_spineSD.SetParent(this.m_trSD, false);
			RectTransform rectTransform = this.m_spineSD.GetRectTransform();
			if (rectTransform != null)
			{
				rectTransform.localPosition = Vector2.zero;
				rectTransform.localScale = Vector3.one;
				rectTransform.localRotation = Quaternion.identity;
			}
			base.ProcessRequired = true;
		}

		// Token: 0x060089F4 RID: 35316 RVA: 0x002EC2E4 File Offset: 0x002EA4E4
		public override void FinishProcess()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			this.m_bFinished = true;
			base.StopAllCoroutines();
		}

		// Token: 0x060089F5 RID: 35317 RVA: 0x002EC301 File Offset: 0x002EA501
		public override bool IsProcessFinished()
		{
			return this.m_bFinished;
		}

		// Token: 0x060089F6 RID: 35318 RVA: 0x002EC309 File Offset: 0x002EA509
		protected override IEnumerator InnerProcess(bool bAutoSkip)
		{
			this.m_bFinished = false;
			yield return null;
			this.m_spineSD.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_WIN, true, 0, true, 0f, true);
			yield return null;
			float aniTime = this.m_spineSD.GetAnimationTime(NKCASUIUnitIllust.eAnimation.SD_WIN);
			float deltaTime = 0f;
			while (deltaTime < aniTime)
			{
				deltaTime += Time.deltaTime;
				yield return null;
			}
			yield return null;
			this.FinishProcess();
			yield break;
		}

		// Token: 0x0400766B RID: 30315
		public Text m_lbText;

		// Token: 0x0400766C RID: 30316
		private Transform m_trSD;

		// Token: 0x0400766D RID: 30317
		private NKCASUIUnitIllust m_spineSD;

		// Token: 0x0400766E RID: 30318
		private bool m_bInit;

		// Token: 0x0400766F RID: 30319
		private bool m_bFinished;
	}
}
