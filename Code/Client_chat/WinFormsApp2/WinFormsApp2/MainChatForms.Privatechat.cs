using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsApp2 
{
    public partial class MainChatForms
    {
        private static Dictionary<string, PrivateChatForm> openChats = new Dictionary<string, PrivateChatForm>();
        public void OpenOrUpdatePrivateChatWindow(string targetUser, string incomingMessage = null)
        {
            this.Invoke(new Action(() =>
            {
                if (!openChats.ContainsKey(targetUser) || openChats[targetUser].IsDisposed)
                {
                    PrivateChatForm chatForm = new PrivateChatForm(username, targetUser, stream);
                    openChats[targetUser] = chatForm;
                    chatForm.Show();
                }
                else
                {
            
                    openChats[targetUser].BringToFront();
                }

                
                if (!string.IsNullOrEmpty(incomingMessage))
                {
                    openChats[targetUser].AppendMessage(targetUser, incomingMessage);
                }
            }));
        }
        private void lstOnlineUsers_DoubleClick(object sender, EventArgs e)
        {
            if (lstOnlineUsers.SelectedItem != null)
            {
                string targetUser = lstOnlineUsers.SelectedItem.ToString();

                if (targetUser == username) return; 

                OpenOrUpdatePrivateChatWindow(targetUser);
            }
        }
    }
}
