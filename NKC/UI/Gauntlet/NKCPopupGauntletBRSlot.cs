using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B5D RID: 2909
	public class NKCPopupGauntletBRSlot : MonoBehaviour
	{
		// Token: 0x0600849F RID: 33951 RVA: 0x002CBCE1 File Offset: 0x002C9EE1
		public int GetIndex()
		{
			return this.m_Index;
		}

		// Token: 0x060084A0 RID: 33952 RVA: 0x002CBCEC File Offset: 0x002C9EEC
		public static NKCPopupGauntletBRSlot GetNewInstance(Transform parent, NKCPopupGauntletBRSlot.OnClick _onClick)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_POPUP_RECORD_SLOT", false, null);
			NKCPopupGauntletBRSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCPopupGauntletBRSlot>();
			if (component == null)
			{
				Debug.LogError("NKCPopupGauntletBRSlot Prefab null!");
				return null;
			}
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.m_InstanceData = nkcassetInstanceData;
			component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, 0f);
			component.dOnClick = _onClick;
			component.gameObject.SetActive(false);
			component.m_ctglBRSlot.OnValueChanged.RemoveAllListeners();
			component.m_ctglBRSlot.OnValueChanged.AddListener(new UnityAction<bool>(component.OnClicked));
			return component;
		}

		// Token: 0x060084A1 RID: 33953 RVA: 0x002CBDBD File Offset: 0x002C9FBD
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060084A2 RID: 33954 RVA: 0x002CBDDC File Offset: 0x002C9FDC
		private void OnClicked(bool bSet)
		{
			if (bSet && this.dOnClick != null)
			{
				this.dOnClick(this.m_Index);
			}
		}

		// Token: 0x060084A3 RID: 33955 RVA: 0x002CBDFC File Offset: 0x002C9FFC
		public void SetUI(PvpSingleHistory cPvpSingleHistory, int index)
		{
			if (cPvpSingleHistory == null)
			{
				return;
			}
			this.m_Index = index;
			if (cPvpSingleHistory.Result == PVP_RESULT.WIN)
			{
				this.m_lbGameResult.text = NKCUtilString.GET_STRING_WIN;
				this.m_lbGameResult.color = NKCUtil.GetColor("#FFDF5D");
				this.m_imgGameResult.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_GAUNTLET_SPRITE", "AB_UI_NKM_UI_GAUNTLET_RESULTSMALL_WIN", false);
			}
			else if (cPvpSingleHistory.Result == PVP_RESULT.LOSE)
			{
				this.m_lbGameResult.text = NKCUtilString.GET_STRING_LOSE;
				this.m_lbGameResult.color = NKCUtil.GetColor("#FF2626");
				this.m_imgGameResult.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_GAUNTLET_SPRITE", "AB_UI_NKM_UI_GAUNTLET_RESULTSMALL_LOSE", false);
			}
			else
			{
				this.m_lbGameResult.text = NKCUtilString.GET_STRING_DRAW;
				this.m_lbGameResult.color = NKCUtil.GetColor("#D4D4D4");
				this.m_imgGameResult.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_GAUNTLET_SPRITE", "AB_UI_NKM_UI_GAUNTLET_RESULTSMALL_DRAW", false);
			}
			string assetName = "";
			string msg = "";
			bool bValue = false;
			NKM_GAME_TYPE gameType = cPvpSingleHistory.GameType;
			if (gameType != NKM_GAME_TYPE.NGT_PVP_RANK)
			{
				if (gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
				{
					switch (gameType)
					{
					case NKM_GAME_TYPE.NGT_PVP_PRIVATE:
						assetName = "AB_UI_NKM_UI_GAUNTLET_ELLIPSE_NORMAL";
						msg = NKCUtilString.GET_STRING_PRIVATE_PVP;
						bValue = false;
						goto IL_184;
					case NKM_GAME_TYPE.NGT_PVP_LEAGUE:
						assetName = "AB_UI_NKM_UI_GAUNTLET_ELLIPSE_LEAGUE";
						msg = NKCUtilString.GET_STRING_GAUNTLET_LEAGUE_GAME;
						bValue = true;
						goto IL_184;
					case NKM_GAME_TYPE.NGT_PVP_STRATEGY:
					case NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE:
					case NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC:
						break;
					default:
						Debug.LogError("NKCPopupGauntletBRSlot.SetUI - Not Set Game Type " + cPvpSingleHistory.GameType.ToString());
						goto IL_184;
					}
				}
				assetName = "AB_UI_NKM_UI_GAUNTLET_ELLIPSE_ASYNCMAYCH";
				msg = NKCUtilString.GET_STRING_GAUNTLET_ASYNC_GAME;
				bValue = true;
			}
			else
			{
				assetName = "AB_UI_NKM_UI_GAUNTLET_ELLIPSE_RANK";
				msg = NKCUtilString.GET_STRING_GAUNTLET_RANK_GAME;
				bValue = true;
			}
			IL_184:
			this.m_imgModeBG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_GAUNTLET_SPRITE", assetName, false);
			NKCUtil.SetLabelTextColor(this.m_lbMode, NKCUtil.GetColor("#FFFFFFFF"));
			NKCUtil.SetLabelText(this.m_lbMode, msg);
			NKCUtil.SetGameobjectActive(this.m_lbAddScore, bValue);
			this.m_lbLevel.text = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, cPvpSingleHistory.TargetUserLevel.ToString());
			this.m_lbUserNickName.text = NKCUtilString.GetUserNickname(cPvpSingleHistory.TargetNickName, true);
			DateTime dateTime = new DateTime(cPvpSingleHistory.RegdateTick);
			dateTime = dateTime.ToLocalTime();
			this.m_lbDate.text = string.Format(NKCUtilString.GET_STRING_DATE_FOUR_PARAM, new object[]
			{
				dateTime.Month,
				dateTime.Day,
				dateTime.Hour,
				dateTime.Minute.ToString("#00")
			});
			if (cPvpSingleHistory.GainScore > 0)
			{
				this.m_lbAddScore.text = "+" + cPvpSingleHistory.GainScore.ToString();
				this.m_lbAddScore.color = NKCUtil.GetColor("#FFDF5D");
				return;
			}
			if (cPvpSingleHistory.GainScore < 0)
			{
				this.m_lbAddScore.text = cPvpSingleHistory.GainScore.ToString();
				this.m_lbAddScore.color = NKCUtil.GetColor("#FF4747");
				return;
			}
			this.m_lbAddScore.text = cPvpSingleHistory.GainScore.ToString();
			this.m_lbAddScore.color = NKCUtil.GetColor("#C1C1C1");
		}

		// Token: 0x040070DE RID: 28894
		private NKCPopupGauntletBRSlot.OnClick dOnClick;

		// Token: 0x040070DF RID: 28895
		public Text m_lbGameResult;

		// Token: 0x040070E0 RID: 28896
		public Image m_imgGameResult;

		// Token: 0x040070E1 RID: 28897
		public Image m_imgModeBG;

		// Token: 0x040070E2 RID: 28898
		public Text m_lbMode;

		// Token: 0x040070E3 RID: 28899
		public Text m_lbLevel;

		// Token: 0x040070E4 RID: 28900
		public Text m_lbUserNickName;

		// Token: 0x040070E5 RID: 28901
		public Text m_lbDate;

		// Token: 0x040070E6 RID: 28902
		public Text m_lbAddScore;

		// Token: 0x040070E7 RID: 28903
		public NKCUIComToggle m_ctglBRSlot;

		// Token: 0x040070E8 RID: 28904
		private int m_Index;

		// Token: 0x040070E9 RID: 28905
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x020018F7 RID: 6391
		// (Invoke) Token: 0x0600B74A RID: 46922
		public delegate void OnClick(int index);
	}
}
