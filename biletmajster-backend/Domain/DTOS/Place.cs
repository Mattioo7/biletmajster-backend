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

namespace biletmajster_backend.Domain.DTOS
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Place : IEquatable<Place>
    { 
        /// <summary>
        /// Gets or Sets Id
        /// </summary>

        [DataMember(Name="id")]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or Sets Free
        /// </summary>

        [DataMember(Name="free")]
        public bool? Free { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Place {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Free: ").Append(Free).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Place)obj);
        }

        /// <summary>
        /// Returns true if Place instances are equal
        /// </summary>
        /// <param name="other">Instance of Place to be compared</param>
        /// <returns>Boolean</returns>
        /// 

        [DataMember(Name = "SeatNumber")]
        public long SeatNumber { get; set; }

        public Place(long SeatNr)
        {
            SeatNumber = SeatNr;
        }

        public bool Equals(Place other)
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
                    Free == other.Free ||
                    Free != null &&
                    Free.Equals(other.Free)
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
                    if (Free != null)
                    hashCode = hashCode * 59 + Free.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Place left, Place right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Place left, Place right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
