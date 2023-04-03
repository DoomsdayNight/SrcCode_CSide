using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using ClientPacket.Common;
using ClientPacket.Warfare;
using Cs.Logging;
using Cs.Math;
using NKC.PacketHandler;
using NKC.Publisher;
using NKC.Trim;
using NKC.UI;
using NKC.UI.Component;
using NKC.UI.Result;
using NKM;
using NKM.Guild;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Base;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020006EE RID: 1774
	public class NKCUtil
	{
		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06003E6D RID: 15981 RVA: 0x0014389E File Offset: 0x00141A9E
		// (set) Token: 0x06003E6E RID: 15982 RVA: 0x001438A5 File Offset: 0x00141AA5
		public static string PatchVersion
		{
			get
			{
				return NKCUtil.m_PatchVersion;
			}
			set
			{
				NKCUtil.m_PatchVersion = value;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06003E6F RID: 15983 RVA: 0x001438AD File Offset: 0x00141AAD
		// (set) Token: 0x06003E70 RID: 15984 RVA: 0x001438B4 File Offset: 0x00141AB4
		public static string PatchVersionEA
		{
			get
			{
				return NKCUtil.m_PatchVersionEA;
			}
			set
			{
				NKCUtil.m_PatchVersionEA = value;
			}
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x001438BC File Offset: 0x00141ABC
		public static string GetExtraDownloadPath()
		{
			if (Application.isEditor)
			{
				return Environment.CurrentDirectory.Replace("\\", "/") + "/ExtraAsset/";
			}
			if (NKCDefineManager.DEFINE_PC_EXTRA_DOWNLOAD_IN_EXE_FOLDER())
			{
				return Application.dataPath + "/../ExtraAsset/";
			}
			return Application.persistentDataPath + "/ExtraAsset/";
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x00143918 File Offset: 0x00141B18
		public static bool CheckFinalCaptionEnabled()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.NPC_SUBTITLE, 0, 0))
			{
				return false;
			}
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			return gameOptionData != null && gameOptionData.UseNpcSubtitle;
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x0014394B File Offset: 0x00141B4B
		public static void WindowMsgBox(string msg)
		{
			NativeWinAlert.MessageBox(NativeWinAlert.GetWindowHandle(), msg, "Log", 16U);
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x00143960 File Offset: 0x00141B60
		public static void SetGameObjectPos(GameObject go, float x, float y, float z)
		{
			Vector3 position = new Vector3(x, y, z);
			go.transform.position = position;
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x00143984 File Offset: 0x00141B84
		public static void SetGameObjectLocalPos(GameObject go, float x, float y, float z)
		{
			Vector3 localPosition = new Vector3(x, y, z);
			go.transform.localPosition = localPosition;
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x001439A8 File Offset: 0x00141BA8
		public static void SetRectTransformLocalRotate(RectTransform rt, float x, float y, float z)
		{
			Quaternion localRotation = rt.localRotation;
			Vector3 eulerAngles = localRotation.eulerAngles;
			eulerAngles.Set(x, y, z);
			localRotation.eulerAngles = eulerAngles;
			rt.localRotation = localRotation;
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x001439E0 File Offset: 0x00141BE0
		public static void SetGameObjectLocalScale(GameObject go, float x = -1f, float y = -1f, float z = -1f)
		{
			Vector3 localScale = go.transform.localScale;
			if (x != -1f)
			{
				localScale.x = x;
			}
			if (y != -1f)
			{
				localScale.y = y;
			}
			if (z != -1f)
			{
				localScale.z = z;
			}
			go.transform.localScale = localScale;
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x00143A38 File Offset: 0x00141C38
		public static void SetGameObjectLocalScale(RectTransform rt, float x, float y, float z)
		{
			Vector3 localScale = new Vector3(x, y, z);
			rt.localScale = localScale;
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x00143A58 File Offset: 0x00141C58
		public static void SetGameObjectLocalScaleRel(GameObject go, float x, float y, float z)
		{
			Vector3 localScale = go.transform.localScale;
			localScale.Set(localScale.x + x, localScale.y + y, localScale.z + z);
			go.transform.localScale = localScale;
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x00143A9C File Offset: 0x00141C9C
		public static void SetGameObjectLocalScaleRel(GameObject go, float fFactor)
		{
			Vector3 localScale = go.transform.localScale;
			localScale.Set(localScale.x * fFactor, localScale.y * fFactor, localScale.z);
			go.transform.localScale = localScale;
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x00143AE0 File Offset: 0x00141CE0
		public static void SetRectTranformSizeDelta(RectTransform rt, float x, float y)
		{
			Vector2 sizeDelta = new Vector2(x, y);
			rt.sizeDelta = sizeDelta;
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x00143B00 File Offset: 0x00141D00
		public static void SetSpriteRendererSortOrder(SpriteRenderer[] aSpriteRenderer, Material cMaterial = null, float fOffsetZ = 0f)
		{
			for (int i = 0; i < aSpriteRenderer.Length; i++)
			{
				GameObject gameObject = aSpriteRenderer[i].gameObject;
				if (cMaterial != null)
				{
					aSpriteRenderer[i].material = cMaterial;
				}
			}
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x00143B38 File Offset: 0x00141D38
		public static void SetParticleSystemRendererSortOrder(ParticleSystemRenderer[] aParticleSystemRenderer, bool bEnable = true)
		{
			for (int i = 0; i < aParticleSystemRenderer.Length; i++)
			{
				Vector3 position = aParticleSystemRenderer[i].gameObject.transform.position;
				if (!bEnable)
				{
					aParticleSystemRenderer[i].enabled = false;
				}
				else
				{
					aParticleSystemRenderer[i].enabled = true;
				}
			}
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x00143B80 File Offset: 0x00141D80
		public static void SetScrollSize(Text scrollText, RectTransform scrollTextRect, float scrollYOrg)
		{
			if (scrollYOrg < scrollText.preferredHeight)
			{
				Vector2 sizeDelta = scrollTextRect.sizeDelta;
				sizeDelta.y = scrollText.preferredHeight;
				scrollTextRect.sizeDelta = sizeDelta;
			}
			else
			{
				Vector2 sizeDelta2 = scrollTextRect.sizeDelta;
				sizeDelta2.y = scrollYOrg;
				scrollTextRect.sizeDelta = sizeDelta2;
			}
			Vector2 anchoredPosition = scrollTextRect.anchoredPosition;
			anchoredPosition.y = 0f;
			scrollTextRect.anchoredPosition = anchoredPosition;
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x00143BE4 File Offset: 0x00141DE4
		public static string LoadFileString(string fileName, bool bUsePersistentDataPath = false)
		{
			string path;
			if (bUsePersistentDataPath)
			{
				path = Application.persistentDataPath + "/" + fileName;
			}
			else
			{
				path = Application.dataPath + "/" + fileName;
			}
			if (File.Exists(path))
			{
				FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
				StreamReader streamReader = new StreamReader(fileStream);
				string result = streamReader.ReadLine();
				streamReader.Close();
				fileStream.Close();
				return result;
			}
			return "";
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x00143C48 File Offset: 0x00141E48
		public static string LoadFileFullString(string fileName, bool bUsePersistentDataPath = false)
		{
			string path;
			if (bUsePersistentDataPath)
			{
				path = Application.persistentDataPath + "/" + fileName;
			}
			else
			{
				path = Application.dataPath + "/" + fileName;
			}
			if (File.Exists(path))
			{
				FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
				TextReader textReader = new StreamReader(fileStream);
				StringBuilder builder = NKMString.GetBuilder();
				string text = textReader.ReadLine();
				while (text != null)
				{
					builder.Append(text);
					text = textReader.ReadLine();
					if (text != null)
					{
						builder.Append(Environment.NewLine);
					}
				}
				textReader.Close();
				fileStream.Close();
				return builder.ToString();
			}
			return "";
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x00143CE4 File Offset: 0x00141EE4
		public static void SaveFileString(string fileName, string text, bool bUsePersistentDataPath = false, bool bForceWriteReadOnlyFile = false)
		{
			string text2;
			if (bUsePersistentDataPath)
			{
				text2 = Application.persistentDataPath + "/" + fileName;
			}
			else
			{
				text2 = Application.dataPath + "/" + fileName;
			}
			FileStream fileStream;
			if (!File.Exists(text2))
			{
				fileStream = new FileStream(text2, FileMode.Create, FileAccess.Write, FileShare.Read);
			}
			else
			{
				if (bForceWriteReadOnlyFile)
				{
					FileInfo fileInfo = new FileInfo(text2);
					if (fileInfo.IsReadOnly)
					{
						fileInfo.Attributes = FileAttributes.Normal;
					}
				}
				fileStream = File.Open(text2, FileMode.Truncate, FileAccess.Write, FileShare.Read);
			}
			StreamWriter streamWriter = new StreamWriter(fileStream);
			streamWriter.WriteLine(text);
			streamWriter.Close();
			fileStream.Close();
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x00143D6C File Offset: 0x00141F6C
		public static void SaveFileBinary(string fileName, byte[] bytes)
		{
			string path = Application.persistentDataPath + "/" + fileName;
			FileStream fileStream;
			if (!File.Exists(path))
			{
				fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read);
			}
			else
			{
				fileStream = File.Open(path, FileMode.Truncate, FileAccess.Write, FileShare.Read);
			}
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			binaryWriter.Write(bytes, 0, bytes.Length);
			binaryWriter.Close();
			fileStream.Close();
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x00143DC4 File Offset: 0x00141FC4
		public static void LoadFileBinary(string fileName, out byte[] bytes)
		{
			string path = Application.persistentDataPath + "/" + fileName;
			if (File.Exists(path))
			{
				FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
				BinaryReader binaryReader = new BinaryReader(fileStream);
				bytes = binaryReader.ReadBytes((int)fileStream.Length);
				binaryReader.Close();
				fileStream.Close();
				return;
			}
			bytes = null;
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x00143E1C File Offset: 0x0014201C
		public static int GetStarCntByUnitGrade(NKMUnitTempletBase templetBase)
		{
			int result = 1;
			if (templetBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_N)
			{
				result = 1;
			}
			else if (templetBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_R)
			{
				result = 1;
			}
			else if (templetBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SR)
			{
				result = 2;
			}
			else if (templetBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SSR)
			{
				result = 3;
			}
			return result;
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x00143E60 File Offset: 0x00142060
		public static void SetStarRank(List<GameObject> lstStar, NKMUnitData unitData)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			switch (unitTempletBase.m_NKM_UNIT_TYPE)
			{
			case NKM_UNIT_TYPE.NUT_SHIP:
				NKCUtil.SetStarRank(lstStar, unitData.GetStarGrade(unitTempletBase), 6);
				return;
			case NKM_UNIT_TYPE.NUT_OPERATOR:
				NKCUtil.SetStarRank(lstStar, -1, -1);
				return;
			}
			NKCUtil.SetStarRank(lstStar, unitData.GetStarGrade(unitTempletBase), unitTempletBase.m_StarGradeMax);
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x00143EC4 File Offset: 0x001420C4
		public static void SetStarRank(List<GameObject> lstStar, int starRank, int maxStarRank)
		{
			for (int i = 0; i < lstStar.Count; i++)
			{
				if (!(lstStar[i] == null))
				{
					Transform transform = lstStar[i].transform.Find("ON");
					Transform transform2 = lstStar[i].transform.Find("OFF");
					if (transform != null && transform2 != null)
					{
						transform.gameObject.SetActive(i < starRank);
						transform2.gameObject.SetActive(i >= starRank && i < maxStarRank);
						lstStar[i].gameObject.SetActive(i < maxStarRank);
					}
					else
					{
						lstStar[i].SetActive(i < starRank);
					}
				}
			}
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x00143F88 File Offset: 0x00142188
		public static void SetSkillUnlockStarRank(List<GameObject> lstStar, NKMUnitSkillTemplet skillTemplet, int unitStarGradeMax)
		{
			int num = unitStarGradeMax - 3;
			NKCUtil.SetStarRank(lstStar, NKMUnitSkillManager.GetUnlockReqUpgradeFromSkillId(skillTemplet.m_ID) + num, unitStarGradeMax);
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x00143FAD File Offset: 0x001421AD
		public static Color GetUITextColor(bool bActive = true)
		{
			if (bActive)
			{
				return new Color(0.345f, 0.141f, 0.09f);
			}
			return new Color(0.133f, 0.133f, 0.133f);
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x00143FDB File Offset: 0x001421DB
		public static Color GetButtonUIColor(bool Active = true)
		{
			if (Active)
			{
				return new Color(0.345098f, 0.1568628f, 0.09019608f);
			}
			return new Color(0.13f, 0.13f, 0.1333333f);
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x0014400C File Offset: 0x0014220C
		public static Color GetBonusColor(float bonus)
		{
			if (bonus > 0f)
			{
				return new Color(0.30588236f, 0.7607843f, 0.9529412f);
			}
			if (bonus < 0f)
			{
				return new Color(1f, 0f, 0f);
			}
			return Color.white;
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x00144058 File Offset: 0x00142258
		public static void SetGameobjectActive(GameObject targetObj, bool bValue)
		{
			if (targetObj != null && targetObj.activeSelf != bValue)
			{
				targetObj.SetActive(bValue);
			}
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x00144073 File Offset: 0x00142273
		public static void SetGameobjectActive(Transform targetTransform, bool bValue)
		{
			if (targetTransform != null && targetTransform.gameObject.activeSelf != bValue)
			{
				targetTransform.gameObject.SetActive(bValue);
			}
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x00144098 File Offset: 0x00142298
		public static void SetGameobjectActive(MonoBehaviour targetMono, bool bValue)
		{
			if (targetMono != null && targetMono.gameObject.activeSelf != bValue)
			{
				targetMono.gameObject.SetActive(bValue);
			}
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x001440BD File Offset: 0x001422BD
		public static void SetCanvasGroupAlpha(CanvasGroup canvasGroup, float fValue)
		{
			if (canvasGroup != null)
			{
				canvasGroup.alpha = fValue;
			}
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x001440CF File Offset: 0x001422CF
		public static void SetLabelText(TextMeshProUGUI label, string msg)
		{
			if (label != null)
			{
				label.text = msg;
				label.SetLayoutDirty();
			}
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x001440E7 File Offset: 0x001422E7
		public static void SetLabelText(Text label, string msg)
		{
			if (label != null)
			{
				label.text = msg;
				label.SetLayoutDirty();
			}
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x001440FF File Offset: 0x001422FF
		public static void SetLabelText(TMP_Text label, string msg)
		{
			if (label != null)
			{
				label.text = msg;
				label.SetLayoutDirty();
			}
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x00144117 File Offset: 0x00142317
		public static void SetLabelTextColor(Text label, Color col)
		{
			if (label != null)
			{
				label.color = col;
			}
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x00144129 File Offset: 0x00142329
		public static void SetLabelTextColor(TMP_Text label, Color col)
		{
			if (label != null)
			{
				label.color = col;
			}
		}

		// Token: 0x06003E94 RID: 16020 RVA: 0x0014413B File Offset: 0x0014233B
		public static void SetLabelText(TextMeshProUGUI label, string msg, params object[] args)
		{
			if (label != null)
			{
				label.text = string.Format(msg, args);
				label.SetLayoutDirty();
			}
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x00144159 File Offset: 0x00142359
		public static void SetLabelText(Text label, string msg, params object[] args)
		{
			if (label != null)
			{
				label.text = string.Format(msg, args);
			}
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x00144174 File Offset: 0x00142374
		public static void SetLabelWidthScale(ref Text label)
		{
			if (label == null)
			{
				return;
			}
			RectTransform component = label.GetComponent<RectTransform>();
			float width = component.GetWidth();
			Font font = label.font;
			float num = 0f;
			foreach (char ch in label.text)
			{
				CharacterInfo characterInfo;
				font.GetCharacterInfo(ch, out characterInfo, label.fontSize, label.fontStyle);
				num += (float)characterInfo.advance;
			}
			if (width >= num)
			{
				component.localScale = new Vector3(component.localScale.y, component.localScale.y, component.localScale.y);
				return;
			}
			float num2 = width / num;
			component.localScale = new Vector3(component.localScale.y * num2, component.localScale.y, component.localScale.y);
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x0014425C File Offset: 0x0014245C
		public static string RemoveLabelCharText(string str, string removeChar)
		{
			if (str == null)
			{
				return "";
			}
			return str.Replace(removeChar, "");
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x00144274 File Offset: 0x00142474
		public static string LabelLongTextCut(Text label)
		{
			if (label == null)
			{
				return "";
			}
			string text = label.text.Replace("\n", " ");
			float num = 0f;
			float width = label.GetComponent<RectTransform>().GetWidth();
			int fontSize = label.fontSize;
			Font font = label.font;
			FontStyle fontStyle = label.fontStyle;
			int num2 = 0;
			foreach (char ch in text)
			{
				CharacterInfo characterInfo;
				if (!font.GetCharacterInfo(ch, out characterInfo, fontSize, fontStyle))
				{
					font.RequestCharactersInTexture(ch.ToString(), fontSize, fontStyle);
					if (!font.GetCharacterInfo(ch, out characterInfo, fontSize, fontStyle))
					{
						UnityEngine.Debug.LogWarning("Text CharInfo is null: " + ch.ToString());
					}
				}
				num += (float)characterInfo.advance;
				if (num >= width)
				{
					int num3 = 3;
					int num4 = num2 - num3;
					if (num4 > 0)
					{
						return text.Remove(num4) + "...";
					}
				}
				num2++;
			}
			return text;
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x0014437B File Offset: 0x0014257B
		public static void SetImageSprite(Image image, Sprite sp, bool bDisableIfSpriteNull = false)
		{
			if (image != null)
			{
				image.sprite = sp;
			}
			if (bDisableIfSpriteNull)
			{
				NKCUtil.SetGameobjectActive(image, sp != null);
			}
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x0014439D File Offset: 0x0014259D
		public static void SetImageColor(Image image, Color color)
		{
			if (image != null)
			{
				image.color = color;
			}
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x001443AF File Offset: 0x001425AF
		public static void SetImageFillAmount(Image image, float value)
		{
			if (image != null)
			{
				image.fillAmount = value;
			}
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x001443C1 File Offset: 0x001425C1
		public static void SetImageMaterial(Image image, Material mat)
		{
			if (image != null)
			{
				image.material = mat;
			}
		}

		// Token: 0x06003E9D RID: 16029 RVA: 0x001443D3 File Offset: 0x001425D3
		public static void SetButtonClickDelegate(NKCUIComStateButton button, UnityAction call)
		{
			if (button == null)
			{
				return;
			}
			button.PointerClick.RemoveAllListeners();
			button.PointerClick.AddListener(call);
		}

		// Token: 0x06003E9E RID: 16030 RVA: 0x001443F6 File Offset: 0x001425F6
		public static void SetButtonPointerDownDelegate(NKCUIComButton button, UnityAction<PointerEventData> call)
		{
			if (button == null)
			{
				return;
			}
			button.PointerDown.RemoveAllListeners();
			button.PointerDown.AddListener(call);
		}

		// Token: 0x06003E9F RID: 16031 RVA: 0x00144419 File Offset: 0x00142619
		public static void SetButtonPointerDownDelegate(NKCUIComStateButton button, UnityAction<PointerEventData> call)
		{
			if (button == null)
			{
				return;
			}
			button.PointerDown.RemoveAllListeners();
			button.PointerDown.AddListener(call);
		}

		// Token: 0x06003EA0 RID: 16032 RVA: 0x0014443C File Offset: 0x0014263C
		public static void SetButtonClickDelegate(NKCUIComStateButton button, UnityAction<int> call)
		{
			if (button == null)
			{
				return;
			}
			if (button.PointerClickWithData == null)
			{
				button.PointerClickWithData = new NKCUnityEventInt();
			}
			button.PointerClickWithData.RemoveAllListeners();
			button.PointerClickWithData.AddListener(call);
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x00144472 File Offset: 0x00142672
		public static void SetButtonClickDelegate(NKCUIComButton button, UnityAction call)
		{
			if (button == null)
			{
				return;
			}
			button.PointerClick.RemoveAllListeners();
			button.PointerClick.AddListener(call);
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x00144495 File Offset: 0x00142695
		public static void SetButtonClickDelegate(NKCUIComToggle toggle, UnityAction<bool> call)
		{
			NKCUtil.SetToggleValueChangedDelegate(toggle, call);
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x0014449E File Offset: 0x0014269E
		public static void SetToggleValueChangedDelegate(NKCUIComToggle toggle, UnityAction<bool> call)
		{
			if (toggle == null)
			{
				return;
			}
			toggle.OnValueChanged.RemoveAllListeners();
			toggle.OnValueChanged.AddListener(call);
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x001444C1 File Offset: 0x001426C1
		public static void SetSliderValueChangedDelegate(Slider slider, UnityAction<float> call)
		{
			if (slider == null)
			{
				return;
			}
			slider.onValueChanged.RemoveAllListeners();
			slider.onValueChanged.AddListener(call);
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x001444E4 File Offset: 0x001426E4
		public static void SetEventTriggerDelegate(EventTrigger evtTrigger, UnityAction call)
		{
			NKCUtil.SetEventTriggerDelegate(evtTrigger, delegate(BaseEventData e)
			{
				call();
			}, EventTriggerType.PointerClick, true);
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x00144514 File Offset: 0x00142714
		public static void SetEventTriggerDelegate(EventTrigger evtTrigger, UnityAction<BaseEventData> call, EventTriggerType type = EventTriggerType.PointerClick, bool bInit = true)
		{
			if (evtTrigger == null || call == null)
			{
				return;
			}
			if (bInit)
			{
				evtTrigger.triggers.Clear();
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = type;
			entry.callback.AddListener(call);
			evtTrigger.triggers.Add(entry);
		}

		// Token: 0x06003EA7 RID: 16039 RVA: 0x00144561 File Offset: 0x00142761
		public static void SetSliderValue(Slider slider, float value)
		{
			if (slider != null)
			{
				slider.value = value;
			}
		}

		// Token: 0x06003EA8 RID: 16040 RVA: 0x00144573 File Offset: 0x00142773
		public static void SetSliderMinMax(Slider slider, float min, float max)
		{
			if (slider != null)
			{
				slider.minValue = min;
				slider.maxValue = max;
			}
		}

		// Token: 0x06003EA9 RID: 16041 RVA: 0x0014458C File Offset: 0x0014278C
		public static void SetScrollHotKey(LoopScrollRect loopScroll, NKCUIBase uiRoot = null)
		{
			NKCUIComLoopScrollHotkey.AddHotkey(loopScroll, uiRoot);
		}

		// Token: 0x06003EAA RID: 16042 RVA: 0x00144595 File Offset: 0x00142795
		public static void SetScrollHotKey(ScrollRect sr, NKCUIBase uiRoot = null)
		{
			NKCUIComScrollRectHotkey.AddHotkey(sr, uiRoot);
		}

		// Token: 0x06003EAB RID: 16043 RVA: 0x001445A0 File Offset: 0x001427A0
		public static Color GetColorForGrade(NKMSkinTemplet.SKIN_GRADE skinGrade)
		{
			switch (skinGrade)
			{
			case NKMSkinTemplet.SKIN_GRADE.SG_VARIATION:
				return new Color(0.6705883f, 0.6705883f, 0.6705883f, 1f);
			case NKMSkinTemplet.SKIN_GRADE.SG_NORMAL:
				return new Color(0f, 0.5647059f, 1f, 1f);
			case NKMSkinTemplet.SKIN_GRADE.SG_RARE:
				return new Color(0.7529412f, 0f, 1f, 1f);
			case NKMSkinTemplet.SKIN_GRADE.SG_PREMIUM:
				return new Color(1f, 0.7529412f, 0.003921569f, 1f);
			case NKMSkinTemplet.SKIN_GRADE.SG_SPECIAL:
			{
				Color result;
				ColorUtility.TryParseHtmlString("#B0C9FC", out result);
				return result;
			}
			default:
				return new Color(1f, 1f, 1f, 1f);
			}
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x00144659 File Offset: 0x00142859
		public static string GetStringForGrade(NKMSkinTemplet.SKIN_GRADE skinGrade)
		{
			switch (skinGrade)
			{
			case NKMSkinTemplet.SKIN_GRADE.SG_VARIATION:
				return NKCUtilString.GET_STRING_SKIN_GRADE_VARIATION;
			case NKMSkinTemplet.SKIN_GRADE.SG_RARE:
				return NKCUtilString.GET_STRING_SKIN_GRADE_RARE;
			case NKMSkinTemplet.SKIN_GRADE.SG_PREMIUM:
				return NKCUtilString.GET_STRING_SKIN_GRADE_PREMIUM;
			case NKMSkinTemplet.SKIN_GRADE.SG_SPECIAL:
				return NKCUtilString.GET_STRING_SKIN_GRADE_SPECIAL;
			}
			return NKCUtilString.GET_STRING_SKIN_GRADE_NORMAL;
		}

		// Token: 0x06003EAD RID: 16045 RVA: 0x00144694 File Offset: 0x00142894
		public static Color GetColorForUnitGrade(NKM_UNIT_GRADE eNKM_UNIT_GRADE)
		{
			if (eNKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_N)
			{
				return new Color(0.6705883f, 0.6705883f, 0.6705883f, 1f);
			}
			if (eNKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_R)
			{
				return new Color(0f, 0.5647059f, 1f, 1f);
			}
			if (eNKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SR)
			{
				return new Color(0.7529412f, 0f, 1f, 1f);
			}
			if (eNKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SSR)
			{
				return new Color(1f, 0.7529412f, 0.003921569f, 1f);
			}
			return new Color(1f, 1f, 1f, 1f);
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x00144734 File Offset: 0x00142934
		public static Color GetColorForItemGrade(NKM_ITEM_GRADE eNKM_ITEM_GRADE)
		{
			if (eNKM_ITEM_GRADE == NKM_ITEM_GRADE.NIG_N)
			{
				return new Color(0.6705883f, 0.6705883f, 0.6705883f, 1f);
			}
			if (eNKM_ITEM_GRADE == NKM_ITEM_GRADE.NIG_R)
			{
				return new Color(0f, 0.5647059f, 1f, 1f);
			}
			if (eNKM_ITEM_GRADE == NKM_ITEM_GRADE.NIG_SR)
			{
				return new Color(0.7529412f, 0f, 1f, 1f);
			}
			if (eNKM_ITEM_GRADE == NKM_ITEM_GRADE.NIG_SSR)
			{
				return new Color(1f, 0.7529412f, 0.003921569f, 1f);
			}
			return new Color(1f, 1f, 1f, 1f);
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x001447D1 File Offset: 0x001429D1
		public static string GetColorTagForUnitGrade(NKM_UNIT_GRADE eNKM_UNIT_GRADE)
		{
			return "<color=#" + NKCUtil.GetColorCodeForUnitGrade(eNKM_UNIT_GRADE) + ">";
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x001447E8 File Offset: 0x001429E8
		public static string GetColorCodeForUnitGrade(NKM_UNIT_GRADE eNKM_UNIT_GRADE)
		{
			if (eNKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_N)
			{
				return "CFCFCFFF";
			}
			if (eNKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_R)
			{
				return "008FFFFF";
			}
			if (eNKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SR)
			{
				return "FF00FEFF";
			}
			if (eNKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SSR)
			{
				return "FFF000FF";
			}
			return "FFFFFFFF";
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x00144818 File Offset: 0x00142A18
		public static HashSet<long> GetEquipsBeingUsed(Dictionary<long, NKMUnitData> _dicUnit, NKMInventoryData cNKMInventoryData)
		{
			HashSet<long> hashSet = new HashSet<long>();
			foreach (KeyValuePair<long, NKMUnitData> keyValuePair in _dicUnit)
			{
				NKMUnitData value = keyValuePair.Value;
				for (int i = 0; i < 4; i++)
				{
					long equipUid = value.GetEquipUid((ITEM_EQUIP_POSITION)i);
					if (equipUid > 0L)
					{
						NKMEquipItemData itemEquip = cNKMInventoryData.GetItemEquip(equipUid);
						if (itemEquip != null)
						{
							hashSet.Add(itemEquip.m_ItemUid);
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x001448AC File Offset: 0x00142AAC
		public static string GetEquipTypeIconIMGName(NKMEquipTemplet equipTemplet)
		{
			ITEM_EQUIP_POSITION itemEquipPosition = equipTemplet.m_ItemEquipPosition;
			NKM_UNIT_STYLE_TYPE equipUnitStyleType = equipTemplet.m_EquipUnitStyleType;
			string text = "";
			switch (equipUnitStyleType)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_COUNTER:
				switch (itemEquipPosition)
				{
				case ITEM_EQUIP_POSITION.IEP_WEAPON:
					return text + "AB_UI_ITEM_EQUIP_SLOT_ITEM_TYPE_COUNTER_WEAPON";
				case ITEM_EQUIP_POSITION.IEP_DEFENCE:
					return text + "AB_UI_ITEM_EQUIP_SLOT_ITEM_TYPE_COUNTER_DEFENCE";
				case ITEM_EQUIP_POSITION.IEP_ACC:
				case ITEM_EQUIP_POSITION.IEP_ACC2:
					return text + "AB_UI_ITEM_EQUIP_SLOT_ITEM_TYPE_COUNTER_ACC";
				case ITEM_EQUIP_POSITION.IEP_ENCHANT:
					return text + "AB_UI_ITEM_EQUIP_SLOT_ITEM_TYPE_ENCHANT";
				}
				return "";
			case NKM_UNIT_STYLE_TYPE.NUST_SOLDIER:
				switch (itemEquipPosition)
				{
				case ITEM_EQUIP_POSITION.IEP_WEAPON:
					return text + "AB_UI_ITEM_EQUIP_SLOT_ITEM_TYPE_SOLDIER_WEAPON";
				case ITEM_EQUIP_POSITION.IEP_DEFENCE:
					return text + "AB_UI_ITEM_EQUIP_SLOT_ITEM_TYPE_SOLDIER_DEFENCE";
				case ITEM_EQUIP_POSITION.IEP_ACC:
				case ITEM_EQUIP_POSITION.IEP_ACC2:
					return text + "AB_UI_ITEM_EQUIP_SLOT_ITEM_TYPE_SOLDIER_ACC";
				case ITEM_EQUIP_POSITION.IEP_ENCHANT:
					return text + "AB_UI_ITEM_EQUIP_SLOT_ITEM_TYPE_ENCHANT";
				}
				return "";
			case NKM_UNIT_STYLE_TYPE.NUST_MECHANIC:
				switch (itemEquipPosition)
				{
				case ITEM_EQUIP_POSITION.IEP_WEAPON:
					return text + "AB_UI_ITEM_EQUIP_SLOT_ITEM_TYPE_MECHANIC_WEAPON";
				case ITEM_EQUIP_POSITION.IEP_DEFENCE:
					return text + "AB_UI_ITEM_EQUIP_SLOT_ITEM_TYPE_MECHANIC_DEFENCE";
				case ITEM_EQUIP_POSITION.IEP_ACC:
				case ITEM_EQUIP_POSITION.IEP_ACC2:
					return text + "AB_UI_ITEM_EQUIP_SLOT_ITEM_TYPE_MECHANIC_ACC";
				case ITEM_EQUIP_POSITION.IEP_ENCHANT:
					return text + "AB_UI_ITEM_EQUIP_SLOT_ITEM_TYPE_ENCHANT";
				}
				return "";
			default:
				if (equipUnitStyleType != NKM_UNIT_STYLE_TYPE.NUST_ENCHANT)
				{
					return "";
				}
				text += "AB_UI_ITEM_EQUIP_SLOT_ITEM_TYPE_ENCHANT";
				break;
			}
			return text;
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x00144A34 File Offset: 0x00142C34
		public static Color GetColor(string hexRGB)
		{
			Color white = Color.white;
			ColorUtility.TryParseHtmlString(hexRGB, out white);
			return white;
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x00144A54 File Offset: 0x00142C54
		public static Sprite GetButtonSprite(NKCUtil.ButtonColor type)
		{
			string bundleName = "AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX_SPRITE";
			string text;
			switch (type)
			{
			case NKCUtil.ButtonColor.BC_GRAY:
				text = "NKM_UI_POPUP_BUTTON_02";
				break;
			case NKCUtil.ButtonColor.BC_YELLOW:
				text = "NKM_UI_POPUP_BUTTON_01";
				break;
			case NKCUtil.ButtonColor.BC_BLUE:
				text = "NKM_UI_POPUP_BLUEBUTTON";
				break;
			case NKCUtil.ButtonColor.BC_RED:
				text = "NKM_UI_POPUP_BUTTON_03";
				break;
			case NKCUtil.ButtonColor.BC_LOGIN_BLUE:
				text = "AB_UI_LOGIN_BUTTON1";
				bundleName = "AB_UI_LOGIN_SPRITE";
				break;
			case NKCUtil.ButtonColor.BC_LOGIN_YELLOW:
				text = "AB_UI_LOGIN_BUTTON2";
				bundleName = "AB_UI_LOGIN_SPRITE";
				break;
			case NKCUtil.ButtonColor.BC_COMMON_ENABLE:
				text = "NKM_UI_COMMON_BTN_02";
				break;
			case NKCUtil.ButtonColor.BC_COMMON_DISABLE:
				text = "NKM_UI_COMMON_BTN_01";
				break;
			default:
				UnityEngine.Debug.LogError("unknown GetButtonSprite type");
				return null;
			}
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(bundleName, text, false);
			if (orLoadAssetResource == null)
			{
				UnityEngine.Debug.LogError(string.Format("UnitSprite {0}(From ButtonColor_TYPE {1}) not found", text, type.ToString()));
			}
			return orLoadAssetResource;
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x00144B1C File Offset: 0x00142D1C
		public static Sprite GetSpriteCommonIConStar(int starLv)
		{
			if (starLv < 1 || starLv > 6)
			{
				return null;
			}
			string bundleName = "AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX_SPRITE";
			string text;
			switch (starLv)
			{
			case 1:
				text = "NKM_UI_COMMON_ICON_STAR_1";
				break;
			case 2:
				text = "NKM_UI_COMMON_ICON_STAR_2";
				break;
			case 3:
				text = "NKM_UI_COMMON_ICON_STAR_3";
				break;
			case 4:
				text = "NKM_UI_COMMON_ICON_STAR_4";
				break;
			case 5:
				text = "NKM_UI_COMMON_ICON_STAR_5";
				break;
			case 6:
				text = "NKM_UI_COMMON_ICON_STAR_6";
				break;
			default:
				UnityEngine.Debug.LogError("unknown GetButtonSprite type");
				return null;
			}
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(bundleName, text, false);
			if (orLoadAssetResource == null)
			{
				UnityEngine.Debug.LogError(string.Format("UnitSprite {0}(From GetSpriteCommonIConStar {1}) not found", text, starLv));
			}
			return orLoadAssetResource;
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x00144BC4 File Offset: 0x00142DC4
		public static Sprite GetSpriteUnitGrade(NKM_UNIT_GRADE grade)
		{
			string assetName;
			switch (grade)
			{
			default:
				assetName = "NKM_UI_COMMON_RANK_N";
				break;
			case NKM_UNIT_GRADE.NUG_R:
				assetName = "NKM_UI_COMMON_RANK_R";
				break;
			case NKM_UNIT_GRADE.NUG_SR:
				assetName = "NKM_UI_COMMON_RANK_SR";
				break;
			case NKM_UNIT_GRADE.NUG_SSR:
				assetName = "NKM_UI_COMMON_RANK_SSR";
				break;
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COMMON_ICON", assetName, false);
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x00144C11 File Offset: 0x00142E11
		public static Sprite GetSpriteBattleConditionICon(NKMBattleConditionTemplet templet)
		{
			if (templet != null)
			{
				return NKCUtil.GetSpriteBattleConditionICon(templet.BattleCondInfoIcon);
			}
			return null;
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x00144C23 File Offset: 0x00142E23
		public static Sprite GetSpriteBattleConditionICon(string name)
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COMMON_BC", name, false);
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x00144C34 File Offset: 0x00142E34
		public static Sprite GetSpriteFierceBattleBackgroud(bool bNightMareMode = false)
		{
			string assetName = bNightMareMode ? "NKM_UI_FIERCE_BATTLE_BG_2" : "NKM_UI_FIERCE_BATTLE_BG";
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_fierce_battle_bg", assetName, false);
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x00144C5D File Offset: 0x00142E5D
		public static Sprite GetSpriteEquipSetOptionIcon(NKMItemEquipSetOptionTemplet templet)
		{
			if (templet != null)
			{
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COMMON_EQUIP_SET_ICON", templet.m_EquipSetIcon, false);
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COMMON_EQUIP_SET_ICON", "ICON_SET_NONE", false);
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x00144C84 File Offset: 0x00142E84
		public static Sprite GetSpriteOperatorBG(NKM_UNIT_GRADE grade)
		{
			string bundleName = "ab_ui_nkm_ui_operator_deck_sprite";
			string assetName;
			switch (grade)
			{
			case NKM_UNIT_GRADE.NUG_N:
				assetName = "NKM_UI_OPERATOR_DECK_SLOT_RARE_N";
				break;
			case NKM_UNIT_GRADE.NUG_R:
				assetName = "NKM_UI_OPERATOR_DECK_SLOT_RARE_R";
				break;
			case NKM_UNIT_GRADE.NUG_SR:
				assetName = "NKM_UI_OPERATOR_DECK_SLOT_RARE_SR";
				break;
			case NKM_UNIT_GRADE.NUG_SSR:
				assetName = "NKM_UI_OPERATOR_DECK_SLOT_RARE_SSR";
				break;
			default:
				UnityEngine.Debug.LogError("unknown GetButtonSprite type");
				return null;
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>(bundleName, assetName, false);
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x00144CEC File Offset: 0x00142EEC
		public static Color GetSkillTypeColor(NKM_SKILL_TYPE type)
		{
			Color black = Color.black;
			switch (type)
			{
			case NKM_SKILL_TYPE.NST_PASSIVE:
				ColorUtility.TryParseHtmlString("#FED427", out black);
				break;
			case NKM_SKILL_TYPE.NST_ATTACK:
			case NKM_SKILL_TYPE.NST_SKILL:
				ColorUtility.TryParseHtmlString("#4EC2F2", out black);
				break;
			case NKM_SKILL_TYPE.NST_HYPER:
			case NKM_SKILL_TYPE.NST_SHIP_ACTIVE:
				ColorUtility.TryParseHtmlString("#D600D4", out black);
				break;
			case NKM_SKILL_TYPE.NST_LEADER:
				ColorUtility.TryParseHtmlString("#FED427", out black);
				break;
			default:
				UnityEngine.Debug.LogError("Unknown skill type");
				break;
			}
			return black;
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x00144D68 File Offset: 0x00142F68
		public static Sprite GetSkillIconSprite(NKMUnitSkillTemplet unitskillTemplet)
		{
			if (unitskillTemplet == null)
			{
				return null;
			}
			string unitSkillIcon = unitskillTemplet.m_UnitSkillIcon;
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_UNIT_SKILL_ICON", unitSkillIcon, false);
			if (orLoadAssetResource == null)
			{
				UnityEngine.Debug.LogError(string.Format("SkillSprite {0}(From UnitSkillTemplet StrID {1}) not found", unitSkillIcon, unitskillTemplet.m_strID));
			}
			return orLoadAssetResource;
		}

		// Token: 0x06003EBE RID: 16062 RVA: 0x00144DAC File Offset: 0x00142FAC
		public static Sprite GetSkillIconSprite(NKMOperatorSkillTemplet skillTemplet)
		{
			if (skillTemplet == null)
			{
				return null;
			}
			string operSkillIcon = skillTemplet.m_OperSkillIcon;
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_TACTICAL_COMMAND_ICON", operSkillIcon, false);
			if (orLoadAssetResource == null)
			{
				UnityEngine.Debug.LogError(string.Format("SkillSprite {0}(From UnitSkillTemplet StrID {1}) not found", operSkillIcon, skillTemplet.m_OperSkillNameStrID));
			}
			return orLoadAssetResource;
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x00144DF0 File Offset: 0x00142FF0
		public static Sprite GetSkillIconSprite(NKMShipSkillTemplet shipSkillTemplet)
		{
			if (shipSkillTemplet == null)
			{
				return null;
			}
			string shipSkillIcon = shipSkillTemplet.m_ShipSkillIcon;
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_SHIP_SKILL_ICON", shipSkillIcon, false);
			if (orLoadAssetResource == null)
			{
				UnityEngine.Debug.LogError(string.Format("SkillSprite {0}(From ShipSkillTemplet StrID {1}) not found", shipSkillIcon, shipSkillTemplet.m_ShipSkillStrID));
			}
			return orLoadAssetResource;
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x00144E34 File Offset: 0x00143034
		public static Sprite GetMoveTypeImg(bool bAirUnit)
		{
			if (bAirUnit)
			{
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COMMON_ICON", "NKM_UI_COMMON_UNIT_BATTLE_TYPE_ICON_AIR", false);
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COMMON_ICON", "NKM_UI_COMMON_UNIT_BATTLE_TYPE_ICON_GROUND", false);
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x00144E5C File Offset: 0x0014305C
		public static Sprite GetMissionThumbnailSprite(NKMMissionTemplet missionTemplet)
		{
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(missionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				return null;
			}
			if (missionTabTemplet.m_MissionType != NKM_MISSION_TYPE.GROWTH_COMPLETE)
			{
				return NKCUtil.GetMissionThumbnailSprite(missionTemplet.m_MissionTabId);
			}
			if (NKMMissionManager.GetMissionTemplet(missionTemplet.m_MissionRequire) != null)
			{
				return NKCUtil.GetMissionThumbnailSprite(NKMMissionManager.GetMissionTemplet(missionTemplet.m_MissionRequire).m_MissionTabId);
			}
			return null;
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x00144EB4 File Offset: 0x001430B4
		public static Sprite GetMissionThumbnailSprite(int tabID)
		{
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(tabID);
			if (missionTabTemplet == null)
			{
				return null;
			}
			string bundleName = "AB_UI_NKM_UI_MISSION_TEXTURE";
			if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION)
			{
				bundleName = "ui_mission_guide_texture";
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>(bundleName, missionTabTemplet.m_SlotBannerName, false);
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x00144EF0 File Offset: 0x001430F0
		public static Sprite GetGrowthMissionHamburgerIconSprite(NKMMissionTemplet missionTemplet)
		{
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(missionTemplet.m_MissionTabId);
			if (missionTabTemplet != null)
			{
				if (missionTabTemplet.m_MissionType != NKM_MISSION_TYPE.GROWTH_COMPLETE)
				{
					return NKCUtil.GetGrowthMissionHamburgerIconSprite(missionTemplet.m_MissionTabId);
				}
				if (NKMMissionManager.GetMissionTemplet(missionTemplet.m_MissionRequire) != null)
				{
					return NKCUtil.GetGrowthMissionHamburgerIconSprite(NKMMissionManager.GetMissionTemplet(missionTemplet.m_MissionRequire).m_MissionTabId);
				}
			}
			return null;
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x00144F48 File Offset: 0x00143148
		public static Sprite GetGrowthMissionHamburgerIconSprite(int tabID)
		{
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(tabID);
			if (missionTabTemplet == null)
			{
				return null;
			}
			string arg = missionTabTemplet.m_MissionTabIconName.Substring(missionTabTemplet.m_MissionTabIconName.Length - 2);
			string assetName = "";
			if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.GROWTH)
			{
				assetName = string.Format("NKM_UI_COMMON_ICON_GROWTH_{0}", arg);
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX_SPRITE", assetName, false);
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x00144FA4 File Offset: 0x001431A4
		public static Sprite GetCompanyBuffIconSprite(NKMCompanyBuffData buff)
		{
			NKMCompanyBuffTemplet nkmcompanyBuffTemplet = NKMTempletContainer<NKMCompanyBuffTemplet>.Find(buff.Id);
			string iconName;
			if (nkmcompanyBuffTemplet != null && !string.IsNullOrEmpty(nkmcompanyBuffTemplet.m_CompanyBuffIcon))
			{
				iconName = nkmcompanyBuffTemplet.m_CompanyBuffIcon;
			}
			else
			{
				iconName = "EVENTBUFF_SAMPLE";
			}
			return NKCUtil.GetCompanyBuffIconSprite(iconName);
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x00144FE8 File Offset: 0x001431E8
		public static Sprite GetCompanyBuffIconSprite(string iconName)
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COMMON_EVENTBUFF_ICON", iconName, false);
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x00144FF8 File Offset: 0x001431F8
		public static Sprite GetShopSprite(string spritePath)
		{
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_SHOP_SKIN_SPRITE", spritePath, false);
			if (orLoadAssetResource != null)
			{
				return orLoadAssetResource;
			}
			return null;
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x0014501E File Offset: 0x0014321E
		public static void SetShopReddotImage(ShopReddotType reddotType, GameObject objReddotRoot, GameObject objRed, GameObject objYellow)
		{
			if (objReddotRoot == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(objReddotRoot, reddotType > ShopReddotType.NONE);
			NKCUtil.SetGameobjectActive(objRed, reddotType == ShopReddotType.REDDOT_PURCHASED);
			NKCUtil.SetGameobjectActive(objYellow, reddotType == ShopReddotType.REDDOT_CHECKED);
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x00145048 File Offset: 0x00143248
		public static void SetShopReddotLabel(ShopReddotType reddotType, Text lbReddot, int reddotCount)
		{
			if (lbReddot == null)
			{
				return;
			}
			if (reddotCount <= 0)
			{
				NKCUtil.SetLabelText(lbReddot, "");
				return;
			}
			if (reddotCount > 99)
			{
				NKCUtil.SetLabelText(lbReddot, "...");
				return;
			}
			NKCUtil.SetLabelText(lbReddot, reddotCount.ToString());
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x00145084 File Offset: 0x00143284
		public static string GetCompanyBuffDesc(int buffID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			NKMCompanyBuffTemplet nkmcompanyBuffTemplet = NKMTempletContainer<NKMCompanyBuffTemplet>.Find(buffID);
			if (nkmcompanyBuffTemplet != null)
			{
				int num = 0;
				while (num < nkmcompanyBuffTemplet.m_CompanyBuffInfoList.Count && nkmcompanyBuffTemplet.m_CompanyBuffInfoList[num].m_CompanyBuffType != NKMConst.Buff.BuffType.NONE)
				{
					if (num != 0)
					{
						stringBuilder.Append("\n");
					}
					switch (nkmcompanyBuffTemplet.m_CompanyBuffInfoList[num].m_CompanyBuffType)
					{
					case NKMConst.Buff.BuffType.NONE:
						return string.Empty;
					case NKMConst.Buff.BuffType.WARFARE_DUNGEON_REWARD_CREDIT:
						stringBuilder.Append(NKCUtilString.GET_EVENT_BUFF_TYPE_RWDBOUNS_CREDIT);
						break;
					case NKMConst.Buff.BuffType.WARFARE_DUNGEON_REWARD_EXP_UNIT:
						stringBuilder.Append(NKCUtilString.GET_EVENT_BUFF_TYPE_RWDBOUNS_EXP_UNIT);
						break;
					case NKMConst.Buff.BuffType.WARFARE_DUNGEON_REWARD_EXP_COMPANY:
						stringBuilder.Append(NKCUtilString.GET_EVENT_BUFF_TYPE_RWDBOUNS_EXP_PLAYER);
						break;
					case NKMConst.Buff.BuffType.WARFARE_ETERNIUM_DISCOUNT:
						stringBuilder.Append(NKCUtilString.GET_EVENT_BUFF_TYPE_WARFARE_ETNM_DISCOUNT);
						break;
					case NKMConst.Buff.BuffType.WARFARE_DUNGEON_ETERNIUM_DISCOUNT:
						stringBuilder.Append(NKCUtilString.GET_EVENT_BUFF_TYPE_WARFARE_DUNGEON_ETNM_DISCOUNT);
						break;
					case NKMConst.Buff.BuffType.PVP_POINT_CHARGE:
						stringBuilder.Append(NKCUtilString.GET_EVENT_BUFF_TYPE_PVP_POINT_CHARGE);
						break;
					case NKMConst.Buff.BuffType.ALL_PVP_POINT_REWARD:
						stringBuilder.Append(NKCUtilString.GET_EVENT_BUFF_TYPE_PVP_POINT_REWARD);
						break;
					case NKMConst.Buff.BuffType.WORLDMAP_MISSION_COMPLETE_RATIO_BONUS:
						stringBuilder.Append(NKCUtilString.GET_EVENT_BUFF_TYPE_CITY_MISSION_WMMR_S_UP);
						break;
					case NKMConst.Buff.BuffType.BASE_PERSONNAL_NEGOTIATION_CREDIT_DISCOUNT:
						stringBuilder.Append(NKCUtilString.GET_EVENT_BUFF_TYPE_NEGOTIATION_CREDIT_DISCOUNT);
						break;
					case NKMConst.Buff.BuffType.BASE_FACTORY_CRAFT_CREDIT_DISCOUNT:
						stringBuilder.Append(NKCUtilString.GET_EVENT_BUFF_TYPE_FACTORY_CRAFT_CREDIT_DISCOUNT);
						break;
					case NKMConst.Buff.BuffType.BASE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT:
						stringBuilder.Append(NKCUtilString.GET_EVENT_BUFF_TYPE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT);
						break;
					}
					stringBuilder.Append(" ");
					if (nkmcompanyBuffTemplet.m_CompanyBuffInfoList[num].m_CompanyBuffType == NKMConst.Buff.BuffType.WARFARE_ETERNIUM_DISCOUNT)
					{
						stringBuilder.AppendFormat("-{0}%", nkmcompanyBuffTemplet.m_CompanyBuffInfoList[num].m_CompanyBuffRatio);
					}
					else
					{
						stringBuilder.AppendFormat("+{0}%", nkmcompanyBuffTemplet.m_CompanyBuffInfoList[num].m_CompanyBuffRatio);
					}
					num++;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x00145240 File Offset: 0x00143440
		public static Sprite GetBounsTypeIcon(RewardTuningType type, bool big = true)
		{
			string text = "";
			switch (type)
			{
			case RewardTuningType.UserExp:
				if (big)
				{
					text = "NKM_UI_OPERATION_BONUSTYPE_ICON_USER_EXP";
				}
				else
				{
					text = "NKM_UI_OPERATION_BONUSTYPE_ICON_USER_EXP_TEXT";
				}
				break;
			case RewardTuningType.UnitExp:
				if (big)
				{
					text = "NKM_UI_OPERATION_BONUSTYPE_ICON_UNIT_EXP";
				}
				else
				{
					text = "NKM_UI_OPERATION_BONUSTYPE_ICON_UNIT_EXP_TEXT";
				}
				break;
			case RewardTuningType.Credit:
				if (big)
				{
					text = "NKM_UI_OPERATION_BONUSTYPE_ICON_CREDIT";
				}
				else
				{
					text = "NKM_UI_OPERATION_BONUSTYPE_ICON_CREDIT_TEXT";
				}
				break;
			case RewardTuningType.Eternium:
				if (big)
				{
					text = "NKM_UI_OPERATION_BONUSTYPE_ICON_ETERNIUM";
				}
				else
				{
					text = "NKM_UI_OPERATION_BONUSTYPE_ICON_ETERNIUM_TEXT";
				}
				break;
			}
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_common_operation_bonustype", text, false);
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x001452D0 File Offset: 0x001434D0
		public static float TrackValue(TRACKING_DATA_TYPE type, float beginValue, float endValue, float currentTime, float totalTime)
		{
			float progress = NKMTrackingFloat.TrackRatio(type, currentTime, totalTime, 3f);
			return NKCUtil.Lerp(beginValue, endValue, progress);
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x001452F4 File Offset: 0x001434F4
		public static Vector2 TrackValue(TRACKING_DATA_TYPE type, Vector2 beginValue, Vector2 endValue, float currentTime, float totalTime)
		{
			float progress = NKMTrackingFloat.TrackRatio(type, currentTime, totalTime, 3f);
			return NKCUtil.Lerp(beginValue, endValue, progress);
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x00145318 File Offset: 0x00143518
		public static Vector3 TrackValue(TRACKING_DATA_TYPE type, Vector3 beginValue, Vector3 endValue, float currentTime, float totalTime)
		{
			float progress = NKMTrackingFloat.TrackRatio(type, currentTime, totalTime, 3f);
			return NKCUtil.Lerp(beginValue, endValue, progress);
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x0014533C File Offset: 0x0014353C
		public static float Lerp(float beginValue, float endValue, float progress)
		{
			return beginValue + (endValue - beginValue) * progress;
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x00145345 File Offset: 0x00143545
		public static Vector2 Lerp(Vector2 beginValue, Vector2 endValue, float progress)
		{
			return beginValue + (endValue - beginValue) * progress;
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x0014535A File Offset: 0x0014355A
		public static Vector3 Lerp(Vector3 beginValue, Vector3 endValue, float progress)
		{
			return beginValue + (endValue - beginValue) * progress;
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x0014536F File Offset: 0x0014356F
		public static int GetItemStarCount(NKM_ITEM_GRADE grade)
		{
			switch (grade)
			{
			case NKM_ITEM_GRADE.NIG_N:
				return 2;
			case NKM_ITEM_GRADE.NIG_R:
				return 3;
			case NKM_ITEM_GRADE.NIG_SR:
				return 4;
			case NKM_ITEM_GRADE.NIG_SSR:
				return 5;
			default:
				return 1;
			}
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x00145394 File Offset: 0x00143594
		public static Sprite GetShipGradeSprite(NKM_UNIT_GRADE grade)
		{
			string text = "ab_ui_ship_slot_card_sprite";
			string assetName = "";
			switch (grade)
			{
			case NKM_UNIT_GRADE.NUG_N:
				assetName = "NKM_UI_SHIP_SELECT_LIST_SHIP_SLOT_N";
				break;
			case NKM_UNIT_GRADE.NUG_R:
				assetName = "NKM_UI_SHIP_SELECT_LIST_SHIP_SLOT_R";
				break;
			case NKM_UNIT_GRADE.NUG_SR:
				assetName = "NKM_UI_SHIP_SELECT_LIST_SHIP_SLOT_SR";
				break;
			case NKM_UNIT_GRADE.NUG_SSR:
				assetName = "NKM_UI_SHIP_SELECT_LIST_SHIP_SLOT_SSR";
				break;
			}
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(text, assetName, false);
			if (orLoadAssetResource == null)
			{
				UnityEngine.Debug.LogError(string.Format("Fail - Get Ship Grade Sprite, not found {0} - {1}", grade, text));
			}
			return orLoadAssetResource;
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x0014540A File Offset: 0x0014360A
		public static bool ProcessDeckErrorMsg(NKM_ERROR_CODE errorCode)
		{
			if (errorCode != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(errorCode, null, "");
				return false;
			}
			return true;
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x00145420 File Offset: 0x00143620
		public static void SetLayer(Transform trans, int layer)
		{
			if (trans == null)
			{
				return;
			}
			trans.gameObject.layer = layer;
			foreach (object obj in trans)
			{
				NKCUtil.SetLayer((Transform)obj, layer);
			}
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x00145488 File Offset: 0x00143688
		public static bool CheckExistRewardType(List<int> groupIds, NKM_REWARD_TYPE rewardType)
		{
			if (rewardType == NKM_REWARD_TYPE.RT_MISC && (groupIds.Contains(1031) || groupIds.Contains(1032) || groupIds.Contains(1033)))
			{
				return true;
			}
			for (int i = 0; i < groupIds.Count; i++)
			{
				NKMRewardGroupTemplet rewardGroup = NKMRewardManager.GetRewardGroup(groupIds[i]);
				if (rewardGroup != null)
				{
					for (int j = 0; j < rewardGroup.List.Count; j++)
					{
						if (rewardGroup.List[j].m_eRewardType == rewardType)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x00145510 File Offset: 0x00143710
		public static HashSet<int> GetRewardIDs(List<int> groupIds, NKM_REWARD_TYPE rewardType)
		{
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < groupIds.Count; i++)
			{
				NKMRewardGroupTemplet rewardGroup = NKMRewardManager.GetRewardGroup(groupIds[i]);
				if (rewardGroup != null)
				{
					for (int j = 0; j < rewardGroup.List.Count; j++)
					{
						if (rewardGroup.List[j].m_eRewardType == rewardType)
						{
							hashSet.Add(rewardGroup.List[j].m_RewardID);
						}
					}
				}
				if (rewardType == NKM_REWARD_TYPE.RT_MISC && (groupIds[i] == 1031 || groupIds[i] == 1032 || groupIds[i] == 1033))
				{
					hashSet.Add(groupIds[i]);
				}
			}
			return hashSet;
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x001455CC File Offset: 0x001437CC
		public static int GetMaxGradeInRewardGroups(List<int> groupIds, NKM_REWARD_TYPE rewardType)
		{
			int num = -1;
			int i = 0;
			while (i < groupIds.Count)
			{
				if (rewardType != NKM_REWARD_TYPE.RT_MISC)
				{
					goto IL_43;
				}
				if (groupIds[i] == 1031)
				{
					num = 0;
				}
				else if (groupIds[i] == 1032)
				{
					num = 1;
				}
				else
				{
					if (groupIds[i] != 1033)
					{
						goto IL_43;
					}
					num = 2;
				}
				IL_9E:
				i++;
				continue;
				IL_43:
				NKMRewardGroupTemplet rewardGroup = NKMRewardManager.GetRewardGroup(groupIds[i]);
				if (rewardGroup != null)
				{
					for (int j = 0; j < rewardGroup.List.Count; j++)
					{
						if (rewardGroup.List[j].m_eRewardType == rewardType)
						{
							int rewardGrade = NKCUtil.GetRewardGrade(rewardGroup.List[j].m_RewardID, rewardType);
							if (num < rewardGrade)
							{
								num = rewardGrade;
							}
						}
					}
					goto IL_9E;
				}
				goto IL_9E;
			}
			return num;
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x00145688 File Offset: 0x00143888
		public static int GetMaxGradeInRewardGroups(int rewardGroupID)
		{
			NKMRewardGroupTemplet rewardGroup = NKMRewardManager.GetRewardGroup(rewardGroupID);
			if (rewardGroup == null)
			{
				return -1;
			}
			int num = -1;
			for (int i = 0; i < rewardGroup.List.Count; i++)
			{
				int rewardGrade = NKCUtil.GetRewardGrade(rewardGroup.List[i].m_RewardID, rewardGroup.List[i].m_eRewardType);
				if (num < rewardGrade)
				{
					num = rewardGrade;
				}
			}
			return num;
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x001456E8 File Offset: 0x001438E8
		public static int GetRewardGrade(int rewardID, NKM_REWARD_TYPE rewardType)
		{
			int result = -1;
			switch (rewardType)
			{
			case NKM_REWARD_TYPE.RT_UNIT:
			case NKM_REWARD_TYPE.RT_SHIP:
				break;
			case NKM_REWARD_TYPE.RT_MISC:
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(rewardID);
				if (itemMiscTempletByID != null)
				{
					return (int)itemMiscTempletByID.m_NKM_ITEM_GRADE;
				}
				return result;
			}
			case NKM_REWARD_TYPE.RT_USER_EXP:
				goto IL_78;
			case NKM_REWARD_TYPE.RT_EQUIP:
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(rewardID);
				if (equipTemplet != null)
				{
					return (int)equipTemplet.m_NKM_ITEM_GRADE;
				}
				return result;
			}
			case NKM_REWARD_TYPE.RT_MOLD:
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(rewardID);
				if (itemMoldTempletByID != null)
				{
					return (int)itemMoldTempletByID.m_Grade;
				}
				return result;
			}
			default:
				if (rewardType != NKM_REWARD_TYPE.RT_OPERATOR)
				{
					goto IL_78;
				}
				break;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(rewardID);
			if (unitTempletBase != null)
			{
				return (int)unitTempletBase.m_NKM_UNIT_GRADE;
			}
			return result;
			IL_78:
			UnityEngine.Debug.LogError("not yet implemented type : " + rewardType.ToString());
			return 0;
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x0014578C File Offset: 0x0014398C
		public static void PlayStartCutscenAndStartGame(NKMGameData cNKMGameData)
		{
			bool flag = false;
			if (cNKMGameData != null && !cNKMGameData.m_bLocal)
			{
				bool flag2 = true;
				if (NKCScenManager.CurrentUserData() != null)
				{
					flag2 = NKCScenManager.CurrentUserData().m_UserOption.m_bPlayCutscene;
				}
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				int dungeonID = cNKMGameData.m_DungeonID;
				NKMStageTempletV2 stageTemplet;
				if (NKCPhaseManager.IsCurrentPhaseDungeon(dungeonID))
				{
					stageTemplet = NKCPhaseManager.GetStageTemplet();
				}
				else
				{
					stageTemplet = NKMDungeonManager.GetDungeonTempletBase(cNKMGameData.m_DungeonID).StageTemplet;
				}
				bool flag3 = false;
				if (cNKMGameData.GetGameType() == NKM_GAME_TYPE.NGT_WARFARE)
				{
					flag3 = NKCScenManager.CurrentUserData().m_UserOption.m_bAutoWarfare;
				}
				else if (cNKMGameData.GetGameType() == NKM_GAME_TYPE.NGT_DIVE)
				{
					flag3 = NKCScenManager.CurrentUserData().m_UserOption.m_bAutoDive;
				}
				bool isOnGoing = NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing();
				bool flag4 = false;
				if (cNKMGameData.GetGameType() == NKM_GAME_TYPE.NGT_TRIM)
				{
					if (NKCTrimManager.TrimModeState != null)
					{
						flag4 = NKCTrimManager.WillPlayTrimDungeonCutscene(NKCTrimManager.TrimModeState.trimId, dungeonID, NKCTrimManager.TrimModeState.trimLevel);
					}
				}
				else
				{
					flag4 = (dungeonID > 0 && !flag3 && (!myUserData.CheckStageCleared(stageTemplet) || (flag2 && !isOnGoing)));
				}
				if (flag4)
				{
					NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonID);
					if (dungeonTempletBase != null)
					{
						NKCCutScenTemplet cutScenTemple = NKCCutScenManager.GetCutScenTemple(dungeonTempletBase.m_CutScenStrIDBefore);
						if (cutScenTemple != null)
						{
							NKCScenManager.GetScenManager().Get_NKC_SCEN_CUTSCEN_DUNGEON().SetReservedOneCutscenType(cutScenTemple.m_CutScenStrID, delegate()
							{
								NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAME, true);
							}, dungeonTempletBase.m_DungeonStrID);
							NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_CUTSCENE_DUNGEON, true);
							flag = true;
						}
					}
				}
			}
			if (!flag)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAME, true);
			}
			if (cNKMGameData != null && cNKMGameData.m_WarfareID == 0 && cNKMGameData.m_DungeonID > 0)
			{
				NKMDungeonTempletBase dungeonTempletBase2 = NKMDungeonManager.GetDungeonTempletBase(cNKMGameData.m_DungeonID);
				if (dungeonTempletBase2 != null)
				{
					string key = string.Format("{0}_{1}", NKCScenManager.CurrentUserData().m_UserUID, dungeonTempletBase2.m_DungeonStrID);
					if (!PlayerPrefs.HasKey(key) && !NKCScenManager.CurrentUserData().CheckWarfareClear(dungeonTempletBase2.m_DungeonStrID))
					{
						PlayerPrefs.SetInt(key, 0);
					}
				}
			}
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x00145982 File Offset: 0x00143B82
		public static bool IsCanStartEterniumStage(NKMStageTempletV2 stageTemplet, bool bCallLackPopup = false)
		{
			return stageTemplet == null || NKCUtil.IsCanStartEterniumStage(stageTemplet.m_StageReqItemID, stageTemplet.m_StageReqItemCount, bCallLackPopup);
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x0014599C File Offset: 0x00143B9C
		public static bool IsCanStartEterniumStage(int reqItemID, int reqItemCount, bool bCallLackPopup = false)
		{
			if (reqItemID == 2)
			{
				NKCCompanyBuff.SetDiscountOfEterniumInEnteringDungeon(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref reqItemCount);
				if (!NKCScenManager.CurrentUserData().CheckPrice(reqItemCount, 2))
				{
					if (bCallLackPopup)
					{
						if (!NKCAdManager.IsAdRewardItem(2))
						{
							NKCShopManager.OpenItemLackPopup(2, reqItemCount);
						}
						else
						{
							NKCPopupItemLack.Instance.OpenItemLackAdRewardPopup(2, delegate
							{
								NKCShopManager.OpenItemLackPopup(2, reqItemCount);
							});
						}
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x00145A18 File Offset: 0x00143C18
		public static void ChangeEquip(long unitUID, ITEM_EQUIP_POSITION equipPos, NKCUISlotEquip.OnSelectedEquipSlot _OnClickEmptySlot = null, long selectedItemUID = 0L, bool bShowFierceUI = false)
		{
			NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(unitUID);
			if (unitFromUID == null)
			{
				return;
			}
			if (unitFromUID.IsSeized)
			{
				NKCPopupMessageManager.AddPopupMessage(NKM_ERROR_CODE.NEC_FAIL_UNIT_IS_SEIZED, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitFromUID.m_UnitID);
			if (unitTempletBase == null)
			{
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMUnitManager.IsUnitBusy(NKCScenManager.GetScenManager().GetMyUserData(), unitFromUID, true);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString(nkm_ERROR_CODE.ToString(), false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCUIInventory.EquipSelectListOptions options = new NKCUIInventory.EquipSelectListOptions(NKC_INVENTORY_OPEN_TYPE.NIOT_NORMAL, false, true);
			options.m_dOnClickEmptySlot = _OnClickEmptySlot;
			options.m_NKC_INVENTORY_OPEN_TYPE = NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_SELECT;
			options.lastSelectedItemUID = selectedItemUID;
			options.m_EquipListOptions.iTargetUnitID = unitFromUID.m_UnitID;
			options.bShowFierceUI = bShowFierceUI;
			options.lEquipOptionCachingByUnitUID = unitUID;
			NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(unitFromUID.m_UnitID);
			if (unitTempletBase2 != null)
			{
				options.strUpsideMenuName = string.Format(NKCUtilString.GET_STRING_CHOICE_ONE_PARAM, NKCUtilString.GetEquipPosSimpleStrByUnitStyle(unitTempletBase2.m_NKM_UNIT_STYLE_TYPE, equipPos));
			}
			options.setFilterOption = new HashSet<NKCEquipSortSystem.eFilterOption>
			{
				NKCEquipSortSystem.GetFilterOptionByEquipPosition(equipPos)
			};
			if (unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_COUNTER)
			{
				options.setFilterOption.Add(NKCEquipSortSystem.eFilterOption.Equip_Counter);
			}
			else if (unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_SOLDIER)
			{
				options.setFilterOption.Add(NKCEquipSortSystem.eFilterOption.Equip_Soldier);
			}
			else if (unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_MECHANIC)
			{
				options.setFilterOption.Add(NKCEquipSortSystem.eFilterOption.Equip_Mechanic);
			}
			if (equipPos == ITEM_EQUIP_POSITION.IEP_ACC || equipPos == ITEM_EQUIP_POSITION.IEP_ACC2)
			{
				options.setExcludeEquipUID = new HashSet<long>();
				if (unitFromUID.GetEquipUid(ITEM_EQUIP_POSITION.IEP_ACC) > 0L)
				{
					options.setExcludeEquipUID.Add(unitFromUID.GetEquipUid(ITEM_EQUIP_POSITION.IEP_ACC));
				}
				if (unitFromUID.GetEquipUid(ITEM_EQUIP_POSITION.IEP_ACC2) > 0L)
				{
					options.setExcludeEquipUID.Add(unitFromUID.GetEquipUid(ITEM_EQUIP_POSITION.IEP_ACC2));
				}
			}
			else if (unitFromUID.GetEquipUid(equipPos) > 0L)
			{
				options.setExcludeEquipUID = new HashSet<long>
				{
					unitFromUID.GetEquipUid(equipPos)
				};
			}
			options.equipChangeTargetPosition = equipPos;
			options.lstSortOption = new List<NKCEquipSortSystem.eSortOption>
			{
				NKCEquipSortSystem.eSortOption.Equipped_Last,
				NKCEquipSortSystem.eSortOption.Enhance_High,
				NKCEquipSortSystem.eSortOption.Tier_High,
				NKCEquipSortSystem.eSortOption.Rarity_High,
				NKCEquipSortSystem.eSortOption.UnitType_First,
				NKCEquipSortSystem.eSortOption.EquipType_FIrst,
				NKCEquipSortSystem.eSortOption.ID_First,
				NKCEquipSortSystem.eSortOption.UID_First
			};
			options.m_EquipListOptions.setExcludeFilterOption = new HashSet<NKCEquipSortSystem.eFilterOption>
			{
				NKCEquipSortSystem.eFilterOption.Equip_Enchant
			};
			options.strEmptyMessage = NKCUtilString.GET_STRING_NO_EXIST_EQUIP_TO_CHANGE;
			options.m_ButtonMenuType = NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_CHANGE;
			NKCUIInventory.Instance.Open(options, null, unitUID, NKCUIInventory.NKC_INVENTORY_TAB.NIT_NONE);
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x00145C7C File Offset: 0x00143E7C
		public static void ChangePresetEquip(long unitUId, int presetIndex, long equipUId, List<long> presetEquipUId, ITEM_EQUIP_POSITION equipPos, NKM_UNIT_STYLE_TYPE unitStyleType, bool bShowFierceUI = false, NKCUISlotEquip.OnSelectedEquipSlot _OnClickEmptySlot = null)
		{
			if (NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(unitUId) == null)
			{
				return;
			}
			NKCUIInventory.EquipSelectListOptions options = new NKCUIInventory.EquipSelectListOptions(NKC_INVENTORY_OPEN_TYPE.NIOT_NORMAL, false, true);
			options.m_dOnClickEmptySlot = _OnClickEmptySlot;
			options.m_NKC_INVENTORY_OPEN_TYPE = NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_SELECT;
			options.lastSelectedItemUID = equipUId;
			options.bShowFierceUI = bShowFierceUI;
			options.iPresetIndex = presetIndex;
			options.lEquipOptionCachingByUnitUID = unitUId;
			options.setFilterOption = new HashSet<NKCEquipSortSystem.eFilterOption>
			{
				NKCEquipSortSystem.GetFilterOptionByEquipPosition(equipPos)
			};
			options.presetUnitStyeType = unitStyleType;
			switch (unitStyleType)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_COUNTER:
				options.setFilterOption.Add(NKCEquipSortSystem.eFilterOption.Equip_Counter);
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SOLDIER:
				options.setFilterOption.Add(NKCEquipSortSystem.eFilterOption.Equip_Soldier);
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_MECHANIC:
				options.setFilterOption.Add(NKCEquipSortSystem.eFilterOption.Equip_Mechanic);
				break;
			}
			if (equipPos == ITEM_EQUIP_POSITION.IEP_ACC || equipPos == ITEM_EQUIP_POSITION.IEP_ACC2)
			{
				options.setExcludeEquipUID = new HashSet<long>();
				int num = 2;
				if (presetEquipUId.Count > num && presetEquipUId[num] > 0L)
				{
					options.setExcludeEquipUID.Add(presetEquipUId[num]);
				}
				num = 3;
				if (presetEquipUId.Count > num && presetEquipUId[num] > 0L)
				{
					options.setExcludeEquipUID.Add(presetEquipUId[num]);
				}
			}
			else if (presetEquipUId.Count > (int)equipPos && presetEquipUId[(int)equipPos] > 0L)
			{
				options.setExcludeEquipUID = new HashSet<long>
				{
					presetEquipUId[(int)equipPos]
				};
			}
			options.equipChangeTargetPosition = equipPos;
			options.lstSortOption = new List<NKCEquipSortSystem.eSortOption>
			{
				NKCEquipSortSystem.eSortOption.Equipped_Last,
				NKCEquipSortSystem.eSortOption.Enhance_High,
				NKCEquipSortSystem.eSortOption.Tier_High,
				NKCEquipSortSystem.eSortOption.Rarity_High,
				NKCEquipSortSystem.eSortOption.UnitType_First,
				NKCEquipSortSystem.eSortOption.EquipType_FIrst,
				NKCEquipSortSystem.eSortOption.ID_First,
				NKCEquipSortSystem.eSortOption.UID_First
			};
			options.m_EquipListOptions.setExcludeFilterOption = new HashSet<NKCEquipSortSystem.eFilterOption>
			{
				NKCEquipSortSystem.eFilterOption.Equip_Enchant
			};
			options.strEmptyMessage = NKCUtilString.GET_STRING_NO_EXIST_EQUIP_TO_CHANGE;
			options.m_ButtonMenuType = NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_PRESET_CHANGE;
			NKCUIInventory.Instance.Open(options, null, unitUId, NKCUIInventory.NKC_INVENTORY_TAB.NIT_NONE);
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x00145E70 File Offset: 0x00144070
		public static bool IsPrivateEquipAlreadyEquipped(IReadOnlyList<long> lstEquipResult)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null || lstEquipResult == null)
			{
				return false;
			}
			int num = 0;
			int count = lstEquipResult.Count;
			for (int i = 0; i < count; i++)
			{
				long itemUid = lstEquipResult[i];
				NKMEquipItemData itemEquip = nkmuserData.m_InventoryData.GetItemEquip(itemUid);
				if (itemEquip != null)
				{
					NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
					if (equipTemplet != null && equipTemplet.IsPrivateEquip())
					{
						num++;
					}
				}
			}
			return num > 1;
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x00145EE2 File Offset: 0x001440E2
		public static bool ProcessWFExpireTime()
		{
			if (NKCScenManager.GetScenManager().WarfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_EXPIRED), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
			}
			return true;
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x00145F0E File Offset: 0x0014410E
		public static bool ProcessDiveExpireTime()
		{
			if (NKCScenManager.GetScenManager().GetMyUserData().m_DiveGameData == null)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_DIVE_EXPIRED), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
			}
			return true;
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x00145F3C File Offset: 0x0014413C
		public static void SetDiveTargetEventID()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			bool flag = false;
			if (nkmuserData != null && nkmuserData.m_DiveGameData != null && nkmuserData.m_DiveGameData.Floor.Templet.IsEventDive)
			{
				int cityIDByEventData = nkmuserData.m_WorldmapData.GetCityIDByEventData(NKM_WORLDMAP_EVENT_TYPE.WET_DIVE, nkmuserData.m_DiveGameData.DiveUid);
				if (cityIDByEventData != -1)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE_READY().SetTargetEventID(cityIDByEventData, nkmuserData.m_DiveGameData.Floor.Templet.StageID);
					flag = true;
				}
			}
			if (!flag)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE_READY().SetTargetEventID(0, 0);
			}
		}

		// Token: 0x06003EE4 RID: 16100 RVA: 0x00145FCC File Offset: 0x001441CC
		public static int GetEquipCreatableCount(NKMMoldItemData cNKMMoldItemData, NKMInventoryData cNKMInventoryData)
		{
			if (cNKMMoldItemData == null || cNKMInventoryData == null)
			{
				return 0;
			}
			int num = 10;
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(cNKMMoldItemData.m_MoldID);
			if (itemMoldTempletByID != null)
			{
				if (!itemMoldTempletByID.m_bPermanent && (long)num > cNKMMoldItemData.m_Count)
				{
					num = (int)cNKMMoldItemData.m_Count;
				}
				for (int i = 0; i < itemMoldTempletByID.m_MaterialList.Count; i++)
				{
					NKMItemMoldMaterialData nkmitemMoldMaterialData = itemMoldTempletByID.m_MaterialList[i];
					if (nkmitemMoldMaterialData.m_MaterialType == NKM_REWARD_TYPE.RT_MISC)
					{
						long num2 = cNKMInventoryData.GetCountMiscItem(nkmitemMoldMaterialData.m_MaterialID) / (long)itemMoldTempletByID.m_MaterialList[i].m_MaterialValue;
						if ((long)num > num2)
						{
							num = (int)num2;
						}
					}
				}
				return num;
			}
			return 0;
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x0014606C File Offset: 0x0014426C
		public static Dictionary<string, string> ParseStringTable(string input)
		{
			char[] separator = new char[]
			{
				';'
			};
			char[] separator2 = new char[]
			{
				'='
			};
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in input.Split(separator, StringSplitOptions.RemoveEmptyEntries))
			{
				if (!string.IsNullOrEmpty(text))
				{
					string[] array2 = text.Split(separator2, StringSplitOptions.RemoveEmptyEntries);
					if (array2.Length != 2)
					{
						UnityEngine.Debug.LogError("Table parse error : 2 or more = in single statement");
					}
					else
					{
						string key = array2[0].Trim();
						string value = array2[1].Trim();
						dictionary.Add(key, value);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06003EE6 RID: 16102 RVA: 0x00146100 File Offset: 0x00144300
		public static int GetIntValue(Dictionary<string, string> dicParamTable, string key, int defaultValue = -1)
		{
			if (dicParamTable == null)
			{
				return defaultValue;
			}
			string text;
			if (!dicParamTable.TryGetValue(key, out text))
			{
				return defaultValue;
			}
			int result;
			if (!int.TryParse(key, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x0014612C File Offset: 0x0014432C
		public static Dictionary<int, string> ParseIntKeyTable(string input)
		{
			char[] separator = new char[]
			{
				';'
			};
			char[] separator2 = new char[]
			{
				','
			};
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			foreach (string text in input.Split(separator, StringSplitOptions.RemoveEmptyEntries))
			{
				if (!string.IsNullOrEmpty(text))
				{
					string[] array2 = text.Split(separator2, StringSplitOptions.RemoveEmptyEntries);
					int key;
					if (array2.Length != 2)
					{
						UnityEngine.Debug.LogError("Table parse error : 2 or more , in single statement");
					}
					else if (!int.TryParse(array2[0].Trim(), out key))
					{
						UnityEngine.Debug.LogError("Table parse error : Key parse failed");
					}
					else
					{
						string value = array2[1].Trim();
						dictionary.Add(key, value);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x001461D4 File Offset: 0x001443D4
		public static Sprite GetLeaderBoardPointIcon(LeaderBoardType boardType, int criteria = 0, LEAGUE_TIER_ICON tier = LEAGUE_TIER_ICON.LTI_NONE)
		{
			switch (boardType)
			{
			case LeaderBoardType.BT_ACHIEVE:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_INVEN_ICON_ITEM_MISC_SMALL", "AB_INVEN_ICON_ITEM_MISC_RESOURCE_ACHIEVE_POINT", false);
			case LeaderBoardType.BT_PVP_RANK:
				return NKCUtil.GetTierIcon(tier);
			case LeaderBoardType.BT_SHADOW:
			case LeaderBoardType.BT_TIMEATTACK:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_LEADER_BOARD_SPRITE", "AB_UI_NKM_UI_LEADER_BOARD_TIME_ICON", false);
			case LeaderBoardType.BT_FIERCE:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_LEADER_BOARD_SPRITE", "AB_UI_NKM_UI_LEADER_BOARD_FIERCE_BATTLE_SUPPORT_REWARD", false);
			case LeaderBoardType.BT_GUILD:
				if (criteria == 1)
				{
					return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_LEADER_BOARD_SPRITE", "AB_UI_NKM_UI_LEADER_BOARD_MENU_ICON_EXP", false);
				}
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_LEADER_BOARD_SPRITE", "AB_UI_NKM_UI_LEADER_BOARD_MENU_ICON_SHADOW", false);
			}
			return null;
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x0014626C File Offset: 0x0014446C
		public static Sprite GetRankIcon(int rank)
		{
			switch (rank)
			{
			case 1:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_LEADER_BOARD_SPRITE", "Rank_01", false);
			case 2:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_LEADER_BOARD_SPRITE", "Rank_02", false);
			case 3:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_LEADER_BOARD_SPRITE", "Rank_03", false);
			default:
				return null;
			}
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x001462C4 File Offset: 0x001444C4
		public static Sprite GetTierIcon(LEAGUE_TIER_ICON tier)
		{
			string assetName;
			switch (tier)
			{
			case LEAGUE_TIER_ICON.LTI_BRONZE:
				assetName = "AB_UI_NKM_UI_COMMON_GAUNTLET_TIER_BRONZE_SIMPLE";
				goto IL_78;
			case LEAGUE_TIER_ICON.LTI_SILVER:
				assetName = "AB_UI_NKM_UI_COMMON_GAUNTLET_TIER_SILVER_SIMPLE";
				goto IL_78;
			case LEAGUE_TIER_ICON.LTI_GOLD:
				assetName = "AB_UI_NKM_UI_COMMON_GAUNTLET_TIER_GOLD_SIMPLE";
				goto IL_78;
			case LEAGUE_TIER_ICON.LTI_PLATINUM:
				assetName = "AB_UI_NKM_UI_COMMON_GAUNTLET_TIER_PLATINUM_SIMPLE";
				goto IL_78;
			case LEAGUE_TIER_ICON.LTI_DIAMOND:
				assetName = "AB_UI_NKM_UI_COMMON_GAUNTLET_TIER_DIAMOND_SIMPLE";
				goto IL_78;
			case LEAGUE_TIER_ICON.LTI_MASTER:
				assetName = "AB_UI_NKM_UI_COMMON_GAUNTLET_TIER_MASTER_SIMPLE";
				goto IL_78;
			case LEAGUE_TIER_ICON.LTI_GRANDMASTER:
				assetName = "AB_UI_NKM_UI_COMMON_GAUNTLET_TIER_GRANDMASTER_SIMPLE";
				goto IL_78;
			case LEAGUE_TIER_ICON.LTI_CHALLENGER:
				assetName = "AB_UI_NKM_UI_COMMON_GAUNTLET_TIER_CHALLENGER_SIMPLE";
				goto IL_78;
			}
			assetName = "";
			IL_78:
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COMMON_GAUNTLET_TIER", assetName, false);
		}

		// Token: 0x06003EEB RID: 16107 RVA: 0x00146355 File Offset: 0x00144555
		public static int CalculateNormalizedIndex(int index, int maxCount)
		{
			if (index < 0)
			{
				index = maxCount + index % maxCount;
			}
			if (index >= maxCount)
			{
				index %= maxCount;
			}
			return index;
		}

		// Token: 0x06003EEC RID: 16108 RVA: 0x0014636C File Offset: 0x0014456C
		public static int FindPVPSeasonIDForRank(DateTime nowUTC)
		{
			int result = 0;
			foreach (KeyValuePair<int, NKMPvpRankSeasonTemplet> keyValuePair in NKCPVPManager.dicPvpRankSeasonTemplet)
			{
				if (keyValuePair.Value.CheckSeasonForRank(nowUTC))
				{
					result = keyValuePair.Key;
					break;
				}
			}
			return result;
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x001463D4 File Offset: 0x001445D4
		public static int FindPVPSeasonIDForAsync(DateTime nowUTC)
		{
			int result = 0;
			foreach (KeyValuePair<int, NKMPvpRankSeasonTemplet> keyValuePair in NKCPVPManager.dicAsyncPvpSeasonTemplet)
			{
				if (keyValuePair.Value.CheckSeasonForRank(nowUTC))
				{
					result = keyValuePair.Key;
					break;
				}
			}
			return result;
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x0014643C File Offset: 0x0014463C
		public static int FindPVPSeasonIDForLeague(DateTime nowUTC)
		{
			int result = 0;
			foreach (NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet in NKMLeaguePvpRankSeasonTemplet.Values)
			{
				if (nkmleaguePvpRankSeasonTemplet.CheckSeasonForRank(nowUTC))
				{
					result = nkmleaguePvpRankSeasonTemplet.Key;
					break;
				}
			}
			return result;
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x00146498 File Offset: 0x00144698
		public static int GetScoreBySeason(int currentSeasonID, int lastestPlaySeasonID, int score, NKM_GAME_TYPE gameType)
		{
			if (currentSeasonID != lastestPlaySeasonID)
			{
				score = NKCPVPManager.GetResetScore(currentSeasonID, score, gameType);
			}
			return score;
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x001464AC File Offset: 0x001446AC
		public static int CalcAddScore(LEAGUE_TYPE leagueType, int myScore, int targetScore)
		{
			int num = 25;
			if (leagueType - LEAGUE_TYPE.LEAGUE_TYPE_START <= 1)
			{
				return num;
			}
			if (leagueType != LEAGUE_TYPE.LEAGUE_TYPE_NORMAL)
			{
				return 0;
			}
			int num2 = num + (targetScore - myScore) / 12;
			if (num2 < 5)
			{
				num2 = 5;
			}
			else if (num2 > 45)
			{
				num2 = 45;
			}
			return num2;
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x001464E8 File Offset: 0x001446E8
		public static bool CheckInvalidStringName(string text)
		{
			if (text == null)
			{
				return false;
			}
			for (int i = 0; i < text.Length; i++)
			{
				if ((NKCStringTable.GetNationalCode() != NKM_NATIONAL_CODE.NNC_KOREA || text[i] < '가' || text[i] > '힣') && (text[i] < '0' || text[i] > '9') && (text[i] < 'A' || text[i] > 'Z') && (text[i] < 'a' || text[i] > 'z'))
				{
					char c = text[i];
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x00146580 File Offset: 0x00144780
		public static T GetNextEnum<T>(T enumValue) where T : Enum
		{
			T[] array = (T[])Enum.GetValues(typeof(T));
			int num = Array.IndexOf<T>(array, enumValue) + 1;
			if (array.Length != num)
			{
				return array[num];
			}
			return array[0];
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x001465C4 File Offset: 0x001447C4
		public static T GetPrevEnum<T>(T enumValue) where T : Enum
		{
			T[] array = (T[])Enum.GetValues(typeof(T));
			int num = Array.IndexOf<T>(array, enumValue) - 1;
			if (num >= 0)
			{
				return array[num];
			}
			return array[array.Length - 1];
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x00146608 File Offset: 0x00144808
		public static void ClearGauntletCacheData(NKCScenManager cNKCScenManager)
		{
			if (cNKCScenManager == null)
			{
				return;
			}
			NKC_SCEN_GAUNTLET_INTRO nkc_SCEN_GAUNTLET_INTRO = cNKCScenManager.Get_NKC_SCEN_GAUNTLET_INTRO();
			if (nkc_SCEN_GAUNTLET_INTRO != null)
			{
				nkc_SCEN_GAUNTLET_INTRO.ClearCacheData();
			}
			NKC_SCEN_GAUNTLET_LOBBY nkc_SCEN_GAUNTLET_LOBBY = cNKCScenManager.Get_NKC_SCEN_GAUNTLET_LOBBY();
			if (nkc_SCEN_GAUNTLET_LOBBY != null)
			{
				nkc_SCEN_GAUNTLET_LOBBY.ClearCacheData();
			}
			NKC_SCEN_GAUNTLET_MATCH nkc_SCEN_GAUNTLET_MATCH = cNKCScenManager.Get_NKC_SCEN_GAUNTLET_MATCH();
			if (nkc_SCEN_GAUNTLET_MATCH != null)
			{
				nkc_SCEN_GAUNTLET_MATCH.ClearCacheData();
			}
			if (NKCUIManager.NKCUIGauntletResult != null)
			{
				NKCUIManager.NKCUIGauntletResult.CloseInstance();
				NKCUIManager.NKCUIGauntletResult = null;
			}
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x0014666C File Offset: 0x0014486C
		public static int GetFinalPVPScore(PvpState cNKMPVPData, NKM_GAME_TYPE gameType)
		{
			if (cNKMPVPData == null)
			{
				return 0;
			}
			int num = NKCPVPManager.FindPvPSeasonID(gameType, NKCSynchronizedTime.GetServerUTCTime(0.0));
			if (cNKMPVPData.SeasonID != num)
			{
				return NKCPVPManager.GetResetScore(cNKMPVPData.SeasonID, cNKMPVPData.Score, gameType);
			}
			return cNKMPVPData.Score;
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x001466B5 File Offset: 0x001448B5
		public static bool IsPVPDemotionAlert(NKMPvpRankTemplet cNKMPvpRankTempletByScore, NKMPvpRankTemplet cNKMPvpRankTempletByTier, int pvpScore)
		{
			return cNKMPvpRankTempletByScore != null && cNKMPvpRankTempletByTier != null && (cNKMPvpRankTempletByScore.LeagueTier < cNKMPvpRankTempletByTier.LeagueTier && NKMPvpCommonConst.Instance.DEMOTION_SCORE <= cNKMPvpRankTempletByTier.LeaguePointReq - pvpScore);
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x001466E4 File Offset: 0x001448E4
		public static bool IsPVPDemotionAlert(NKMLeaguePvpRankTemplet cNKMPvpRankTempletByScore, NKMLeaguePvpRankTemplet cNKMPvpRankTempletByTier, int pvpScore)
		{
			return cNKMPvpRankTempletByScore != null && cNKMPvpRankTempletByTier != null && (cNKMPvpRankTempletByScore.LeagueTier < cNKMPvpRankTempletByTier.LeagueTier && NKMPvpCommonConst.Instance.DEMOTION_SCORE <= cNKMPvpRankTempletByTier.LeaguePointReq - pvpScore);
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x00146714 File Offset: 0x00144914
		public static bool IsPVPDemotionAlert(NKM_GAME_TYPE gameType, PvpState cNKMPVPData)
		{
			if (cNKMPVPData == null)
			{
				return false;
			}
			int finalPVPScore = NKCUtil.GetFinalPVPScore(cNKMPVPData, gameType);
			int num = NKCPVPManager.FindPvPSeasonID(gameType, NKCSynchronizedTime.GetServerUTCTime(0.0));
			if (gameType == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet = NKMLeaguePvpRankSeasonTemplet.Find(num);
				NKMLeaguePvpRankTemplet byScore = nkmleaguePvpRankSeasonTemplet.RankGroup.GetByScore(cNKMPVPData.Score);
				NKMLeaguePvpRankTemplet byTier = nkmleaguePvpRankSeasonTemplet.RankGroup.GetByTier(cNKMPVPData.LeagueTierID);
				return NKCUtil.IsPVPDemotionAlert(byScore, byTier, finalPVPScore);
			}
			NKMPvpRankTemplet pvpRankTempletByTier = NKCPVPManager.GetPvpRankTempletByTier(num, cNKMPVPData.LeagueTierID);
			return NKCUtil.IsPVPDemotionAlert(NKCPVPManager.GetPvpRankTempletByScore(num, finalPVPScore), pvpRankTempletByTier, finalPVPScore);
		}

		// Token: 0x06003EF9 RID: 16121 RVA: 0x001467A4 File Offset: 0x001449A4
		public static void OpenPurchasePopupNotInShop(int itemID, int shopID, int priceItemID)
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemID);
			if (itemMiscTempletByID != null)
			{
				ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(shopID);
				if (shopItemTemplet == null)
				{
					Log.Warn(string.Format("상점ID로 ShopItemTemplet을 찾을 수 없음. 상점ID : {0}", shopID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCUtil.cs", 2788);
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_INVALID_SHOP_ID), null, "");
					return;
				}
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				if (myUserData != null)
				{
					if (shopItemTemplet.m_QuantityLimit > 0 && shopItemTemplet.m_QuantityLimit <= myUserData.m_ShopData.GetPurchasedCount(shopItemTemplet))
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_SHOP_FAIL_PURCHASE_COUNT.ToString(), false), null, "");
						return;
					}
					int realPrice = myUserData.m_ShopData.GetRealPrice(shopItemTemplet, 1, false);
					NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_PURCHASE_POPUP_TITLE, string.Format(NKCUtilString.GET_STRING_PURCHASE_POPUP_DESC, itemMiscTempletByID.GetItemName()), priceItemID, realPrice, delegate()
					{
						NKCPacketSender.Send_NKMPacket_SHOP_FIX_SHOP_BUY_REQ(shopID, 1, null);
					}, null, false);
					return;
				}
			}
			else
			{
				Log.Warn(string.Format("비품 아이템을 찾을 수 없습니다. 비품 아이템 아이디 : {0}", itemID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCUtil.cs", 2794);
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_INVALID_MISC_ITEM_ID), null, "");
			}
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x001468F0 File Offset: 0x00144AF0
		public static float HalfToFloat(ushort us)
		{
			if (us == 0)
			{
				return 0f;
			}
			IConvertible convertible = new Half
			{
				value = us
			};
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			return convertible.ToSingle(invariantCulture);
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x00146928 File Offset: 0x00144B28
		public static void MakeUILoyaltyValue(int realBefore, int realGain, out int textBefore, out int textAfter, out int textGain)
		{
			textAfter = Math.Min((realBefore + realGain) / 100, 100);
			textBefore = realBefore / 100;
			textGain = textAfter - textBefore;
		}

		// Token: 0x06003EFC RID: 16124 RVA: 0x00146947 File Offset: 0x00144B47
		public static void SetBindFunction(NKCUIComStateButton btn, UnityAction bindFunc = null)
		{
			if (btn == null)
			{
				return;
			}
			btn.PointerClick.RemoveAllListeners();
			if (bindFunc != null)
			{
				btn.PointerClick.AddListener(bindFunc);
			}
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x0014696D File Offset: 0x00144B6D
		public static void SetBindFunction(NKCUIComButton btn, UnityAction bindFunc = null)
		{
			if (btn == null)
			{
				return;
			}
			btn.PointerClick.RemoveAllListeners();
			if (bindFunc != null)
			{
				btn.PointerClick.AddListener(bindFunc);
			}
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x00146993 File Offset: 0x00144B93
		public static void SetHotkey(NKCUIComStateButtonBase btn, HotkeyEventType hotkeyEvent, NKCUIBase uiBase = null, bool bUpDownEvent = false)
		{
			if (btn == null)
			{
				return;
			}
			btn.SetHotkey(hotkeyEvent, uiBase, bUpDownEvent);
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x001469A8 File Offset: 0x00144BA8
		public static void SetHotkey(NKCUIComButton btn, HotkeyEventType hotkeyEvent)
		{
			if (btn == null)
			{
				return;
			}
			btn.m_HotkeyEventType = hotkeyEvent;
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x001469BC File Offset: 0x00144BBC
		public static bool CalculateContentRectSize(LoopScrollRect loopScrollRect, GridLayoutGroup grid_layout_group, int minColumn, Vector2 cellSize, Vector2 spacing, bool recalcCellSize = false)
		{
			loopScrollRect.ResetContentSpacing();
			RectTransform component = loopScrollRect.GetComponent<RectTransform>();
			RectTransform viewRect = loopScrollRect.viewRect;
			float num = cellSize.x * (float)minColumn + spacing.x * (float)(minColumn - 1);
			if (viewRect.GetWidth() < num)
			{
				UnityEngine.Debug.Log(string.Format("Content Rect Size ... viewRect : {0}, minWidth : {1}", viewRect.GetWidth(), num));
				float num2 = (viewRect.GetWidth() - spacing.x * (float)(minColumn - 1)) / (float)minColumn;
				float num3 = num2 / cellSize.x;
				grid_layout_group.cellSize = new Vector2(num2, cellSize.y * num3);
			}
			else
			{
				grid_layout_group.cellSize = cellSize;
			}
			grid_layout_group.spacing = spacing;
			int num4 = (int)((viewRect.GetWidth() + spacing.x) / (cellSize.x + spacing.x));
			if (recalcCellSize)
			{
				cellSize.x = (viewRect.GetWidth() - (float)(num4 - 1) * spacing.x) / (float)num4;
				grid_layout_group.cellSize = cellSize;
			}
			bool result = loopScrollRect.ContentConstraintCount != num4;
			grid_layout_group.constraintCount = num4;
			loopScrollRect.ContentConstraintCount = num4;
			float num5 = cellSize.x * (float)num4 + spacing.x * (float)(num4 - 1);
			float num6 = (component.GetWidth() - num5) / 2f;
			grid_layout_group.padding.left = (int)num6;
			grid_layout_group.padding.right = (int)num6;
			UnityEngine.Debug.Log(string.Format("CellSize : {0}, rectContentWidth : {1}, scrollRectWidth : {2}, padding : {3}", new object[]
			{
				grid_layout_group.cellSize,
				viewRect.GetWidth(),
				component.GetWidth(),
				grid_layout_group.padding.left
			}));
			return result;
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x00146B64 File Offset: 0x00144D64
		public static bool CalculateContentRectSizeHorizontal(LoopScrollRect loopScrollRect, GridLayoutGroup grid_layout_group, int minRow, Vector2 cellSize, Vector2 spacing, bool recalcCellSize = false)
		{
			loopScrollRect.ResetContentSpacing();
			RectTransform component = loopScrollRect.GetComponent<RectTransform>();
			RectTransform viewRect = loopScrollRect.viewRect;
			float num = cellSize.y * (float)minRow + spacing.y * (float)(minRow - 1);
			if (viewRect.GetHeight() < num)
			{
				UnityEngine.Debug.Log(string.Format("Content Rect Size ... viewRect : {0}, minHeight : {1}", viewRect.GetHeight(), num));
				float num2 = (viewRect.GetHeight() - spacing.y * (float)(minRow - 1)) / (float)minRow;
				float num3 = num2 / cellSize.y;
				grid_layout_group.cellSize = new Vector2(cellSize.x * num3, num2);
			}
			else
			{
				grid_layout_group.cellSize = cellSize;
			}
			grid_layout_group.spacing = spacing;
			int num4 = (int)((viewRect.GetHeight() + spacing.y) / (cellSize.y + spacing.y));
			if (recalcCellSize)
			{
				cellSize.y = (viewRect.GetHeight() - (float)(num4 - 1) * spacing.y) / (float)num4;
				grid_layout_group.cellSize = cellSize;
			}
			bool result = loopScrollRect.ContentConstraintCount != num4;
			grid_layout_group.constraintCount = num4;
			loopScrollRect.ContentConstraintCount = num4;
			float num5 = cellSize.y * (float)num4 + spacing.y * (float)(num4 - 1);
			float num6 = (component.GetHeight() - num5) / 2f;
			grid_layout_group.padding.left = (int)num6;
			grid_layout_group.padding.right = (int)num6;
			UnityEngine.Debug.Log(string.Format("CellSize : {0}, rectContentHeight : {1}, scrollRectHeight : {2}, padding : {3}", new object[]
			{
				grid_layout_group.cellSize,
				viewRect.GetHeight(),
				component.GetHeight(),
				grid_layout_group.padding.left
			}));
			return result;
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x00146D0C File Offset: 0x00144F0C
		public static string TextSplitLine(string fullText, Text cUIText, float fForceRectWidth = 0f)
		{
			RectTransform component = cUIText.gameObject.GetComponent<RectTransform>();
			if (fForceRectWidth == 0f)
			{
				fForceRectWidth = component.rect.width;
			}
			int num = 0;
			bool bExistEmpty = true;
			for (int i = 0; i < fullText.Length; i++)
			{
				if (fullText[i] == ' ')
				{
					num++;
				}
			}
			if (num < 2)
			{
				bExistEmpty = false;
			}
			NKCUtil.newFullTextBuilder.Clear();
			NKCUtil.subTextBuilder.Clear();
			foreach (char c in fullText)
			{
				NKCUtil.subTextBuilder.Append(c);
				if (c == '\n')
				{
					NKCUtil.TextSplitLine(NKCUtil.subTextBuilder, cUIText, fForceRectWidth, NKCUtil.newFullTextBuilder, bExistEmpty);
					NKCUtil.subTextBuilder.Clear();
				}
			}
			NKCUtil.TextSplitLine(NKCUtil.subTextBuilder, cUIText, fForceRectWidth, NKCUtil.newFullTextBuilder, bExistEmpty);
			cUIText.text = "";
			return NKCUtil.newFullTextBuilder.ToString();
		}

		// Token: 0x06003F03 RID: 16131 RVA: 0x00146DFC File Offset: 0x00144FFC
		public static string TextSplitLine(string fullText, TextMeshProUGUI cUIText, float fForceRectWidth = 0f)
		{
			RectTransform component = cUIText.gameObject.GetComponent<RectTransform>();
			if (fForceRectWidth == 0f)
			{
				fForceRectWidth = component.rect.width;
			}
			int num = 0;
			bool bExistEmpty = true;
			for (int i = 0; i < fullText.Length; i++)
			{
				if (fullText[i] == ' ')
				{
					num++;
				}
			}
			if (num < 2)
			{
				bExistEmpty = false;
			}
			NKCUtil.newFullTextBuilder.Clear();
			NKCUtil.subTextBuilder.Clear();
			foreach (char c in fullText)
			{
				NKCUtil.subTextBuilder.Append(c);
				if (c == '\n')
				{
					NKCUtil.TextSplitLine(NKCUtil.subTextBuilder, cUIText, fForceRectWidth, NKCUtil.newFullTextBuilder, bExistEmpty);
					NKCUtil.subTextBuilder.Clear();
				}
			}
			NKCUtil.TextSplitLine(NKCUtil.subTextBuilder, cUIText, fForceRectWidth, NKCUtil.newFullTextBuilder, bExistEmpty);
			cUIText.text = "";
			return NKCUtil.newFullTextBuilder.ToString();
		}

		// Token: 0x06003F04 RID: 16132 RVA: 0x00146EEC File Offset: 0x001450EC
		private static void RemoveTextTag(StringBuilder destTextBuilder, StringBuilder srcTextBuilder)
		{
			destTextBuilder.Remove(0, destTextBuilder.Length);
			bool flag = false;
			for (int i = 0; i < srcTextBuilder.Length; i++)
			{
				char c = srcTextBuilder[i];
				if (c == '<')
				{
					flag = true;
				}
				else
				{
					if (i > 0)
					{
						c = srcTextBuilder[i - 1];
					}
					if (c == '>')
					{
						flag = false;
					}
					if (!flag)
					{
						destTextBuilder.Append(srcTextBuilder[i]);
					}
				}
			}
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x00146F54 File Offset: 0x00145154
		private static bool TextSplitLine(StringBuilder subTextBuilder, Text cUIText, float fForceRectWidth, StringBuilder newFullTextBuilder, bool bExistEmpty)
		{
			string text = subTextBuilder.ToString();
			NKCUtil.RemoveTextTag(NKCUtil.tempTextBuilder, subTextBuilder);
			cUIText.text = NKCUtil.tempTextBuilder.ToString();
			bool flag = false;
			if (cUIText.preferredWidth >= fForceRectWidth - (float)cUIText.fontSize * 1.1f && (double)cUIText.preferredWidth < (double)fForceRectWidth * 1.5)
			{
				int num = 0;
				bool flag2 = false;
				for (int i = 0; i < text.Length / 2; i++)
				{
					char c = text[i];
					if (c == '<')
					{
						flag2 = true;
					}
					if (c == '>')
					{
						flag2 = false;
					}
					if (flag2)
					{
						num++;
					}
				}
				bool flag3 = false;
				int num2 = (int)((double)num * 0.8) + text.Length / 2;
				for (int j = 0; j < num2; j++)
				{
					int num3 = (int)text[j];
					if (j > 0 && text[j - 1] == '>')
					{
						flag3 = false;
					}
					if (num3 == 60)
					{
						flag3 = true;
					}
				}
				for (int k = num2; k < text.Length; k++)
				{
					char c2 = text[k];
					if (k > 0 && text[k - 1] == '>')
					{
						flag3 = false;
					}
					if (c2 == '<')
					{
						flag3 = true;
					}
					if (!flag3)
					{
						bool flag4 = false;
						if (bExistEmpty)
						{
							if (c2 == ' ')
							{
								flag4 = true;
							}
						}
						else
						{
							flag4 = true;
						}
						if (flag4)
						{
							string text2 = text.Substring(0, k);
							cUIText.text = text2;
							if (cUIText.preferredWidth < fForceRectWidth - (float)cUIText.fontSize * 1.1f)
							{
								int num4 = k;
								if (NKCUtil.IsSpacing(text, k) && num4 + 1 < text.Length)
								{
									num4++;
								}
								text = text.Insert(num4, "\n");
								newFullTextBuilder.Append(text);
								text = "";
								flag = true;
								break;
							}
							bool flag5 = false;
							for (k = 0; k < (int)((double)num * 0.7) + text.Length / 2; k++)
							{
								c2 = text[k];
								if (k > 0 && text[k - 1] == '>')
								{
									flag5 = false;
								}
								if (c2 == '<')
								{
									flag5 = true;
								}
							}
							for (k = (int)((double)num * 0.7) + text.Length / 2; k < text.Length; k++)
							{
								c2 = text[k];
								if (k > 0 && text[k - 1] == '>')
								{
									flag5 = false;
								}
								if (c2 == '<')
								{
									flag5 = true;
								}
								if (!flag5)
								{
									bool flag6 = false;
									if (bExistEmpty)
									{
										if (c2 == ' ')
										{
											flag6 = true;
										}
									}
									else
									{
										flag6 = true;
									}
									if (flag6)
									{
										int num5 = k;
										if (NKCUtil.IsSpacing(text, k) && num5 + 1 < text.Length)
										{
											num5++;
										}
										text = text.Insert(num5, "\n");
										newFullTextBuilder.Append(text);
										text = "";
										flag = true;
										break;
									}
								}
							}
						}
						if (flag)
						{
							break;
						}
					}
				}
			}
			if (!flag)
			{
				newFullTextBuilder.Append(text);
			}
			return flag;
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x0014722C File Offset: 0x0014542C
		private static bool TextSplitLine(StringBuilder subTextBuilder, TextMeshProUGUI cUIText, float fForceRectWidth, StringBuilder newFullTextBuilder, bool bExistEmpty)
		{
			string text = subTextBuilder.ToString();
			NKCUtil.RemoveTextTag(NKCUtil.tempTextBuilder, subTextBuilder);
			cUIText.text = NKCUtil.tempTextBuilder.ToString();
			bool flag = false;
			if (cUIText.preferredWidth >= fForceRectWidth - cUIText.fontSize * 1.1f && (double)cUIText.preferredWidth < (double)fForceRectWidth * 1.5)
			{
				int num = 0;
				bool flag2 = false;
				for (int i = 0; i < text.Length / 2; i++)
				{
					char c = text[i];
					if (c == '<')
					{
						flag2 = true;
					}
					if (c == '>')
					{
						flag2 = false;
					}
					if (flag2)
					{
						num++;
					}
				}
				bool flag3 = false;
				int num2 = (int)((double)num * 0.8) + text.Length / 2;
				for (int j = 0; j < num2; j++)
				{
					int num3 = (int)text[j];
					if (j > 0 && text[j - 1] == '>')
					{
						flag3 = false;
					}
					if (num3 == 60)
					{
						flag3 = true;
					}
				}
				for (int k = num2; k < text.Length; k++)
				{
					char c2 = text[k];
					if (k > 0 && text[k - 1] == '>')
					{
						flag3 = false;
					}
					if (c2 == '<')
					{
						flag3 = true;
					}
					if (!flag3)
					{
						bool flag4 = false;
						if (bExistEmpty)
						{
							if (c2 == ' ')
							{
								flag4 = true;
							}
						}
						else
						{
							flag4 = true;
						}
						if (flag4)
						{
							string text2 = text.Substring(0, k);
							cUIText.text = text2;
							if (cUIText.preferredWidth < fForceRectWidth - cUIText.fontSize * 1.1f)
							{
								int num4 = k;
								if (NKCUtil.IsSpacing(text, k) && num4 + 1 < text.Length)
								{
									num4++;
								}
								text = text.Insert(num4, "\n");
								newFullTextBuilder.Append(text);
								text = "";
								flag = true;
								break;
							}
							bool flag5 = false;
							for (k = 0; k < (int)((double)num * 0.7) + text.Length / 2; k++)
							{
								c2 = text[k];
								if (k > 0 && text[k - 1] == '>')
								{
									flag5 = false;
								}
								if (c2 == '<')
								{
									flag5 = true;
								}
							}
							for (k = (int)((double)num * 0.7) + text.Length / 2; k < text.Length; k++)
							{
								c2 = text[k];
								if (k > 0 && text[k - 1] == '>')
								{
									flag5 = false;
								}
								if (c2 == '<')
								{
									flag5 = true;
								}
								if (!flag5)
								{
									bool flag6 = false;
									if (bExistEmpty)
									{
										if (c2 == ' ')
										{
											flag6 = true;
										}
									}
									else
									{
										flag6 = true;
									}
									if (flag6)
									{
										int num5 = k;
										if (NKCUtil.IsSpacing(text, k) && num5 + 1 < text.Length)
										{
											num5++;
										}
										text = text.Insert(num5, "\n");
										newFullTextBuilder.Append(text);
										text = "";
										flag = true;
										break;
									}
								}
							}
						}
						if (flag)
						{
							break;
						}
					}
				}
			}
			if (!flag)
			{
				newFullTextBuilder.Append(text);
			}
			return flag;
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x00147502 File Offset: 0x00145702
		private static bool IsSpacing(string text, int charIndex)
		{
			return charIndex < text.Length && text[charIndex] == ' ';
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x0014751C File Offset: 0x0014571C
		public static NKM_ERROR_CODE CheckCommonStartCond(NKMUserData cNKMUserData)
		{
			if (cNKMUserData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_USER_DATA_NULL;
			}
			if (!cNKMUserData.m_ArmyData.CanGetMoreUnit(1))
			{
				return NKM_ERROR_CODE.NEC_FAIL_ARMY_FULL;
			}
			if (!cNKMUserData.m_ArmyData.CanGetMoreShip(0))
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_FULL;
			}
			if (!cNKMUserData.m_InventoryData.CanGetMoreEquipItem(1))
			{
				return NKM_ERROR_CODE.NEC_FAIL_EQUIP_ITEM_FULL;
			}
			if (!cNKMUserData.m_ArmyData.CanGetMoreOperator(1))
			{
				return NKM_ERROR_CODE.NEC_FAIL_OPERATOR_FULL;
			}
			if (!cNKMUserData.m_ArmyData.CanGetMoreTrophy(1))
			{
				return NKM_ERROR_CODE.NEC_FAIL_TROPHY_FULL;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x0014758C File Offset: 0x0014578C
		public static void OnExpandInventoryPopup(NKM_ERROR_CODE error_code)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKM_INVENTORY_EXPAND_TYPE inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_NONE;
			string title = "";
			NKCPopupInventoryAdd.SliderInfo sliderInfo = default(NKCPopupInventoryAdd.SliderInfo);
			int requiredItemCount = 0;
			switch (error_code)
			{
			case NKM_ERROR_CODE.NEC_FAIL_ARMY_FULL:
				inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_UNIT;
				title = NKCUtilString.GET_STRING_INVENTORY_UNIT;
				sliderInfo.increaseCount = 5;
				sliderInfo.maxCount = 1100;
				sliderInfo.currentCount = myUserData.m_ArmyData.m_MaxUnitCount;
				requiredItemCount = 100;
				break;
			case NKM_ERROR_CODE.NEC_FAIL_SHIP_FULL:
				inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_SHIP;
				title = NKCUtilString.GET_STRING_INVENTORY_SHIP;
				sliderInfo.increaseCount = 1;
				sliderInfo.maxCount = 60;
				sliderInfo.currentCount = myUserData.m_ArmyData.m_MaxShipCount;
				requiredItemCount = 100;
				break;
			case NKM_ERROR_CODE.NEC_FAIL_EQUIP_ITEM_FULL:
				inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP;
				title = NKCUtilString.GET_STRING_INVENTORY_EQUIP;
				sliderInfo.increaseCount = 5;
				sliderInfo.maxCount = 2000;
				sliderInfo.currentCount = myUserData.m_InventoryData.m_MaxItemEqipCount;
				requiredItemCount = 50;
				break;
			case NKM_ERROR_CODE.NEC_FAIL_OPERATOR_FULL:
				inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_OPERATOR;
				title = NKCUtilString.GET_STRING_INVEITORY_OPERATOR_TITLE;
				sliderInfo.increaseCount = 5;
				sliderInfo.maxCount = 500;
				sliderInfo.currentCount = myUserData.m_ArmyData.m_MaxOperatorCount;
				requiredItemCount = 100;
				break;
			default:
				if (error_code == NKM_ERROR_CODE.NEC_FAIL_TROPHY_FULL)
				{
					inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_TROPHY;
					title = NKCUtilString.GET_STRING_TROPHY_UNIT;
					sliderInfo.increaseCount = 10;
					sliderInfo.maxCount = 2000;
					sliderInfo.currentCount = myUserData.m_ArmyData.m_MaxTrophyCount;
					requiredItemCount = 50;
				}
				break;
			}
			sliderInfo.inventoryType = inventoryType;
			string expandDesc = NKCUtilString.GetExpandDesc(inventoryType, true);
			int count = 1;
			if (!NKMInventoryManager.IsValidExpandType(inventoryType))
			{
				return;
			}
			int num;
			bool flag = !NKCAdManager.IsAdRewardInventory(inventoryType) || !NKMInventoryManager.CanExpandInventoryByAd(inventoryType, myUserData, count, out num);
			if (!NKMInventoryManager.CanExpandInventory(inventoryType, myUserData, count, out num) && flag)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCPacketHandlers.GetErrorMessage(error_code), null, "");
				return;
			}
			NKCPopupInventoryAdd.Instance.Open(title, expandDesc, sliderInfo, requiredItemCount, 101, delegate(int value)
			{
				NKCPacketSender.Send_NKMPacket_INVENTORY_EXPAND_REQ(inventoryType, value);
			}, false);
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x001477B0 File Offset: 0x001459B0
		public static bool IsNullObject<T>(T obj, string msg = "")
		{
			if (obj == null)
			{
				StackTrace stackTrace = new StackTrace();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Null Object Exception\ntarget Type : " + typeof(T).FullName + ", ");
				stringBuilder.Append("\nException message : " + msg).Append(" ");
				stringBuilder.Append("\nSTACK TRACE - START").Append(" ");
				for (int i = 1; i < stackTrace.FrameCount; i++)
				{
					MethodBase method = stackTrace.GetFrame(i).GetMethod();
					if (null != method.ReflectedType)
					{
						stringBuilder.AppendFormat("\n{0}.{1}", method.ReflectedType.ToString(), method.Name);
					}
				}
				stringBuilder.Append("\nSTACK TRACE - END");
				UnityEngine.Debug.LogWarning(stringBuilder.ToString());
				return true;
			}
			return false;
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x00147890 File Offset: 0x00145A90
		public static bool CheckPossibleShowBan(NKCUIDeckViewer.DeckViewerMode eDeckViewerMode)
		{
			if (eDeckViewerMode == NKCUIDeckViewer.DeckViewerMode.PrivatePvPReady)
			{
				if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM) && NKCPrivatePVPRoomMgr.PvpGameLobbyState != null)
				{
					return NKCPrivatePVPRoomMgr.PrivatePVPLobbyBanUpState;
				}
				return NKCPrivatePVPMgr.PrivatePVPLobbyBanUpState;
			}
			else
			{
				bool flag = NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_ASYNC_NEW_MODE) && eDeckViewerMode != NKCUIDeckViewer.DeckViewerMode.AsyncPvpDefenseDeck;
				if (!NKCUIDeckViewer.IsPVPSyncMode(eDeckViewerMode) && flag)
				{
					return false;
				}
				if (NKCScenManager.GetScenManager() == null)
				{
					return false;
				}
				if (NKCScenManager.GetScenManager().GetMyUserData() == null)
				{
					return false;
				}
				PvpState pvpState;
				if (eDeckViewerMode == NKCUIDeckViewer.DeckViewerMode.AsyncPvpDefenseDeck && NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_ASYNC_NEW_MODE))
				{
					pvpState = NKCScenManager.GetScenManager().GetMyUserData().m_AsyncData;
				}
				else
				{
					pvpState = NKCScenManager.GetScenManager().GetMyUserData().m_PvpData;
				}
				return pvpState != null && pvpState.IsBanPossibleScore();
			}
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x00147940 File Offset: 0x00145B40
		public static bool CheckPossibleShowUpUnit(NKCUIDeckViewer.DeckViewerMode eDeckViewerMode)
		{
			if (eDeckViewerMode != NKCUIDeckViewer.DeckViewerMode.PrivatePvPReady)
			{
				bool flag = NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_ASYNC_NEW_MODE) && eDeckViewerMode != NKCUIDeckViewer.DeckViewerMode.AsyncPvpDefenseDeck;
				return NKCUIDeckViewer.IsPVPSyncMode(eDeckViewerMode) || !flag;
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM) && NKCPrivatePVPRoomMgr.PvpGameLobbyState != null)
			{
				return NKCPrivatePVPRoomMgr.PrivatePVPLobbyBanUpState;
			}
			return NKCPrivatePVPMgr.PrivatePVPLobbyBanUpState;
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x00147994 File Offset: 0x00145B94
		public static NKMUnitData MakeDummyUnit(int unitID, bool bSetMaximum = false)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			if (unitTempletBase == null)
			{
				UnityEngine.Debug.LogError(string.Format("NKMUnitTempletBase : 데이터를 찾을 수 없습니다 - 유닛ID: {0}", unitID));
				return null;
			}
			NKMUnitData nkmunitData = new NKMUnitData();
			nkmunitData.m_UnitID = unitID;
			nkmunitData.m_UnitUID = (long)unitID;
			nkmunitData.m_UserUID = 0L;
			if (bSetMaximum)
			{
				nkmunitData.m_UnitLevel = 100;
			}
			else
			{
				nkmunitData.m_UnitLevel = 1;
			}
			nkmunitData.m_iUnitLevelEXP = 0;
			if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
			{
				if (bSetMaximum)
				{
					nkmunitData.m_LimitBreakLevel = 3;
				}
				else
				{
					nkmunitData.m_LimitBreakLevel = 0;
				}
				int skillCount = unitTempletBase.GetSkillCount();
				for (int i = 0; i < skillCount; i++)
				{
					string skillStrID = unitTempletBase.GetSkillStrID(i);
					if (bSetMaximum)
					{
						nkmunitData.m_aUnitSkillLevel[i] = NKMUnitSkillManager.GetMaxSkillLevel(skillStrID);
					}
					else
					{
						nkmunitData.m_aUnitSkillLevel[i] = 1;
					}
				}
			}
			for (int j = 0; j <= 5; j++)
			{
				if (bSetMaximum)
				{
					nkmunitData.m_listStatEXP[j] = NKMEnhanceManager.CalculateMaxEXP(nkmunitData, (NKM_STAT_TYPE)j);
				}
				else
				{
					nkmunitData.m_listStatEXP[j] = 0;
				}
			}
			nkmunitData.m_bLock = false;
			return nkmunitData;
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x00147A90 File Offset: 0x00145C90
		public static void SetAwakenFX(Animator anim, NKMUnitTempletBase templetBase)
		{
			if (anim == null)
			{
				return;
			}
			anim.keepAnimatorControllerStateOnDisable = true;
			if (templetBase == null || !templetBase.m_bAwaken)
			{
				anim.SetInteger("Grade", 0);
				return;
			}
			NKM_UNIT_GRADE nkm_UNIT_GRADE = templetBase.m_NKM_UNIT_GRADE;
			if (nkm_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SR)
			{
				anim.SetInteger("Grade", 1);
				return;
			}
			if (nkm_UNIT_GRADE != NKM_UNIT_GRADE.NUG_SSR)
			{
				anim.SetInteger("Grade", 0);
				return;
			}
			anim.SetInteger("Grade", 2);
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x00147AFC File Offset: 0x00145CFC
		public static float GetStatPercentage(NKM_STAT_TYPE eStatType, float number)
		{
			if (number == 0f)
			{
				return 0f;
			}
			float num = 0f;
			switch (eStatType)
			{
			case NKM_STAT_TYPE.NST_DEF:
				num = NKMUnitStatManager.m_fConstDef;
				break;
			case NKM_STAT_TYPE.NST_CRITICAL:
				return (number / NKMUnitStatManager.m_fConstCritical).Clamp(0f, 0.85f) * 100f;
			case NKM_STAT_TYPE.NST_HIT:
				num = NKMUnitStatManager.m_fConstHit;
				break;
			case NKM_STAT_TYPE.NST_EVADE:
				num = NKMUnitStatManager.m_fConstEvade;
				break;
			}
			if (num != 0f)
			{
				return number / (number + num) * 100f;
			}
			return 0f;
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x00147B88 File Offset: 0x00145D88
		public static Sprite GetGuildArtifactBgProbImage(GuildDungeonArtifactTemplet.ArtifactProbType imageName)
		{
			switch (imageName)
			{
			case GuildDungeonArtifactTemplet.ArtifactProbType.HIGH:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_UNIT_SLOT_CARD_SPRITE", "NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_BG_SSR", false);
			case GuildDungeonArtifactTemplet.ArtifactProbType.MIDDLE:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_UNIT_SLOT_CARD_SPRITE", "NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_BG_SR", false);
			case GuildDungeonArtifactTemplet.ArtifactProbType.LOW:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_UNIT_SLOT_CARD_SPRITE", "NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_BG_R", false);
			default:
				return null;
			}
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x00147BE0 File Offset: 0x00145DE0
		public static bool IsValidReward(NKM_REWARD_TYPE rewardType, int rewardValueID)
		{
			switch (rewardType)
			{
			case NKM_REWARD_TYPE.RT_UNIT:
			case NKM_REWARD_TYPE.RT_SHIP:
				break;
			case NKM_REWARD_TYPE.RT_MISC:
			case NKM_REWARD_TYPE.RT_USER_EXP:
			case NKM_REWARD_TYPE.RT_EQUIP:
				return true;
			case NKM_REWARD_TYPE.RT_MOLD:
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(rewardValueID);
				if (itemMoldTempletByID == null || !itemMoldTempletByID.EnableByTag)
				{
					return false;
				}
				return true;
			}
			default:
				if (rewardType != NKM_REWARD_TYPE.RT_OPERATOR)
				{
					return true;
				}
				break;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(rewardValueID);
			if (unitTempletBase == null || !unitTempletBase.CollectionEnableByTag)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x00147C3B File Offset: 0x00145E3B
		public static bool IsJPNPolicyRelatedItem(int itemId)
		{
			return NKCPublisherModule.InAppPurchase.IsJPNPaymentPolicy() && (itemId == 101 || itemId == 102);
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x00147C58 File Offset: 0x00145E58
		public static AnimationClip GetAnimationClip(Animator animator, string name)
		{
			if (animator == null || animator.runtimeAnimatorController == null)
			{
				return null;
			}
			foreach (AnimationClip animationClip in animator.runtimeAnimatorController.animationClips)
			{
				if (animationClip.name == name)
				{
					return animationClip;
				}
			}
			return null;
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x00147CAD File Offset: 0x00145EAD
		public static void OpenRewardPopup(NKMArmyData armyData, NKMRewardData rewardData, NKMAdditionalReward additionalReward, string Title, string subTitle = "", NKCUIResult.OnClose onClose = null)
		{
			if (rewardData.UnitDataList.Count > 0 || rewardData.SkinIdList.Count > 0)
			{
				NKCUIResult.Instance.OpenRewardGain(armyData, rewardData, additionalReward, Title, subTitle, onClose);
				return;
			}
			NKCPopupMessageToastSimple.Instance.Open(rewardData, additionalReward, onClose);
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x00147CEC File Offset: 0x00145EEC
		public static string GetPotentialSocketStatText(bool isPercentStat, float statValue, float statFactor)
		{
			if (isPercentStat)
			{
				decimal num = new decimal(statValue);
				num = Math.Round(num * 1000m) / 1000m;
				return string.Format("+{0:P1}", num) ?? "";
			}
			if (statFactor > 0f)
			{
				decimal num2 = new decimal(statFactor);
				num2 = Math.Round(num2 * 1000m) / 1000m;
				return string.Format("+{0:P1}", num2) ?? "";
			}
			if (statValue > 0f)
			{
				return string.Format("+{0}", statValue) ?? "";
			}
			return "+0";
		}

		// Token: 0x06003F16 RID: 16150 RVA: 0x00147DBC File Offset: 0x00145FBC
		public static string GetPotentialStatText(NKMEquipItemData cNKMEquipItemData)
		{
			int num = 0;
			if (cNKMEquipItemData.potentialOption != null)
			{
				float num2 = 0f;
				float num3 = 0f;
				int num4 = cNKMEquipItemData.potentialOption.sockets.Length;
				for (int i = 0; i < num4; i++)
				{
					if (cNKMEquipItemData.potentialOption.sockets[i] != null)
					{
						num++;
						num2 += cNKMEquipItemData.potentialOption.sockets[i].statValue;
						num3 += cNKMEquipItemData.potentialOption.sockets[i].statFactor;
					}
				}
				if (num > 0)
				{
					bool isPercentStat = NKMUnitStatManager.IsPercentStat(cNKMEquipItemData.potentialOption.statType);
					string statShortName;
					if (NKCUtilString.IsNameReversedIfNegative(cNKMEquipItemData.potentialOption.statType) && (num2 < 0f || num3 < 0f))
					{
						statShortName = NKCUtilString.GetStatShortName(cNKMEquipItemData.potentialOption.statType, true);
						num2 = Mathf.Abs(num2);
						num3 = Mathf.Abs(num3);
					}
					else
					{
						statShortName = NKCUtilString.GetStatShortName(cNKMEquipItemData.potentialOption.statType);
					}
					return statShortName + " " + NKCUtil.GetPotentialSocketStatText(isPercentStat, num2, num3);
				}
			}
			return NKCUtilString.GET_STRING_EQUIP_POTENTIAL_OPEN_REQUIRED;
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x00147EC8 File Offset: 0x001460C8
		public static void SetRaidEventPoint(Image img, NKMRaidTemplet raidTemplet)
		{
			if (img == null || raidTemplet == null)
			{
				return;
			}
			NKCUtil.SetImageSprite(img, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_RENEWAL_EVENT_THUMBNAIL", raidTemplet.EventPointColorName, false), false);
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x00147EEF File Offset: 0x001460EF
		public static void SetDiveEventPoint(Image img, bool bIsSpecial)
		{
			if (img == null)
			{
				return;
			}
			if (bIsSpecial)
			{
				NKCUtil.SetImageSprite(img, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_RENEWAL_EVENT_THUMBNAIL", "EVENT_THUMBNAIL_POINT_DIVE_SPECIAL", false), false);
				return;
			}
			NKCUtil.SetImageSprite(img, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_RENEWAL_EVENT_THUMBNAIL", "EVENT_THUMBNAIL_POINT_DIVE", false), false);
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x00147F30 File Offset: 0x00146130
		public static bool IsUsingSuperUserFunction()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			return nkmuserData != null && nkmuserData.IsSuperUser();
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x00147F54 File Offset: 0x00146154
		public static bool IsUsingAdminUserFunction()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			return nkmuserData != null && nkmuserData.IsAdminUser();
		}

		// Token: 0x04003731 RID: 14129
		private static string m_PatchVersion = "";

		// Token: 0x04003732 RID: 14130
		private static string m_PatchVersionEA = "";

		// Token: 0x04003733 RID: 14131
		private static StringBuilder newFullTextBuilder = new StringBuilder();

		// Token: 0x04003734 RID: 14132
		private static StringBuilder subTextBuilder = new StringBuilder();

		// Token: 0x04003735 RID: 14133
		private static StringBuilder tempTextBuilder = new StringBuilder();

		// Token: 0x04003736 RID: 14134
		public static Color EP_ACHIEVE_COLOR = new Color(1f, 0.87058824f, 0f);

		// Token: 0x04003737 RID: 14135
		public static Color EP_NO_ACHIEVE_COLOR = new Color(0.43529412f, 0.43529412f, 0.43529412f);

		// Token: 0x04003738 RID: 14136
		public static HashSet<int> m_sHsFirstClearDungeon = new HashSet<int>();

		// Token: 0x04003739 RID: 14137
		public static HashSet<int> m_sHsFirstClearWarfare = new HashSet<int>();

		// Token: 0x020013C2 RID: 5058
		public enum ButtonColor
		{
			// Token: 0x04009C1A RID: 39962
			BC_NONE = -1,
			// Token: 0x04009C1B RID: 39963
			BC_GRAY,
			// Token: 0x04009C1C RID: 39964
			BC_YELLOW,
			// Token: 0x04009C1D RID: 39965
			BC_BLUE,
			// Token: 0x04009C1E RID: 39966
			BC_RED,
			// Token: 0x04009C1F RID: 39967
			BC_LOGIN_BLUE,
			// Token: 0x04009C20 RID: 39968
			BC_LOGIN_YELLOW,
			// Token: 0x04009C21 RID: 39969
			BC_COMMON_ENABLE,
			// Token: 0x04009C22 RID: 39970
			BC_COMMON_DISABLE
		}
	}
}
