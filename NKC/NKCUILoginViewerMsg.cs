using System;
using System.Collections;
using System.IO;
using AssetBundles;
using DG.Tweening;
using NKC.Patcher;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007C9 RID: 1993
	public class NKCUILoginViewerMsg : MonoBehaviour
	{
		// Token: 0x06004ECA RID: 20170 RVA: 0x0017C5A8 File Offset: 0x0017A7A8
		public void ForceUpdateMsg()
		{
			if (!this.m_bRunningPolling)
			{
				this.m_fLastUpdateTime = Time.time;
				base.StartCoroutine(this.UpdateLoginMsg());
			}
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x0017C5CA File Offset: 0x0017A7CA
		private void Start()
		{
			this.InvalidMsg();
		}

		// Token: 0x06004ECC RID: 20172 RVA: 0x0017C5D4 File Offset: 0x0017A7D4
		private void SetMsg(string msg)
		{
			if (this.m_lbMessage == null || this.m_cgRoot == null || string.IsNullOrWhiteSpace(msg))
			{
				this.InvalidMsg();
				return;
			}
			if (this.m_lbMessage.text != msg)
			{
				this.m_cgRoot.alpha = 0f;
				this.m_cgRoot.DOFade(1f, 1f);
			}
			else
			{
				this.m_cgRoot.alpha = 1f;
			}
			NKCUtil.SetLabelText(this.m_lbMessage, msg);
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x0017C663 File Offset: 0x0017A863
		private void InvalidMsg()
		{
			if (this.m_lbMessage != null)
			{
				this.m_lbMessage.text = "";
			}
			if (this.m_cgRoot != null)
			{
				this.m_cgRoot.alpha = 0f;
			}
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x0017C6A1 File Offset: 0x0017A8A1
		private void Update()
		{
			if (!this.m_bRunningPolling && 30f + this.m_fLastUpdateTime < Time.time)
			{
				this.m_fLastUpdateTime = Time.time;
				base.StartCoroutine(this.UpdateLoginMsg());
			}
		}

		// Token: 0x06004ECF RID: 20175 RVA: 0x0017C6D6 File Offset: 0x0017A8D6
		private IEnumerator UpdateLoginMsg()
		{
			this.m_bRunningPolling = true;
			string localDownloadPath = AssetBundleManager.GetLocalDownloadPath();
			string text = Application.streamingAssetsPath + "/CSConfigServerAddress.txt";
			bool bSuccessGetMsgFromServer = false;
			if (NKCPatchUtility.IsFileExists(text))
			{
				Debug.Log("CSConfigServerAddress exist");
				string aJSON;
				if (text.Contains("jar:"))
				{
					aJSON = BetterStreamingAssets.ReadAllText(NKCAssetbundleInnerStream.GetJarRelativePath(text));
				}
				else
				{
					aJSON = File.ReadAllText(text);
				}
				JSONNode jsonnode = JSONNode.Parse(aJSON);
				if (jsonnode != null)
				{
					string url = jsonnode["address"];
					string targetFileName = Path.Combine(localDownloadPath, "CSConfig.txt");
					if (!Directory.Exists(Path.GetDirectoryName(targetFileName)))
					{
						Directory.CreateDirectory(Path.GetDirectoryName(targetFileName));
					}
					using (UnityWebRequest uwr = new UnityWebRequest(url))
					{
						uwr.method = "GET";
						uwr.downloadHandler = new DownloadHandlerFile(targetFileName)
						{
							removeFileOnAbort = true
						};
						yield return uwr.SendWebRequest();
						bool flag = false;
						if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
						{
							flag = true;
						}
						if (flag)
						{
							Debug.Log(uwr.error);
							this.m_bRunningPolling = false;
							this.InvalidMsg();
							yield break;
						}
						Debug.Log("Download saved to: " + targetFileName.Replace("/", "\\") + "\r\n" + uwr.error);
						if (NKCPatchUtility.IsFileExists(targetFileName))
						{
							aJSON = File.ReadAllText(targetFileName);
							JSONNode jsonnode2 = JSONNode.Parse(aJSON);
							if (jsonnode2 != null)
							{
								string aKey = "LOGIN_SCREEN_MSG_" + NKCStringTable.GetNationalCode().ToString();
								if (jsonnode2[aKey] != null && !string.IsNullOrWhiteSpace(jsonnode2[aKey]))
								{
									bSuccessGetMsgFromServer = true;
									this.SetMsg(jsonnode2[aKey]);
								}
							}
						}
					}
					UnityWebRequest uwr = null;
					targetFileName = null;
				}
			}
			else
			{
				Debug.Log("CSConfigServerAddress not exist");
			}
			if (!bSuccessGetMsgFromServer)
			{
				this.InvalidMsg();
			}
			this.m_bRunningPolling = false;
			yield break;
			yield break;
		}

		// Token: 0x04003E94 RID: 16020
		public CanvasGroup m_cgRoot;

		// Token: 0x04003E95 RID: 16021
		public Text m_lbMessage;

		// Token: 0x04003E96 RID: 16022
		private bool m_bRunningPolling;

		// Token: 0x04003E97 RID: 16023
		private float m_fLastUpdateTime = float.MinValue;

		// Token: 0x04003E98 RID: 16024
		private const float POLLING_PERIOD = 30f;
	}
}
