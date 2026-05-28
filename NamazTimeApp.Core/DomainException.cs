using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamazTimeApp.Core
{
    /// <summary>
    /// Custom exception to handle any domain error.
    /// </summary>    
    public class DomainException : Exception
    {
        /// <summary>
        /// List of exception message.
        /// </summary>
        private List<Message> _messages;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class.
        /// </summary>
        public DomainException() : base()
        {
            _messages = new List<Message>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DomainException(Message message) : this()
        {
            _messages.Add(message);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class.
        /// </summary>
        /// <param name="messages">The messages.</param>
        public DomainException(IEnumerable<Message> messages) : this()
        {
            _messages.AddRange(messages);
        }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public IReadOnlyList<Message> Messages
        {
            get
            {
                return _messages;
            }
        }
    }
}
