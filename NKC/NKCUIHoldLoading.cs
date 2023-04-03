using System;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x020009A7 RID: 2471
	public class NKCUIHoldLoading : MonoBehaviour
	{
		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x060066F8 RID: 26360 RVA: 0x00210254 File Offset: 0x0020E454
		public static NKCUIHoldLoading Instance
		{
			get
			{
				if (NKCUIHoldLoading.m_instance == null)
				{
					NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_DETAIL_LOADING", false, null);
					if (nkcassetInstanceData.m_Instant == null)
					{
						Debug.LogError("NKM_UI_POPUP_DETAIL_LOADING 없음");
						return null;
					}
					NKCUIHoldLoading.m_instance = nkcassetInstanceData.m_Instant.GetComponent<NKCUIHoldLoading>();
					NKCUIHoldLoading.m_instance.Init();
				}
				return NKCUIHoldLoading.m_instance;
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x060066F9 RID: 26361 RVA: 0x002102B9 File Offset: 0x0020E4B9
		public static bool IsOpen
		{
			get
			{
				return NKCUIHoldLoading.m_instance != null && NKCUIHoldLoading.m_instance.gameObject.activeSelf;
			}
		}

		// Token: 0x060066FA RID: 26362 RVA: 0x002102DC File Offset: 0x0020E4DC
		public void Init()
		{
			if (this.m_animator == null)
			{
				this.m_animator = base.GetComponent<Animator>();
			}
			if (this.m_RectToCalcTouchPos == null)
			{
				GameObject gameObject = new GameObject("goRectToCalcTouchPos", new Type[]
				{
					typeof(RectTransform)
				});
				this.m_RectToCalcTouchPos = gameObject.GetComponent<RectTransform>();
				this.m_RectToCalcTouchPos.anchoredPosition = new Vector2(0f, 0f);
				this.m_RectToCalcTouchPos.offsetMax = new Vector2(0f, 0f);
				this.m_RectToCalcTouchPos.offsetMin = new Vector2(0f, 0f);
				this.m_RectToCalcTouchPos.anchorMax = new Vector2(1f, 1f);
				this.m_RectToCalcTouchPos.anchorMin = new Vector2(0f, 0f);
				this.m_RectToCalcTouchPos.SetWidth((float)Screen.width);
				this.m_RectToCalcTouchPos.SetHeight((float)Screen.height);
			}
			base.transform.SetParent(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIOverlay));
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x060066FB RID: 26363 RVA: 0x00210400 File Offset: 0x0020E600
		public void Open(Vector2 touchPos, float waitTime = -1f)
		{
			this.SetPosition(touchPos);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_animator.Play("NKM_UI_POPUP_DETAIL_LOADING", -1, 0f);
			if (waitTime <= 0f)
			{
				this.m_animator.speed = 1f;
				return;
			}
			AnimationClip animationClip = this.m_animator.runtimeAnimatorController.animationClips[0];
			if (animationClip != null)
			{
				this.m_animator.speed = animationClip.length / waitTime;
				return;
			}
			this.m_animator.speed = 1f;
		}

		// Token: 0x060066FC RID: 26364 RVA: 0x00210490 File Offset: 0x0020E690
		public bool IsPlaying()
		{
			return this.m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f;
		}

		// Token: 0x060066FD RID: 26365 RVA: 0x002104B8 File Offset: 0x0020E6B8
		public void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060066FE RID: 26366 RVA: 0x002104C8 File Offset: 0x0020E6C8
		private void SetPosition(Vector2 touchPos)
		{
			Vector3 vector;
			RectTransformUtility.ScreenPointToWorldPointInRectangle(this.m_RectToCalcTouchPos, touchPos, null, out vector);
			vector.x -= this.m_RectToCalcTouchPos.GetWidth() * NKCUIManager.UIFrontCanvasSafeRectTransform.pivot.x;
			vector.y -= this.m_RectToCalcTouchPos.GetHeight() * NKCUIManager.UIFrontCanvasSafeRectTransform.pivot.y;
			vector.x -= vector.x * (1f - NKCUIManager.UIFrontCanvasSafeRectTransform.GetWidth() / this.m_RectToCalcTouchPos.GetWidth());
			vector.y -= vector.y * (1f - NKCUIManager.UIFrontCanvasSafeRectTransform.GetHeight() / this.m_RectToCalcTouchPos.GetHeight());
			vector.z = 0f;
			base.transform.localPosition = vector;
		}

		// Token: 0x040052FF RID: 21247
		private const string BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x04005300 RID: 21248
		private const string ASSET_NAME = "NKM_UI_POPUP_DETAIL_LOADING";

		// Token: 0x04005301 RID: 21249
		private static NKCUIHoldLoading m_instance;

		// Token: 0x04005302 RID: 21250
		private const string ANI_NAME = "NKM_UI_POPUP_DETAIL_LOADING";

		// Token: 0x04005303 RID: 21251
		private Animator m_animator;

		// Token: 0x04005304 RID: 21252
		private RectTransform m_RectToCalcTouchPos;
	}
}
