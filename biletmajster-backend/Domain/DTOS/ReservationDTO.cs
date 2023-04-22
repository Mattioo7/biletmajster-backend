/*
 * System rezerwacji miejsc na eventy
 *
 * Niniejsza dokumentacja stanowi opis REST API implemtowanego przez serwer centralny. Endpointy 
 *
 * OpenAPI spec version: 1.0.0
 * Contact: XXX@pw.edu.pl
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace biletmajster_backend.Domain.DTOS
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class ReservationDTO : IEquatable<ReservationDTO>
    { 
        /// <summary>
        /// Gets or Sets EventId
        /// </summary>
        [Required]

        [DataMember(Name="eventId")]
        public long? EventId { get; set; }

        /// <summary>
        /// Gets or Sets PlaceId
        /// </summary>
        [Required]

        [DataMember(Name="placeId")]
        public long? PlaceId { get; set; }

        /// <summary>
        /// Gets or Sets ReservationToken
        /// </summary>
        [Required]

        [DataMember(Name="reservationToken")]
        public string ReservationToken { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ReservationDTO {\n");
            sb.Append("  EventId: ").Append(EventId).Append("\n");
            sb.Append("  PlaceId: ").Append(PlaceId).Append("\n");
            sb.Append("  ReservationToken: ").Append(ReservationToken).Append("\n");
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
            return obj.GetType() == GetType() && Equals((ReservationDTO)obj);
        }

        /// <summary>
        /// Returns true if ReservationDTO instances are equal
        /// </summary>
        /// <param name="other">Instance of ReservationDTO to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ReservationDTO other)
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
                    PlaceId == other.PlaceId ||
                    PlaceId != null &&
                    PlaceId.Equals(other.PlaceId)
                ) && 
                (
                    ReservationToken == other.ReservationToken ||
                    ReservationToken != null &&
                    ReservationToken.Equals(other.ReservationToken)
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
                    if (PlaceId != null)
                    hashCode = hashCode * 59 + PlaceId.GetHashCode();
                    if (ReservationToken != null)
                    hashCode = hashCode * 59 + ReservationToken.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(ReservationDTO left, ReservationDTO right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ReservationDTO left, ReservationDTO right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
