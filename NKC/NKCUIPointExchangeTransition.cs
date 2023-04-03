using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x020009CC RID: 2508
	public class NKCUIPointExchangeTransition : NKCUIBase
	{
		// Token: 0x06006B01 RID: 27393 RVA: 0x0022C3DD File Offset: 0x0022A5DD
		public static NKCUIPointExchangeTransition MakeInstance(string bundleName, string assetName)
		{
			NKCUIPointExchangeTransition instance = NKCUIManager.OpenNewInstance<NKCUIPointExchangeTransition>(bundleName, assetName, NKCUIManager.eUIBaseRect.UIFrontCommon, null).GetInstance<NKCUIPointExchangeTransition>();
			instance.InitUI();
			return instance;
		}

		// Token: 0x06006B02 RID: 27394 RVA: 0x0022C3F4 File Offset: 0x0022A5F4
		public static NKCUIPointExchangeTransition MakeInstance(NKMPointExchangeTemplet pointExchangeTemplet = null)
		{
			if (pointExchangeTemplet == null)
			{
				pointExchangeTemplet = NKMPointExchangeTemplet.GetByTime(NKCSynchronizedTime.ServiceTime);
			}
			if (pointExchangeTemplet == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(pointExchangeTemplet.PrefabId))
			{
				return null;
			}
			NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(pointExchangeTemplet.PrefabId, pointExchangeTemplet.PrefabId);
			return NKCUIPointExchangeTransition.MakeInstance(nkmassetName.m_BundleName, nkmassetName.m_AssetName + "_TRANS");
		}

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x06006B03 RID: 27395 RVA: 0x0022C451 File Offset: 0x0022A651
		public override string MenuName
		{
			get
			{
				return "Point Exchange Transition";
			}
		}

		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x06006B04 RID: 27396 RVA: 0x0022C458 File Offset: 0x0022A658
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Overlay;
			}
		}

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x06006B05 RID: 27397 RVA: 0x0022C45B File Offset: 0x0022A65B
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x06006B06 RID: 27398 RVA: 0x0022C45E File Offset: 0x0022A65E
		private void InitUI()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06006B07 RID: 27399 RVA: 0x0022C46C File Offset: 0x0022A66C
		public void Open()
		{
			base.gameObject.SetActive(true);
			base.StartCoroutine(this.IOpeningTransition());
			if (base.IsOpen)
			{
				return;
			}
			base.UIOpened(true);
		}

		// Token: 0x06006B08 RID: 27400 RVA: 0x0022C497 File Offset: 0x0022A697
		private IEnumerator IOpeningTransition()
		{
			if (this.m_enableCodeAnimation)
			{
				float num = NKCUIManager.GetUIFrontCanvasScaler().referenceResolution.x / NKCUIManager.GetUIFrontCanvasScaler().referenceResolution.y;
				float num2 = this.m_rtTransitionRoot.rect.width / this.m_rtTransitionRoot.rect.height;
				float num3 = this.m_rtTransitionScreen.rect.width - this.m_rtTransitionRoot.rect.width;
				if (num2 >= num)
				{
					Vector3 position = this.m_rtTransitionScreen.transform.position;
					position.x = this.m_rtTransitionRoot.rect.width + num3;
					this.m_rtTransitionScreen.transform.position = position;
				}
				else
				{
					float num4 = NKCUIManager.GetUIFrontCanvasScaler().referenceResolution.y / this.m_rtTransitionRoot.rect.height;
					Vector3 position2 = this.m_rtTransitionScreen.transform.position;
					position2.x = this.m_rtTransitionRoot.rect.width * num4 + num3;
					this.m_rtTransitionScreen.transform.position = position2;
				}
				this.m_rtTransitionScreen.DOMoveX(-this.m_rtTransitionRoot.rect.width - num3, this.m_duration, false).SetEase(Ease.Linear).OnComplete(new TweenCallback(base.Close));
			}
			NKCUIPointExchangeTransition.TransInfo.TransType transType = this.m_transInfo.transType;
			if (transType != NKCUIPointExchangeTransition.TransInfo.TransType.SLIDE_FROM_RIGHT)
			{
				if (transType == NKCUIPointExchangeTransition.TransInfo.TransType.SLIDE_FROM_TOP)
				{
					while (this.m_rtTransitionScreen.transform.position.y > this.m_transInfo.popupValue)
					{
						yield return null;
					}
				}
			}
			else
			{
				while (this.m_rtTransitionScreen.transform.position.x > this.m_transInfo.popupValue)
				{
					yield return null;
				}
			}
			NKCPopupPointExchange.Instance.Open();
			NKCPopupPointExchange.Instance.PlayMusic();
			while (this.m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
			base.Close();
			yield break;
		}

		// Token: 0x06006B09 RID: 27401 RVA: 0x0022C4A6 File Offset: 0x0022A6A6
		public override void CloseInternal()
		{
			this.m_animator.Rebind();
			base.gameObject.SetActive(false);
		}

		// Token: 0x040056A6 RID: 22182
		public NKCUIPointExchangeTransition.TransInfo m_transInfo;

		// Token: 0x040056A7 RID: 22183
		public RectTransform m_rtTransitionRoot;

		// Token: 0x040056A8 RID: 22184
		public RectTransform m_rtTransitionScreen;

		// Token: 0x040056A9 RID: 22185
		public Animator m_animator;

		// Token: 0x040056AA RID: 22186
		[Header("�ڵ�� �۵��ϴ� Ʈ������ ����")]
		public float m_duration;

		// Token: 0x040056AB RID: 22187
		public bool m_enableCodeAnimation;

		// Token: 0x020016D1 RID: 5841
		[Serializable]
		public struct TransInfo
		{
			// Token: 0x0400A544 RID: 42308
			public NKCUIPointExchangeTransition.TransInfo.TransType transType;

			// Token: 0x0400A545 RID: 42309
			public float popupValue;

			// Token: 0x02001A87 RID: 6791
			public enum TransType
			{
				// Token: 0x0400AE95 RID: 44693
				SLIDE_FROM_RIGHT,
				// Token: 0x0400AE96 RID: 44694
				SLIDE_FROM_TOP
			}
		}
	}
}
