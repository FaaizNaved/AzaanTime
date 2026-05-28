namespace NamazTimeApp.Core
{
    public class Message
    {
        /// <summary>
        /// Gets or sets field name that cause the message to be raised.
        /// This property is optional. A message can be raised without enter field name.
        /// </summary>
        /// <value>
        /// The name of the field.
        /// </value>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets message value.
        /// </summary>
        /// <value>
        /// The message value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>
        /// The type of the message.
        /// </value>
        public MessageType MessageType { get; set; }
    }
}
