using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using NKC.Publisher;
using NKC.UI;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006C3 RID: 1731
	internal static class NKCReportManager
	{
		// Token: 0x06003B35 RID: 15157 RVA: 0x00130650 File Offset: 0x0012E850
		public static void Update()
		{
			if (!NKCReportManager.IsReportOpened())
			{
				return;
			}
			Action action;
			while (NKCReportManager.m_ReservedActionQueue.TryDequeue(out action))
			{
				action();
			}
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x0013067C File Offset: 0x0012E87C
		public static void SendReport(string message, bool bAttachLog, bool bAttachCapture)
		{
			if (!NKCReportManager.IsReportOpened())
			{
				return;
			}
			List<Attachment> list = new List<Attachment>();
			if (bAttachLog)
			{
				List<string> list2 = new List<string>(NKCLogManager.GetCurrentOpenedLogs());
				NKCLogManager.OpenNewLogFile();
				for (int i = 0; i < list2.Count; i++)
				{
					if (i < NKCReportManager.MAX_SEND_LOG_COUNT_FRONT || i >= list2.Count - NKCReportManager.MAX_SEND_LOG_COUNT_BACK)
					{
						list.Add(new Attachment(list2[i]));
					}
				}
			}
			NKCReportManager.SendReport(message, list);
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x001306EC File Offset: 0x0012E8EC
		public static void SendReport(string message, List<Attachment> attachments)
		{
			string subject = NKCReportManager.BuildSubject();
			string body = NKCReportManager.BuildMessageBody(message);
			NKMPopUpBox.OpenWaitBox(0f, "");
			Task.Run(delegate()
			{
				using (NKCReportManager.EmailSender emailSender = new NKCReportManager.GmailSender())
				{
					try
					{
						emailSender.SendEmail(subject, body, attachments);
						foreach (Attachment attachment in attachments)
						{
							attachment.Dispose();
						}
						NKCReportManager.m_ReservedActionQueue.Enqueue(delegate
						{
							NKCPopupOKCancel.OpenOKBox(NKCStringTable.GetString("SI_PF_BUG_REPORT_POPUP_TITLE", false), NKCStringTable.GetString("SI_PF_BUG_REPORT_POPUP_SUCCESS", false), null, "");
						});
					}
					catch (Exception arg)
					{
						Debug.LogError(string.Format("[Report] Exception Occurred. e : {0}", arg));
						NKCReportManager.m_ReservedActionQueue.Enqueue(delegate
						{
							NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_BUG_REPORT_SEND_MAIL), null, "");
						});
					}
					finally
					{
						NKCReportManager.m_ReservedActionQueue.Enqueue(delegate
						{
							NKMPopUpBox.CloseWaitBox();
						});
					}
				}
			});
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x0013073C File Offset: 0x0012E93C
		private static string BuildSubject()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			string text = "";
			if (myUserData != null)
			{
				text = myUserData.m_UserNickName;
			}
			return NKCStringTable.GetString("SI_SYSTEM_REPORT_SUBJECT", new object[]
			{
				NKMContentsVersionManager.GetCountryTag(),
				text
			});
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x00130780 File Offset: 0x0012E980
		private static string BuildMessageBody(string message)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			string text = "";
			string text2 = "";
			if (myUserData != null)
			{
				text = myUserData.m_UserNickName;
				text2 = myUserData.m_FriendCode.ToString();
			}
			string publisherAccountCode = NKCPublisherModule.Auth.GetPublisherAccountCode();
			RuntimePlatform platform = Application.platform;
			string version = Application.version;
			string patchVersion = NKCUtil.PatchVersion;
			string patchVersionEA = NKCUtil.PatchVersionEA;
			return NKCStringTable.GetString("SI_SYSTEM_REPORT_BODY", new object[]
			{
				text,
				text2,
				publisherAccountCode,
				platform,
				version,
				patchVersion,
				patchVersionEA,
				message
			});
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x0013081A File Offset: 0x0012EA1A
		private static string GetZipPath()
		{
			return Path.Combine(NKCLogManager.GetSavePath(), "Log.zip");
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x0013082B File Offset: 0x0012EA2B
		public static bool IsReportOpened()
		{
			return NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.BUG_REPORT_SYSTEM) && (NKCDefineManager.DEFINE_SAVE_LOG() || NKCDefineManager.DEFINE_USE_CHEAT());
		}

		// Token: 0x0400354E RID: 13646
		private static readonly int MAX_SEND_LOG_COUNT_FRONT = 5;

		// Token: 0x0400354F RID: 13647
		private static readonly int MAX_SEND_LOG_COUNT_BACK = 5;

		// Token: 0x04003550 RID: 13648
		private static ConcurrentQueue<Action> m_ReservedActionQueue = new ConcurrentQueue<Action>();

		// Token: 0x0200138B RID: 5003
		private class GmailSender : NKCReportManager.EmailSender
		{
			// Token: 0x170017F1 RID: 6129
			// (get) Token: 0x0600A618 RID: 42520 RVA: 0x00346651 File Offset: 0x00344851
			protected override string SmtpHost
			{
				get
				{
					return "smtp.gmail.com";
				}
			}

			// Token: 0x170017F2 RID: 6130
			// (get) Token: 0x0600A619 RID: 42521 RVA: 0x00346658 File Offset: 0x00344858
			protected override int SmtpPort
			{
				get
				{
					return 587;
				}
			}

			// Token: 0x170017F3 RID: 6131
			// (get) Token: 0x0600A61A RID: 42522 RVA: 0x0034665F File Offset: 0x0034485F
			protected override string SenderAddress
			{
				get
				{
					return "csbugreporter.sender@gmail.com";
				}
			}

			// Token: 0x170017F4 RID: 6132
			// (get) Token: 0x0600A61B RID: 42523 RVA: 0x00346666 File Offset: 0x00344866
			protected override string SenderKey
			{
				get
				{
					return "iykrbihtphefqjdr";
				}
			}

			// Token: 0x170017F5 RID: 6133
			// (get) Token: 0x0600A61C RID: 42524 RVA: 0x0034666D File Offset: 0x0034486D
			protected override string ReceiverAddress
			{
				get
				{
					if (NKMContentsVersionManager.HasCountryTag(CountryTagType.KOR))
					{
						return "csbugreporter.korea@gmail.com";
					}
					if (NKMContentsVersionManager.HasCountryTag(CountryTagType.GLOBAL))
					{
						return "csbugreporter.global@gmail.com";
					}
					return "";
				}
			}
		}

		// Token: 0x0200138C RID: 5004
		private abstract class EmailSender : IDisposable
		{
			// Token: 0x170017F6 RID: 6134
			// (get) Token: 0x0600A61E RID: 42526
			protected abstract string SmtpHost { get; }

			// Token: 0x170017F7 RID: 6135
			// (get) Token: 0x0600A61F RID: 42527
			protected abstract int SmtpPort { get; }

			// Token: 0x170017F8 RID: 6136
			// (get) Token: 0x0600A620 RID: 42528
			protected abstract string SenderAddress { get; }

			// Token: 0x170017F9 RID: 6137
			// (get) Token: 0x0600A621 RID: 42529
			protected abstract string SenderKey { get; }

			// Token: 0x170017FA RID: 6138
			// (get) Token: 0x0600A622 RID: 42530
			protected abstract string ReceiverAddress { get; }

			// Token: 0x0600A623 RID: 42531 RVA: 0x00346698 File Offset: 0x00344898
			protected EmailSender()
			{
				this.SetSmtp();
			}

			// Token: 0x0600A624 RID: 42532 RVA: 0x003466B4 File Offset: 0x003448B4
			public void SetSmtp()
			{
				this.m_smtpClient.Host = this.SmtpHost;
				this.m_smtpClient.Port = this.SmtpPort;
				this.m_smtpClient.Credentials = new NetworkCredential(this.SenderAddress, this.SenderKey);
				this.m_smtpClient.EnableSsl = true;
				ServicePointManager.ServerCertificateValidationCallback = ((object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true);
			}

			// Token: 0x0600A625 RID: 42533 RVA: 0x00346730 File Offset: 0x00344930
			public void SendEmail(string subject, string body, List<Attachment> attachments)
			{
				using (MailMessage mailMessage = this.BuildMailMessage(this.SenderAddress, this.ReceiverAddress, subject, body, attachments))
				{
					this.m_smtpClient.Send(mailMessage);
				}
			}

			// Token: 0x0600A626 RID: 42534 RVA: 0x0034677C File Offset: 0x0034497C
			protected MailMessage BuildMailMessage(string senderAddress, string receiverAddress, string subject, string body, List<Attachment> attachments)
			{
				if (string.IsNullOrEmpty(receiverAddress) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(body))
				{
					return null;
				}
				MailMessage mailMessage = new MailMessage();
				mailMessage.From = new MailAddress(senderAddress);
				mailMessage.To.Add(receiverAddress);
				mailMessage.Subject = subject;
				mailMessage.Body = body;
				if (attachments != null)
				{
					foreach (Attachment item in attachments)
					{
						mailMessage.Attachments.Add(item);
					}
				}
				return mailMessage;
			}

			// Token: 0x0600A627 RID: 42535 RVA: 0x00346820 File Offset: 0x00344A20
			public void Dispose()
			{
				this.m_smtpClient.Dispose();
			}

			// Token: 0x04009A80 RID: 39552
			protected SmtpClient m_smtpClient = new SmtpClient();
		}
	}
}
