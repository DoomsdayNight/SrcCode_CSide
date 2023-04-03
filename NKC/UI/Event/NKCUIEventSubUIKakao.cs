using System;
using ClientPacket.Event;
using NKM;
using NKM.Event;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Event
{
	// Token: 0x02000BE0 RID: 3040
	public class NKCUIEventSubUIKakao : NKCUIEventSubUIMission
	{
		// Token: 0x06008D09 RID: 36105 RVA: 0x002FF6D2 File Offset: 0x002FD8D2
		public override void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnKakaoReceive, new UnityAction(this.OnClickKakao));
			base.Init();
			this.m_bWaitForFocus = false;
		}

		// Token: 0x06008D0A RID: 36106 RVA: 0x002FF6F8 File Offset: 0x002FD8F8
		public override void Open(NKMEventTabTemplet tabTemplet)
		{
			this.m_bWaitForFocus = false;
			base.Open(tabTemplet);
		}

		// Token: 0x06008D0B RID: 36107 RVA: 0x002FF708 File Offset: 0x002FD908
		public override void Refresh()
		{
			NKCUIEventSubUIKakao.KakaoEmoteUIState kakaoObjects;
			if (!NKCScenManager.CurrentUserData().IsKakaoMissionOngoing())
			{
				kakaoObjects = NKCUIEventSubUIKakao.KakaoEmoteUIState.SoldOut;
			}
			else
			{
				kakaoObjects = this.GetKakaoEmoteState(NKCScenManager.CurrentUserData().kakaoMissionData.state);
			}
			this.SetKakaoObjects(kakaoObjects);
			base.Refresh();
		}

		// Token: 0x06008D0C RID: 36108 RVA: 0x002FF748 File Offset: 0x002FD948
		private NKCUIEventSubUIKakao.KakaoEmoteUIState GetKakaoEmoteState(KakaoMissionState state)
		{
			switch (state)
			{
			default:
				return NKCUIEventSubUIKakao.KakaoEmoteUIState.Open;
			case KakaoMissionState.Confirmed:
				return NKCUIEventSubUIKakao.KakaoEmoteUIState.Complete;
			case KakaoMissionState.NotEnoughBudget:
			case KakaoMissionState.OutOfDate:
				return NKCUIEventSubUIKakao.KakaoEmoteUIState.SoldOut;
			}
		}

		// Token: 0x06008D0D RID: 36109 RVA: 0x002FF775 File Offset: 0x002FD975
		private void SetKakaoObjects(NKCUIEventSubUIKakao.KakaoEmoteUIState state)
		{
			NKCUtil.SetGameobjectActive(this.m_objKakaoComplete, state == NKCUIEventSubUIKakao.KakaoEmoteUIState.Complete);
			NKCUtil.SetGameobjectActive(this.m_objKakaoSoldOut, state == NKCUIEventSubUIKakao.KakaoEmoteUIState.SoldOut);
			NKCUIComStateButton csbtnKakaoReceive = this.m_csbtnKakaoReceive;
			if (csbtnKakaoReceive == null)
			{
				return;
			}
			csbtnKakaoReceive.SetLock(state > NKCUIEventSubUIKakao.KakaoEmoteUIState.Open, false);
		}

		// Token: 0x06008D0E RID: 36110 RVA: 0x002FF7AC File Offset: 0x002FD9AC
		private void OnClickKakao()
		{
			string shortCutParam = this.m_tabTemplet.m_ShortCut.Replace("{user-id}", NKCScenManager.CurrentUserData().m_UserUID.ToString());
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_URL, shortCutParam, false);
			this.m_bWaitForFocus = true;
		}

		// Token: 0x06008D0F RID: 36111 RVA: 0x002FF7EE File Offset: 0x002FD9EE
		private void OnApplicationFocus(bool focus)
		{
			if (focus && this.m_bWaitForFocus)
			{
				this.m_bWaitForFocus = false;
				NKCPacketSender.Send_NKMPacket_KAKAO_MISSION_REFRESH_STATE_REQ(this.m_tabTemplet.m_EventID);
			}
		}

		// Token: 0x040079E1 RID: 31201
		[Header("카카오톡 이벤트용 오브젝트")]
		public GameObject m_objKakaoComplete;

		// Token: 0x040079E2 RID: 31202
		public GameObject m_objKakaoSoldOut;

		// Token: 0x040079E3 RID: 31203
		public NKCUIComStateButton m_csbtnKakaoReceive;

		// Token: 0x040079E4 RID: 31204
		private bool m_bWaitForFocus;

		// Token: 0x020019BE RID: 6590
		private enum KakaoEmoteUIState
		{
			// Token: 0x0400ACB6 RID: 44214
			Open,
			// Token: 0x0400ACB7 RID: 44215
			Complete,
			// Token: 0x0400ACB8 RID: 44216
			SoldOut
		}
	}
}
