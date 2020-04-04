// -----------------------------------------------------------------------
// <copyright file="JwtSettings.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Configurations
{
    /// <summary>
    /// The JWT settings.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>
        /// The secret.
        /// </value>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the expiry in days.
        /// </summary>
        /// <value>
        /// The expiry in days.
        /// </value>
        public int ExpiryInDays { get; set; }

        /// <summary>
        /// Gets or sets the algorithm.
        /// </summary>
        /// <value>
        /// The algorithm.
        /// </value>
        public string Algorithm { get; set; }
    }
}
