using System;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A2C RID: 2604
	public class NKCUIOverlayCharMessage : NKCUIBase
	{
		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x06007205 RID: 29189 RVA: 0x0025ECA5 File Offset: 0x0025CEA5
		public static NKCUIOverlayCharMessage Instance
		{
			get
			{
				if (NKCUIOverlayCharMessage.m_Instance == null)
				{
					NKCUIOverlayCharMessage.m_Instance = NKCUIManager.OpenNewInstance<NKCUIOverlayCharMessage>("ab_ui_nkm_ui_character_message", "NKM_UI_CHARACTER_MESSAGE_BOX", NKCUIManager.eUIBaseRect.UIOverlay, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIOverlayCharMessage.CleanupInstance)).GetInstance<NKCUIOverlayCharMessage>();
				}
				return NKCUIOverlayCharMessage.m_Instance;
			}
		}

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x06007206 RID: 29190 RVA: 0x0025ECDF File Offset: 0x0025CEDF
		public static bool HasInstance
		{
			get
			{
				return NKCUIOverlayCharMessage.m_Instance != null;
			}
		}

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06007207 RID: 29191 RVA: 0x0025ECEC File Offset: 0x0025CEEC
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIOverlayCharMessage.m_Instance != null && NKCUIOverlayCharMessage.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007208 RID: 29192 RVA: 0x0025ED07 File Offset: 0x0025CF07
		public static void CheckInstanceAndClose()
		{
			if (NKCUIOverlayCharMessage.m_Instance != null && NKCUIOverlayCharMessage.m_Instance.IsOpen)
			{
				NKCUIOverlayCharMessage.m_Instance.Close();
			}
		}

		// Token: 0x06007209 RID: 29193 RVA: 0x0025ED2C File Offset: 0x0025CF2C
		private static void CleanupInstance()
		{
			NKCUIOverlayCharMessage.m_Instance = null;
		}

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x0600720A RID: 29194 RVA: 0x0025ED34 File Offset: 0x0025CF34
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Overlay;
			}
		}

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x0600720B RID: 29195 RVA: 0x0025ED37 File Offset: 0x0025CF37
		public override string MenuName
		{
			get
			{
				return "캐릭터 메시지";
			}
		}

		// Token: 0x0600720C RID: 29196 RVA: 0x0025ED3E File Offset: 0x0025CF3E
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x0600720D RID: 29197 RVA: 0x0025ED4C File Offset: 0x0025CF4C
		public override bool IgnoreBackButtonWhenOpen
		{
			get
			{
				return this.m_bModal;
			}
		}

		// Token: 0x0600720E RID: 29198 RVA: 0x0025ED54 File Offset: 0x0025CF54
		public void Open(NKMUnitTempletBase unitTempletBase, string message, float fOpenTime, UnityAction onComplete, bool bForceAnimation = false)
		{
			Sprite spChar = null;
			if (unitTempletBase != null)
			{
				spChar = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase);
			}
			this.Open(spChar, message, fOpenTime, onComplete, bForceAnimation);
		}

		// Token: 0x0600720F RID: 29199 RVA: 0x0025ED7C File Offset: 0x0025CF7C
		public void Open(string invenIconAssetName, string message, float fOpenTime, UnityAction onComplete, bool bForceAnimation = false)
		{
			Sprite spChar = null;
			if (!string.IsNullOrEmpty(invenIconAssetName))
			{
				spChar = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_INVEN_ICON_UNIT", invenIconAssetName, false);
			}
			this.Open(spChar, message, fOpenTime, onComplete, bForceAnimation);
		}

		// Token: 0x06007210 RID: 29200 RVA: 0x0025EDB0 File Offset: 0x0025CFB0
		public void Open(Sprite spChar, string message, float fOpenTime, UnityAction onComplete, bool bForceAnimation = false)
		{
			this.SetData(spChar, message);
			this.dOnComplete = onComplete;
			this.m_bModal = (fOpenTime == 0f);
			this.m_fOpenedTime = 0f;
			this.m_fTargetOpenTime = (this.m_bModal ? 0.4f : fOpenTime);
			NKCUtil.SetGameobjectActive(this.m_imgBackGround, this.m_bModal);
			if (!base.IsOpen)
			{
				base.UIOpened(true);
			}
		}

		// Token: 0x06007211 RID: 29201 RVA: 0x0025EE1C File Offset: 0x0025D01C
		public void SetData(NKMUnitTempletBase unitTempletBase, string message)
		{
			Sprite spChar = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase);
			this.SetData(spChar, message);
		}

		// Token: 0x06007212 RID: 29202 RVA: 0x0025EE39 File Offset: 0x0025D039
		public void SetData(Sprite spChar, string message)
		{
			if (spChar == null)
			{
				NKCUtil.SetGameobjectActive(this.m_goRootCharacter, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_goRootCharacter, true);
				NKCUtil.SetImageSprite(this.m_imgCharacter, spChar, false);
			}
			NKCUtil.SetLabelText(this.m_lbMessage, message);
		}

		// Token: 0x06007213 RID: 29203 RVA: 0x0025EE78 File Offset: 0x0025D078
		private void Update()
		{
			if (this.m_fOpenedTime >= 0f)
			{
				this.m_fOpenedTime += Time.deltaTime;
			}
			if (this.m_fOpenedTime >= this.m_fTargetOpenTime)
			{
				if (this.m_bModal)
				{
					if (Input.anyKeyDown)
					{
						this.m_fOpenedTime = -1f;
						UnityAction unityAction = this.dOnComplete;
						if (unityAction == null)
						{
							return;
						}
						unityAction();
						return;
					}
				}
				else
				{
					this.m_fOpenedTime = -1f;
					UnityAction unityAction2 = this.dOnComplete;
					if (unityAction2 == null)
					{
						return;
					}
					unityAction2();
				}
			}
		}

		// Token: 0x06007214 RID: 29204 RVA: 0x0025EEF8 File Offset: 0x0025D0F8
		public void SetBGScreenAlpha(float alpha)
		{
			this.m_imgBackGround.color = new Color(this.m_imgBackGround.color.r, this.m_imgBackGround.color.g, this.m_imgBackGround.color.b, alpha);
		}

		// Token: 0x04005DF5 RID: 24053
		public const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_character_message";

		// Token: 0x04005DF6 RID: 24054
		public const string UI_ASSET_NAME = "NKM_UI_CHARACTER_MESSAGE_BOX";

		// Token: 0x04005DF7 RID: 24055
		private static NKCUIOverlayCharMessage m_Instance;

		// Token: 0x04005DF8 RID: 24056
		public GameObject m_goRootCharacter;

		// Token: 0x04005DF9 RID: 24057
		public Image m_imgCharacter;

		// Token: 0x04005DFA RID: 24058
		public Text m_lbMessage;

		// Token: 0x04005DFB RID: 24059
		public Image m_imgBackGround;

		// Token: 0x04005DFC RID: 24060
		private bool m_bModal;

		// Token: 0x04005DFD RID: 24061
		private bool m_bWaitForClose;

		// Token: 0x04005DFE RID: 24062
		private UnityAction dOnComplete;

		// Token: 0x04005DFF RID: 24063
		private const float MODAL_DELAY_TIME = 0.4f;

		// Token: 0x04005E00 RID: 24064
		private float m_fTargetOpenTime;

		// Token: 0x04005E01 RID: 24065
		private float m_fOpenedTime;
	}
}
