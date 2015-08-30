using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.ChatStates
{
    /// <summary>
    /// Chat states
    /// </summary>
    public static class ChatStateType
    {
        public static readonly IChatState Active    = new ActiveChatState();
        public static readonly IChatState Composing = new ComposingChatState();
        public static readonly IChatState Gone      = new GoneChatState();
        public static readonly IChatState Inactive  = new GoneChatState();
        public static readonly IChatState Paused    = new PausedChatState();
    }
}
