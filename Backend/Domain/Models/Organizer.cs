/*
 * System rezerwacji miejsc na eventy
 *
 * Niniejsza dokumentacja stanowi opis REST API implemtowanego przez serwer centralny. Endpointy 
 *
 * OpenAPI spec version: 1.0.0
 * Contact: XXX@pw.edu.pl
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace biletmajster_backend.Domain.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Organizer : IEquatable<Organizer>
    { 
        /// <summary>
        /// Gets or Sets Id
        /// </summary>

        [DataMember(Name="id")]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>

        [DataMember(Name="name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Email
        /// </summary>

        [DataMember(Name="email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets Password
        /// </summary>

        [DataMember(Name="password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets Events
        /// </summary>

        [DataMember(Name="events")]
        public List<ModelEvent> Events { get; set; }

        /// <summary>
        /// User Status
        /// </summary>
        /// <value>User Status</value>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum StatusEnum
        {
            /// <summary>
            /// Enum PendingEnum for pending
            /// </summary>
            [EnumMember(Value = "pending")]
            PendingEnum = 0,
            /// <summary>
            /// Enum ConfirmedEnum for confirmed
            /// </summary>
            [EnumMember(Value = "confirmed")]
            ConfirmedEnum = 1        }

        /// <summary>
        /// User Status
        /// </summary>
        /// <value>User Status</value>

        [DataMember(Name="status")]
        public StatusEnum? Status { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Organizer {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("  Password: ").Append(Password).Append("\n");
            sb.Append("  Events: ").Append(Events).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Organizer)obj);
        }

        /// <summary>
        /// Returns true if Organizer instances are equal
        /// </summary>
        /// <param name="other">Instance of Organizer to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Organizer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Id == other.Id ||
                    Id != null &&
                    Id.Equals(other.Id)
                ) && 
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
                ) && 
                (
                    Email == other.Email ||
                    Email != null &&
                    Email.Equals(other.Email)
                ) && 
                (
                    Password == other.Password ||
                    Password != null &&
                    Password.Equals(other.Password)
                ) && 
                (
                    Events == other.Events ||
                    Events != null &&
                    Events.SequenceEqual(other.Events)
                ) && 
                (
                    Status == other.Status ||
                    Status != null &&
                    Status.Equals(other.Status)
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
                    if (Id != null)
                    hashCode = hashCode * 59 + Id.GetHashCode();
                    if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                    if (Email != null)
                    hashCode = hashCode * 59 + Email.GetHashCode();
                    if (Password != null)
                    hashCode = hashCode * 59 + Password.GetHashCode();
                    if (Events != null)
                    hashCode = hashCode * 59 + Events.GetHashCode();
                    if (Status != null)
                    hashCode = hashCode * 59 + Status.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Organizer left, Organizer right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Organizer left, Organizer right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
