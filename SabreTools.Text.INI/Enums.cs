namespace SabreTools.Text.INI
{
    /// <summary>
    /// Different types of INI rows being parsed
    /// </summary>
    public enum RowType
    {
        None = 0,
        SectionHeader = 1,
        KeyValue = 2,
        Comment = 3,
        Invalid = 4,
    }
}
