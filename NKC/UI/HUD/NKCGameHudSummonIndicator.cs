using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.HUD
{
	// Token: 0x02000C46 RID: 3142
	public class NKCGameHudSummonIndicator : MonoBehaviour
	{
		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x0600927F RID: 37503 RVA: 0x0032018C File Offset: 0x0031E38C
		public bool Idle
		{
			get
			{
				return this.m_fOnTime < 0f || !base.gameObject.activeInHierarchy;
			}
		}

		// Token: 0x06009280 RID: 37504 RVA: 0x003201AC File Offset: 0x0031E3AC
		public bool SetData(NKCUnitClient targetUnit, NKCGameClient gameClient, Transform trLeft, Transform trRight)
		{
			this.m_fOnTime = -1f;
			if (targetUnit == null || gameClient == null)
			{
				return false;
			}
			this.m_NKCGameClient = gameClient;
			this.m_NKCUnitClient = targetUnit;
			this.m_trLeft = trLeft;
			this.m_trRight = trRight;
			Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, targetUnit.GetUnitData());
			if (sprite == null)
			{
				return false;
			}
			NKCUtil.SetImageSprite(this.m_imgUnit, sprite, false);
			this.m_fOnTime = this.m_fDelayAfterStartState;
			this.SetDirection();
			base.gameObject.SetActive(true);
			return true;
		}

		// Token: 0x06009281 RID: 37505 RVA: 0x00320230 File Offset: 0x0031E430
		private void Update()
		{
			if (this.m_NKCUnitClient == null)
			{
				base.gameObject.SetActive(false);
				return;
			}
			if (this.m_NKCUnitClient.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_PLAY)
			{
				base.gameObject.SetActive(false);
				return;
			}
			if (this.m_NKCUnitClient.GetUnitStateNow() == null || this.m_NKCUnitClient.GetUnitStateNow().m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_START)
			{
				this.m_fOnTime -= Time.deltaTime;
			}
			if (this.m_fOnTime < 0f)
			{
				base.gameObject.SetActive(false);
			}
			this.SetDirection();
		}

		// Token: 0x06009282 RID: 37506 RVA: 0x003202C4 File Offset: 0x0031E4C4
		private void SetDirection()
		{
			float num = NKCCamera.GetCameraSizeNow() * NKCCamera.GetCameraAspect();
			float num2 = NKCCamera.GetPosNowX(false) - num;
			float num3 = NKCCamera.GetPosNowX(false) + num;
			float posX = this.m_NKCUnitClient.GetUnitSyncData().m_PosX;
			if (posX < num2)
			{
				base.transform.SetParent(this.m_trLeft);
				this.m_rtArrow.localScale = new Vector3(-1f, 1f, 1f);
				NKCUtil.SetGameobjectActive(this.m_objRoot, true);
				return;
			}
			if (posX > num3)
			{
				base.transform.SetParent(this.m_trRight);
				this.m_rtArrow.localScale = Vector3.one;
				NKCUtil.SetGameobjectActive(this.m_objRoot, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objRoot, false);
		}

		// Token: 0x04007F7A RID: 32634
		public GameObject m_objRoot;

		// Token: 0x04007F7B RID: 32635
		public RectTransform m_rtArrow;

		// Token: 0x04007F7C RID: 32636
		public Image m_imgUnit;

		// Token: 0x04007F7D RID: 32637
		public float m_fDelayAfterStartState = 0.5f;

		// Token: 0x04007F7E RID: 32638
		public float m_fPositionOffset = 250f;

		// Token: 0x04007F7F RID: 32639
		private NKCGameClient m_NKCGameClient;

		// Token: 0x04007F80 RID: 32640
		private NKCUnitClient m_NKCUnitClient;

		// Token: 0x04007F81 RID: 32641
		private Transform m_trLeft;

		// Token: 0x04007F82 RID: 32642
		private Transform m_trRight;

		// Token: 0x04007F83 RID: 32643
		private float m_fOnTime;
	}
}
