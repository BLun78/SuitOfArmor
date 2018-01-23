namespace BLun.SuitOfArmor.Filter
{
    /// <summary>
    /// Enum for XssProtection
    /// </summary>
    public enum XssProtection
    {
        /// <summary>
        /// nothing is taken (default)
        /// </summary>
        None = 0,
        
        /// <summary>
        /// Disables the X-XSS-Protection: 0
        /// </summary>
        Disables = 1,
        
        /// <summary>
        /// Enables the X-XSS-Protection: 1
        /// </summary>
        Enables =  2,
        
        /// <summary>
        /// IE8 protection X-XSS-Protection: 1; mode=block
        /// </summary>
        EnablesForOldIe8 = 3,
    }
}