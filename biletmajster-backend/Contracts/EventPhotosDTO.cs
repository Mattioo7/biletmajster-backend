using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace biletmajster_backend.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class EventPhotosDTO : IEquatable<EventPhotosDTO>
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [Required]

        [DataMember(Name = "EventId")]
        public long? EventId { get; set; }

        /// <summary>
        /// Gets or Sets Free
        /// </summary>
        [Required]

        [DataMember(Name = "DownloadLink")]
        public string? DownloadLink { get; set; }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Place {\n");
            sb.Append("  EventId: ").Append(EventId).Append("\n");
            sb.Append("  DownloadLink: ").Append(DownloadLink).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((EventPhotosDTO)obj);
        }
        public bool Equals(EventPhotosDTO other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    EventId == other.EventId ||
                    EventId != null &&
                    EventId.Equals(other.EventId)
                ) &&
                (
                    DownloadLink == other.DownloadLink ||
                    DownloadLink != null &&
                    DownloadLink.Equals(other.DownloadLink)
                );
        }
        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                if (EventId != null)
                    hashCode = hashCode * 59 + EventId.GetHashCode();
                if (DownloadLink != null)
                    hashCode = hashCode * 59 + DownloadLink.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(EventPhotosDTO left, EventPhotosDTO right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EventPhotosDTO left, EventPhotosDTO right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
