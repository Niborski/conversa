// Copyright (c) Carlos Guzm�n �lvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;
using Conversa.Net.Xmpp.Shared;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Conversa.Net.Xmpp.Client
{
    /// <summary>
    /// A simple XMPP XML message parser
    /// </summary>
    internal sealed class XmppStreamParser
        : IDisposable
    {
        private static string GetXmlNamespace(string tag)
        {
            return null;
        }

        private static string GetTagName(string tag)
        {
            var tagName = String.Empty;
            int index   = 1;

            while (true)
            {
                char c = tag[index++];

                if (!Char.IsWhiteSpace(c) && c != '>' && c != '/')
                {
                    tagName += c;
                }
                else
                {
                    break;
                }
            }

            return tagName;
        }

        private static bool IsStartTag(string tag)
        {
            return (tag.StartsWith("<", StringComparison.Ordinal)  && !tag.StartsWith("</", StringComparison.Ordinal));
        }

        private static bool IsEndTag(string tag)
        {
            return (tag.StartsWith("</", StringComparison.Ordinal) || tag.EndsWith("/>", StringComparison.Ordinal));
        }

        private static bool IsProcessingInstruction(string tag)
        {
            return (tag.StartsWith("<?", StringComparison.Ordinal));
        }

        private static bool IsCharacterDataAndMarkup(string tag)
        {
            return (tag.StartsWith("<![", StringComparison.Ordinal));
        }

        private static bool IsStreamStartTag(string tag)
        {
            return tag.StartsWith(XmppCodes.StartStream, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsStreamEndTag(string tag)
        {
            return (String.Compare(tag, XmppCodes.EndStream, StringComparison.OrdinalIgnoreCase) == 0);
        }

        private MemoryStream  stream;
        private BinaryReader  reader;
        private StringBuilder node;
        private StringBuilder currentTag;
        private long          depth;
        private string        nodeName;
        private string        nodeNamespace;
		private object        syncObject;
        private bool          isDisposed;

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:XmppStreamParser"/> is EOF.
        /// </summary>
        /// <value><c>true</c> if EOF; otherwise, <c>false</c>.</value>
        internal bool EOF
        {
            get { return this.stream.EOF(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:XmppStreamParser"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        internal XmppStreamParser()
        {
            this.stream     = new MemoryStream();
            this.reader     = new BinaryReader(this.stream, XmppEncoding.Utf8, false);
            this.node       = new StringBuilder();
            this.currentTag = new StringBuilder();
			this.syncObject = new Object();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the specified disposing.
        /// </summary>
        /// <param name="disposing">if set to <c>true</c> [disposing].</param>
        private void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    // Release managed resources here
                    this.depth      = 0;
                    this.currentTag = null;
                    this.node       = null;
                    this.nodeName   = null;
                    this.syncObject = null;
                    if (this.reader != null)
                    {
                        this.reader.Dispose();
                        this.reader = null;
                    }
                    if (this.stream != null)
                    {
                        this.stream.Dispose();
                        this.stream = null;
                    }
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
            }

            this.isDisposed = true;
        }

        internal void Reset(bool clearInputBuffer)
		{
			if (clearInputBuffer)
			{
				Monitor.Enter(this.syncObject);

                this.stream.Clear();

				Monitor.Exit(this.syncObject);
			}

			this.node.Length       = 0;
			this.currentTag.Length = 0;
			this.depth             = -1;
			this.nodeName          = null;
			this.nodeNamespace     = null;
		}

        internal void WriteBytes(byte[] buffer)
		{
			Monitor.Enter(this.syncObject);

            long currentPosition = this.stream.Position;

            this.stream.Seek(0, SeekOrigin.End);
            this.stream.Write(buffer, 0, buffer.Length);
            this.stream.Seek(currentPosition, SeekOrigin.Begin);

			Monitor.Exit(this.syncObject);
		}

        /// <summary>
        /// Reads the next node.
        /// </summary>
        /// <returns></returns>
        internal XmppStreamElement ReadNextNode()
        {
            if (this.node.Length == 0)
            {
                this.depth          = -1;
                this.nodeName       = null;
                this.nodeNamespace  = null;
            }

            while (!this.EOF && this.depth != 0)
            {
                this.SkipWhiteSpace();

                int next = this.Peek();
                if (next == '<' || this.currentTag.Length > 0)
                {
                    if (!this.ReadTag())
                    {
                        break;
                    }

                    string tag = this.currentTag.ToString();

                    if (!XmppStreamParser.IsProcessingInstruction(tag))
                    {
                        if (this.node.Length == 0 && XmppStreamParser.IsStreamStartTag(tag))
                        {
                            this.nodeName = XmppStreamParser.GetTagName(tag);
                            this.depth    = 0;
                        }
                        else if (this.node.Length == 0 && XmppStreamParser.IsStreamEndTag(tag))
                        {
                            this.nodeName = XmppStreamParser.GetTagName(tag);
                            this.depth    = 0;
                        }
                        else
                        {
                            if (!XmppStreamParser.IsCharacterDataAndMarkup(tag))
                            {
                                if (XmppStreamParser.IsStartTag(tag))
                                {
                                    if (this.depth == -1)
                                    {
                                        this.nodeName      = XmppStreamParser.GetTagName(tag);
                                        this.nodeNamespace = XmppStreamParser.GetXmlNamespace(tag);

                                        this.depth++;
                                    }

                                    depth++;
                                }

                                if (XmppStreamParser.IsEndTag(tag))
                                {
                                    this.depth--;
                                }
                            }
                        }

                        this.node.Append(tag);
                    }

                    this.currentTag.Length = 0;
                }
                else if (next != -1)
                {
                    if (!this.ReadText())   // Element Text
                    {
                        break;
                    }
                }
            }

            XmppStreamElement result = null;

            if (this.depth == 0)
            {
                result = new XmppStreamElement(this.nodeName, this.nodeNamespace, this.node.ToString());

				this.Reset(false);
            }

            return result;
        }

        private bool ReadTag()
        {
            this.SkipWhiteSpace();

            int next = this.Peek();
            if (next != '<' && this.currentTag.Length == 0)
            {
                throw new IOException();
            }

            while (true)
            {
                next = this.Peek();
                if (next == -1)
                {
                    return false;
                }
                else
                {
                    this.currentTag.Append((char)this.Read());

                    if (next == '>')
                    {
                        return true;
                    }
                }
            }
        }

        private void SkipWhiteSpace()
        {
            while (true)
            {
                int next = this.Peek();
                if (next == -1)
                {
                    break;
                }
                else if (Char.IsWhiteSpace((char)next))
                {
                    this.Read();
                }
                else
                {
                    break;
                }
            }
        }

        private bool ReadText()
        {
            while (true)
            {
                if (this.Peek() == -1)
                {
                    return false;
                }

                if (this.Peek() != '<')
                {
                    this.node.Append(this.Read());
                }
                else
                {
                    break;
                }
            }

            return true;
        }

        private int Peek()
        {
            return this.reader.PeekChar();
        }

        private char Read()
        {
            return this.reader.ReadChar();
        }
    }
}
